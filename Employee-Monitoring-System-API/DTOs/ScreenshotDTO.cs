namespace Employee_Monitoring_System_API.DTOs
{
    public class ScreenshotDTO
    {
        public int ScreenshotId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string ImagePath { get; set; }
    }
}
