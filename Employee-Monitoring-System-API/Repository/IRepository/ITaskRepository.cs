using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface ITaskRepository
    {
        _Task GetTask(int id);
        IEnumerable<_Task> GetTasks();
        _Task Add(_Task task);
        _Task Update(_Task taskChanges);
        _Task Delete(int id);
    }
}
