namespace Employee_Monitoring_System_API.DTOs
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
