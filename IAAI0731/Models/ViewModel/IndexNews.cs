using IAAI0731.Models.BackModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.ViewModel
{
    public class IndexNews
    {
        public IEnumerable<News> NewsItems { get; set; }
        public IEnumerable<NewsPhoto> NewsPhotos { get; set; }
    }
}