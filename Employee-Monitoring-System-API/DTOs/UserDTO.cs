namespace Employee_Monitoring_System_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? LastLogin { get; set; } = DateTime.UtcNow;
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public Dictionary<string, string>? TechnicalSkills { get; set; }
        public List<string>? Certifications { get; set; }
        public string? ProfileImageBase64 { get; set; } // optional if you plan to show the image
        public List<TaskMiniDTO>? Tasks { get; set; } // from UserTasks
        public List<ProjectMiniDTO>? Projects { get; set; } // from ProjectMemberships

    }
}
