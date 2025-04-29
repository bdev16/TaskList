using AutoMapper;
using TaskList.Model;
using Task = TaskList.Model.Task;


namespace TaskList.DTOs.Extensions
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile() 
        {
            CreateMap<Task, TaskDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, LoginModelDTO>().ReverseMap();
            CreateMap<User, RegisterModelDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
