using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class UserRepository : EfRepository, IUserRepository
    {      
        public UserRepository(ICatchMeContext context) : base(context){}

        public int Create(UserEntity user)
        {
            CatchMeContext.UserProfiles.Add(user.Profile);
            CatchMeContext.Users.Add(user);
            CatchMeContext.SaveChanges();

            return user.Id;
        }

        public void Update(UserEntity user)
        {
            var userToUpdate = CatchMeContext.Users.Find(user.Id);

            if (userToUpdate != null)
            {
                var userProfile = CatchMeContext.UserProfiles.FirstOrDefault(up => up.UserId == user.Id);

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

                CatchMeContext.SaveChanges();
            }
        }

        public void Delete(UserEntity user)
        {
            var userToDelete = CatchMeContext.Users             
                .FirstOrDefault(u => u.Id == user.Id);

            if (userToDelete != null)
            {                
                CatchMeContext.Users.Remove(userToDelete);
                CatchMeContext.SaveChanges();
            }
        }

        public List<UserEntity> GetAll()
        {
            var users = CatchMeContext.Users.Include(u => u.UserProfiles).ToList();

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
            var user = CatchMeContext.Users
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
            var user = CatchMeContext.Users
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
            var user = CatchMeContext.Users
                .Include(u => u.UserProfiles)
                .FirstOrDefault(u => u.Id == id);

            user.Profile = user.UserProfiles.FirstOrDefault();

            return user;
        }

        public void AddToRole(UserEntity user, string roleName)
        {
            var role = CatchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                var foundedUser = CatchMeContext.Users
                    .Where(u => u.Id == user.Id)
                    .Include(x => x.Roles)
                    .FirstOrDefault();
                foundedUser.Roles.Add(role);
                CatchMeContext.SaveChanges();
            }
        }

        public void RemoveFromRole(UserEntity user, string roleName)
        {
            var role = CatchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role != null)
            {
                var foundedUser = CatchMeContext.Users
                    .Where(u => u.Id == user.Id)
                    .Include(x => x.Roles)
                    .FirstOrDefault();

                if (foundedUser != null)
                {
                    foundedUser.Roles.Remove(role);
                    CatchMeContext.SaveChanges();
                }
            }
        }

        public IList<string> GetRoles(UserEntity user)
        {
            var foundedUser = CatchMeContext.Users
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
