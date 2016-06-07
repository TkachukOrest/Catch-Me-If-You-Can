using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Abstract
{
    public interface IRoleStorageService<TRole> : IRoleStore<TRole, int>,
                                           IQueryableRoleStore<TRole, int>
                                           where TRole : IdentityRole
    {
    }
}
