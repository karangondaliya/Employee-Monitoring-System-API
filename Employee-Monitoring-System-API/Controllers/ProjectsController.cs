using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _pr;

        public ProjectsController(IProjectRepository pr)
        {
            _pr = pr;
        }

        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            var projects = _pr.GetAllProjects();
            return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            var project = _pr.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            var updatedProject = _pr.Update(project);
            if (updatedProject != null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Project> PostProject(Project project)
        {
            var addedProject = _pr.Add(project);
            return CreatedAtAction("GetProject", new { id = addedProject.ProjectId }, addedProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _pr.Delete(id);
            if (project == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool ProjectExists(Guid id)
        //{
        //    return _context.Projects.Any(e => e.ProjectId == id);
        //}
    }
}
