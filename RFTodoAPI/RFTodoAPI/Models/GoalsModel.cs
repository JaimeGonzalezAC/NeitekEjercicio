using System.ComponentModel.DataAnnotations;

namespace RFTodoAPI.Models
{
    public class Goals
    {
        [Key]
        public Guid GoalId { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }

    public class GoalDto
    {
        public Guid GoalId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public double CompletionPercentage { get; set; }
    }

    public class Goals2
    {
        public class Goal
        {
            public Guid GoalId { get; set; } 
            public string Name { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.Now;
        }
    }
}
