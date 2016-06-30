using System;
using System.Data;
using System.Data.SqlClient;
using CatchMe.Infrastructure.Extensions;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql.Abstract
{
    public abstract class SqlRepository
    {
        protected IRepositorySettings RepositorySettings;

        protected SqlRepository(IRepositorySettings repositorySettings)
        {
            RepositorySettings = repositorySettings;
        }


        protected void ExecuteDataReaderProc(string storedProcedureName, SqlParameter[] parameters, Action<SqlDataReader> onReaderExecuted)
        {
            using (SqlConnection connection = new SqlConnection(RepositorySettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        onReaderExecuted(reader);
                    }
                }
            }
        }

        protected int ExecuteNonQueryProc(string storedProcedureName, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(RepositorySettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        protected int ExecuteScalar(string storedProcedureName, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(RepositorySettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteScalar().To<int>();
                }
            }
        }

        protected SqlParameter CreateSqlParameter(string parameterName, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value)
            {
                Direction = direction,
                DbType = dbType
            };
        }
    }
}
