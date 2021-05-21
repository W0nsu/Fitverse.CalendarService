﻿// <auto-generated />
using System;
using Fitverse.CalendarService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitverse.CalendarService.Migrations
{
    [DbContext(typeof(CalendarContext))]
    [Migration("20210316103116_TimetableAdd-and-classEdit")]
    partial class TimetableAddandclassEdit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fitverse.CalendarService.Models.CalendarClass", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndingTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartingTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TimetableId")
                        .HasColumnType("int");

                    b.HasKey("ClassId");

                    b.HasIndex("TimetableId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Fitverse.CalendarService.Models.ClassType", b =>
                {
                    b.Property<int>("ClassTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Limit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Room")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ClassTypeId");

                    b.ToTable("ClassTypes");
                });

            modelBuilder.Entity("Fitverse.CalendarService.Models.Timetable", b =>
                {
                    b.Property<int>("TimetableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ClassesStartingTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndingDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("Date");

                    b.HasKey("TimetableId");

                    b.HasIndex("ClassTypeId");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("Fitverse.CalendarService.Models.CalendarClass", b =>
                {
                    b.HasOne("Fitverse.CalendarService.Models.Timetable", "Timetable")
                        .WithMany()
                        .HasForeignKey("TimetableId");

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("Fitverse.CalendarService.Models.Timetable", b =>
                {
                    b.HasOne("Fitverse.CalendarService.Models.ClassType", "ClassType")
                        .WithMany()
                        .HasForeignKey("ClassTypeId");

                    b.Navigation("ClassType");
                });
#pragma warning restore 612, 618
        }
    }
}