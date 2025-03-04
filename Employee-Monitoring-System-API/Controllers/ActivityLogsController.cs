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
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityRepository _ar;
        private readonly IMapper _mapper;
        public ActivityLogsController(IActivityRepository ar, IMapper mapper)
        {
            _ar = ar;
            _mapper = mapper;
        }

        // GET: api/ActivityLogs
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<ActivityLogDTO>> GetActivityLogs()
        {
            var activityLogs = _ar.GetAll();
            if(activityLogs == null || !activityLogs.Any())
            {
                return NotFound("ActivityLogs Not Found.");
            }
            var activityLogDTOs = _mapper.Map<IEnumerable<ActivityLogDTO>>(activityLogs);
            return Ok(activityLogDTOs);
        }

        // GET: api/ActivityLogs/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<ActivityLogDTO> GetActivityLog(int id)
        {
            var activityLog = _ar.GetById(id);

            if (activityLog == null)
            {
                return NotFound("ActivityLog Not Found.");
            }
            var activityLogDTO = _mapper.Map<ActivityLogDTO>(activityLog);
            return Ok(activityLogDTO);
        }

        // PUT: api/ActivityLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PatchActivityLog(int id, [FromBody] JsonPatchDocument<ActivityLogDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Invalid patch document.");

            var existingLog = _ar.GetById(id);
            if (existingLog == null)
                return NotFound($"Activity log with ID {id} not found.");

            var activityLogDTO = _mapper.Map<ActivityLogDTO>(existingLog);
            patchDoc.ApplyTo(activityLogDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedLog = _mapper.Map<ActivityLog>(activityLogDTO);
            _ar.Update(updatedLog);

            return Ok("ActivityLog Updated."); // Indicating successful update
        }

        // POST: api/ActivityLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<ActivityLogDTO> PostActivityLog(ActivityLogDTO activityLogDTO)
        {
            var activityLog = _mapper.Map<ActivityLog>(activityLogDTO);
            _ar.Add(activityLog);
            return Ok("ActivityLog Added.");
        }

        // DELETE: api/ActivityLogs/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteActivityLog(int id)
        {
            var activityLog = _ar.GetById(id);
            if(activityLog == null)
            {
                return NotFound("ActivityLog Not Found.");
            }
            _ar.Delete(id);
            return Ok("ActivityLog Deleted " + id);
        }

        //private bool ActivityLogExists(int id)
        //{
        //    return _context.ActivityLogs.Any(e => e.LogId == id);
        //}
    }
}
