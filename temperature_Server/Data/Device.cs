namespace temperature_Server.Data
{
    public class Device
    {

        public Guid DeviceId { get; set; }

        public string DisplayName { get; set; }

        public DateTime LastKonwStartTime { get; set; }

        public int PlacementWeihgt { get; set; }
    }
}
