namespace Employee_Monitoring_System_API.DTOs
{
    public class HolidayDTO
    {
        public int HolidayId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsRecurring { get; set; }
    }

}
