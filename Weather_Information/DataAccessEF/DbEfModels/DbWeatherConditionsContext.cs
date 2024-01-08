using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessEF.DbEfModels;

public partial class DbWeatherConditionsContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbWeatherConditionsContext()
    {
    }

    public DbWeatherConditionsContext(DbContextOptions<DbWeatherConditionsContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<TblCity> TblCities { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblWeatherLog> TblWeatherLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("Connectionstring");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCity>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__TblCity__F2D21A961560D8EF");

            entity.ToTable("TblCity");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateOn).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TblCities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__TblCity__Country__267ABA7A");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__TblCount__10D160BFE1A3A009");

            entity.ToTable("TblCountry");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CapitalCity).HasMaxLength(100);
            entity.Property(e => e.Continent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.Iso2).HasMaxLength(2);
            entity.Property(e => e.Iso3).HasMaxLength(3);
        });

        modelBuilder.Entity<TblWeatherLog>(entity =>
        {
            entity.HasKey(e => e.WeatherLogsId).HasName("PK__TblWeath__D2B5861EC23E0183");

            entity.Property(e => e.WeatherLogsId).HasColumnName("WeatherLogsID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Humidity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Icon).HasMaxLength(200);
            entity.Property(e => e.TempC)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Temp_c");
            entity.Property(e => e.TempF)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Temp_f");
            entity.Property(e => e.WindDir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Wind_dir");
            entity.Property(e => e.WindKph)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Wind_kph");

            entity.HasOne(d => d.City).WithMany(p => p.TblWeatherLogs)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__TblWeathe__CityI__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
