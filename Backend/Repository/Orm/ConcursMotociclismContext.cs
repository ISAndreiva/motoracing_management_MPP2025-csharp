using System.Configuration;
using ConcursMotociclism.domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Orm;

public partial class ConcursMotociclismContext : DbContext
{
    public ConcursMotociclismContext()
    {
    }

    public ConcursMotociclismContext(DbContextOptions<ConcursMotociclismContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<RaceRegistration> RaceRegistrations { get; set; }

    public virtual DbSet<Racer> Racers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var urlLin = ConfigurationManager.ConnectionStrings["linux_db"].ConnectionString;
        var urlWin = ConfigurationManager.ConnectionStrings["windows_db"].ConnectionString;
        var url = Environment.OSVersion.ToString().Contains("Unix") ? urlLin : urlWin;
        optionsBuilder.UseSqlite(url);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Race");

            entity.Property(e => e.Id).HasColumnName("uuid").HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.RaceClass).HasColumnType("INT").HasColumnName("class");
            entity.Property(e => e.RaceName).HasColumnType("varchar(100)").HasColumnName("name");
        });

        modelBuilder.Entity<RaceRegistration>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RaceRegistration");

            entity.Property(e => e.Id).HasColumnName("uuid").HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.HasOne(d => d.Race).WithMany().HasForeignKey("race");
            entity.HasOne(d => d.Racer).WithMany().HasForeignKey("racer");
            
        });

        modelBuilder.Entity<Racer>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Racer");

            entity.Property(e => e.Id).HasColumnName("uuid").HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.Cnp).HasColumnType("varchar(13)").HasColumnName("cnp");
            entity.Property(e => e.Name).HasColumnType("varchar(100)").HasColumnName("name");

            entity.HasOne(d => d.Team).WithMany().HasForeignKey("team");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Team");

            entity.Property(e => e.Id).HasColumnName("uuid").HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.Name).HasColumnType("varchar(100)").HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("uuid").HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.PasswordHash).HasColumnType("varchar(128)").HasColumnName("password_hash");
            entity.Property(e => e.Username).HasColumnType("varchar(100)").HasColumnName("username");
        });
    }
}
