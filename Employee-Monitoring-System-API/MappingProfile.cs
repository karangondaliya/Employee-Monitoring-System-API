using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mapping
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchName : null));
            CreateMap<UserDTO, User>(); // Reverse mapping

            // Task Mapping
            CreateMap<_Task, TaskDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : null))
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.FullName : null));
            CreateMap<TaskDTO, _Task>(); // Reverse mapping

            // Branch Mapping
            CreateMap<Branch, BranchDTO>();
            CreateMap<BranchDTO, Branch>(); // Reverse mapping

            // Project Mapping
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchName : null))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Id : (int?)null));
            CreateMap<ProjectDTO, Project>(); // Reverse mapping

            // ActivityLog Mapping
            CreateMap<ActivityLog, ActivityLogDTO>();
            CreateMap<ActivityLogDTO, ActivityLog>(); // Reverse mapping

            // Screenshot Mapping
            CreateMap<Screenshot, ScreenshotDTO>();
            CreateMap<ScreenshotDTO, Screenshot>(); // Reverse mapping

            // Notification Mapping
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null));
            CreateMap<NotificationDTO, Notification>(); // Reverse mapping

            // LeaveRequest Mapping (Fixing Duplicate)
            CreateMap<LeaveRequest, LeaveRequestDTO>();
            CreateMap<LeaveRequestDTO, LeaveRequest>(); // Reverse mapping

            // AppSettings Mapping
            CreateMap<AppSettings, AppSettingDTO>();
            CreateMap<AppSettingDTO, AppSettings>(); // Reverse mapping

            // User Login Mapping
            CreateMap<User, UserLoginDTO>();
            CreateMap<UserLoginDTO, User>(); // Reverse mapping
        }
    }
}
