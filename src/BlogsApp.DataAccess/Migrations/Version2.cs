using BlogsApp.DataAccess.Attributes;
using BlogsApp.DataAccess.Tools;

namespace BlogsApp.DataAccess.Migrations
{
    /// <summary>
    /// Migration v2
    /// </summary>
    [Migration(2, "version 2")]
    public class Version2 : AbstactMigration
    {
        public override string[] GetMigrationScripts()
        {
            var scripts = new[]
            {
                FileReader.GetFileContentAsString(BasePath + @"Version2\alter.sql"),
            };

            return scripts;
        }
    }
}
