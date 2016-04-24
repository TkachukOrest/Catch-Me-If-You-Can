using CatchMe.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Models
{
    public class IdentityRole : IRole<string>
    {
        #region Properties
        public string Id { get; set; }

        public string Name { get; set; }
        #endregion

        #region Operators

        public static explicit operator IdentityRole(RoleEntity role)
        {
            if (role == null) return null;

            var identityRole = new IdentityRole();

            identityRole.Id = role.Id;
            identityRole.Name = role.Name;

            return identityRole;
        }

        public static explicit operator RoleEntity(IdentityRole identityRole)
        {
            if (identityRole == null) return null;

            var role = new RoleEntity();

            role.Id = identityRole.Id;
            role.Name = identityRole.Name;

            return role;
        }

        #endregion
    }    
}
