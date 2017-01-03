using System;

namespace BlogsApp.DataAccess.Attributes
{
    /// <summary>
    /// Migration attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MigrationAttribute : Attribute
    {
        /// <summary>
        /// Migration id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Migration name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Migration id</param>
        /// <param name="name">Migration name</param>
        public MigrationAttribute(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
