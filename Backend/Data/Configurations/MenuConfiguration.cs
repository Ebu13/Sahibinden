using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(e => e.MenuId).HasName("PK__Menu__4CA0FADC3E077BAA");

            builder.ToTable("Menu");

            builder.HasIndex(e => e.ParentId, "IX_Menu_parent_id");

            builder.Property(e => e.MenuId).HasColumnName("menu_id");
            builder.Property(e => e.Amblem)
                .HasMaxLength(30)
                .HasColumnName("amblem");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            builder.Property(e => e.ParentId).HasColumnName("parent_id");

            builder.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Menu__parent_id__398D8EEE");
        }
    }
}
