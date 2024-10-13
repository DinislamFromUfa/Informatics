using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder
                .HasKey(l => l.Id);

            builder
                .HasOne(l => l.Homework)
                .WithOne(h => h.Lesson)
                .HasForeignKey<Homework>(h => h.LessonId);
        }
    }
}
