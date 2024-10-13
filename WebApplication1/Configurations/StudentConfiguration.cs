using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder
                .HasMany(s => s.Courses);

            builder
                .HasMany(s => s.Grades)
                .WithOne(v => v.Student)
                .HasForeignKey(v => v.StudentId);

            builder
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId);
        }
    }
}
