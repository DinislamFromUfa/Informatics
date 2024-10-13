using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class ReviewOnCourse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }


        //Внешний ключ для студента
        public Guid StudentId { get; set; }
        public Student Student { get; set; }


        //Внешний ключ для курса
        public Guid CourseId { get; set; }

        public Course Course { get; set; }

    }
}
