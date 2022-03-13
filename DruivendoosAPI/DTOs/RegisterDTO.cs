using System;
using System.ComponentModel.DataAnnotations;

namespace DruivendoosAPI.DTOs
{
    public class RegisterDTO : LoginDTO
    {

        [Required]
        [StringLength(50)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public String LastName { get; set; }

        [Required]
        [StringLength(200)]
        public String Street { get; set; }

        [Required]
        [StringLength(10)]
        public String HouseNumber { get; set; }

        [Required]
        [StringLength(10)]
        public String PostalCode { get; set; }

        [Required]
        [StringLength(100)]
        public String City { get; set; }

        [Required]
        [StringLength(100)]
        public String TelephoneNumber { get; set; }

        [Required]
        [Compare("Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain all of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public String PasswordConfirmation { get; set; }
    }
}
