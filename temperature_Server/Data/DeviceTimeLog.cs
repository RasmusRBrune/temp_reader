namespace temperature_Server.Data
{
    public class DeviceTimeLog : BaseEntity<int>
    {

        public Guid DeviceId { get; set; }

        public DateTime TimeStarted { get; set; } 

        public DateTime? TimeStopped { get; set; }

        public TemperatureReaderDevice Device { get; set; }
    }
}
