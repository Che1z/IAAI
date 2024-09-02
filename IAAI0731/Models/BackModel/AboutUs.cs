using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class AboutUs : Abstract
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "標題")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }
    }
}