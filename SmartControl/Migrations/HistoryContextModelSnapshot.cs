﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartControl.Api.SQL;

namespace SmartControl.Migrations
{
    [DbContext(typeof(HistoryContext))]
    partial class HistoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("SmartControl.Api.SQL.SqlParameter", b =>
                {
                    b.Property<int>("SqlParameterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SqlParameterId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("SmartControl.Api.SQL.SqlTimePeriod", b =>
                {
                    b.Property<string>("Begining")
                        .HasColumnType("TEXT");

                    b.Property<string>("End")
                        .HasColumnType("TEXT");

                    b.Property<int>("SqlParameterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Begining", "End");

                    b.ToTable("TimePeriods");
                });

            modelBuilder.Entity("SmartControl.Api.SQL.SqlValueInTime", b =>
                {
                    b.Property<string>("SqlValueInTimeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SqlParameterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("SqlValueInTimeId");

                    b.HasIndex("SqlParameterId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("SmartControl.Api.SQL.SqlValueInTime", b =>
                {
                    b.HasOne("SmartControl.Api.SQL.SqlParameter", null)
                        .WithMany("Values")
                        .HasForeignKey("SqlParameterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
