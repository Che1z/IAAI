using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI0731.Models
{
    public class EnumList
    {
    }
    public enum Gender
    {
        男,
        女
    }

    public enum MembershipType
    {
        正式會員,
        準會員,
        個人贊助會員,
        學生會員
    }

    // 設定為0，代表為預設值
    public enum HighestEducation
    {
        高中,
        學士 = 0,
        碩士,
        博士
    }

}