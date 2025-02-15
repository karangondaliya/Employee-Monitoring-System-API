using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _TaskController : ControllerBase
    {
        private readonly ITaskRepository _tr;

        public _TaskController(ITaskRepository tr)
        {
            _tr = tr;
        }

        // GET: api/_Task
        [HttpGet]
        public ActionResult<IEnumerable<_Task>> GetTasks()
        {
            var task = _tr.GetTasks();
            return Ok(task);
        }

        // GET: api/_Task/5
        [HttpGet("{id}")]
        public ActionResult<_Task> Get_Task(int id)
        {
            var _Task = _tr.GetTask(id);

            if (_Task == null)
            {
                return NotFound();
            }

            return Ok(_Task);
        }

        // PUT: api/_Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult Put_Task(int id, _Task _Task)
        {
            if (id != _Task.TaskId)
            {
                return BadRequest();
            }

            var updatedTask = _tr.Update(_Task);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        // POST: api/_Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<_Task> Post_Task(_Task _Task)
        {
            var addedTask = _tr.Add(_Task);
            return CreatedAtAction("Get_Task", new { id = addedTask.TaskId }, addedTask);
        }

        // DELETE: api/_Task/5
        [HttpDelete("{id}")]
        public IActionResult Delete_Task(int id)
        {
            var _Task = _tr.Delete(id);
            if (_Task == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        //private bool _TaskExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.TaskId == id);
        //}
    }
}
