using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany(c => c.ListOfStudentsOnCourse)
                .WithMany(s => s.Courses);
            builder
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course).HasForeignKey(l => l.CourseId);
        }
    }
}
