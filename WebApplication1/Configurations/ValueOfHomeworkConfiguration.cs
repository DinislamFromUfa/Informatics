using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class ValueOfHomeworkConfiguration: IEntityTypeConfiguration<ValueOfHomework>
    {
        public void Configure(EntityTypeBuilder<ValueOfHomework> builder)
        {
            builder.HasKey(v => v.Id);
        }
    }
}
