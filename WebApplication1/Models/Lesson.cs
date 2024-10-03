namespace WebApplication1.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
