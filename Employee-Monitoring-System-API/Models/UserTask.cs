namespace Employee_Monitoring_System_API.Models
{
    public class UserTask
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TaskId { get; set; }  // changed from `Task` to `_Task`
        public _Task Task { get; set; }  // corrected to match your `_Task` entity
    }

}
