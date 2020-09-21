﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NpgsqlWithOwnedEntities;

namespace NpgsqlWithOwnedEntities.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("NpgsqlWithOwnedEntities.SimpleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SimpleEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SimpleEntity");
                });

            modelBuilder.Entity("NpgsqlWithOwnedEntities.ExtendedEntity", b =>
                {
                    b.HasBaseType("NpgsqlWithOwnedEntities.SimpleEntity");

                    b.Property<string>("ExtendedName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.HasDiscriminator().HasValue("ExtendedEntity");
                });

            modelBuilder.Entity("NpgsqlWithOwnedEntities.SimpleEntity", b =>
                {
                    b.OwnsOne("NpgsqlWithOwnedEntities.OwnedOne", "OwnedOne", b1 =>
                        {
                            b1.Property<int>("SimpleEntityId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name1")
                                .HasColumnType("TEXT")
                                .HasMaxLength(120);

                            b1.Property<int>("Number2")
                                .HasColumnType("INTEGER");

                            b1.HasKey("SimpleEntityId");

                            b1.ToTable("SimpleEntities1");

                            b1.WithOwner()
                                .HasForeignKey("SimpleEntityId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}