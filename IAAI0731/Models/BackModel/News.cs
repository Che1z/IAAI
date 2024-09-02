using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class News : Abstract
    {
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(200)]
        [Display(Name = "標題")]

        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(200)]
        [Display(Name = "描述")]

        public string Description { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內文")]
        public string Content { get; set; }  // 用於存儲 CKEditor 內容

        [Display(Name = "會員編號")]
        public int? MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Membership MembershipsFK { get; set; }

        // 導航屬性
        public virtual ICollection<NewsPhoto> Photos { get; set; }
    }
}