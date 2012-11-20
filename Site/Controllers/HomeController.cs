using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Email(string address)
        {
            const string subject = "Someone is interested in Resounding Work";
            var headers = string.Empty;
            foreach (var header in Request.Headers.AllKeys) {
                headers += "\t" + header + ": " + Request.Headers[header] + "\n";
            }

            var body = string.Format("Email: {0},\n\nHeaders: \n{1}", address, headers);
            var fromAddress = new MailAddress("hodgkinson@gmail.com", "Cliffe Hodgkinson");
            var toAddress = new MailAddress("cliffe@resounding.ca", "Cliffe Hodgkinson");

            using (var smtp = new SmtpClient()) {
                using (var message = new MailMessage(fromAddress, toAddress) {
                    Subject = subject,
                    Body = body
                }) {
                    smtp.Send(message);
                }
            }

            return new EmptyResult();
        }
    }
}
