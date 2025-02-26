using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _TaskController : ControllerBase
    {
        private readonly ITaskRepository _tr;
        private readonly IMapper _mapper;
        public _TaskController(ITaskRepository tr, IMapper mapper)
        {
            _tr = tr;
            _mapper = mapper;
        }

        // GET: api/_Task
        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> GetTasks()
        {
            var task = _tr.GetTasks();
            var taskDTO = _mapper.Map<IEnumerable<TaskDTO>>(task);
            return Ok(taskDTO);
        }

        // GET: api/_Task/5
        [HttpGet("{id}")]
        public ActionResult<TaskDTO> Get_Task(int id)
        {
            var _task = _tr.GetTask(id);

            if (_task == null)
            {
                return NotFound();
            }
            var taskDTO = _mapper.Map<TaskDTO>(_task);
            return Ok(taskDTO);
        }

        // PUT: api/_Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult Put_Task(int id, TaskDTO taskDTO)
        {
            if (id != taskDTO.TaskId)
            {
                return BadRequest();
            }
            var task = _mapper.Map<_Task>(taskDTO);
            var updatedTask = _tr.Update(task);
            if (updatedTask == null)
            {
                return NotFound();
            }
            var updatedTaskDTO = _mapper.Map<TaskDTO>(updatedTask);
            return Ok(updatedTaskDTO);
        }

        // POST: api/_Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TaskDTO> Post_Task(TaskDTO taskDTO)
        {
            var task = _mapper.Map<_Task>(taskDTO);
            var addedTask = _tr.Add(task);
            var CreatedTaskDTO = _mapper.Map<TaskDTO>(addedTask);
            return CreatedAtAction(nameof(Get_Task), new { id = CreatedTaskDTO.TaskId }, CreatedTaskDTO);
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
