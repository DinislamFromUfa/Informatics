namespace WebApplication1.Models
{
    public class ValueOfHomework
    {
        public Guid Id { get; set; }

        public int HomeworkId { get; set; }
        public Homework? Homework { get; set; }
        public int Grade { get; set; }

        public Student Student { get; set; }

        public Guid StudentId { get; set; }
    }
}
