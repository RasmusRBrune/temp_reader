namespace temperature_Server.Data
{
    public class DeviceTimeLog
    {
        public int Id { get; set; }

        public Guid DeviceGuid { get; set; }

        public DateTime TimeStart { get; set; } 

        public DateTime TimeStopped { get; set; }
    }
}
