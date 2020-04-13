using System.Collections.Generic;

namespace DressWell.Data.Models
{
    /// <summary>
    /// Model for holding the input commands passed by the user
    /// </summary>
    public class UserInputModel
    {
        /// <summary>
        /// Construct for holding the user supplied commands
        /// </summary>
        public List<string> Input { get; set; }
    }
}
