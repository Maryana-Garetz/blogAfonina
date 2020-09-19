using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using blogAfonina.Models;
using Microsoft.Extensions.Options;
using blogAfonina.Options;

namespace blogAfonina.Controllers
{
    /// <summary>
    /// controller for working with main page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;
        /// <summary>
        /// information about the server
        /// </summary>
        private readonly IOptions<ServerOptions> _serverOptions;

        /// <summary>
        /// class constructor <see cref="HomeController"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serverOptions">information about the server</param>
        public HomeController(ILogger<HomeController> logger, IOptions<ServerOptions> serverOptions)
        {
            _logger = logger;
            _serverOptions = serverOptions;
        }

        /// <summary>
        /// showing the main page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// showing the privacy page
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// showing the blog page
        /// </summary>
        /// <returns></returns>
        public IActionResult Blog()
        {
            return new BlogController().Index();
        }

        /// <summary>
        /// showing error
        /// when a page isn't found
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
