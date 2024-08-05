using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data.Configurations;

namespace Backend.Data;

public partial class SahibindenContext : DbContext
{
    public SahibindenContext()
    {
    }

    public SahibindenContext(DbContextOptions<SahibindenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<Home> Homes { get; set; }
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=EBUBEKIR13;Database=Sahibinden;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new HomeConfiguration());
        modelBuilder.ApplyConfiguration(new MenuConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
