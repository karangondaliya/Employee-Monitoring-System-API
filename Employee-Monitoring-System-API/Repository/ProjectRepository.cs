using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

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
            return _context.Projects.ToList();
        }

        public Project GetProject(int id)
        {
            return _context.Projects.Find(id);
        }

        public Project Update(Project projectChanges)
        {
            var pr = _context.Projects.Attach(projectChanges);
            pr.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return projectChanges;
        }
    }
}
