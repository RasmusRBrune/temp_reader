namespace temperature_Server.Data
{
    public class DeviceTimeLog
    {
        public int Id { get; set; }

        public Guid DeviceId { get; set; }

        public DateTime TimeStarted { get; set; } 

        public DateTime? TimeStopped { get; set; }

        public TemperatureReaderDevice Device { get; set; }
    }
}
