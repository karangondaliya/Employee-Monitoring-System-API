using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public Notification Delete(int id)
        {
            Notification notification = _context.Notifications.Find(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                _context.SaveChanges();
            }
            return notification;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return _context.Notifications.ToList();
        }

        public Notification GetNotification(int id)
        {
            return _context.Notifications.Find(id);
        }

        public Notification Update(Notification notificationChanges)
        {
            var n = _context.Notifications.Attach(notificationChanges);
            n.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return notificationChanges;
        }
    }
}
