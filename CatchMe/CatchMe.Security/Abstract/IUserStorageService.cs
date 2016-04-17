using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Abstract
{
    public interface IUserStorageService<TUser> : IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserEmailStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserRoleStore<TUser>,
        IUserLoginStore<TUser>,
        IQueryableUserStore<TUser>
        where TUser : IdentityUser
    {
    }
}
