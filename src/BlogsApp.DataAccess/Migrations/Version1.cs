using BlogsApp.DataAccess.Attributes;
using BlogsApp.DataAccess.Tools;

namespace BlogsApp.DataAccess.Migrations
{
    /// <summary>
    /// Migration v1
    /// </summary>
    [Migration(1, "version 1")]
    public class Version1 : AbstactMigration
    {
        public override string[] GetMigrationScripts()
        {
            var scripts = new[]
            {
                FileReader.GetFileContentAsString(BasePath + @"Version1\alter.sql"),
                FileReader.GetFileContentAsString(BasePath + @"Version1\userRole.sql")
            };

            return scripts;
        }
    }
}
