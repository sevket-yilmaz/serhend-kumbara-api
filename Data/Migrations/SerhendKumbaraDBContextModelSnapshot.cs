﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SerhendKumbara.Data.Entity;

#nullable disable

namespace SerhendKumbara.Data.Migrations
{
    [DbContext(typeof(SerhendKumbaraDBContext))]
    partial class SerhendKumbaraDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SerhendKumbara.Data.Entity.Placemark", b =>
                {
                    b.Property<int>("PlacemarkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlacemarkID"));

                    b.Property<DateTime>("LastVisit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("VisitPeriod")
                        .HasColumnType("integer");

                    b.HasKey("PlacemarkID");

                    b.ToTable("Placemarks");
                });
#pragma warning restore 612, 618
        }
    }
}
