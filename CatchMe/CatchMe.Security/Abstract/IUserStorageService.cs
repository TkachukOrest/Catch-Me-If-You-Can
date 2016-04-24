using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Abstract
{
    public interface IUserStorageService<TUser> : IUserStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserEmailStore<TUser>,        
        IUserRoleStore<TUser>         
        where TUser : IdentityUser
    {
    }
}
