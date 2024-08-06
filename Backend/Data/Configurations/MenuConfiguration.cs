using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__4CA0FADC3E077BAA");

            entity.ToTable("Menu");

            entity.HasIndex(e => e.ParentId, "IX_Menu_parent_id");

            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.Amblem)
                .HasMaxLength(30)
                .HasColumnName("amblem");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Menu__parent_id__398D8EEE");
        }
    }
}
