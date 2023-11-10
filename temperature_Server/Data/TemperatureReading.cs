namespace temperature_Server.Data
{
    public class TemperatureReading : BaseEntity<int>
    {
        public Guid DeviceId { get; set; }

        public float Temperature { get; set; }

        public DateTime TimeStamp { get; set; }

        public TemperatureReaderDevice Device { get; set; }
    }
}
