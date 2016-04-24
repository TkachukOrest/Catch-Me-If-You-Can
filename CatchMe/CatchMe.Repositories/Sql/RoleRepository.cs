using System;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql
{
    public class RoleRepository: DataBaseRepository, IRoleRepository
    {
        public RoleRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }

        public void Create(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void Delete(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public RoleEntity FindById(string roleId)
        {
            throw new NotImplementedException();
        }

        public RoleEntity FindByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }        
    }
}
