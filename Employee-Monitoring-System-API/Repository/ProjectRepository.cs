using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Employee_Monitoring_System_API.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public Project Add(Project project)
        {
            project.StartDate = project.StartDate.ToUniversalTime();
            project.EndDate = project.EndDate?.ToUniversalTime();
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public Project Delete(int id)
        {
            Project pr = _context.Projects.Find(id);
            if (pr != null)
            {
                _context.Projects.Remove(pr);
                _context.SaveChanges();
            }
            return pr;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects
            .Include(p => p.ProjectMembers).ThenInclude(pm => pm.User)
            .Include(p => p.Tasks)
            .ToList();
        }

        public Project GetProject(int id)
        {
            return _context.Projects
           .Include(p => p.ProjectMembers).ThenInclude(pm => pm.User)
           .Include(p => p.Tasks)
           .FirstOrDefault(p => p.ProjectId == id);
        }

        public Project Update(Project projectChanges)
        {
            var pr = _context.Projects.Find(projectChanges.ProjectId);
            if (pr != null)
            {
                _context.Entry(pr).CurrentValues.SetValues(projectChanges);
                _context.SaveChanges();
                return pr;
            }
            return null; // Or handle the case where the project doesn't exist
        }
    }
}
