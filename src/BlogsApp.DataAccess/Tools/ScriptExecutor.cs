using Dapper;

using System;
using System.Data;
using System.Data.SqlClient;

namespace BlogsApp.DataAccess.Tools
{
    /// <summary>
    /// Script executor
    /// </summary>
    public class ScriptExecutor
    {
        private const string BatchSeparator = "GO";

        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        public ScriptExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Execute script
        /// </summary>
        /// <param name="script">Script</param>
        /// <param name="param">Parameters</param>
        public void Execute(string script, object param = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var scripts = GetBatchedScripts(script);
                foreach (var subScript in scripts)
                {
                    connection.Execute(sql: subScript, param: param, commandType: CommandType.Text);
                }
            }
        }

        private string[] GetBatchedScripts(string script)
        {
            return script.Split(new[] { BatchSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
