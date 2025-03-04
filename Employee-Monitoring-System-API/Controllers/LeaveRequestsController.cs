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
            if(LeaveRequests == null || !LeaveRequests.Any())
            {
                return NotFound("No leave requests found.");
            }
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
                return NotFound("LeaveRequest Not Found.");
            }
            var leaveRequestDTO = _mapper.Map<LeaveRequestDTO>(leaveRequest);
            return Ok(leaveRequest);
        }

        // PUT: api/LeaveRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult PatchLeaveRequest(int id, [FromBody] JsonPatchDocument<LeaveRequestDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var leaveRequest = _lr.GetLeaveRequest(id);
            if (leaveRequest == null)
            {
                return NotFound("Leave request not found.");
            }

            var leaveRequestDTO = _mapper.Map<LeaveRequestDTO>(leaveRequest);
            patchDoc.ApplyTo(leaveRequestDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedLeaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
            _lr.Update(updatedLeaveRequest);

            return Ok(_mapper.Map<LeaveRequestDTO>(updatedLeaveRequest));
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
            var leaveRequest = _lr.GetLeaveRequest(id);
            if (leaveRequest == null)
            {
                return NotFound("Leave Request Not Found.");
            }
            _lr.Delete(id);
            return Ok("Leave Request Deleted " + id);
        }

        //private bool LeaveRequestExists(int id)
        //{
        //    return _context.LeaveRequests.Any(e => e.LeaveRequestId == id);
        //}
    }
}
