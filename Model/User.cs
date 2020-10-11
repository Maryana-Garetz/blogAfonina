using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogAfonina.Model
{
    /// <summary>
    /// user
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// user profile
        /// </summary>
        public Profile Profile { get; set; }
    }
}
