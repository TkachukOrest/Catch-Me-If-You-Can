﻿using CatchMe.Infrastructure;
using CatchMe.Security.Abstract;
using CatchMe.Security.Concrete;
using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CatchMe.Security.Configurations
{
    public class IdentityUserManager : UserManager<IdentityUser>
    {
        public IdentityUserManager(IUserStore<IdentityUser> store) : base(store) { }

        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options, IOwinContext context)
        {
            var userStorage = ServiceProvider.Get<IUserStorageService<IdentityUser>>();
            var manager = new IdentityUserManager(userStorage);            

            manager.UserValidator = new UserValidator<IdentityUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}