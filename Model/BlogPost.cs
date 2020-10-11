using blogAfonina.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogAfonina.Model
{
    /// <summary>
    /// blog post
    /// </summary>
    public class BlogPost : Entity
    {
        /// <summary>
        /// post author
        /// </summary>
        public Profile Author { get; set; }

        /// <summary>
        /// date and time of creation
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// post data
        /// </summary>
        public string Data { get; set; }
    }
}
