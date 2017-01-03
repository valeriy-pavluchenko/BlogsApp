using BlogsApp.DataAccess.Attributes;
using BlogsApp.DataAccess.Entities;

using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BlogsApp.DataAccess.Tools
{
    /// <summary>
    /// Database migrator
    /// </summary>
    public class DbMigrator
    {
        private const string BasePath = @"..\BlogsApp.DataAccess\Migrations\Sql\";
        
        private readonly string _connectionString;

        private readonly ScriptExecutor _scriptExecutor;

        public DbMigrator(string connectionString)
        {
            _connectionString = connectionString;
            _scriptExecutor = new ScriptExecutor(connectionString);
        }
        
        /// <summary>
        /// Drop database
        /// </summary>
        public void DropDatabase()
        {
            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                var script = FileReader.GetFileContentAsString(BasePath + @"Common\dropDatabase.sql");
                var parameters = GetParametersFromConnectionString(_connectionString);
                connection.Execute(script, new
                {
                    DatabaseName = parameters["Database"]
                });
            }
        }

        /// <summary>
        /// Create database
        /// </summary>
        public void CreateDatabase()
        {
            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                var script = FileReader.GetFileContentAsString(BasePath + @"Common\createDatabase.sql");
                var parameters = GetParametersFromConnectionString(_connectionString);
                connection.Execute(script, new
                {
                    DatabaseName = parameters["Database"]
                });
            }
        }

        /// <summary>
        /// Flush database
        /// </summary>
        public void FlushDatabase()
        {
            var script = FileReader.GetFileContentAsString(BasePath + @"Common\flushDatabase.sql");
            _scriptExecutor.Execute(script);
        }

        /// <summary>
        /// Database initial configuring
        /// </summary>
        public void InitDatabase()
        {
            var script = FileReader.GetFileContentAsString(BasePath + @"Common\initDatabase.sql");
            _scriptExecutor.Execute(script);
        }

        /// <summary>
        /// Checks is database exist
        /// </summary>
        /// <returns>Is database exist</returns>
        public bool IsDatabaseExist()
        {
            using (var connection = new SqlConnection(GetMasterConnectionString()))
            {
                var script = FileReader.GetFileContentAsString(BasePath + @"Common\isDatabaseExist.sql");
                var parameters = GetParametersFromConnectionString(_connectionString);
                var isDatabaseExist = connection.ExecuteScalar<bool>(sql: script, commandType: CommandType.Text, param: new
                {
                    DatabaseName = parameters["Database"]
                });

                return isDatabaseExist;
            }
        }

        /// <summary>
        /// Perform database migration to the latest version
        /// </summary>
        public void MigrateDatabaseToLatestVersion()
        {
            var types = Assembly.Load(new AssemblyName("BlogsApp.DataAccess"))
                .GetTypes()
                .Where(x => x.GetTypeInfo().GetCustomAttribute<MigrationAttribute>() != null)
                .OrderBy(x => x.GetTypeInfo().GetCustomAttribute<MigrationAttribute>().Id);

            var appliedMigrations = GetAppliedMigrations();

            foreach (var type in types)
            {
                var attribute = type.GetTypeInfo().GetCustomAttribute<MigrationAttribute>();
                if (!appliedMigrations.Select(x => x.MigrationId).Contains(attribute.Id))
                {
                    var instance = (AbstactMigration)Activator.CreateInstance(type);
                    var scripts = instance.GetMigrationScripts();
                    foreach (var script in scripts)
                    {
                        _scriptExecutor.Execute(script);
                    }
                    
                    InsertMigration(new Migration
                    {
                        MigrationId = attribute.Id,
                        Name = attribute.Name,
                        AppliedOnUtc = DateTime.Now.ToUniversalTime()
                    });
                }
            }

            AddStoredProcedures();
        }

        private void AddStoredProcedures()
        {
            var filesNames = Directory.GetFiles(BasePath + @"StoredProcedures\", "*.sql", SearchOption.AllDirectories);
            foreach (var fileName in filesNames)
            {
                var script = FileReader.GetFileContentAsString(fileName);
                _scriptExecutor.Execute(script);
            }
        }

        private Migration[] GetAppliedMigrations()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Migration>(
                    sql: @"SELECT
                               MigrationId,
                               Name
                           FROM
                               Migration",
                    commandType: CommandType.Text).ToArray();
            }
        }

        private void InsertMigration(Migration migration)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    sql: @"INSERT INTO Migration
                           (
                               MigrationId,
                               Name,
                               AppliedOnUtc
                           )
                           VALUES
                           (
                               @MigrationId,
                               @Name,
                               @AppliedOnUtc
                           )",
                    commandType: CommandType.Text,
                    param: new
                    {
                        migration.MigrationId,
                        migration.Name,
                        migration.AppliedOnUtc
                    });
            }
        }

        private Dictionary<string, string> GetParametersFromConnectionString(string connectionString)
        {
            return connectionString
                .Split(new [] {";"}, StringSplitOptions.RemoveEmptyEntries)
                .ToDictionary(x => x.Split('=').First(), x => x.Split('=').Last());
        }

        private string GetConnectionStringFromParameters(Dictionary<string, string> dictionary)
        {
            return dictionary
                .Select(x => x.Key + "=" + x.Value + ";")
                .Aggregate((x, y) => x + y);
        }

        private string GetMasterConnectionString()
        {
            var parameters = GetParametersFromConnectionString(_connectionString);
            parameters["Database"] = "master";

            return GetConnectionStringFromParameters(parameters);
        }
    }
}
