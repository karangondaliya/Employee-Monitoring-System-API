using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

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
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<NotificationDTO>> GetNotifications()
        {
            var notifications = _nr.GetAllNotifications();
            if(notifications == null || !notifications.Any())
            {
                return NotFound("No notifications found.");
            }
            var notificationDTOs = _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
            return Ok(notificationDTOs);
        }

        [HttpGet("GetUserNotifications/{userId}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<NotificationDTO>> GetUserNotifications(int userId)
        {
            var notifications = _nr.GetUserNotifications(userId); // Implement this in the repository
            if (notifications == null || !notifications.Any())
            {
                return NotFound("No notifications found.");
            }
            return Ok(_mapper.Map<IEnumerable<NotificationDTO>>(notifications));
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<NotificationDTO> GetNotification(int id)
        {
            var notification = _nr.GetNotification(id);

            if (notification == null)
            {
                return NotFound("Notification Not Found.");
            }

            var notificationDTO = _mapper.Map<NotificationDTO>(notification);
            return Ok(notificationDTO);
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult PatchNotification(int id, [FromBody] JsonPatchDocument<NotificationDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var notification = _nr.GetNotification(id);
            if (notification == null)
            {
                return NotFound("Notification not found.");
            }

            var notificationDTO = _mapper.Map<NotificationDTO>(notification);
            patchDoc.ApplyTo(notificationDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedNotification = _mapper.Map<Notification>(notificationDTO);
            _nr.Update(updatedNotification);

            return Ok(_mapper.Map<NotificationDTO>(updatedNotification));
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<Notification> PostNotification(NotificationDTO notificationDTO)
        {
            var notification = _mapper.Map<Notification>(notificationDTO);
            _nr.Add(notification);
            return Ok("Notification Added");
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _nr.GetNotification(id);
            if (notification == null)
            {
                return NotFound("Notification Not Found.");
            }
            _nr.Delete(id);
            return Ok("Notification Deleted " + id);
        }

        //private bool NotificationExists(int id)
        //{
        //    return _context.Notifications.Any(e => e.NotificationId == id);
        //}
    }
}
