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
        public IActionResult GetTodoTasks([FromQuery] string? status)
        {
            var todoTasks = context.TodoTasks.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "Completed")
                    todoTasks = todoTasks.Where(t => t.IsCompleted);
                else if (status == "Not Completed")
                    todoTasks = todoTasks.Where(t => !t.IsCompleted);
            }

            var result = todoTasks.OrderByDescending(t => t.Id).ToList();
            return Ok(result);
        }

        [HttpGet("filter")]
        public IActionResult GetFilteredTasks([FromQuery] int? categoryId, [FromQuery] string? status)
        {
            var query = context.TodoTasks.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "Completed")
                    query = query.Where(t => t.IsCompleted);
                else if (status == "Not Completed")
                    query = query.Where(t => !t.IsCompleted);
            }

            var filteredTasks = query.OrderByDescending(t => t.Id).ToList();
            return Ok(filteredTasks);
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

        [HttpPatch("{Id}/complete")]
        public IActionResult MarkAsCompleted(int Id)
        {
            var todotask = context.TodoTasks.Find(Id);
            if (todotask == null) return NotFound();

            if (todotask.IsCompleted)
            {
                return BadRequest("Task is already completed.");
            }

            if (todotask.DueDate < DateTime.Now)
            {
                return BadRequest("Cannot complete a task past its due date.");
            }

            todotask.IsCompleted = true;
            todotask.CompleteDate = DateTime.Now;
            context.SaveChanges();

            return Ok(todotask);
        }


    }
}
