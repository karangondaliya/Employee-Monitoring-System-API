using System.ComponentModel.DataAnnotations;

namespace Employee_Monitoring_System_API.Models
{
    public class ProjectMember
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string RoleInProject { get; set; }  // optional (e.g., Developer, Tester, Manager)
    }

}
