using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class NewsPhoto : Abstract
    {
        [Required]
        public string FilePath { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        public int NewsId { get; set; }  // 外鍵


        [ForeignKey("NewsId")]
        // 導航屬性
        public virtual News NewsFK { get; set; }

    }
}