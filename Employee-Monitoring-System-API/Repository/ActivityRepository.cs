using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ActivityLog activity)
        {
            activity.Timestamp = activity.Timestamp.ToUniversalTime();
            _context.ActivityLogs.Add(activity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            ActivityLog al = _context.ActivityLogs.Find(id);
            if (al != null)
            {
                _context.ActivityLogs.Remove(al);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ActivityLog> GetAll()
        {
            return _context.ActivityLogs.ToList();
        }

        public ActivityLog GetById(int id)
        {
            return _context.ActivityLogs.Find(id);
        }

        public void Update(ActivityLog activityChanges)
        {
            var al = _context.ActivityLogs.Attach(activityChanges);
            al.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
