using System;
using CatchMe.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Models
{
    public class IdentityUser : IUser<int>
    {
        #region Properties        
        public int Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string UserName { get; set; }

        public DateTime CreationTime { get; set; }

        public UserProfileEntity Profile { get; set; }
        #endregion

        #region Operators

        public static explicit operator IdentityUser(UserEntity user)
        {
            if (user == null) return null;

            var identityUser = new IdentityUser();

            identityUser.Id = user.Id;
            identityUser.Email = user.Email;
            identityUser.EmailConfirmed = user.EmailConfirmed;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;
            identityUser.UserName = user.UserName;
            identityUser.Profile = user.Profile;
            identityUser.CreationTime = user.CreationTime;

            return identityUser;
        }

        public static explicit operator UserEntity(IdentityUser identityUser)
        {
            if (identityUser == null) return null;

            var user = new UserEntity();

            user.Id = identityUser.Id;
            user.Email = identityUser.Email;
            user.EmailConfirmed = identityUser.EmailConfirmed;
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.UserName = identityUser.UserName;
            user.CreationTime = identityUser.CreationTime;
            user.Profile = identityUser.Profile;

            return user;
        }

        #endregion
    }
}
