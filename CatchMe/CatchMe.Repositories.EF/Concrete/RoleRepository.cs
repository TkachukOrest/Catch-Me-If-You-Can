using System.Collections.Generic;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ICatchMeContext _catchMeContext;

        public RoleRepository(ICatchMeContext context)
        {
            _catchMeContext = context;
        }

        public IEnumerable<RoleEntity> GetAll()
        {
            var roles = _catchMeContext.Roles.ToList();

            return roles;
        }

        public int Create(RoleEntity role)
        {
            _catchMeContext.Roles.Add(role);
            _catchMeContext.SaveChanges();

            return role.Id;
        }

        public void Delete(RoleEntity role)
        {
            _catchMeContext.Roles.Remove(role);
            _catchMeContext.SaveChanges();
        }

        public RoleEntity FindById(int roleId)
        {
            var role = _catchMeContext.Roles.Find(roleId);

            return role;
        }

        public RoleEntity FindByName(string roleName)
        {
            var role = _catchMeContext.Roles.FirstOrDefault(r => r.Name == roleName);

            return role;
        }

        public void Update(RoleEntity role)
        {
            var roleToUpdate = _catchMeContext.Roles.Find(role.Id);

            if (roleToUpdate != null)
            {
                roleToUpdate.Name = role.Name;
                _catchMeContext.SaveChanges();
            }
        }
    }
}
