namespace temperature_Server.Data
{
    public class Account : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public List<TemperatureReaderDevice> Devices { get; set; }
    }
}