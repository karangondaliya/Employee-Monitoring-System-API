using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _nr;

        public NotificationsController(INotificationRepository nr)
        {
            _nr = nr;
        }

        // GET: api/Notifications
        [HttpGet]
        public ActionResult<IEnumerable<Notification>> GetNotifications()
        {
            var notifications = _nr.GetAllNotifications();
            return Ok(notifications);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public ActionResult<Notification> GetNotification(int id)
        {
            var notification = _nr.GetNotification(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutNotification(int id, Notification notification)
        {
            if (id != notification.NotificationId)
            {
                return BadRequest();
            }

            var updatedNotification = _nr.Update(notification);

            if(updatedNotification == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Notification> PostNotification(Notification notification)
        {
            _nr.Add(notification);
            return NoContent();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _nr.Delete(id);
            if (notification == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool NotificationExists(int id)
        //{
        //    return _context.Notifications.Any(e => e.NotificationId == id);
        //}
    }
}
