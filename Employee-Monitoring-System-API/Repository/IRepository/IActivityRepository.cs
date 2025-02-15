using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IActivityRepository
    {
        IEnumerable<ActivityLog> GetAll();
        ActivityLog GetById(int id);
        void Add(ActivityLog activity);
        void Update(ActivityLog activityChanges);
        void Delete(int id);
    }
}
