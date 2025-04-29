using AutoMapper;
using Task = TaskList.Model.Task;


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
