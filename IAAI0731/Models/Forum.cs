using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    [Table("Forum")]
    public class Forum : Abstract
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }

        // 導航屬性
        public virtual ICollection<ForumReply> ForumReplies { get; set; }
    }
}