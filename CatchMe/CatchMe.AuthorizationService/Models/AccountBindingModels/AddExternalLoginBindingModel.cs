using System.ComponentModel.DataAnnotations;

namespace CatchMe.SecurityService.Models.AccountBindingModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        public string ExternalAccessToken { get; set; }
    }
}