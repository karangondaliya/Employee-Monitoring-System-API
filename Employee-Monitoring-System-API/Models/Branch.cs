using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Monitoring_System_API.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BranchName { get; set; }

        [MaxLength(250)]
        public string? Location { get; set; }

        public int? HeadUserId { get; set; }

        [ForeignKey("HeadUserId")]
        public User? HeadUser { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ICollection<User>? Users { get; set; }
        public ICollection<Project>? Projects { get; set; }

        public string? Description { get; set; } // New field for branch description

        public double? Latitude { get; set; } // New field for location coordinates

        public double? Longitude { get; set; } // New field for location coordinates
    }
}
