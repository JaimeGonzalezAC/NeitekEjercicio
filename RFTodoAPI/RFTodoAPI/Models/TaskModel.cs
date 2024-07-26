using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFTodoAPI.Models
{
    public class Task
    {
        [Key]
        public Guid TaskId { get; set; }
        [Required]
        [ForeignKey("Goals")]
        public Guid GoalId { get; set; }
        public Goals Goal { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Abierta";
    }
}
