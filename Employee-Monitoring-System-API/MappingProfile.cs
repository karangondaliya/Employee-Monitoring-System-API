using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<_Task, TaskDTO>();
            CreateMap<Branch, BranchDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ActivityLog, ActivityLogDTO>();
            CreateMap<LeaveRequest, LeaveRequestDTO>();
            CreateMap<Screenshot, ScreenshotDTO>();
            CreateMap<Notification, NotificationDTO>();
            CreateMap<LeaveRequest, LeaveRequestDTO>();
            CreateMap<User, UserLoginDTO>();
        }
    }
}
