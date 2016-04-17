using System.ComponentModel.DataAnnotations;

namespace CatchMe.SecurityService.Models.AccountBindingModels
{
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
}