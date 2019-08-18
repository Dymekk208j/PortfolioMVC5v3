using PortfolioMVC5v3.Utilities;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string email, string subject, string message, string name)
        {
            SmtpClient client = new SmtpClient("smtp.webio.pl")
            {
                Credentials = new NetworkCredential("Portfolio@damiandziura.pl", "Damian131"),
                Port = 465,
                EnableSsl = true,
                
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(email)
            };
            mailMessage.To.Add("Kontakt@DamianDziura.pl");
            mailMessage.Subject = $"{subject} - {name}";
            mailMessage.Body = message;

            try
            {
                client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new HttpStatusCodeResult(200);

        }
    }
}