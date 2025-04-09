namespace Employee_Monitoring_System_API.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public int? CompletionPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public List<UserMiniDTO>? TeamMembers { get; set; }

    }
}
