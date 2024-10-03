namespace WebApplication1.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string CourseName { get; set; } = string.Empty;

        //Внешний ключ на модель Teacher
        public Guid TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

        public List<Student> ListOfStudentsOnCourse { get; set; } = [];

        public List<Lesson> Lessons { get; set; } = [];
    }
}
