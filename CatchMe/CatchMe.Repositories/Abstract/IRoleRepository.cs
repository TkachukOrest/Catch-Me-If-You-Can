using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface IRoleRepository
    {
        IEnumerable<RoleEntity> GetAll();

        int Create(RoleEntity role);

        void Delete(RoleEntity role);

        RoleEntity FindById(int roleId);

        RoleEntity FindByName(string roleName);

        void Update(RoleEntity role);
    }
}
