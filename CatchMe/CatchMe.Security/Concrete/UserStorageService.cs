using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Security.Abstract;
using CatchMe.Security.Models;

namespace CatchMe.Security.Concrete
{
    public class UserStorageService : IUserStorageService<IdentityUser>
    {
        private readonly IUserRepository _userRepository;

        public IQueryable<IdentityUser> Users
        {
            get { return _userRepository.GetAll().Select(u => (IdentityUser)u).AsQueryable(); }
        }

        public UserStorageService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task CreateAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                _userRepository.Create((UserEntity)user);
            });
        }

        public Task UpdateAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                _userRepository.Update((UserEntity)user);
            });
        }

        public Task DeleteAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                _userRepository.Delete((UserEntity)user);
            });
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Task<IdentityUser>.Factory.StartNew(() => (IdentityUser)_userRepository.FindById(userId));
        }        

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task<IdentityUser>.Factory.StartNew(() => (IdentityUser)_userRepository.FindByName(userName));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PasswordHash = passwordHash;
                //_userRepository.SetPasswordHash((UserEntity)user, passwordHash);
            });
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task<string>.Factory.StartNew(() => _userRepository.GetPasswordHash((UserEntity)user));
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task<bool>.Factory.StartNew(() => _userRepository.HasPassword((UserEntity)user));
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            return Task.Factory.StartNew(() => { 
                user.Email = email;
                //_userRepository.SetEmail((UserEntity)user, email)
            });
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.Factory.StartNew(() => _userRepository.GetEmail((UserEntity)user));
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task<bool>.Factory.StartNew(() => _userRepository.GetEmailConfirmed((UserEntity)user));
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            return Task.Factory.StartNew(() => { 
                user.EmailConfirmed = confirmed;
                //_userRepository.SetEmailConfirmed((UserEntity)user, confirmed)
            });
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            return Task<IdentityUser>.Factory.StartNew(() => (IdentityUser)_userRepository.FindByEmail(email));
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            return Task.Factory.StartNew(() => _userRepository.AddToRole((UserEntity)user, roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            return Task.Factory.StartNew(() => _userRepository.AddToRole((UserEntity)user, roleName));
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return Task<IList<string>>.Factory.StartNew(() => _userRepository.GetRoles((UserEntity)user));
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return Task<bool>.Factory.StartNew(() => _userRepository.IsInRole((UserEntity)user, roleName));
        }

        public void Dispose() { }
    }
}
