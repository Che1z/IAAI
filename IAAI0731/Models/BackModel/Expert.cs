using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.BackModel
{
    public class Expert : Abstract
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "照片路徑")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "照片名稱")]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "現職描述")]
        public string CurrentJobDescription { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "額外描述")]
        public string  AdditionalInfo{ get; set; }
    }
}