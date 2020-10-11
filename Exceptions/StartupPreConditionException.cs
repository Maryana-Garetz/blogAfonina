using System;

namespace blogAfonina.Infrastructure
{
    /// <summary>
    /// exception caused by invalid application startup environment
    /// further execution is impossible
    /// </summary>
    public class StartupPreConditionException : Exception
    {
        /// <summary>
        /// instantiating a class <seealso cref="StartupPreConditionException"/>
        /// </summary>
        /// <param name="message"> message </param>
        public StartupPreConditionException(string message) : base(message)
        {
        }
    }
}
