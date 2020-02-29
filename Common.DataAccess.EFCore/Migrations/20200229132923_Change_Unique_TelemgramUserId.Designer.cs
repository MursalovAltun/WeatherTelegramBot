﻿// <auto-generated />
using System;
using Common.DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.DataAccess.EFCore.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200229132923_Change_Unique_TelemgramUserId")]
    partial class Change_Unique_TelemgramUserId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Common.Entities.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(2) CHARACTER SET utf8mb4")
                        .HasMaxLength(2)
                        .HasDefaultValue("en");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TelegramUserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UtcOffset")
                        .HasColumnType("int");

                    b.Property<string>("WaitingFor")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("TelegramUserId")
                        .IsUnique();

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("Common.Entities.SubscriberSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReceiveDailyWeather")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MeasureSystem")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10)
                        .HasDefaultValue("metric");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId")
                        .IsUnique();

                    b.ToTable("SubscriberSettings");
                });

            modelBuilder.Entity("Common.Entities.SubscriberSettings", b =>
                {
                    b.HasOne("Common.Entities.Subscriber", "Subscriber")
                        .WithOne("Settings")
                        .HasForeignKey("Common.Entities.SubscriberSettings", "SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
