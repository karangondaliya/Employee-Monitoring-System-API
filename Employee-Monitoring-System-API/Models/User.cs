using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Monitoring_System_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } // Store hashed password, not plain text.
        [Required]
        public string Role { get; set; } = "Employee";

        public int? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        [Phone]
        public string? PhoneNumber { get; set; } // New field for contact

        public DateTime? LastLogin { get; set; }

        public ICollection<ActivityLog>? ActivityLogs { get; set; }
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
        public ICollection<Screenshot>? Screenshots { get; set; }
        public ICollection<_Task>? Tasks { get; set; } 

        public ICollection<Notification>? Notifications { get; set; }


    }
}
