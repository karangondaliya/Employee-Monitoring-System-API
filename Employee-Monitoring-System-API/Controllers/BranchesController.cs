using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

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
        public ActionResult<IEnumerable<BranchDTO>> GetBranches()
        {
            var branches = _br.GetAll();
            var branchesDTO = _mapper.Map<IEnumerable<BranchDTO>>(branches);
            return Ok(branchesDTO);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public ActionResult<BranchDTO> GetBranch(int id)
        {
            var branch = _br.GetById(id);

            if (branch == null)
            {
                return NotFound();
            }
            var branchDTO = _mapper.Map<BranchDTO>(branch);
            return Ok(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutBranch(int id, BranchDTO branchDTO)
        {
            if (id != branchDTO.BranchId)
            {
                return BadRequest();
            }

            var branch = _mapper.Map<Branch>(branchDTO);
            _br.Update(branch);

            return Ok();
        }

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<BranchDTO> PostBranch(BranchDTO branchDTO)
        {
            var branch = _mapper.Map<Branch>(branchDTO);
            _br.Add(branch);

            return Ok();
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {
            _br.Delete(id);
            return NoContent();
        }

        //private bool BranchExists(Guid id)
        //{
        //    return _context.Branches.Any(e => e.BranchId == id);
        //}
    }
}
