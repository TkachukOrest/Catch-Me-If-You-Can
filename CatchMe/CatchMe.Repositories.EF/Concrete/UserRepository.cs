using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ICatchMeContext _catchMeContext;

        public UserRepository(ICatchMeContext context)
        {
            _catchMeContext = context;
        }

        public int Create(UserEntity user)
        {
            _catchMeContext.UserProfiles.Add(user.Profile);
            _catchMeContext.Users.Add(user);
            _catchMeContext.SaveChanges();

            return user.Id;
        }

        public void Update(UserEntity user)
        {
            var userToUpdate = _catchMeContext.Users.Find(user.Id);

            if (userToUpdate != null)
            {
                var userProfile = _catchMeContext.UserProfiles.FirstOrDefault(up => up.UserId == user.Id);

                if (userProfile != null)
                {
                    userProfile.FirstName = user.Profile.FirstName;
                    userProfile.LastName = user.Profile.LastName;
                    userProfile.PhoneNumber = user.Profile.PhoneNumber;
                }

                userToUpdate.Email = user.Email;
                userToUpdate.EmailConfirmed = user.EmailConfirmed;
                userToUpdate.PasswordHash = user.PasswordHash;
                userToUpdate.SecurityStamp = user.SecurityStamp;
                userToUpdate.UserName = user.UserName;
                userToUpdate.Profile = user.Profile;
                userToUpdate.CreationTime = user.CreationTime;

                _catchMeContext.SaveChanges();
            }
        }

        public void Delete(UserEntity user)
        {
            var userToDelete = _catchMeContext.Users             
                .FirstOrDefault(u => u.Id == user.Id);

            if (userToDelete != null)
            {                
                _catchMeContext.Users.Remove(userToDelete);
                _catchMeContext.SaveChanges();
            }
        }

        public List<UserEntity> GetAll()
        {
            var users = _catchMeContext.Users.Include(u => u.UserProfiles).ToList();

            users.ForEach(user =>
            {
                if (user != null)
                {
                    user.Profile = user.UserProfiles.FirstOrDefault();
                }
            });

            return users;
        }

        public UserEntity FindByName(string userName)
        {
            var user = _catchMeContext.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                user.Profile = user.UserProfiles.FirstOrDefault();
            }

            return user;
        }

        public UserEntity FindByEmail(string email)
        {
            var user = _catchMeContext.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.Profile = user.UserProfiles.FirstOrDefault();
            }

            return user;
        }

        public UserEntity FindById(int id)
        {
            var user = _catchMeContext.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefault(u => u.Id == id);

            user.Profile = user.UserProfiles.FirstOrDefault();

            return user;
        }

        public void AddToRole(UserEntity user, string roleName)
        {
            var role = _catchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                var foundedUser = _catchMeContext.Users
                    .Where(u => u.Id == user.Id)
                    .Include(x => x.Roles)
                    .FirstOrDefault();
                foundedUser.Roles.Add(role);
                _catchMeContext.SaveChanges();
            }
        }

        public void RemoveFromRole(UserEntity user, string roleName)
        {
            var role = _catchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                var foundedUser = _catchMeContext.Users
                    .Where(u => u.Id == user.Id)
                    .Include(x => x.Roles)
                    .FirstOrDefault();

                if (foundedUser != null)
                {
                    foundedUser.Roles.Remove(role);
                    _catchMeContext.SaveChanges();
                }
            }
        }

        public IList<string> GetRoles(UserEntity user)
        {
            var foundedUser = _catchMeContext.Users
                .Where(u => u.Id == user.Id)
                .Include(x => x.Roles)
                .First();

            return foundedUser.Roles.Select(r => r.Name).ToList();
        }

        public bool IsInRole(UserEntity user, string roleName)
        {
            var userRoles = GetRoles(user).ToList();

            return userRoles.Contains(roleName);
        }
    }
}
