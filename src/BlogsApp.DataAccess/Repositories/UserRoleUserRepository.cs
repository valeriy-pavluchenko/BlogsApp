using Dapper;

using System.Data;

namespace BlogsApp.DataAccess.Repositories
{
    public class UserRoleUserRepository : BaseRepository
    {
        public UserRoleUserRepository(string connectionString) : base(connectionString)
        {
        }

        public void Insert(int userId, int userRoleId)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_UserRoleUser_Insert",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId,
                        UserRoleId = userRoleId
                    });
            }
        }

        public void Delete(int userId, int userRoleId)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_UserRoleUser_Delete",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId,
                        UserRoleId = userRoleId
                    });
            }
        }
    }
}
