using System.ComponentModel.DataAnnotations;

namespace DruivendoosAPI.DTOs
{
    //Deze klasse geeft de inlog gegevens door.
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
