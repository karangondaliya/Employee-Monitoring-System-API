using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _br;

        public BranchesController(IBranchRepository br)
        {
            _br = br;
        }

        // GET: api/Branches
        [HttpGet]
        public ActionResult<IEnumerable<Branch>> GetBranches()
        {
            var branches = _br.GetAll();
            return Ok(branches);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public ActionResult<Branch> GetBranch(int id)
        {
            var branch = _br.GetById(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutBranch(int id, Branch branch)
        {
            if (id != branch.BranchId)
            {
                return BadRequest();
            }

            _br.Update(branch);

            return Ok();
        }

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Branch> PostBranch(Branch branch)
        {
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
