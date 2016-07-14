using System.Collections.Generic;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class RoleRepository : EfRepository, IRoleRepository
    {        
        public RoleRepository(ICatchMeContext context) : base(context) { }            

        public IEnumerable<RoleEntity> GetAll()
        {
            var roles = CatchMeContext.Roles.ToList();

            return roles;
        }

        public int Create(RoleEntity role)
        {
            CatchMeContext.Roles.Add(role);
            CatchMeContext.SaveChanges();

            return role.Id;
        }

        public void Delete(RoleEntity role)
        {
            CatchMeContext.Roles.Remove(role);
            CatchMeContext.SaveChanges();
        }

        public RoleEntity FindById(int roleId)
        {
            var role = CatchMeContext.Roles.Find(roleId);

            return role;
        }

        public RoleEntity FindByName(string roleName)
        {
            var role = CatchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            return role;
        }

        public void Update(RoleEntity role)
        {
            var roleToUpdate = CatchMeContext.Roles.Find(role.Id);

            if (roleToUpdate != null)
            {
                roleToUpdate.Name = role.Name;
                CatchMeContext.SaveChanges();
            }
        }
    }
}
