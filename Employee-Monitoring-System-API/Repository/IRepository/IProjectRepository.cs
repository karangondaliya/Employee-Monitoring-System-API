using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(int id);
        Project Add(Project project);
        Project Update(Project projectChanges);
        Project Delete(int id);
    }
}
