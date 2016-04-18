using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Repositories.Abstract;


namespace CatchMe.Repositories.Sql
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }

        public void Create(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity FindById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity FindByName(string userName)
        {
            throw new System.NotImplementedException();
        }

        public void SetPasswordHash(UserEntity user, string passwordHash)
        {
            throw new System.NotImplementedException();
        }

        public string GetPasswordHash(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public bool HasPassword(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void SetEmail(UserEntity user, string email)
        {
            throw new System.NotImplementedException();
        }

        public string GetEmail(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public bool GetEmailConfirmed(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void SetEmailConfirmed(UserEntity user, bool confirmed)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity FindByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public void SetSecurityStamp(UserEntity user, string stamp)
        {
            throw new System.NotImplementedException();
        }

        public string GetSecurityStamp(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void AddToRole(UserEntity user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFromRole(UserEntity user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public IList<string> GetRoles(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public bool IsInRole(UserEntity user, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public void AddLogin(UserEntity user, UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveLogin(UserEntity user, UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public IList<UserLoginInfo> GetLogins(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity Find(UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }        
    }
}
