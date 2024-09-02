using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    public class ServiceExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserList User { get; set; }

        [MaxLength(100)]
        [Display(Name = "服務單位")]

        public string ServiceCompany { get; set; }

        [MaxLength(100)]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }

        [Display(Name = "起-年")]
        public int StartYear { get; set; }

        [Display(Name = "起-月")]
        public int StartMonth { get; set; }
        [Display(Name = "迄-年")]
        public int? EndYear { get; set; }
        [Display(Name = "迄-月")]
        public int? EndMonth { get; set; }
    }
}