﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlantCareFramework.Data;

#nullable disable

namespace PlantCareFramework.Migrations
{
    [DbContext(typeof(PlantCareContext))]
    partial class PlantCareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlantCareFramework.Models.Light", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LightIntensity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Light");
                });

            modelBuilder.Entity("PlantCareFramework.Models.Place", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("PlantCareFramework.Models.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LightId")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("WaterQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LightId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Plant");
                });

            modelBuilder.Entity("PlantCareFramework.Models.Plant", b =>
                {
                    b.HasOne("PlantCareFramework.Models.Light", "Light")
                        .WithMany()
                        .HasForeignKey("LightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlantCareFramework.Models.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Light");

                    b.Navigation("Place");
                });
#pragma warning restore 612, 618
        }
    }
}