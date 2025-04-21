using AutoMapper;


namespace TaskList.DTOs.Extensions
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile() 
        {
            CreateMap<Task, TaskDTO>().ReverseMap();
        }
    }
}
