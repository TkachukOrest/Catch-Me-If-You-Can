using System.ComponentModel.DataAnnotations;

namespace CatchMe.SecurityService.Models.AccountBindingModels
{
    public class RemoveLoginBindingModel
    {
        [Required]
        public string LoginProvider { get; set; }

        [Required]
        public string ProviderKey { get; set; }
    }
}