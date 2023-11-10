using Microsoft.EntityFrameworkCore;

namespace temperature_Server.Data.Context
{
    public class TempReaderContext : DbContext
    {
        public TempReaderContext(DbContextOptions<TempReaderContext> options) : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<TemperatureReading> TempsReading { get; set; }
        public virtual DbSet<DeviceTimeLog> DeviceTimeLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
