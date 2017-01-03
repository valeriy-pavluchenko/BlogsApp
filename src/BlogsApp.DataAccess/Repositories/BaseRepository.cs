using System.Data;
using System.Data.SqlClient;

namespace BlogsApp.DataAccess.Repositories
{
    /// <summary>
    /// Base repository
    /// </summary>
    public class BaseRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Sql connection
        /// </summary>
        protected IDbConnection DbConnection => new SqlConnection(_connectionString);
    }
}
