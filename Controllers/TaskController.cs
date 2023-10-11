using Microsoft.AspNetCore.Mvc;
using App.Data;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly DataContext _context;

        public TaskController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet("GetTasks")]
        public IEnumerable<string> GetTasks()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("GetTaskById/{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost("CreateTask")]
        public async Task<ActionResult<Models.Task>> CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpDelete("DeleteTask{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
