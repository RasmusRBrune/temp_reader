namespace temperature_Server.Data
{
    public class TemperatureReading
    {
        public int Id { get; set; }

        public Guid DeviceGuid { get; set; }

        public float Temperature { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
