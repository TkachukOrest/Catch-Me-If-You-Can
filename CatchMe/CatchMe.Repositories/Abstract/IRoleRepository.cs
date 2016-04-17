using System;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface IRoleRepository : IDisposable
    {
        void Create(RoleEntity role);

        void Delete(RoleEntity role);

        RoleEntity FindById(string roleId);

        RoleEntity FindByName(string roleName);

        void Update(RoleEntity role);
    }
}
