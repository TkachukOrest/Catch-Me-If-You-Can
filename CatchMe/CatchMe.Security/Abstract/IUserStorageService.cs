using CatchMe.Security.Models;
using Microsoft.AspNet.Identity;

namespace CatchMe.Security.Abstract
{
    public interface IUserStorageService<TUser> : IUserStore<TUser, int>,
        IQueryableUserStore<TUser, int>,
        IUserPasswordStore<TUser, int>,
        IUserEmailStore<TUser, int>,        
        IUserRoleStore<TUser, int>         
        where TUser : IdentityUser
    {
    }
}
