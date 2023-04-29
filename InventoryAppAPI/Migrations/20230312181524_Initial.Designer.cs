﻿// <auto-generated />
using System;
using InventoryAppAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryAppAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230312181524_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryAppAPI.Entities.Base.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.StockItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("InventoriedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("InventoriedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("StockItems", (string)null);
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Building", b =>
                {
                    b.HasBaseType("InventoryAppAPI.Entities.Base.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.ToTable("Buildings", "dict");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Location", b =>
                {
                    b.HasBaseType("InventoryAppAPI.Entities.Base.Entity");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("BuildingId");

                    b.HasIndex("RoomId");

                    b.ToTable("Locations", "dict");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Product", b =>
                {
                    b.HasBaseType("InventoryAppAPI.Entities.Base.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.ToTable("Products", "dict");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Room", b =>
                {
                    b.HasBaseType("InventoryAppAPI.Entities.Base.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.ToTable("Rooms", "dict");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.StockItems", b =>
                {
                    b.HasOne("InventoryAppAPI.Entities.Dicts.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryAppAPI.Entities.Dicts.Product", "Product")
                        .WithMany("StockItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Building", b =>
                {
                    b.HasOne("InventoryAppAPI.Entities.Base.Entity", null)
                        .WithOne()
                        .HasForeignKey("InventoryAppAPI.Entities.Dicts.Building", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Location", b =>
                {
                    b.HasOne("InventoryAppAPI.Entities.Dicts.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryAppAPI.Entities.Base.Entity", null)
                        .WithOne()
                        .HasForeignKey("InventoryAppAPI.Entities.Dicts.Location", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InventoryAppAPI.Entities.Dicts.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Product", b =>
                {
                    b.HasOne("InventoryAppAPI.Entities.Base.Entity", null)
                        .WithOne()
                        .HasForeignKey("InventoryAppAPI.Entities.Dicts.Product", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Room", b =>
                {
                    b.HasOne("InventoryAppAPI.Entities.Base.Entity", null)
                        .WithOne()
                        .HasForeignKey("InventoryAppAPI.Entities.Dicts.Room", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryAppAPI.Entities.Dicts.Product", b =>
                {
                    b.Navigation("StockItems");
                });
#pragma warning restore 612, 618
        }
    }
}
