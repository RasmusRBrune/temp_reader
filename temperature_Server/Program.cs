using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using temperature_Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using temperature_Server.Services;
using temperature_Server.Repositories;
using temperature_Server.Data.Context;
using Microsoft.AspNetCore.Components.Authorization;
using temperature_Server.Areas.Identity;
using System.Security.Cryptography.X509Certificates;
using ApexCharts;
using Microsoft.AspNetCore.WebSockets;
using temperature_Server.Sockets;
using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceRepository), typeof(TemperatureReaderDeviceRepository));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceKeyRepository), typeof(TemperatureReaderDeviceKeyRepository));
builder.Services.AddTransient(typeof(IDeviceTimeLogRepository), typeof(DeviceTimeLogRepository));
builder.Services.AddTransient(typeof(ITemperatureReadingRepository), typeof(TemperatureReadingRepository));

builder.Services.AddTransient(typeof(IAccountService), typeof(AccountService));
builder.Services.AddTransient(typeof(ITemperatureReaderDeviceService), typeof(TemperatureReaderDeviceService));
builder.Services.AddTransient(typeof(IDeviceTimeLogService), typeof(DeviceTimeLogService));
builder.Services.AddTransient(typeof(ITemperatureReadingService), typeof(TemperatureReadingService));

var IdentityConnection = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'temperature_ServerContextConnection' not found.");

builder.Services.AddDbContext<temperature_ServerContext>(options => options.UseSqlServer(IdentityConnection));

var connectionString = builder.Configuration.GetConnectionString("TempConnection") ?? throw new InvalidOperationException("Connection string 'temperature_ServerContextConnection' not found.");

builder.Services.AddDbContext<TempReaderContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<temperature_ServerContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddHttpClient("LocalApi", client => client.BaseAddress = new Uri("https://esp32temperaturereader.dk"));
//builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(options =>
    {
        // Grab the secret value from the secret store.
        string? secretValue = builder.Configuration.GetValue<string>("KestrelCertificatePassword");
        options.ServerCertificate = new X509Certificate2("ESP32TemperatureReader.pfx", secretValue);
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

//webSocketOptions.AllowedOrigins.Add("https://client.com");
//webSocketOptions.AllowedOrigins.Add("https://www.client.com");
//webSocketOptions.AllowedOrigins.Add("*");

app.UseWebSockets(webSocketOptions);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            //using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //await SocketExtensions.Echo(webSocket);
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var webSocketHandler = ActivatorUtilities.CreateInstance<TemperatureReaderWebSocketHandler>(app.Services);

            await webSocketHandler.HandleWebSocketAsync(context, webSocket);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    else
    {
        await next(context);
    }

});
app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();




