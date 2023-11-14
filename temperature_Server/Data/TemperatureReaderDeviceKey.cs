namespace temperature_Server.Data
{
	public class TemperatureReaderDeviceKey : BaseEntity<int>
	{
		public Guid DeviceId { get; set; }
		public string Key { get; set; }
		public TemperatureReaderDevice Device { get; set; }

	}
}
