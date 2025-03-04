using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

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
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<TaskDTO>> GetTasks()
        {
            var task = _tr.GetTasks();
            if (task == null || !task.Any())
            {
                return NotFound("Tasks Not Found.");
            }
            var taskDTO = _mapper.Map<IEnumerable<TaskDTO>>(task);
            return Ok(taskDTO);
        }

        // GET: api/_Task/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<TaskDTO> Get_Task(int id)
        {
            var _task = _tr.GetTask(id);
            if (_task == null)
            {
                return NotFound("Task Not Found.");
            }
            var taskDTO = _mapper.Map<TaskDTO>(_task);
            return Ok(taskDTO);
        }

        // PUT: api/_Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "TeamLeadPolicy")]
        public IActionResult Patch_Task(int id, [FromBody] JsonPatchDocument<TaskDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Invalid patch document.");

            var existingTask = _tr.GetTask(id);
            if (existingTask == null)
                return NotFound($"Task with ID {id} not found.");

            var taskDTO = _mapper.Map<TaskDTO>(existingTask);
            patchDoc.ApplyTo(taskDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTask = _mapper.Map<_Task>(taskDTO);
            _tr.Update(updatedTask);

            return Ok("Task Updated."); // Success - No content returned
        }

        // POST: api/_Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "TeamLeadPolicy")]
        public ActionResult<TaskDTO> Post_Task(TaskDTO taskDTO)
        {
            var task = _mapper.Map<_Task>(taskDTO);
            var addedTask = _tr.Add(task);
            var CreatedTaskDTO = _mapper.Map<TaskDTO>(addedTask);
            return CreatedAtAction(nameof(Get_Task), new { id = CreatedTaskDTO.TaskId }, CreatedTaskDTO);
        }

        // DELETE: api/_Task/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "TeamLeadPolicy")]
        public IActionResult Delete_Task(int id)
        {
            var _Task = _tr.GetTask(id);
            if (_Task == null)
            {
                return NotFound("Task Not Found.");
            }
            _tr.Delete(id);
            return Ok("Task Deleted " + id);
        }

        //private bool _TaskExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.TaskId == id);
        //}
    }
}
