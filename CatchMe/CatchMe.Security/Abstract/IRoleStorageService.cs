using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Abstract
{
    public interface IRoleStorageService<TRole> : IRoleStore<TRole, string>,
                                           IQueryableRoleStore<TRole, string>
                                           where TRole : IdentityRole
    {
    }
}
