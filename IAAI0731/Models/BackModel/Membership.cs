using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class Membership : Abstract
    {
       

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "密碼")]

        public string Password { get; set; }

        [Display(Name = "密碼鹽")]
        public string Salt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "性別")]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "信箱")]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Display(Name = "權限")]
        [MaxLength(500)]
        public string NewPermission { get; set; }




    }
}