using System.ComponentModel.DataAnnotations;

namespace blogAfonina.ViewModels.Blog
{
    /// <summary>
    /// post model addition
    /// </summary>
    public class NewPostViewModel
    {

        /// <summary>
        /// title
        /// </summary>
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// post data
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Data")]
        public string Data { get; set; }

    }
}
