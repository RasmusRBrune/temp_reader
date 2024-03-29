﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using temperature_Server.Data.Context;

#nullable disable

namespace temperature_Server.Migrations.TempReader
{
    [DbContext(typeof(TempReaderContext))]
    [Migration("20231114120936_test1")]
    partial class test1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("temperature_Server.Data.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("temperature_Server.Data.DeviceTimeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStarted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("TimeStopped")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceTimeLog");
                });

            modelBuilder.Entity("temperature_Server.Data.TemperatureReaderDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.Property<int>("PlacementWeight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DisplayName")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("temperature_Server.Data.TemperatureReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("TempsReading");
                });

            modelBuilder.Entity("temperature_Server.Data.DeviceTimeLog", b =>
                {
                    b.HasOne("temperature_Server.Data.TemperatureReaderDevice", "Device")
                        .WithMany("TimeLogs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("temperature_Server.Data.TemperatureReaderDevice", b =>
                {
                    b.HasOne("temperature_Server.Data.Account", "Account")
                        .WithMany("Devices")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("temperature_Server.Data.TemperatureReading", b =>
                {
                    b.HasOne("temperature_Server.Data.TemperatureReaderDevice", "Device")
                        .WithMany("ReadingLogs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("temperature_Server.Data.Account", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("temperature_Server.Data.TemperatureReaderDevice", b =>
                {
                    b.Navigation("ReadingLogs");

                    b.Navigation("TimeLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
