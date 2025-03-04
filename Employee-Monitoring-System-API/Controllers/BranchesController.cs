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
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _br;
        private readonly IMapper _mapper;
        public BranchesController(IBranchRepository br, IMapper mapper)
        {
            _br = br;
            _mapper = mapper;
        }

        // GET: api/Branches
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<BranchDTO>> GetBranches()
        {
            var branches = _br.GetAll();
            if(branches == null || !branches.Any())
            {
                return NotFound("Branches Not Found.");
            }
            var branchesDTO = _mapper.Map<IEnumerable<BranchDTO>>(branches);
            return Ok(branchesDTO);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<BranchDTO> GetBranch(int id)
        {
            var branch = _br.GetById(id);
            if (branch == null)
            {
                return NotFound("Branch Not Found.");
            }
            var branchDTO = _mapper.Map<BranchDTO>(branch);
            return Ok(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PatchBranch(int id, [FromBody] JsonPatchDocument<BranchDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var branch = _br.GetById(id);
            if (branch == null)
            {
                return NotFound("Branch not found.");
            }

            var branchDTO = _mapper.Map<BranchDTO>(branch);
            patchDoc.ApplyTo(branchDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBranch = _mapper.Map<Branch>(branchDTO);
            _br.Update(updatedBranch);

            return Ok(_mapper.Map<BranchDTO>(updatedBranch));
        }

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<BranchDTO> PostBranch(BranchDTO branchDTO)
        {
            var branch = _mapper.Map<Branch>(branchDTO);
            _br.Add(branch);

            return Ok("Branch Added");
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBranch(int id)
        {
            var branch = _br.GetById(id);
            if (branch == null)
            {
                return NotFound("Branch not found.");
            }
            _br.Delete(id);
            return Ok("Branch Deleted " + id);
        }

        //private bool BranchExists(Guid id)
        //{
        //    return _context.Branches.Any(e => e.BranchId == id);
        //}
    }
}
