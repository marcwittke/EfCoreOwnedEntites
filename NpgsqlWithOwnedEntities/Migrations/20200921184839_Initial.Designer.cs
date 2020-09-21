﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NpgsqlWithOwnedEntities;

namespace NpgsqlWithOwnedEntities.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20200921184839_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("test33")
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NpgsqlWithOwnedEntities.SimpleEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SimpleEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SimpleEntity");
                });

            modelBuilder.Entity("NpgsqlWithOwnedEntities.ExtendedEntity", b =>
                {
                    b.HasBaseType("NpgsqlWithOwnedEntities.SimpleEntity");

                    b.Property<string>("ExtendedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasDiscriminator().HasValue("ExtendedEntity");
                });
#pragma warning restore 612, 618
        }
    }
}