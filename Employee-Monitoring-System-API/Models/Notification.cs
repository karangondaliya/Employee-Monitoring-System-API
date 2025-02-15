using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Employee_Monitoring_System_API.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
