using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _nr;
        private readonly IMapper _mapper;
        public NotificationsController(INotificationRepository nr, IMapper mapper)
        {
            _nr = nr;
            _mapper = mapper;
        }

        // GET: api/Notifications
        [HttpGet]
        public ActionResult<IEnumerable<NotificationDTO>> GetNotifications()
        {
            var notifications = _nr.GetAllNotifications();
            var notificationDTOs = _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
            return Ok(notificationDTOs);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public ActionResult<NotificationDTO> GetNotification(int id)
        {
            var notification = _nr.GetNotification(id);

            if (notification == null)
            {
                return NotFound();
            }

            var notificationDTO = _mapper.Map<NotificationDTO>(notification);
            return Ok(notificationDTO);
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutNotification(int id, NotificationDTO notificationDTO)
        {
            if (id != notificationDTO.NotificationId)
            {
                return BadRequest();
            }

            var notification = _mapper.Map<Notification>(notificationDTO);
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
        public ActionResult<Notification> PostNotification(NotificationDTO notificationDTO)
        {
            var notification = _mapper.Map<Notification>(notificationDTO);
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
