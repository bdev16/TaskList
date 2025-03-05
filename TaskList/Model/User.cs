using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "varchar(80)")]
        [StringLength(80, ErrorMessage = "O Nome informado ultrapassa os limites de 80 caracteres...")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Email não pode ser vazio...")]
        [Column(TypeName = "text")]
        [EmailAddress(ErrorMessage = "O Email não segue a estrutura correta...")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "A senha não pode ser vazia...")]
        public string Password { get; set; } = string.Empty;
        public ICollection<Task> Tasks { get; set; }
    }
}
