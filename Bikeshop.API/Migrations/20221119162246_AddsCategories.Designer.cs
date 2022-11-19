﻿// <auto-generated />
using System;
using Bikeshop.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bikeshop.API.Migrations
{
    [DbContext(typeof(BikeshopContext))]
    [Migration("20221119162246_AddsCategories")]
    partial class AddsCategories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bikeshop.API.Entities.Bike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullDescription")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Bikes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("184c54ce-1811-40d9-9f6f-8eeff98035ca"),
                            FullDescription = "It might look very silly nowadays, but it sure does turn heads.",
                            Name = "Penny Farthing",
                            ShortDescription = "That iconic bike"
                        },
                        new
                        {
                            Id = new Guid("aa655ffe-74e0-480f-bb3c-49bd7055620e"),
                            FullDescription = "The number one choice for mountainous adventures.",
                            Name = "Aplha Explorer",
                            ShortDescription = "Mud has nothing on it"
                        });
                });

            modelBuilder.Entity("Bikeshop.API.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"),
                            Description = "The bikes you always wanted",
                            Name = "Specialty bikes"
                        },
                        new
                        {
                            Id = new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"),
                            Description = "Metal goats",
                            Name = "Mountainbikes"
                        });
                });

            modelBuilder.Entity("Bikeshop.API.Entities.Bike", b =>
                {
                    b.HasOne("Bikeshop.API.Entities.Category", "Category")
                        .WithMany("Bikes")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bikeshop.API.Entities.Category", b =>
                {
                    b.Navigation("Bikes");
                });
#pragma warning restore 612, 618
        }
    }
}
