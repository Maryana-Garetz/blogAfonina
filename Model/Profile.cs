using blogAfonina.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogAfonina.Model
{
    /// <summary>
    /// profile
    /// </summary>
    public class Profile : Entity
    {
        /// <summary>
        /// first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// second name
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// returns a full name
        /// </summary>
        public string FullName
        {
            get => FirstName + " " + Surname;
        }
    }

    
}
