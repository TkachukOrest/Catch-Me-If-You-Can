using System;
using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;

namespace CatchMe.Repositories.Abstract
{
    public interface IUserRepository : IDisposable
    {        
        void Create(UserEntity user);

        void Update(UserEntity user);        

        void Delete(UserEntity user);

        UserEntity FindById(string userId);        

        UserEntity FindByName(string userName);

        void SetPasswordHash(UserEntity user, string passwordHash);

        string GetPasswordHash(UserEntity user);

        bool HasPassword(UserEntity user);

        void SetEmail(UserEntity user, string email);

        string GetEmail(UserEntity user);

        bool GetEmailConfirmed(UserEntity user);

        void SetEmailConfirmed(UserEntity user, bool confirmed);                  

        UserEntity FindByEmail(string email);

        void SetSecurityStamp(UserEntity user, string stamp);

        string GetSecurityStamp(UserEntity user);

        void AddToRole(UserEntity user, string roleName);

        void RemoveFromRole(UserEntity user, string roleName);

        IList<string> GetRoles(UserEntity user);

        bool IsInRole(UserEntity user, string roleName);

        void AddLogin(UserEntity user, UserLoginInfo login);

        void RemoveLogin(UserEntity user, UserLoginInfo login);

        IList<UserLoginInfo> GetLogins(UserEntity user);       

        UserEntity Find(UserLoginInfo login);        
    }
}
