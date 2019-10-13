using FIT5032_Week08A.Models;
using FIT5032_Week08A.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Week08A.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            // Please comment out these codes once you have registered your API key.
            //EmailSender es = new EmailSender();
            //es.RegisterAPIKey();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]


        
        [ValidateInput(false)]
        public ActionResult Send_Email(SendEmailViewModel sendEmail)
        {
            string path = "~/Content/Upload/" + Guid.NewGuid() + "/";
            if (ModelState.IsValid)
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gzhu0004@student.monash.edu");
                mailMessage.To.Add(new MailAddress(sendEmail.to));
              

                mailMessage.Subject = sendEmail.subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = sendEmail.body;

                if (sendEmail.file[0] != null)
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                    foreach (HttpPostedFileBase file in sendEmail.file)
                    {
                        string filePath = Server.MapPath(path + file.FileName);
                        file.SaveAs(filePath);

                        mailMessage.Attachments.Add(new Attachment(Server.MapPath(path + file.FileName)));
                    }
                }

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("gzhu0004@student.monash.edu", "I like Miku");
                client.Host = "smtp.gmail.com";

                try
                {
                    client.Send(mailMessage);
                    ViewBag.Result = "Mail Sent";
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }

            return View();
        }
        

    }
}