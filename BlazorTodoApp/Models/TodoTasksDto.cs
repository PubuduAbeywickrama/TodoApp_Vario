using System.ComponentModel.DataAnnotations;

namespace BlazorTodoApp.Models
{
    public class TodoTasksDto
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CategoryId { get; set; } = 4;
        public string? UserId { get; set; } = "U002";
    }
}
