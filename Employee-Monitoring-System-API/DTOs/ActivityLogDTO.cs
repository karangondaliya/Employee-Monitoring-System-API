namespace Employee_Monitoring_System_API.DTOs
{
    public class ActivityLogDTO
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string ActiveApplication { get; set; }
        public int Duration { get; set; }
    }
}
