using System;

namespace blogAfonina.Exceptions.Http
{
    /// <summary>
    /// error corresponding to HTTP protocol error
    /// </summary>
    public abstract class HttpException : Exception
    {
        /// <summary>
        /// instantiating a class <see cref="HttpException"/>
        /// </summary>
        /// <param name="statusCode">error code</param>
        /// <param name="errorObject">error description object</param>
        protected HttpException(int statusCode, object errorObject)
        {
            StatusCode = statusCode;
            ErrorObject = errorObject;
        }

        /// <summary>
        /// instantiating a class <see cref="HttpException"/>
        /// </summary>
        /// <param name="statusCode">error code</param>
        /// <param name="reasonPhrase">error description object</param>
        protected HttpException(int statusCode, string reasonPhrase)
            : base(reasonPhrase)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// error code <see cref="Microsoft.AspNetCore.Http.StatusCodes"/>
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// error description object
        /// </summary>
        public object ErrorObject { get; }
    }
}
