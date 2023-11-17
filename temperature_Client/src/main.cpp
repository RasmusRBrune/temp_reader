/*********
  @file main.cpp
  @brief ESP32 Temperature Sensor with WiFi Manager and AsyncWebServer

  @details
  This code sets up an ESP32 as a temperature sensor with WiFi Manager for easy WiFi configuration.
  It uses AsyncWebServer for handling HTTP requests and a DS18B20 temperature sensor for measuring temperature.
  The measured data is sent to an ASP.NET server via HTTP POST requests via HTTPClient.

  @author Rasmus,Philip og Jasper 
  @version 1.0
  @date 2023-11-16

  @license
  Permission is hereby granted, free of charge, to any person obtaining a copy of this software
  and associated documentation files. The above copyright notice and this permission notice
  shall be included in all copies or substantial portions of the Software.
*********/

#pragma region include
#include <Arduino.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ESPAsyncWebServer.h>
#include <UUID.h>
#include <ArduinoJson.h>
#include <AsyncTCP.h>
#include "SPIFFS.h"
#include <OneWire.h>
#include <DallasTemperature.h>
#include <chrono>
#pragma endregion

#pragma region variabler
// Create AsyncWebServer object on port 80
AsyncWebServer server(80);

// Create a WebSocket object
AsyncWebSocket ws("/ws");

HTTPClient http;
UUID uuid;

String BaseUrl = "https://192.168.0.111/api/TemperatureReader/";

// Search for parameter in HTTP POST request
const char *PARAM_INPUT_1 = "ssid";
const char *PARAM_INPUT_2 = "pass";
const char *PARAM_INPUT_3 = "ip";
const char *PARAM_INPUT_4 = "gateway";

const int led_pin = 14;
// Variables to save values from HTML form
String ssid;
String pass;
String ip;
String gateway;

String guid;
int ReadingInterval;

// File paths to save input values permanently
const char *ssidPath = "/ssid.txt";
const char *passPath = "/pass.txt";
const char *ipPath = "/ip.txt";
const char *gatewayPath = "/gateway.txt";
const char *deviceinfo = "/deviceinfo.text";
IPAddress localIP;
// IPAddress localIP(192, 168, 1, 200); // hardcoded

// Set your Gateway IP address
IPAddress localGateway;
// IPAddress localGateway(192, 168, 1, 1); //hardcoded
IPAddress subnet(255, 255, 0, 0);

// Timer variables
unsigned long previousMillis = 0;
const long interval = 10000; // interval to wait for Wi-Fi connection (milliseconds)

// GPIO where the DS18B20 is connected to
const int oneWireBus = 4;

// Setup a oneWire instance to communicate with any OneWire devices
OneWire oneWire(oneWireBus);

// Pass our oneWire reference to Dallas Temperature sensor
DallasTemperature sensors(&oneWire);

// Set LED GPIO
const int ledPin = 2;
// Stores LED state

String ledState;

String GetInterval();

unsigned long GettheInterval(String jsonString);
#pragma endregion

/**
 * @brief Initialize SPIFFS
 */
void initSPIFFS()
{
  if (!SPIFFS.begin(true))
  {
    Serial.println("An error has occurred while mounting SPIFFS");
  }
  Serial.println("SPIFFS mounted successfully");
}

/**
 * @brief Read file content from SPIFFS
 * 
 * @param fs File system object
 * @param path File path
 * @return String File content
 */
String readFile(fs::FS &fs, const char *path)
{
  Serial.printf("Reading file: %s\r\n", path);

  File file = fs.open(path);
  if (!file || file.isDirectory())
  {
    Serial.println("- failed to open file for reading");
    return String();
  }

  String fileContent;
  while (file.available())
  {
    fileContent = file.readStringUntil('\n');
    break;
  }
  return fileContent;
}

/**
 * @brief Write data to file in SPIFFS
 * 
 * @param fs File system object
 * @param path File path
 * @param message Data to write
 */
void writeFile(fs::FS &fs, const char *path, const char *message)
{
  Serial.printf("Writing file: %s\r\n", path);

  File file = fs.open(path, FILE_WRITE);
  if (!file)
  {
    Serial.println("- failed to open file for writing");
    return;
  }
  if (file.print(message))
  {
    Serial.println("- file written");
  }
  else
  {
    Serial.println("- write failed");
  }
}

/**
 * @brief Create device information file
 * 
 * @param fs File system object
 */
void CreatInfoFile(fs::FS &fs)
{
  Serial.printf("Writing file: %s\r\n", deviceinfo);

  File file = fs.open(deviceinfo, FILE_WRITE);
  if (!file)
  {
    Serial.println("- failed to open file for writing");
    return;
  }
  // Write the GUID to the file
  String textline ="DF23555A-E0F3-4F44-E851-08DBE7383B82 " + GetInterval();
  file.print(textline);

  // Close the file
  file.close();
  http.end();
  
}

