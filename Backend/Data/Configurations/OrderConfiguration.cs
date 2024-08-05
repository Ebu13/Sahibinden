using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderId).HasName("PK__Orders__4659622964ACD4C3");

            builder.Property(e => e.OrderId).HasColumnName("order_id");
            builder.Property(e => e.MenuId).HasColumnName("menu_id");
            builder.Property(e => e.ProductType)
                .HasMaxLength(50)
                .HasColumnName("product_type");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Menu).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__menu_id__70DDC3D8");

            builder.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__user_id__6FE99F9F");
        }
    }
}
