using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder
                .HasKey(h => h.Id);
            builder
                .HasOne(h => h.ValueOfHomework)
                .WithOne(v => v.Homework)
                .HasForeignKey<ValueOfHomework>(v => v.HomeworkId);
        }
    }
}
