using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface IUserRepository
    {        
        void Create(UserEntity user);

        void Update(UserEntity user);        

        void Delete(UserEntity user);

        List<UserEntity> GetAll();        

        UserEntity FindByName(string userName);

        UserEntity FindByEmail(string email);

        UserEntity FindById(string id);

        bool HasPassword(UserEntity user);

        void SetPasswordHash(UserEntity user, string passwordHash);

        string GetPasswordHash(UserEntity user);        

        void SetEmail(UserEntity user, string email);

        string GetEmail(UserEntity user);

        bool GetEmailConfirmed(UserEntity user);

        void SetEmailConfirmed(UserEntity user, bool confirmed);

        void AddToRole(UserEntity user, string roleName);

        void RemoveFromRole(UserEntity user, string roleName);

        IList<string> GetRoles(UserEntity user);

        bool IsInRole(UserEntity user, string roleName);
    }
}
