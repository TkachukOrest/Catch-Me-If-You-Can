using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CatchMe.Domain.Entities;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.Sql.Abstract;

namespace CatchMe.Repositories.Sql.Concrete
{
    public class RoleRepository : SqlRepository, IRoleRepository
    {
        public RoleRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }

        #region IRoleRepository

        public IEnumerable<RoleEntity> GetAll()
        {
            List<RoleEntity> roles = new List<RoleEntity>();

            ExecuteDataReaderProc(SpNames.GetRoles, null, (reader) =>
            {
                while (reader.Read())
                {
                    roles.Add(this.PopulateRoleEntity(reader));
                }
            });

            return roles;
        }

        public int Create(RoleEntity role)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.RoleCommon.RoleName, role.Name) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            var roleId = ExecuteScalar(SpNames.AddRole, parameters);

            return roleId;
        }

        public void Delete(RoleEntity role)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.RoleCommon.RoleId, role.Name) { Direction = ParameterDirection.Input, DbType = DbType.Int32 }
            };

            ExecuteNonQueryProc(SpNames.DeleteRoleById, parameters);
        }

        public RoleEntity FindById(int roleId)
        {
            RoleEntity role = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.RoleCommon.RoleId, roleId) { Direction = ParameterDirection.Input, DbType = DbType.Int32 }
            };

            ExecuteDataReaderProc(SpNames.FindRoleById, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    role = this.PopulateRoleEntity(reader);
                }
            });

            return role;
        }

        public RoleEntity FindByName(string roleName)
        {
            RoleEntity role = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.RoleCommon.RoleName, roleName) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            ExecuteDataReaderProc(SpNames.FindRoleByName, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    role = this.PopulateRoleEntity(reader);
                }
            });

            return role;
        }

        public void Update(RoleEntity role)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.RoleCommon.RoleId, role.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.RoleCommon.RoleName, role.Name) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            ExecuteNonQueryProc(SpNames.UpdateRoleById, parameters);
        }        
        #endregion

        #region Helpers

        public RoleEntity PopulateRoleEntity(SqlDataReader reader)
        {
            var role = new RoleEntity()
            {
                Id = reader["RoleId"].FromDb<int>(),
                Name = reader["RoleName"].FromDb<string>(),
            };

            return role;
        }

        #endregion
    }
}
