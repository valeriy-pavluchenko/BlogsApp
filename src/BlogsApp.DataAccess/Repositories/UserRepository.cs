using System;

using BlogsApp.DataAccess.Entities;
using BlogsApp.DataAccess.Exceptions;

using Dapper;

using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BlogsApp.DataAccess.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public int Insert(User entity)
        {
            using (var connection = DbConnection)
            {
                try
                {
                    var entityId = connection.Query<int>(
                        sql: "usp_User_Insert",
                        commandType: CommandType.StoredProcedure,
                        param: new
                        {
                            entity.Email,
                            entity.PasswordHash,
                            entity.RegisteredOnUtc
                        }).Single();

                    return entityId;
                }
                catch (SqlException sqlException)
                {
                    if (sqlException.Number == 2627)
                        throw new DbRecordAlreadyExistException("User with this email already exists");
                    throw;
                }
            }
        }

        public User GetById(int userId)
        {
            using (var connection = DbConnection)
            {
                var entity = connection.Query<User>(
                    sql: "usp_User_GetById",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId
                    }).SingleOrDefault();

                if (entity == null)
                    throw new DbNotFoundException("Can't find the user.");

                return entity;
            }
        }

        public User GetByEmail(string email)
        {
            using (var connection = DbConnection)
            {
                var entity = connection.Query<User>(
                    sql: "usp_User_GetByEmail",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        Email = email
                    }).SingleOrDefault();

                return entity;
            }
        }

        public User[] GetList(int limit, int offset)
        {
            using (var connection = DbConnection)
            {
                var entities =  connection.Query<User>(
                    sql: "usp_User_GetList",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        Limit = limit,
                        Offset = offset
                    }).ToArray();

                return entities;
            }
        }

        public void Delete(int userId)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_User_Delete",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId
                    });
            }
        }
    }
}
