namespace temperature_Server.Data
{
    public class TemperatureReaderDevice : BaseEntity<Guid>
    {
        public Guid? AccountId { get; set; }
        public string DisplayName { get; set; }

        public int IntervalInMinutes { get; set; }
        public int PlacementWeight { get; set; }
        public Account? Account { get; set; }

        public List<TemperatureReading> ReadingLogs { get; set; }
        public List<DeviceTimeLog> TimeLogs { get; set; }
    }
}
