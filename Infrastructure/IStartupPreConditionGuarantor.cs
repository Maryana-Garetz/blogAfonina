using System;

namespace blogAfonina.Infrastructure
{
    /// <summary>
    /// interface to ensure the integrity (possibility of correct operation) of the system at startup
    /// implementations of this interface can, for example, check the correspondence of the database schema and the data model
    /// or the fact of starting a particular service, server availability
    /// </summary>
    internal interface IStartupPreConditionGuarantor
    {
        /// <summary>
        /// ensuring launch capability 
        /// </summary>
        /// <param name="services">IoC container</param>
        /// <exception cref="StartupPreConditionException"> If this exception occurs, the system cannot be started </exception>
        void Ensure(IServiceProvider services);
    }
}
