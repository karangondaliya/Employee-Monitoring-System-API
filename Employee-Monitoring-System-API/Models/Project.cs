using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Monitoring_System_API.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; }
        public int? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(50)]
        public string Status { get; set; } = "Planned"; // Planned, In Progress, Completed
        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User? Creator { get; set; }
        public ICollection<_Task>? Tasks { get; set; }
        public string? Description { get; set; }
        public int? CompletionPercentage { get; set; } = 0;
        public double? Budget { get; set; } // New field for project budget
        public List<ProjectMember> ProjectMembers { get; set; }
    }
}
