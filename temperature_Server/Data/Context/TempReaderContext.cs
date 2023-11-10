﻿using Microsoft.EntityFrameworkCore;

namespace temperature_Server.Data.Context
{
    public class TempReaderContext : DbContext
    {
        public TempReaderContext(DbContextOptions<TempReaderContext> options) : base(options)
        {
        }

        public virtual DbSet<TemperatureReaderDevice> Devices { get; set; }
        public virtual DbSet<TemperatureReading> TempsReading { get; set; }
        public virtual DbSet<DeviceTimeLog> DeviceTimeLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TemperatureReaderDevice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.PlacementWeight).IsRequired();
                entity.Property(e => e.DisplayName).HasMaxLength(48).IsRequired();
                entity.HasIndex(e => e.DisplayName);

                entity.HasMany(e=>e.ReadingLogs).WithOne(e=>e.Device).HasForeignKey(e=>e.DeviceId);
                entity.HasMany(e=>e.TimeLogs).WithOne(e=>e.Device).HasForeignKey(e=>e.DeviceId);
            });
            modelBuilder.Entity<TemperatureReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.Temperature).IsRequired();
                entity.Property(e => e.TimeStamp).HasDefaultValueSql("getdate()").IsRequired();
            });
            modelBuilder.Entity<DeviceTimeLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.TimeStarted).HasDefaultValueSql("getdate()").IsRequired();
                entity.Property(e => e.TimeStopped).IsRequired(false);

            });
        }
    }
}