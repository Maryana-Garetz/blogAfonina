using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using blogAfonina.Model;
using blogAfonina.Security;
using blogAfonina.ViewModels.Account;
using blogAfonina.DB;

namespace blogAfonina.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly BlogDbContext _blogDbContext;

        /// <summary>
        /// class constructor <see cref="AccountController"/>
        /// </summary>
        /// <param name="userManager"> user manager </param>
        /// <param name="blogDbContext"> database context </param>
        public AccountController(UserManager<User> userManager, BlogDbContext blogDbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        /// <summary>
        /// login form
        /// </summary>
        /// <param name="returnUrl"> navigation path after authorization </param>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // clear existing cookies for correct login
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// authorization
        /// </summary>
        /// <param name="signInManager"> authorization manager </param>
        /// <param name="model"> form input </param>
        /// <param name="returnUrl"> navigation path after authorization </param>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromServices] SignInManager<User> signInManager, LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.Login).Result;
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Check your username or password");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);

                if (result.IsLockedOut)
                    return RedirectToAction(nameof(Lockout));

                ModelState.AddModelError(string.Empty, "Wrong login or password");
                return View(model);
            }

            return View(model);
        }


        /// <summary>
        /// registration
        /// </summary>
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        /// <summary>
        /// registration
        /// </summary>
        /// <param name="model"> new user data </param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUserAsync(NewUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_userManager.Users.Any(x => x.UserName.ToLower() == model.UserName.ToLower()))
                ModelState.AddModelError("Username", "A user with the same username already exists!");

            if (_userManager.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                ModelState.AddModelError("Email", "This email is already used in the system");

            if (ModelState.ErrorCount > 0)
                return View(model);

            var profile = new Profile
            {
                FirstName = model.FirstName,
                Surname = model.Surname
            };

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Profile = profile
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, SecurityConstants.СustomerRole);
            _blogDbContext.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        /// <summary>
        /// log out
        /// </summary>
        /// <param name="signInManager"> authorization manager </param>
        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();


            return RedirectToAction("Index", "Blog");
        }

        /// <summary>
        /// returning the page if the user is blocked
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// password reset confirmation
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// access Denied Page
        /// </summary>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Blog");
            }
        }

        #endregion
    }
}