/**
 * @brief Read device information from file
 * 
 * @param fs File system object
 * @return String Device information
 */
String ReadInfoFile(fs::FS &fs)
{
  Serial.printf("Reading file: %s\r\n", deviceinfo);

  File file = fs.open(deviceinfo, FILE_READ);
  if (!file || file.isDirectory())
  {
    Serial.println("- failed to open file for reading");
    return String();
  }

  String fileContent;
  while (file.available())
  {
    fileContent = file.readStringUntil('\n');
    break;
  }
  return fileContent;
}

/**
 * @brief check if the it can run wifi
 * 
 */
bool initWiFi()
{
  if (ssid == "" || ip == "")
  {
    Serial.println("Undefined SSID or IP address.");
    return false;
  }

  WiFi.mode(WIFI_STA);
  localIP.fromString(ip.c_str());
  localGateway.fromString(gateway.c_str());

  if (!WiFi.config(localIP, localGateway, subnet))
  {
    Serial.println("STA Failed to configure");
    return false;
  }
  WiFi.begin(ssid.c_str(), pass.c_str());
  Serial.println("Connecting to WiFi...");

  unsigned long currentMillis = millis();
  previousMillis = currentMillis;

  while (WiFi.status() != WL_CONNECTED)
  {
    currentMillis = millis();
    if (currentMillis - previousMillis >= interval)
    {
      Serial.println("Failed to connect.");
      return false;
    }
  }

  Serial.println(WiFi.localIP());
  return true;
}

/**
 * @brief Get the Temperatures from the DS18B20 temperature sensor in Celsius
 * 
 * @return the float as String 
 */
String readDSTemperatureC()
{
  // Call sensors.requestTemperatures() to issue a global temperature and Requests to all devices on the bus
  sensors.requestTemperatures();
  float tempC = sensors.getTempCByIndex(0);

  if (tempC == -127.00)
  {
    Serial.println("Failed to read from DS18B20 sensor");
    return "--";
  }
  else
  {
    Serial.print("Temperature Celsius: ");
    Serial.println(tempC);
  }
  return String(tempC);
}

// Replaces placeholder with LED state value
String processor(const String &var)
{
  if (var == "STATE")
  {
    if (digitalRead(ledPin))
    {
      ledState = "ON";
    }
    else
    {
      ledState = "OFF";
    }
    return ledState;
  }
  return String();
}

/**
 * @brief Get the interval from the server
 * 
 * @return String Interval in milliseconds
 */
String GetInterval()
{
  http.begin(BaseUrl + "GetDeviceById/DF23555A-E0F3-4F44-E851-08DBE7383B82");
  http.addHeader("Content-Type", "application/x-www-form-urlencoded");
  int httpCode = http.GET();

  // httpCode will be negative on error
  if (httpCode > 0)
  {
    // file found at server
    if (httpCode == HTTP_CODE_OK)
    {
      return String(GettheInterval(http.getString()));
      
    }
    else
    {
      
      Serial.print(httpCode);
      // HTTP header has been send and Server response header has been handled
      Serial.printf("[HTTP] GET... code: %d\n", httpCode);
    }
  }
  else
  {
    Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
    
  }
  delay(5000);
  return "60000";
  
}

/**
 * @brief Parse interval from JSON response
 * 
 * @param jsonString JSON response from the server
 * @return unsigned long Interval in milliseconds
 */
unsigned long GettheInterval(String jsonString)
{
  const size_t bufferSize = 2 * JSON_OBJECT_SIZE(6);
  DynamicJsonDocument doc(bufferSize);

  // Parse the JSON string
  DeserializationError error = deserializeJson(doc, jsonString);

  // Check for parsing errors
  if (error)
  {
    Serial.print("Error parsing JSON: ");
    Serial.println(error.c_str());
  }

  // if intervalIn is null it will return the default value 60000 miliseconds meaning 1 minute
  if(doc["intervalInMinutes"] == "Null"){
    return 60000; 
  }
  // Extract the intervalInMinutes value
  int intervalInMinutes = doc["intervalInMinutes"];
  // return the value in miliseconds
  return intervalInMinutes * 60UL * 1000UL;
}

/**
 * @brief Send data to the server via HTTP POST
 */
