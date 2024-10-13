using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Homework
    {
        public int Id { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public List<string> Question { get; set; }

        public List<string> Answer { get; set; }

        public List<int> CorrectAnswer { get; set; }

        public ValueOfHomework? ValueOfHomework { get; set; }
    }
}
