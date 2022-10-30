﻿// <auto-generated />
using System;
using Library.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Database.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20221030185243_CreateEventStore")]
    partial class CreateEventStore
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Library.Database.Entities.EventStore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("BLOB");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Revision")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StreamId", "Revision")
                        .IsUnique();

                    b.ToTable("EventStore");
                });
#pragma warning restore 612, 618
        }
    }
}
