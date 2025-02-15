using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Employee_Monitoring_System_API.Models
{
    public class _Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(150)]
        public string TaskName { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        [JsonIgnore]
        public Project? Project { get; set; }
        [Required]
        public int AssignedTo { get; set; }
        [ForeignKey("AssignedTo")]
        [JsonIgnore]
        public User? AssignedUser { get; set; }
        [MaxLength(50)]
        public string Priority { get; set; } = "Medium"; // High, Medium, Low
        [MaxLength(50)]
        public string Status { get; set; } = "Not Started"; // Not Started, In Progress, Completed

        [Required]
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public int TimeSpent { get; set; } = 0; // Time in minutes
        public DateTime? CompletionDate { get; set; }
    }
}
