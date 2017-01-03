using System.Runtime.Serialization;

namespace BlogsApp.Api.Models
{
    /// <summary>
    /// Handled exception data
    /// </summary>
    [DataContract(Name = "error")]
    public class HandledException
    {
        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Exception
        /// </summary>
        [DataMember(Name = "exception")]
        public string Exception { get; set; }
    }
}
