using DigitalNotesManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalNotesManager.Infrastructure.Data.EntityConfigs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.CreatedAt)
                .IsRequired();


            // Unique index on UserName for faster lookups and to prevent duplicates
            builder.HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}