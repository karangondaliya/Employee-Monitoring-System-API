using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _pr;

        public ProjectsController(IProjectRepository pr, IMapper mapper)
        {
            _pr = pr;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<ProjectDTO>> GetProjects()
        {
            var projects = _pr.GetAllProjects();
            var projectDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
            return Ok(projectDTO);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public ActionResult<ProjectDTO> GetProject(int id)
        {
            var project = _pr.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectDTO = _mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProject(int id, ProjectDTO projectDTO)
        {
            if (id != projectDTO.ProjectId)
            {
                return BadRequest();
            }

            var project = _mapper.Map<Project>(projectDTO);
            var updatedProject = _pr.Update(project);
            if (updatedProject == null)
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

            var createdProjectDTO = _mapper.Map<ProjectDTO>(addedProject);
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
