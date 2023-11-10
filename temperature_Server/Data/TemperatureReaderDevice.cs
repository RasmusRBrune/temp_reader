namespace temperature_Server.Data
{
    public class TemperatureReaderDevice : BaseEntity<Guid>
    {
        public string DisplayName { get; set; }

        public int PlacementWeight { get; set; }

        public List<TemperatureReading> ReadingLogs { get; set; }
        public List<DeviceTimeLog> TimeLogs { get; set; }
    }
}
