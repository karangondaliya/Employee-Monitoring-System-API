using System.ComponentModel.DataAnnotations;

namespace Employee_Monitoring_System_API.Models
{
    public class AppSettings
    {
        [Key]
        [MaxLength(100)]
        public string SettingKey { get; set; }

        [Required]
        public string SettingValue { get; set; }
    }
}
