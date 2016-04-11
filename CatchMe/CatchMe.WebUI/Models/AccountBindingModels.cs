using System.ComponentModel.DataAnnotations;

namespace CatchMe.WebUI.Models
{   
    public class AddExternalLoginBindingModel
    {
        [Required]        
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]        
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]        
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

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
    }

    public class RegisterExternalBindingModel
    {
        [Required(ErrorMessage = "Field First Name can't be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field Last Name can't be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field Email can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]        
        public string LoginProvider { get; set; }

        [Required]        
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]        
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
