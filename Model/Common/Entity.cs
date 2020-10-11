using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogAfonina.Model.Common
{
    /// <summary>
    /// domain entity model interface
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// creating an instance of a domain entity model
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// entity id
        /// </summary>
        public virtual long Id { get; set; }
    }
}
