using System;
using System.Linq;
using System.Threading.Tasks;
using CatchMe.Security.Abstract;
using CatchMe.Security.Models;

namespace CatchMe.Security.Concrete
{
    public class RoleStorageService : IRoleStorageService<IdentityRole>
    {
        public IQueryable<IdentityRole> Roles { get; }

        public Task CreateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
        
        public void Dispose()
        {
           
        }
    }
}
