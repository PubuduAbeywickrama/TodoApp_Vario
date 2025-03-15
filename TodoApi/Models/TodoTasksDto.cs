using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoTasksDto
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
