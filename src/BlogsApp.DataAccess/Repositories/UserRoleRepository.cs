using BlogsApp.DataAccess.Entities;
using BlogsApp.DataAccess.Exceptions;

using Dapper;

using System.Data;
using System.Linq;

namespace BlogsApp.DataAccess.Repositories
{
    public class UserRoleRepository : BaseRepository
    {
        public UserRoleRepository(string connectionString) : base(connectionString)
        {
        }

        public UserRole GetById(int userRoleId)
        {
            using (var connection = DbConnection)
            {
                var entity = connection.Query<UserRole>(
                    sql: "usp_UserRole_GetById",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserRoleId = userRoleId
                    }).SingleOrDefault();

                if (entity == null)
                    throw new DbNotFoundException("Can't find user's role.");

                return entity;
            }
        }

        public UserRole[] GetList()
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<UserRole>(
                    sql: "usp_UserRole_GetList",
                    commandType: CommandType.StoredProcedure).ToArray();

                return entities;
            }
        }

        public UserRole[] GetListByUserId(int userId)
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<UserRole>(
                    sql: "usp_UserRole_GetListByUserId",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId
                    }).ToArray();

                return entities;
            }
        }
    }
}
