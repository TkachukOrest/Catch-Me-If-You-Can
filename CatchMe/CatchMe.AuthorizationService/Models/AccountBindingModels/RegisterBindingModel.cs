using System.ComponentModel.DataAnnotations;

namespace CatchMe.SecurityService.Models.AccountBindingModels
{
    public class RegisterBindingModel
    {
        [Required(ErrorMessage = "Field First Name can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field Last Name can't be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Field PhoneNumber can't be empty")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "PhoneNumber is not valid")]
        public string PhoneNumber { get; set; }
    }
}