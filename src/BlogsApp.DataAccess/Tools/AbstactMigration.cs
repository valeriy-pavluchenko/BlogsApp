namespace BlogsApp.DataAccess.Tools
{
    /// <summary>
    /// Abstract migration
    /// </summary>
    public abstract class AbstactMigration
    {
        protected const string BasePath = @"..\BlogsApp.DataAccess\Migrations\Sql\Versions\";

        public abstract string[] GetMigrationScripts();
    }
}
