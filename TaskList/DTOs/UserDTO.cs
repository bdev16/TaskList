using Task = TaskList.Model.Task;

namespace TaskList.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public ICollection<Task>? tasks { get; set; }
    }
}
