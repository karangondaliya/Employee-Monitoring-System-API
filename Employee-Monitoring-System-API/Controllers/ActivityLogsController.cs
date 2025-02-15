using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityRepository _ar;

        public ActivityLogsController(IActivityRepository ar)
        {
            _ar = ar;
        }

        // GET: api/ActivityLogs
        [HttpGet]
        public ActionResult<IEnumerable<ActivityLog>> GetActivityLogs()
        {
            var activityLog = _ar.GetAll();
            return Ok(activityLog);
        }

        // GET: api/ActivityLogs/5
        [HttpGet("{id}")]
        public ActionResult<ActivityLog> GetActivityLog(int id)
        {
            var activityLog = _ar.GetById(id);

            if (activityLog == null)
            {
                return NotFound();
            }

            return Ok(activityLog);
        }

        // PUT: api/ActivityLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutActivityLog(int id, ActivityLog activityLog)
        {
            if (id != activityLog.LogId)
            {
                return BadRequest();
            }

            _ar.Update(activityLog);
            return Ok();
        }

        // POST: api/ActivityLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ActivityLog> PostActivityLog(ActivityLog activityLog)
        {
            _ar.Add(activityLog);
            return Ok();
        }

        // DELETE: api/ActivityLogs/5
        [HttpDelete("{id}")]
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
