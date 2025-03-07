using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface INotificationRepository
    {
        void Add(Notification notification);
        IEnumerable<Notification> GetAllNotifications();
        Notification GetNotification(int id);
        Notification Update(Notification notificationChanges);
        Notification Delete(int id);
        IEnumerable<Notification> GetUserNotifications(int userId);
    }
}
