using System.ComponentModel.DataAnnotations;

namespace Employee_Monitoring_System_API.Models
{
    public class Holiday
    {
        [Key]
        public int HolidayId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public bool IsRecurring { get; set; } = false; // For annual holidays like Independence Day
    }

}
