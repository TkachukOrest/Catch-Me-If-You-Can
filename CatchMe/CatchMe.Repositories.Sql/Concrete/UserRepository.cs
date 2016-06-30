using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.Sql.Abstract;

namespace CatchMe.Repositories.Sql.Concrete
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }

        #region IUserRepository
        public List<UserEntity> GetAll()
        {
            var users = new List<UserEntity>();

            ExecuteDataReaderProc(SpNames.GetUsers, null, (reader) =>
            {
                while (reader.Read())
                {
                    users.Add(this.PopulateUserEntity(reader));
                }
            });

            return users;
        }

        public int Create(UserEntity user)
        {
            var parameters = new[]
            {
                CreateSqlParameter(SpParams.SaveUser.Email, user.Email, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.EmailConfirmed, user.EmailConfirmed, DbType.Byte),
                CreateSqlParameter(SpParams.SaveUser.PasswordHash, user.PasswordHash, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.UserName, user.UserName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.SecurityStamp, user.SecurityStamp, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.CreationTime, user.CreationTime, DbType.DateTime),

                CreateSqlParameter(SpParams.SaveUser.FirstName, user.Profile.FirstName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.LastName, user.Profile.LastName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.PhoneNumber, user.Profile.PhoneNumber, DbType.String)
            };

            var userId = ExecuteScalar(SpNames.AddUser, parameters);

            return userId;
        }

        public void Update(UserEntity user)
        {
            var parameters = new[]
           {
                CreateSqlParameter(SpParams.SaveUser.UserId, user.Id, DbType.Int32),
                CreateSqlParameter(SpParams.SaveUser.Email, user.Email, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.EmailConfirmed, user.EmailConfirmed, DbType.Byte),
                CreateSqlParameter(SpParams.SaveUser.PasswordHash, user.PasswordHash, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.UserName, user.UserName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.SecurityStamp, user.SecurityStamp, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.CreationTime, user.CreationTime, DbType.DateTime),

                CreateSqlParameter(SpParams.SaveUser.FirstName, user.Profile.FirstName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.LastName, user.Profile.LastName, DbType.String),
                CreateSqlParameter(SpParams.SaveUser.PhoneNumber, user.Profile.PhoneNumber, DbType.String)
            };

            ExecuteNonQueryProc(SpNames.UpdateUserById, parameters);
        }

        public void Delete(UserEntity user)
        {
            var parameters = new[]
        {
                new SqlParameter(SpParams.UserCommon.UserId, user.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 }
            };

            ExecuteNonQueryProc(SpNames.DeleteUserById, parameters);
        }

        public UserEntity FindById(int userId)
        {
            UserEntity user = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserId, userId) { Direction = ParameterDirection.Input, DbType = DbType.Int32 }
            };

            ExecuteDataReaderProc(SpNames.FindUserById, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    user = this.PopulateUserEntity(reader);
                }
            });

            return user;
        }

        public UserEntity FindByName(string userName)
        {
            UserEntity user = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserName, userName) { Direction = ParameterDirection.Input, DbType = DbType.String}
            };

            ExecuteDataReaderProc(SpNames.FindUserByName, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    user = this.PopulateUserEntity(reader);
                }
            });

            return user;
        }

        public UserEntity FindByEmail(string userEmail)
        {
            UserEntity user = null;

            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserEmail, userEmail) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            ExecuteDataReaderProc(SpNames.FindUserByEmail, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    user = this.PopulateUserEntity(reader);
                }
            });

            return user;
        }

        public void AddToRole(UserEntity user, string roleName)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserId, user.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.RoleCommon.RoleName, roleName) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            ExecuteNonQueryProc(SpNames.AddUserToRole, parameters);
        }

        public void RemoveFromRole(UserEntity user, string roleName)
        {
            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserId, user.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 },
                new SqlParameter(SpParams.RoleCommon.RoleName, roleName) { Direction = ParameterDirection.Input, DbType = DbType.String }
            };

            ExecuteNonQueryProc(SpNames.RemoveUserFromRole, parameters);
        }

        public IList<string> GetRoles(UserEntity user)
        {
            List<string> userRoles = new List<string>();

            var parameters = new[]
            {
                new SqlParameter(SpParams.UserCommon.UserId, user.Id) { Direction = ParameterDirection.Input, DbType = DbType.Int32 }
            };

            ExecuteDataReaderProc(SpNames.GetUserRoles, parameters, (reader) =>
            {
                if (reader.Read())
                {
                    userRoles.Add(this.PopulateUserRole(reader));
                }
            });

            return userRoles;
        }

        public bool IsInRole(UserEntity user, string roleName)
        {
            var userRoles = GetRoles(user).ToList();

            return userRoles.Contains(roleName);
        }
        #endregion

        #region Helpers

        public UserEntity PopulateUserEntity(SqlDataReader reader)
        {
            var user = new UserEntity()
            {
                Id = reader["UserId"].FromDb<int>(),
                Email = reader["Email"].FromDb<string>(),
                EmailConfirmed = reader["EmailConfirmed"].FromDb<bool>(),
                PasswordHash = reader["PasswordHash"].FromDb<string>(),
                SecurityStamp = reader["SecurityStamp"].FromDb<string>(),
                UserName = reader["UserName"].FromDb<string>(),
                CreationTime = reader["CreationTime"].FromDb<DateTime>(),
                Profile = new UserProfileEntity()
                {
                    Id = reader["UserProfileId"].FromDb<int>(),
                    UserId = reader["UserId"].FromDb<int>(),
                    FirstName = reader["FirstName"].FromDb<string>(),
                    LastName = reader["LastName"].FromDb<string>(),
                    PhoneNumber = reader["PhoneNumber"].FromDb<string>()
                }
            };

            return user;
        }

        public string PopulateUserRole(SqlDataReader reader)
        {
            return reader["UserRole"].FromDb<string>();
        }
        #endregion
    }
}
