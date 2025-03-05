using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TaskList.Model
{
    public class User : IdentityUser
    {
        public User() 
        { 
            Tasks = new Collection<Task>();
        }
        //[Key]
        //public int Id { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
