using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.ViewModel
{
    public class ContactForm
    {
        [Required(ErrorMessage = "姓名必填")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "性別必填")]
        [Display(Name = "性別")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "聯絡電話必填")]
        [Display(Name = "聯絡電話")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-mail必填")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "詢問內容")]
        public string Message { get; set; }

        [Required(ErrorMessage = "驗證碼必填")]
        [Display(Name = "驗證碼")]
        public string CaptchaCode { get; set; }
    }
}