using System.Collections.ObjectModel;

namespace TaskList.Model
{
    public class User
    {
        public User() 
        { 
            Tasks = new Collection<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Task> Tasks { get; set; }
    }
}
