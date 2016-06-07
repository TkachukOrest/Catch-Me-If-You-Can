using System;

namespace CatchMe.Domain.Entities
{
    public class UserEntity
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

        #region Constructor
        public UserEntity()
        {
            Profile = new UserProfileEntity();
        }
        #endregion
    }
}
