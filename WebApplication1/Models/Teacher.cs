using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Teacher
    {
        public Guid ID { get; set; }

        public string Role {  get; set; }

        //Имя преподавателя
        public string Name { get; set; }

        //Фамилия преподавателя
        public string Surname { get; set; }

        public string Password { get; set; }

        public List<Course> Courses { get; set; } = [];

    }
}
