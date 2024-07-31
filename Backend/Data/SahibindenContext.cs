using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{

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

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Server=EBUBEKIR13;Database=Sahibinden;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId).HasName("PK__Cars__4C9A0DB3759964D0");

                entity.Property(e => e.CarId).HasColumnName("car_id");
                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Menu).WithMany(p => p.Cars)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cars__menu_id__3D5E1FD2");

                entity.HasOne(d => d.User).WithMany(p => p.Cars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Cars__user_id__3C69FB99");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.HasKey(e => e.HomeId).HasName("PK__Homes__8ED7E2137B8363FF");

                entity.Property(e => e.HomeId).HasColumnName("home_id");
                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .HasColumnName("location");
                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Size).HasColumnName("size");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Menu).WithMany(p => p.Homes)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Homes__menu_id__412EB0B6");

                entity.HasOne(d => d.User).WithMany(p => p.Homes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Homes__user_id__403A8C7D");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenuId).HasName("PK__Menu__4CA0FADC3E077BAA");

                entity.ToTable("Menu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Menu__parent_id__398D8EEE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FDD334116");

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}