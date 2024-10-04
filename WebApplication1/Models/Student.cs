using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Role {  get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public List<Course> Courses { get; set; } = [];

    }
}
