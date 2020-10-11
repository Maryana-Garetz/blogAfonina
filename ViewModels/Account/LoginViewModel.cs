using System.ComponentModel.DataAnnotations;

namespace blogAfonina.ViewModels.Account
{
    /// <summary>
    /// user authorization model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// login
        /// </summary>
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// whether to remember the account in the browser
        /// </summary>
        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }
}
