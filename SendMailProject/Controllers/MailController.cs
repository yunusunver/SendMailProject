using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SendMailProject.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendMailToUser(string mesaj)
        {
            var result = false;
            result = SendEmail("kimegonderilecek@gmail.com","Mail Başlığı", mesaj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmail(string toEmail,string subject,string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword= System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client=new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials=new NetworkCredential(senderEmail,senderPassword);

                MailMessage mailMessage=new MailMessage(senderEmail,toEmail,subject,emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}