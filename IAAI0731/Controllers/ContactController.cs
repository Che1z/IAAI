using IAAI0731.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using IAAI0731.Filters;

namespace IAAI0731.Controllers
{
    [SidebarFilter]
    [FrontPageDirectionFilter]

    public class ContactController : Controller
    {
        // GET: ContactForm
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactForm(ContactForm contactForm)
        {
            if (contactForm == null || !ModelState.IsValid)
            {
                ViewBag.Require = "請填寫所有必填項目";
                return View();
            }
            else if (contactForm.CaptchaCode != "FRJN2" && !string.IsNullOrEmpty(contactForm.CaptchaCode))
            {
                ViewBag.Error = "驗證碼錯誤";
                return View();
            }
            else
            {

                MailMessage mail = new MailMessage("yacht20240423@gmail.com", contactForm.Email);
                // 添加 CC 收件人（包含自己）
                mail.CC.Add("yacht20240423@gmail.com");

                mail.Subject = "IAAI協會-聯絡資訊";
                mail.IsBodyHtml = true;

                string salutation = contactForm.Gender.ToString() == "男" ? "先生" : "女士";

                string htmlBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }}
        h1 {{
            color: #0066cc;
        }}
        .message {{
            background-color: #f9f9f9;
            padding: 15px;
            border-left: 4px solid #0066cc;
            margin: 20px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-style: italic;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>IAAI協會 - 聯絡回覆</h1>
        <p>{contactForm.Name}{salutation}，您好：</p>
        <p>感謝您的聯繫。我們已收到您的詢問，內容如下：</p>
        <div class='message'>
            {contactForm.Message}
        </div>
        <p>我們將盡快處理您的請求並回覆您。</p>
        <p class='footer'>此郵件為自動發送，請勿直接回覆。如有任何其他問題，請隨時與我們聯繫。</p>
    </div>
</body>
</html>";

                mail.Body = htmlBody;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nw = new NetworkCredential("yacht20240423@gmail.com", "bgtr aumj bnoq bjkd");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nw;
                smtp.Send(mail);
                ViewBag.Error = "已送出申請";

                return View(); ;
            }
        }
    }
}