void PostData()
{
  http.begin(BaseUrl + "UpLoadData");
  http.addHeader("Content-Type", "application/x-www-form-urlencoded");
  //String data = ReadInfoFile(SPIFFS); 
  String data = "DF23555A-E0F3-4F44-E851-08DBE7383B82 60000";
  int specificcharacter = data.indexOf(" ");
  // Extract the substring from the beginning of 'data' to 'specificcharacter'
  Serial.println("------------------");
  Serial.println(data);
  String id = data.substring(0, specificcharacter);
  Serial.println(id+"|");
  Serial.println("------------------");
  // Assuming 'readDSTemperatureC()' is a function that returns temperature as a string
  String temperature = readDSTemperatureC();
  temperature.replace('.',',');
  // Construct the query string
  String queryString = "Id=" + id + "&Temperature=" + temperature;
  Serial.println(queryString);
  int httpCode = http.POST(queryString);

  // httpCode will be negative on error
  if (httpCode > 0)
  {
    // file found at server
    if (httpCode == HTTP_CODE_OK)
    {
      String payload = http.getString();
      Serial.println(payload);
    }
    else
    {
      // HTTP header has been send and Server response header has been handled
      Serial.printf("[HTTP] POST... code: %d\n", httpCode);
    }
  }
  else
  {
    Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
  }

  http.end();
  int delayTime = data.substring(specificcharacter,data.length()-1).toInt();
  delay(delayTime);
}

/**
 * @brief Setup function
 */
void setup()
{
  // Serial port for debugging purposes
  Serial.begin(115200);
  sensors.begin();
  initSPIFFS();

  // Set GPIO 2 as an OUTPUT
  pinMode(led_pin, OUTPUT);
  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, LOW);

  // Load values saved in SPIFFS
  ssid = readFile(SPIFFS, ssidPath);
  pass = readFile(SPIFFS, passPath);
  ip = readFile(SPIFFS, ipPath);
  gateway = readFile(SPIFFS, gatewayPath);
  //CreatInfoFile(SPIFFS);
  Serial.println(ssid);
  Serial.println(pass);
  Serial.println(ip);
  Serial.println(gateway);

  if (initWiFi())
  {
    // Route for root / web page
    server.on("/", HTTP_GET, [](AsyncWebServerRequest *request)
              { request->send(SPIFFS, "/index.html", "text/html", false, processor); });
    server.serveStatic("/", SPIFFS, "/");

    // Route to set GPIO state to HIGH
    server.on("/on", HTTP_GET, [](AsyncWebServerRequest *request)
              {
      digitalWrite(ledPin, HIGH);
      request->send(SPIFFS, "/index.html", "text/html", false, processor); });

    // Route to set GPIO state to LOW
    server.on("/off", HTTP_GET, [](AsyncWebServerRequest *request)
              {
      digitalWrite(ledPin, LOW);
      request->send(SPIFFS, "/index.html", "text/html", false, processor); });
    server.begin();
  }
  else
  {
    // Connect to Wi-Fi network with SSID and password
    Serial.println("Setting AP (Access Point)");
    // NULL sets an open Access Point
    WiFi.softAP("ESP-DaJiaHao", NULL);

    IPAddress IP = WiFi.softAPIP();
    Serial.print("AP IP address: ");
    Serial.println(IP);

    // Web Server Root URL
    server.on("/", HTTP_GET, [](AsyncWebServerRequest *request)
              { request->send(SPIFFS, "/wifimanager.html", "text/html"); });

    server.serveStatic("/", SPIFFS, "/");

    server.on("/", HTTP_POST, [](AsyncWebServerRequest *request)
              {
      int params = request->params();
      for(int i=0;i<params;i++){
        AsyncWebParameter* p = request->getParam(i);
        if(p->isPost()){
          // HTTP POST ssid value
          if (p->name() == PARAM_INPUT_1) {
            ssid = p->value().c_str();
            Serial.print("SSID set to: ");
            Serial.println(ssid);
            // Write file to save value
            writeFile(SPIFFS, ssidPath, ssid.c_str());
          }
          // HTTP POST pass value
          if (p->name() == PARAM_INPUT_2) {
            pass = p->value().c_str();
            Serial.print("Password set to: ");
            Serial.println(pass);
            // Write file to save value
            writeFile(SPIFFS, passPath, pass.c_str());
          }
          // HTTP POST ip value
          if (p->name() == PARAM_INPUT_3) {
            ip = p->value().c_str();
            Serial.print("IP Address set to: ");
            Serial.println(ip);
            // Write file to save value
            writeFile(SPIFFS, ipPath, ip.c_str());
          }
          // HTTP POST gateway value
          if (p->name() == PARAM_INPUT_4) {
            gateway = p->value().c_str();
            Serial.print("Gateway set to: ");
            Serial.println(gateway);
            // Write file to save value
            writeFile(SPIFFS, gatewayPath, gateway.c_str());
          }
          //Serial.printf("POST[%s]: %s\n", p->name().c_str(), p->value().c_str());
        }
      }
      request->send(200, "text/plain", "Done. ESP will restart, connect to your router and go to IP address: " + ip);
      delay(3000);
      ESP.restart(); });
    server.begin();
  }
}


/**
 * @brief Loop function
 */
void loop()
{
  PostData();
}