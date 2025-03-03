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
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRepository _lr;
        private readonly IMapper _mapper;
        public LeaveRequestsController(ILeaveRepository lr, IMapper mapper)
        {
            _lr = lr;
            _mapper = mapper;
        }

        // GET: api/LeaveRequests
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<LeaveRequestDTO>> GetLeaveRequests()
        {
            var LeaveRequests = _lr.GetAllLeaveRequests();
            var LeaveRequestsDTO = _mapper.Map<IEnumerable<LeaveRequestDTO>>(LeaveRequests);
            return Ok(LeaveRequestsDTO);
        }

        // GET: api/LeaveRequests/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<LeaveRequestDTO> GetLeaveRequest(int id)
        {
            var leaveRequest = _lr.GetLeaveRequest(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }
            var leaveRequestDTO = _mapper.Map<LeaveRequestDTO>(leaveRequest);
            return Ok(leaveRequest);
        }

        // PUT: api/LeaveRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult PutLeaveRequest(int id, LeaveRequestDTO leaveRequestDTO)
        {
            if (id != leaveRequestDTO.LeaveRequestId)
            {
                return BadRequest();
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
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
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<LeaveRequestDTO> PostLeaveRequest(LeaveRequestDTO leaveRequestDTO)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
            var addedLeave = _lr.Add(leaveRequest);

            var createdlrDTO = _mapper.Map<LeaveRequestDTO>(addedLeave);
            return CreatedAtAction(nameof(GetLeaveRequest), new { id = createdlrDTO.LeaveRequestId }, createdlrDTO);
        }

        // DELETE: api/LeaveRequests/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
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
