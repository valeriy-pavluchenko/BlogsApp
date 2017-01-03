using BlogsApp.DataAccess.Entities;

using Dapper;

using System.Data;
using System.Linq;

namespace BlogsApp.DataAccess.Repositories
{
    public class MigrationRepository : BaseRepository
    {
        public MigrationRepository(string connectionString) : base(connectionString)
        {
        }

        public Migration[] GetList()
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<Migration>(
                    sql: "usp_Migration_GetList",
                    commandType: CommandType.StoredProcedure).ToArray();

                return entities;
            }
        }

        public int Insert(Migration entity)
        {
            using (var connection = DbConnection)
            {
                var entityId = connection.Query<int>(
                    sql: "usp_Migration_Insert",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        entity.MigrationId,
                        entity.Name,
                        entity.AppliedOnUtc
                    }).Single();

                return entityId;
            }
        }
    }
}
