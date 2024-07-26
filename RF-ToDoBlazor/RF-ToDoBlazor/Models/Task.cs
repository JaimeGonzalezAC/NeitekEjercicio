using System.ComponentModel.DataAnnotations;

namespace RF_ToDoBlazor.Models
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public Guid GoalId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }
}
