namespace Employee_Monitoring_System_API.DTOs
{
    public class LeaveRequestDTO
    {
        public int LeaveRequestId { get; set; }
        public int UserId { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;
    }
}
