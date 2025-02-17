namespace Employee_Monitoring_System_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? LastLogin { get; set; } = DateTime.UtcNow;
    }
}
