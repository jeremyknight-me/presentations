﻿// <auto-generated />
using DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataPersistence.Migrations
{
    [DbContext(typeof(SimpleContext))]
    [Migration("20230418041421_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataPersistence.Lookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Lookups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = true,
                            Name = "virtual array"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = true,
                            Name = "virtual system"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = true,
                            Name = "solid state bandwidth"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "neural hard drive"
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = true,
                            Name = "multi-byte driver"
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = true,
                            Name = "1080p interface"
                        },
                        new
                        {
                            Id = 7,
                            IsDeleted = true,
                            Name = "back-end application"
                        },
                        new
                        {
                            Id = 8,
                            IsDeleted = true,
                            Name = "solid state monitor"
                        },
                        new
                        {
                            Id = 9,
                            IsDeleted = true,
                            Name = "1080p matrix"
                        },
                        new
                        {
                            Id = 10,
                            IsDeleted = true,
                            Name = "multi-byte protocol"
                        },
                        new
                        {
                            Id = 11,
                            IsDeleted = true,
                            Name = "bluetooth matrix"
                        },
                        new
                        {
                            Id = 12,
                            IsDeleted = false,
                            Name = "cross-platform card"
                        },
                        new
                        {
                            Id = 13,
                            IsDeleted = false,
                            Name = "primary driver"
                        },
                        new
                        {
                            Id = 14,
                            IsDeleted = false,
                            Name = "solid state feed"
                        },
                        new
                        {
                            Id = 15,
                            IsDeleted = true,
                            Name = "back-end pixel"
                        },
                        new
                        {
                            Id = 16,
                            IsDeleted = false,
                            Name = "virtual system"
                        },
                        new
                        {
                            Id = 17,
                            IsDeleted = false,
                            Name = "haptic program"
                        },
                        new
                        {
                            Id = 18,
                            IsDeleted = true,
                            Name = "1080p system"
                        },
                        new
                        {
                            Id = 19,
                            IsDeleted = false,
                            Name = "primary interface"
                        },
                        new
                        {
                            Id = 20,
                            IsDeleted = true,
                            Name = "neural firewall"
                        },
                        new
                        {
                            Id = 21,
                            IsDeleted = true,
                            Name = "auxiliary array"
                        },
                        new
                        {
                            Id = 22,
                            IsDeleted = false,
                            Name = "optical microchip"
                        },
                        new
                        {
                            Id = 23,
                            IsDeleted = false,
                            Name = "multi-byte program"
                        },
                        new
                        {
                            Id = 24,
                            IsDeleted = true,
                            Name = "virtual alarm"
                        },
                        new
                        {
                            Id = 25,
                            IsDeleted = true,
                            Name = "open-source program"
                        },
                        new
                        {
                            Id = 26,
                            IsDeleted = true,
                            Name = "digital application"
                        },
                        new
                        {
                            Id = 27,
                            IsDeleted = false,
                            Name = "digital system"
                        },
                        new
                        {
                            Id = 28,
                            IsDeleted = false,
                            Name = "haptic protocol"
                        },
                        new
                        {
                            Id = 29,
                            IsDeleted = false,
                            Name = "1080p circuit"
                        },
                        new
                        {
                            Id = 30,
                            IsDeleted = false,
                            Name = "mobile port"
                        },
                        new
                        {
                            Id = 31,
                            IsDeleted = true,
                            Name = "cross-platform port"
                        },
                        new
                        {
                            Id = 32,
                            IsDeleted = true,
                            Name = "cross-platform circuit"
                        },
                        new
                        {
                            Id = 33,
                            IsDeleted = true,
                            Name = "mobile card"
                        },
                        new
                        {
                            Id = 34,
                            IsDeleted = true,
                            Name = "redundant application"
                        },
                        new
                        {
                            Id = 35,
                            IsDeleted = false,
                            Name = "digital microchip"
                        },
                        new
                        {
                            Id = 36,
                            IsDeleted = true,
                            Name = "open-source firewall"
                        },
                        new
                        {
                            Id = 37,
                            IsDeleted = false,
                            Name = "cross-platform circuit"
                        },
                        new
                        {
                            Id = 38,
                            IsDeleted = true,
                            Name = "online alarm"
                        },
                        new
                        {
                            Id = 39,
                            IsDeleted = true,
                            Name = "optical firewall"
                        },
                        new
                        {
                            Id = 40,
                            IsDeleted = true,
                            Name = "back-end bandwidth"
                        },
                        new
                        {
                            Id = 41,
                            IsDeleted = false,
                            Name = "auxiliary pixel"
                        },
                        new
                        {
                            Id = 42,
                            IsDeleted = true,
                            Name = "neural matrix"
                        },
                        new
                        {
                            Id = 43,
                            IsDeleted = true,
                            Name = "optical circuit"
                        },
                        new
                        {
                            Id = 44,
                            IsDeleted = true,
                            Name = "1080p program"
                        },
                        new
                        {
                            Id = 45,
                            IsDeleted = false,
                            Name = "wireless monitor"
                        },
                        new
                        {
                            Id = 46,
                            IsDeleted = false,
                            Name = "cross-platform card"
                        },
                        new
                        {
                            Id = 47,
                            IsDeleted = true,
                            Name = "virtual sensor"
                        },
                        new
                        {
                            Id = 48,
                            IsDeleted = true,
                            Name = "cross-platform alarm"
                        },
                        new
                        {
                            Id = 49,
                            IsDeleted = false,
                            Name = "open-source circuit"
                        },
                        new
                        {
                            Id = 50,
                            IsDeleted = true,
                            Name = "redundant sensor"
                        },
                        new
                        {
                            Id = 51,
                            IsDeleted = true,
                            Name = "primary transmitter"
                        },
                        new
                        {
                            Id = 52,
                            IsDeleted = true,
                            Name = "back-end interface"
                        },
                        new
                        {
                            Id = 53,
                            IsDeleted = true,
                            Name = "digital array"
                        },
                        new
                        {
                            Id = 54,
                            IsDeleted = false,
                            Name = "back-end interface"
                        },
                        new
                        {
                            Id = 55,
                            IsDeleted = false,
                            Name = "optical pixel"
                        },
                        new
                        {
                            Id = 56,
                            IsDeleted = false,
                            Name = "digital monitor"
                        },
                        new
                        {
                            Id = 57,
                            IsDeleted = true,
                            Name = "redundant sensor"
                        },
                        new
                        {
                            Id = 58,
                            IsDeleted = false,
                            Name = "redundant bus"
                        },
                        new
                        {
                            Id = 59,
                            IsDeleted = false,
                            Name = "solid state feed"
                        },
                        new
                        {
                            Id = 60,
                            IsDeleted = true,
                            Name = "multi-byte hard drive"
                        },
                        new
                        {
                            Id = 61,
                            IsDeleted = true,
                            Name = "neural sensor"
                        },
                        new
                        {
                            Id = 62,
                            IsDeleted = true,
                            Name = "mobile feed"
                        },
                        new
                        {
                            Id = 63,
                            IsDeleted = true,
                            Name = "open-source pixel"
                        },
                        new
                        {
                            Id = 64,
                            IsDeleted = false,
                            Name = "haptic microchip"
                        },
                        new
                        {
                            Id = 65,
                            IsDeleted = true,
                            Name = "wireless panel"
                        },
                        new
                        {
                            Id = 66,
                            IsDeleted = true,
                            Name = "1080p sensor"
                        },
                        new
                        {
                            Id = 67,
                            IsDeleted = true,
                            Name = "1080p bandwidth"
                        },
                        new
                        {
                            Id = 68,
                            IsDeleted = false,
                            Name = "wireless matrix"
                        },
                        new
                        {
                            Id = 69,
                            IsDeleted = false,
                            Name = "digital alarm"
                        },
                        new
                        {
                            Id = 70,
                            IsDeleted = false,
                            Name = "haptic microchip"
                        },
                        new
                        {
                            Id = 71,
                            IsDeleted = false,
                            Name = "open-source alarm"
                        },
                        new
                        {
                            Id = 72,
                            IsDeleted = false,
                            Name = "primary interface"
                        },
                        new
                        {
                            Id = 73,
                            IsDeleted = false,
                            Name = "digital bus"
                        },
                        new
                        {
                            Id = 74,
                            IsDeleted = true,
                            Name = "digital circuit"
                        },
                        new
                        {
                            Id = 75,
                            IsDeleted = true,
                            Name = "multi-byte circuit"
                        },
                        new
                        {
                            Id = 76,
                            IsDeleted = false,
                            Name = "open-source monitor"
                        },
                        new
                        {
                            Id = 77,
                            IsDeleted = true,
                            Name = "multi-byte panel"
                        },
                        new
                        {
                            Id = 78,
                            IsDeleted = true,
                            Name = "solid state transmitter"
                        },
                        new
                        {
                            Id = 79,
                            IsDeleted = true,
                            Name = "solid state protocol"
                        },
                        new
                        {
                            Id = 80,
                            IsDeleted = true,
                            Name = "redundant transmitter"
                        },
                        new
                        {
                            Id = 81,
                            IsDeleted = true,
                            Name = "online monitor"
                        },
                        new
                        {
                            Id = 82,
                            IsDeleted = false,
                            Name = "bluetooth port"
                        },
                        new
                        {
                            Id = 83,
                            IsDeleted = false,
                            Name = "back-end hard drive"
                        },
                        new
                        {
                            Id = 84,
                            IsDeleted = false,
                            Name = "back-end pixel"
                        },
                        new
                        {
                            Id = 85,
                            IsDeleted = true,
                            Name = "back-end firewall"
                        },
                        new
                        {
                            Id = 86,
                            IsDeleted = true,
                            Name = "auxiliary panel"
                        },
                        new
                        {
                            Id = 87,
                            IsDeleted = false,
                            Name = "wireless capacitor"
                        },
                        new
                        {
                            Id = 88,
                            IsDeleted = true,
                            Name = "open-source matrix"
                        },
                        new
                        {
                            Id = 89,
                            IsDeleted = false,
                            Name = "haptic array"
                        },
                        new
                        {
                            Id = 90,
                            IsDeleted = true,
                            Name = "cross-platform microchip"
                        },
                        new
                        {
                            Id = 91,
                            IsDeleted = true,
                            Name = "redundant monitor"
                        },
                        new
                        {
                            Id = 92,
                            IsDeleted = true,
                            Name = "optical monitor"
                        },
                        new
                        {
                            Id = 93,
                            IsDeleted = false,
                            Name = "1080p capacitor"
                        },
                        new
                        {
                            Id = 94,
                            IsDeleted = true,
                            Name = "1080p panel"
                        },
                        new
                        {
                            Id = 95,
                            IsDeleted = false,
                            Name = "mobile system"
                        },
                        new
                        {
                            Id = 96,
                            IsDeleted = true,
                            Name = "multi-byte application"
                        },
                        new
                        {
                            Id = 97,
                            IsDeleted = false,
                            Name = "open-source capacitor"
                        },
                        new
                        {
                            Id = 98,
                            IsDeleted = false,
                            Name = "redundant application"
                        },
                        new
                        {
                            Id = 99,
                            IsDeleted = false,
                            Name = "back-end program"
                        },
                        new
                        {
                            Id = 100,
                            IsDeleted = false,
                            Name = "virtual bandwidth"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
