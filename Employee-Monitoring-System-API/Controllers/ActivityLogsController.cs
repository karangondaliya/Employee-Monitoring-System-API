using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;

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
                return NotFound();
            }
            var activityLogDTO = _mapper.Map<ActivityLogDTO>(activityLog);
            return Ok(activityLogDTO);
        }

        // PUT: api/ActivityLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PutActivityLog(int id, ActivityLogDTO activityLogDTO)
        {
            if (id != activityLogDTO.LogId)
            {
                return BadRequest();
            }
            var activityLog = _mapper.Map<ActivityLog>(activityLogDTO);
            _ar.Update(activityLog);
            return Ok();
        }

        // POST: api/ActivityLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<ActivityLogDTO> PostActivityLog(ActivityLogDTO activityLogDTO)
        {
            var activityLog = _mapper.Map<ActivityLog>(activityLogDTO);
            _ar.Add(activityLog);
            return Ok();
        }

        // DELETE: api/ActivityLogs/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteActivityLog(int id)
        {
            _ar.Delete(id);
            return NoContent();
        }

        //private bool ActivityLogExists(int id)
        //{
        //    return _context.ActivityLogs.Any(e => e.LogId == id);
        //}
    }
}
