using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(e => e.CarId).HasName("PK__Cars__4C9A0DB3759964D0");

            builder.HasIndex(e => e.MenuId, "IX_Cars_menu_id");
            builder.HasIndex(e => e.UserId, "IX_Cars_user_id");

            builder.Property(e => e.CarId).HasColumnName("car_id");
            builder.Property(e => e.MenuId).HasColumnName("menu_id");
            builder.Property(e => e.PhotoPath)
                .HasMaxLength(20)
                .HasColumnName("photoPath");
            builder.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Year).HasColumnName("year");

            builder.HasOne(d => d.Menu).WithMany(p => p.Cars)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__menu_id__3D5E1FD2");

            builder.HasOne(d => d.User).WithMany(p => p.Cars)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cars__user_id__3C69FB99");
        }
    }
}
