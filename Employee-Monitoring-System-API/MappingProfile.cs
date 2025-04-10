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
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchName : null))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.UserTasks.Select(ut => ut.Task)))
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectMemberships.Select(pm => pm.Project)))
                .ForMember(dest => dest.ProfileImageBase64, opt => opt.MapFrom(src => src.ProfileImage != null ? Convert.ToBase64String(src.ProfileImage) : null));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()); // Reverse mapping

            // Task Mapping
            CreateMap<_Task, TaskDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : null))
                .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src => src.UserTasks.Select(ut => ut.User)));

            CreateMap<TaskDTO, _Task>()
                .ForMember(dest => dest.UserTasks, opt => opt.Ignore()); // Handle assignment logic separately, Reverse mapping

            // Branch Mapping
            CreateMap<Branch, BranchDTO>();
            CreateMap<BranchDTO, Branch>(); // Reverse mapping

            // Project Mapping
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.BranchName : null))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Id : (int?)null))
                .ForMember(dest => dest.TeamMembers, opt => opt.MapFrom(src => src.ProjectMembers.Select(pm => pm.User)));

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore()); // Handle project-member links in logic, Reverse mapping

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

            CreateMap<Holiday, HolidayDTO>();
            CreateMap<HolidayDTO, Holiday>();

            CreateMap<User, UserMiniDTO>();
            CreateMap<_Task, TaskMiniDTO>();
            CreateMap<Project, ProjectMiniDTO>();

        }
    }
}
