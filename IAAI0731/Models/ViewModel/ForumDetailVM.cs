using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI0731.Models.ViewModel
{
    public class ForumDetailVM
    {
        public Forum Forum {  get; set; }
        public IPagedList<ForumReply> ForumReplies { get; set; } // 分頁的回覆列表
    }
}