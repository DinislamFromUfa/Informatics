using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class ReviewOnCourseConfiguration: IEntityTypeConfiguration<ReviewOnCourse>
    {
        public void Configure(EntityTypeBuilder<ReviewOnCourse> builder)
        {
            builder
                .HasKey(r => r.Id);

        }
    }
}
