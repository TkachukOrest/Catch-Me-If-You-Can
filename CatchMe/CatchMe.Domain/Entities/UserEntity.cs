using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CatchMe.Domain.Entities
{
    [Table("User")]
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

        [NotMapped]
        public UserProfileEntity Profile { get; set; }
        #endregion

        #region Constructor
        public UserEntity()
        {
            Profile = new UserProfileEntity();
        }
        #endregion
        
        #region Navigation properties 
        [JsonIgnore]
        public virtual ICollection<PassengerEntity> Passengers { get; set; }
        [JsonIgnore]
        public virtual ICollection<TripEntity> Trips { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserProfileEntity> UserProfiles { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleEntity> Roles { get; set; }        
        #endregion
    }
}
