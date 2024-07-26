using System.ComponentModel.DataAnnotations;

namespace RF_ToDoBlazor.Models
{
    public class Goals
{
        public Guid GoalId { get; set; }
        public string Name { get; set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;
}
}
