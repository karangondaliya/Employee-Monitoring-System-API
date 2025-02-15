using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface ILeaveRepository
    {
        LeaveRequest GetLeaveRequest(int id);
        IEnumerable<LeaveRequest> GetAllLeaveRequests();
        LeaveRequest Add(LeaveRequest leaveRequest);
        LeaveRequest Update(LeaveRequest leaveRequestChanges);
        LeaveRequest Delete(int id);
    }
}
