using System;

namespace BlogsApp.DataAccess.Entities
{
    /// <summary>
    /// Migration
    /// </summary>
    public class Migration
    {
        public int MigrationId { get; set; }
        public string Name { get; set; }
        public DateTime AppliedOnUtc { get; set; }
    }
}
