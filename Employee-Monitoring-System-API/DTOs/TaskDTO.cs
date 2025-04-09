namespace Employee_Monitoring_System_API.DTOs
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public List<UserMiniDTO>? AssignedUsers { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public int TimeSpent { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
