using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Role { get; set; }

        [Required]
        public string Username { get; set; }

        //Имя студента
        [Required]
        public string FirstName { get; set; }

        //Фамилия студента
        [Required]
        public string LastName { get; set; }

        //Отчество студента
        [Required]
        public string MiddleName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Course> Courses { get; set; } = [];

    }
}
