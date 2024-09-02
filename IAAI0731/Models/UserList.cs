using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    public class UserList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "會員類別")]
        public MembershipType MembershipType { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "地址")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "信箱")]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        
        [Display(Name = "國際會籍")]
        public bool IsInternationalMember { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "現職單位")]
        [MaxLength(50)]
        public string CurrentPosition { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "職稱")]
        [MaxLength(50)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "最高學歷")]
        public HighestEducation HighestEducation { get; set; }
    }
}