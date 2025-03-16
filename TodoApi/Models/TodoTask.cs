namespace TodoApi.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string? UserId { get; set; }
        public DateTime? CompleteDate { get; set; }

    }
}
