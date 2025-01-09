using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Model
{
    public class User
    {
        public User() 
        { 
            Tasks = new Collection<Task>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome não pode ser vazio...")]
        [StringLength(80, ErrorMessage = "O Nome informado ultrapassa os limites de 80 caracteres...")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "O Email não pode ser vazio...")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha não pode ser vazia...")]
        public string Password { get; set; } = string.Empty;
        public ICollection<Task> Tasks { get; set; }
    }
}
