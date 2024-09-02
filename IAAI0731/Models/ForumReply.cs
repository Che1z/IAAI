using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    public class ForumReply:Abstract
    {

        
        public int PostNumber { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "回應內容")]
        public string ReplyContent { get; set; }

        [ForeignKey("PostNumber")]
        // 導航屬性
        public virtual Forum ForumFK { get; set; }
    }
}