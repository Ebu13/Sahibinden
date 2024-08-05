using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FDD334116");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            builder.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        }
    }
}
