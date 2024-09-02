using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.ViewModel
{
    public class UserRegistrationVM
    {
        public UserList User { get; set; }
        public ServiceExperience ServiceExperience { get; set; }

        [Required(ErrorMessage = "驗證碼必填")]
        [Display(Name = "驗證碼")]

        public string Captcha { get; set; }


        [Display(Name = "相關年資合計")]
        public int TotalYearsOfExperience { get; set; }
        public int TotalMonthsOfExperience { get; set; }
    }
}