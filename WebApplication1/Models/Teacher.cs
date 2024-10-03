namespace WebApplication1.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }

        //Имя преподавателя
        public string Name { get; set; }

        //Фамилия преподавателя
        public string Surname { get; set; }

        public List<Course> Courses { get; set; } = [];
    }
}
