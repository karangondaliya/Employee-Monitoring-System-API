namespace Employee_Monitoring_System_API.DTOs
{
    public class UpdatePasswordDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
