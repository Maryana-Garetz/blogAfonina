using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blogAfonina.ViewModels.Blog
{
    public class PostItemViewModel
    {

        /// <summary>
        /// author
        /// </summary>
        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        /// <summary>
        /// creation date
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created")]
        public System.DateTime Created { get; set; }

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
