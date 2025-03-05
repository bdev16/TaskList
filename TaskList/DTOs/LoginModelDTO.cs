using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace TaskList.DTOs
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; }
	}
}
