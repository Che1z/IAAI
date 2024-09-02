using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class Knowledge : Abstract
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "檔案位置")]

        public string FilePath { get; set; }

        [Display(Name = "會員編號")]
        public int? MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Membership MembershipsFK { get; set; }
    }
}