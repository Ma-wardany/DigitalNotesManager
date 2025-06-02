using DigitalNotesManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalNotesManager.Infrastructure.Data.EntityConfigs
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(n => n.CreationDate)
                .IsRequired();

            builder.Property(n => n.ReminderDate)
                .IsRequired(false);


            builder.HasOne(n => n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Index on CategoryId for performance
            builder.HasIndex(n => n.CategoryId);
        }
    }
}