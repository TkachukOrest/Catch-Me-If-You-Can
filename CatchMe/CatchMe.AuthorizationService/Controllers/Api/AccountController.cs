using System;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Http;
using CatchMe.Domain.Entities;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Security.Models;
using CatchMe.SecurityService.Models.AccountBindingModels;
using Microsoft.AspNet.Identity;

namespace CatchMe.SecurityService.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Consts
        private const string LocalLoginProvider = "Local";
        #endregion

        #region Fields        
        private IdentityUserManager _userManager;        
        private readonly IConfigurationService _configurationService;
        #endregion

        #region Constructors                     
        public AccountController(IdentityUserManager userManager,            
            IConfigurationService configurationService)
        {
            _userManager = userManager;            
            _configurationService = configurationService;
        }
        #endregion
        
        [HttpGet]
        public async Task<IHttpActionResult> VerifyUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            
            return Ok(user != null);
        }

        public async Task<IHttpActionResult> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return Ok(user);
        }

        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId().To<int>(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId().To<int>(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Profile =
                    new UserProfileEntity
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber
                    }
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Link("ConfirmEmail", new { userId = user.Id, code = code });

            await _userManager.SendEmailAsync(user.Id,
                "Account confirmation",
                "Please confirm your account by clicking this link: " + callbackUrl);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                ModelState.AddModelError("", "User id and code are required");
                return BadRequest(ModelState);
            }

            var result = await _userManager.ConfirmEmailAsync(userId.To<int>(), code);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var redirectUrl = _configurationService.GetConfiguration("ConfirmationEmailRedirectUrl");
            return Redirect(redirectUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        #endregion
    }
}
