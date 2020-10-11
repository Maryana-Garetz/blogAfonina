using blogAfonina.Exceptions.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using blogAfonina.Model;

namespace blogAfonina.Security.Extensions
{
    /// <summary>
    /// security Extension Methods Used in ControllersМетоды расширения безопасности, применяемые в контроллерах
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// returns the ID of a successfully authorized user <see cref="User"/>
        /// </summary>
        /// <param name="controller">controller</param>
        /// <returns>ID of a successfully authorized user <see cref="User"/></returns>
        private static long GetAuthorizedUserId(this ControllerBase controller)
        {
            var claims = controller.HttpContext.User.Claims;
            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return claim != null ? Int32.Parse(claim.Value) : 0;
        }

        /// <summary>
        /// returns a successfully authorized user <see cref="User"/>
        /// </summary>
        /// <param name="controller">controller</param>
        /// <returns>a successfully authorized user <see cref="User"/></returns>
        private static User TryGetAuthorizedUser(this ControllerBase controller)
        {
            var userId = controller.GetAuthorizedUserId();
            var userManager = controller.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            return userManager.Users.Include(x => x.Profile).FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// returns a successfully authorized user <see cref="User"/>
        /// </summary>
        /// <param name="controller">controller</param>
        /// <exception cref="UnauthorizedException">failed to get authorized user</exception>
        /// <returns>successfully logged in user <see cref="User"/></returns>
        public static User GetAuthorizedUser(this ControllerBase controller)
        {
            return controller.TryGetAuthorizedUser() ?? throw new UnauthorizedException();
        }

        /// <summary>
        /// returns the ip address of the remote client
        /// </summary>
        /// <param name="controller">controller</param>
        /// <returns>client ip address</returns>
        public static string GetUserRemoteIpAddress(this ControllerBase controller)
        {
            return controller.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
