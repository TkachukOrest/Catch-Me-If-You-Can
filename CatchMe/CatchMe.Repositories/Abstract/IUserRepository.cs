using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface IUserRepository
    {        
        int Create(UserEntity user);

        void Update(UserEntity user);        

        void Delete(UserEntity user);

        List<UserEntity> GetAll();        

        UserEntity FindByName(string userName);

        UserEntity FindByEmail(string email);

        UserEntity FindById(int id);
     
        void AddToRole(UserEntity user, string roleName);

        void RemoveFromRole(UserEntity user, string roleName);

        IList<string> GetRoles(UserEntity user);

        bool IsInRole(UserEntity user, string roleName);
    }
}
