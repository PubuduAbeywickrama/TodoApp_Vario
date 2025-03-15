using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoTaskController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public  TodoTaskController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetTodoTasks()
        {
            var todoTasks = context.TodoTasks.OrderByDescending(t => t.Id).ToList();
            return Ok(todoTasks);
        }

        [HttpGet("{Id}")]
        public IActionResult GetTodoTask(int Id) 
        { 
            var task = context.TodoTasks.Find(Id);
            if (task == null) { return NotFound(); }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTodoTask(TodoTasksDto todoTaskDto)
        {
            var todotask = new TodoTask
            {
                Name = todoTaskDto.Name,
                Description = todoTaskDto.Description,
                Created = todoTaskDto.Created,
                DueDate = todoTaskDto.DueDate,
                IsCompleted = todoTaskDto.IsCompleted,
                CategoryId = todoTaskDto.CategoryId,
                UserId = todoTaskDto.UserId,
            };

            context.TodoTasks.Add(todotask);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult EditTodoTas(int Id, TodoTasksDto todoTasksDto)
        {
            var todotask = context.TodoTasks.Find(Id);
            if (todotask == null) { return NotFound(); }

            todotask.Name = todoTasksDto.Name;
            todotask.Description = todoTasksDto.Description;
            todotask.Created = todoTasksDto.Created;
            todotask.DueDate = todoTasksDto.DueDate;
            todotask.IsCompleted = todoTasksDto.IsCompleted;
            todotask.CategoryId = todoTasksDto.CategoryId;

            context.SaveChanges();
            return Ok(todotask);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteTodo(int Id)
        {
            var todotask = context.TodoTasks.Find(Id);
            if (todotask == null) { return NotFound(); }

            context.TodoTasks.Remove(todotask);
            context.SaveChanges();
            return Ok();
        }
    }
}
