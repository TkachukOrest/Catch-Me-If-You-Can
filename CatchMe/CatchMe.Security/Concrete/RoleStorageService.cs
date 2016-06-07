using System.Linq;
using System.Threading.Tasks;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Security.Abstract;
using CatchMe.Security.Models;

namespace CatchMe.Security.Concrete
{
    public class RoleStorageService : IRoleStorageService<IdentityRole>
    {
        private readonly IRoleRepository _roleRepository;

        public IQueryable<IdentityRole> Roles
        {
            get { return _roleRepository.GetAll().Select(u => (IdentityRole)u).AsQueryable(); }
        }

        public RoleStorageService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task CreateAsync(IdentityRole role)
        {
            return Task.Factory.StartNew(() =>
            {
                role.Id = _roleRepository.Create((RoleEntity)role);               
            });
        }

        public Task UpdateAsync(IdentityRole role)
        {
            return Task.Factory.StartNew(() =>
            {
                _roleRepository.Update((RoleEntity)role);
            });
        }

        public Task DeleteAsync(IdentityRole role)
        {
            return Task.Factory.StartNew(() =>
            {
                _roleRepository.Delete((RoleEntity)role);
            });
        }

        public Task<IdentityRole> FindByIdAsync(int roleId)
        {
            return Task<IdentityRole>.Factory.StartNew(() => (IdentityRole)_roleRepository.FindById(roleId));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return Task<IdentityRole>.Factory.StartNew(() => (IdentityRole)_roleRepository.FindByName(roleName));
        }

        public void Dispose() {}
    }
}
