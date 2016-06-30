using System.Collections.Generic;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Static.Concrete
{
    public class StaticUserRepository : IUserRepository
    {
        private static readonly List<UserEntity> _users = new List<UserEntity>()
        {
            new UserEntity()
            {
                Id = 1,
                PasswordHash = "AIY1sp1VMZac3/DENKUosDj2Zc0L7pa1jNb6/PQr+j1yvwQ/QvPCA1kNbcbD5igryg==",
                Email = "orcoss36@gmail.com",
                EmailConfirmed = true,
                UserName = "orcoss36@gmail.com",
                Profile =
                    new UserProfileEntity()
                    {
                        FirstName = "Orest",
                        LastName = "Tkachuk",
                        PhoneNumber = "0931331195",
                        UserId = 1,
                        Id = 1
                    }
            }
        };
        private static readonly List<UserInRole> _userRoles = new List<UserInRole>();

        public int Create(UserEntity user)
        {
            var maxId = _users.Max(u => u.Id);
            var newId = maxId != 0 ? maxId + 1 : 1;            

            _users.Add(user);

            return newId;
        }

        public void Update(UserEntity user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);

            _users[index] = user;
        }

        public void Delete(UserEntity user)
        {
            _users.RemoveAll(u => u.Id == user.Id && u.UserName == user.UserName);
        }

        public List<UserEntity> GetAll()
        {
            return _users;
        }

        public UserEntity FindById(int userId)
        {
            var user = _users.Find(u => u.Id == userId);

            return user;
        }

        public UserEntity FindByName(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            return user;
        }

        public void SetPasswordHash(UserEntity user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
        }

        public string GetPasswordHash(UserEntity user)
        {
            var passwordHash = _users.Find(u => u.Id == user.Id).PasswordHash;

            return passwordHash;
        }

        public bool HasPassword(UserEntity user)
        {
            var passwordHash = _users.Find(u => u.Id == user.Id).PasswordHash;
            var hasPassword = !string.IsNullOrEmpty(passwordHash);

            return hasPassword;
        }

        public void SetEmail(UserEntity user, string email)
        {
            user.Email = email;
        }

        public string GetEmail(UserEntity user)
        {
            var entity = _users.Find(u => u.Id == user.Id);

            return entity != null ? entity.Email : user.Email;
        }

        public bool GetEmailConfirmed(UserEntity user)
        {
            var emailConfirmed = _users.Find(u => u.Id == user.Id).EmailConfirmed;

            return emailConfirmed;
        }

        public void SetEmailConfirmed(UserEntity user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
        }

        public UserEntity FindByEmail(string email)
        {
            var user = _users.Find(u => u.Email == email);

            return user;
        }

        public void AddToRole(UserEntity user, string roleName)
        {
            _userRoles.Add(new UserInRole() { User = user, Role = roleName });
        }

        public void RemoveFromRole(UserEntity user, string roleName)
        {
            _userRoles.Remove(_userRoles.Find(ur => ur.User.Id == user.Id && ur.Role == roleName));
        }

        public IList<string> GetRoles(UserEntity user)
        {
            var roles = _userRoles.Where(ur => ur.User.Id == user.Id).Select(ur => ur.Role);

            return roles.ToList();
        }

        public bool IsInRole(UserEntity user, string roleName)
        {
            var userInRole = _userRoles.Find(ur => ur.User.Id == user.Id && ur.Role == roleName);

            return userInRole != null;
        }
    }

    internal class UserInRole
    {
        public UserEntity User { get; set; }
        public string Role { get; set; }
    }
}
