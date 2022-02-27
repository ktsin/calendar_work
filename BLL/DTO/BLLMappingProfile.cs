using AutoMapper;
using DAL.Entities;

namespace BLL.DTO
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<CalendarEvent, CalendarEventDTO>()
                .ReverseMap();
            CreateMap<Group, GroupDTO>()
                .ReverseMap();
            CreateMap<Message, MessageDTO>()
                .ReverseMap();
            CreateMap<Project, ProjectDTO>()
                .ReverseMap();
            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ReverseMap();
            CreateMap<Tag, TagDTO>()
                .ReverseMap();
            CreateMap<DAL.Entities.TaskPriority, BLL.DTO.TaskPriority>()
                .ReverseMap();
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}