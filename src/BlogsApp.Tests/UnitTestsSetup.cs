using BlogsApp.DataAccess.Repositories;
using BlogsApp.DataAccess.Tools;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

namespace BlogsApp.Tests
{
    /// <summary>
    /// Initial unit test project setup
    /// </summary>
    [TestClass]
    public class UnitTestsSetup
    {
        protected static readonly IConfigurationRoot Configuration;

        protected static readonly IRepositories Repositories;

        static UnitTestsSetup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Repositories = new Repositories(Configuration.GetConnectionString("BlogsAppApi"));
        }

        [AssemblyInitialize]
        public static void Init(TestContext testContext)
        {
            var migrator = new DbMigrator(Configuration.GetConnectionString("BlogsAppApi"));

            if (migrator.IsDatabaseExist())
            {
                migrator.FlushDatabase();
            }
            else
            {
                migrator.CreateDatabase();
            }
            
            migrator.InitDatabase();
            migrator.MigrateDatabaseToLatestVersion();
        }
    }
}
