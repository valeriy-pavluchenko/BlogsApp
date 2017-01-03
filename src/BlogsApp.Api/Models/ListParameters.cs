using System.Runtime.Serialization;

namespace BlogsApp.Api.Models
{
    /// <summary>
    /// List parameters
    /// </summary>
    [DataContract(Name = "listParameters")]
    public class ListParameters
    {
        /// <summary>
        /// Limit
        /// </summary>
        [DataMember(Name = "limit")]
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Offset
        /// </summary>
        [DataMember(Name = "offset")]
        public int Offset { get; set; }
    }
}
