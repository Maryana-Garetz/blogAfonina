using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace blogAfonina.Exceptions.Http
{
    /// <summary>
    /// not found (code 404)
    /// </summary>
    public class NotFoundException : HttpException
    {
        /// <summary>
        /// entity type
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// entity id
        /// </summary>
        public object EntityId { get; private set; }

        /// <summary>
        /// instantiating a class <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="entityType">error description object</param>
        /// <param name="entityId">entity ID</param>
        public NotFoundException(Type entityType, object entityId)
            : base(StatusCodes.Status404NotFound, $"{entityType.Name}:{JsonConvert.SerializeObject(entityId)}")
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        /// <summary>
        /// instantiating a class <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException(Type entityType)
            : base(StatusCodes.Status404NotFound, $"Absent {entityType.Name}")
        {
            EntityType = entityType;
        }

        /// <summary>
        /// instantiating a class <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException(string reasonPhrase)
            : base(StatusCodes.Status404NotFound, reasonPhrase)
        { }
    }
}
