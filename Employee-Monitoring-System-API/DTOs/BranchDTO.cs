namespace Employee_Monitoring_System_API.DTOs
{
    public class BranchDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
    }
}
