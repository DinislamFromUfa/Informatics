using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid CourseId { get; set; }

        public Course? Course { get; set; }

        public List<string> ContentFiles { get; set; }
    }
}
