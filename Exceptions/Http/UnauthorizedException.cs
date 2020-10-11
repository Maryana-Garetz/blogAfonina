using Microsoft.AspNetCore.Http;

namespace blogAfonina.Exceptions.Http
{
    /// <summary>
    /// authorisation Error (code 401)
    /// </summary>
    public class UnauthorizedException : HttpException
    {
        /// <summary>
        /// instantiating a class <see cref="UnauthorizedException"/>
        /// </summary>
        /// <param name="errorObject">error description object</param>
        public UnauthorizedException(object errorObject)
            : base(StatusCodes.Status401Unauthorized, errorObject)
        { }

        /// <summary>
        /// instantiating a class <see cref="UnauthorizedException"/>
        /// </summary>
        public UnauthorizedException()
            : this(null)
        { }
    }
}
