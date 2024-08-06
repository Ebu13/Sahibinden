using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class HomeConfiguration : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> entity)
        {
            entity.HasKey(e => e.HomeId).HasName("PK__Homes__8ED7E2137B8363FF");

            entity.HasIndex(e => e.MenuId, "IX_Homes_menu_id");
            entity.HasIndex(e => e.UserId, "IX_Homes_user_id");

            entity.Property(e => e.HomeId).HasColumnName("home_id");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(20)
                .HasColumnName("photoPath");
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
        }
    }
}
