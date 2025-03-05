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
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<ProjectDTO>> GetProjects()
        {
            var projects = _pr.GetAllProjects();
            if (projects == null || !projects.Any())
            {
                return NotFound("No projects found.");
            }
            var projectDTO = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
            return Ok(projectDTO);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<ProjectDTO> GetProject(int id)
        {
            var project = _pr.GetProject(id);

            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            var projectDTO = _mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PatchProject(int id, [FromBody] JsonPatchDocument<ProjectDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var project = _pr.GetProject(id);
            if (project == null)
            {
                return NotFound("Project not found.");
            }

            var projectDTO = _mapper.Map<ProjectDTO>(project);
            patchDoc.ApplyTo(projectDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProject = _mapper.Map<Project>(projectDTO);
            _pr.Update(updatedProject);

            return Ok(_mapper.Map<ProjectDTO>(updatedProject));
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<ProjectDTO> PostProject(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var addedProject = _pr.Add(project);
            var createdProjectDTO = _mapper.Map<ProjectDTO>(addedProject);

            return CreatedAtAction(nameof(GetProject), new { id = createdProjectDTO.ProjectId }, createdProjectDTO);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteProject(int id)
        {
            var project = _pr.GetProject(id);
            if (project == null)
            {
                return NotFound("Project not found.");
            }

            _pr.Delete(id);
            return Ok("Project Deleted " + id);
        }

        [HttpGet("test-mapping")]
        public IActionResult TestAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Ensure this matches your actual profile class name
            });

            configuration.AssertConfigurationIsValid(); // This will throw an error if the mapping is invalid

            return Ok("AutoMapper configuration is valid!");
        }


        //private bool ProjectExists(Guid id)
        //{
        //    return _context.Projects.Any(e => e.ProjectId == id);
        //}
    }
}
