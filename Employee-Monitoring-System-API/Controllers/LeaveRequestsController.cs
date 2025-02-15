using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRepository _lr;

        public LeaveRequestsController(ILeaveRepository lr)
        {
            _lr = lr;
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequest>> GetLeaveRequests()
        {
            var LeaveRequests = _lr.GetAllLeaveRequests();
            return Ok(LeaveRequests);
        }

        // GET: api/LeaveRequests/5
        [HttpGet("{id}")]
        public ActionResult<LeaveRequest> GetLeaveRequest(int id)
        {
            var leaveRequest = _lr.GetLeaveRequest(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return Ok(leaveRequest);
        }

        // PUT: api/LeaveRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutLeaveRequest(int id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.LeaveRequestId)
            {
                return BadRequest();
            }

            var updatedLeave = _lr.Update(leaveRequest);
            if(updatedLeave == null)
            {
                return NotFound();
            }
            return Ok(updatedLeave);
        }

        // POST: api/LeaveRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<LeaveRequest> PostLeaveRequest(LeaveRequest leaveRequest)
        {
            var addedLeave = _lr.Add(leaveRequest);
            return CreatedAtAction("GetLeaveRequest", new { id = addedLeave.LeaveRequestId }, addedLeave);
        }

        // DELETE: api/LeaveRequests/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveRequest(int id)
        {
            var leaveRequest = _lr.Delete(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool LeaveRequestExists(int id)
        //{
        //    return _context.LeaveRequests.Any(e => e.LeaveRequestId == id);
        //}
    }
}
