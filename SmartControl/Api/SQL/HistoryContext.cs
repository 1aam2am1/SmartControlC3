using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace SmartControl.Api.SQL
{
    public class HistoryContext : DbContext
    {
        public DbSet<SqlParameter> Parameters { get; set; }
        public DbSet<SqlValueInTime> Values { get; set; }
        public DbSet<SqlTimePeriod> TimePeriods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=HistoryData.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<DateTime, string>(
                v => v.ToString("yyyy-MM-ddTH:mm"),
                v => DateTime.ParseExact(v, "yyyy-MM-ddTH:mm", CultureInfo.InvariantCulture));

            modelBuilder.Entity<SqlParameter>()
                .Property(v => v.SqlParameterId)
                .IsRequired()
                .ValueGeneratedNever();

            modelBuilder.Entity<SqlValueInTime>()
                .Property(v => v.SqlValueInTimeId)
                .HasConversion(converter);

            modelBuilder.Entity<SqlValueInTime>()
                .Property(v => v.SqlParameterId)
                .IsRequired();

            modelBuilder.Entity<SqlTimePeriod>()
                .Property(v => v.SqlParameterId)
                .IsRequired();

            modelBuilder.Entity<SqlTimePeriod>()
                .Property(v => v.Begining)
                .HasConversion(converter);

            modelBuilder.Entity<SqlTimePeriod>()
                .Property(v => v.End)
                .HasConversion(converter);

            modelBuilder.Entity<SqlTimePeriod>()
                .HasKey(v => new { v.Begining, v.End });
        }
    }
}
