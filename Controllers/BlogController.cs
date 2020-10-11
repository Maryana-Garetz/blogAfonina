using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using blogAfonina.DB;
using blogAfonina.Model;
using blogAfonina.Security.Extensions;
using blogAfonina.ViewModels.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace blogAfonina.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogController(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        public IActionResult Index()
        {
            var posts = _blogDbContext.BlogPosts
                .Select(x => new PostItemViewModel
                {
                    Author = x.Author.FullName,
                    Created = x.Created,
                    Data = x.Data,
                    Title = x.Title
                }).OrderByDescending(x => x.Created);
            
            return View(posts);
        }

        /// <summary>
        /// post addition
        /// </summary>
        /// <returns>posts of a user</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(NewPostViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = this.GetAuthorizedUser();

            var post = new BlogPost
            {
                Created = DateTime.Now,
                Data = model.Data,
                Title = model.Title,
                Author = user.Profile
            };

            _blogDbContext.BlogPosts.Add(post);

            _blogDbContext.SaveChanges();

            return View();
        }
    }
}
