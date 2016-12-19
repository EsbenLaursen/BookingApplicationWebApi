using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace BookingApplicationWebApi.Controllers
{
    
    public class EmailController : ApiController
    {
      
        [ResponseType(typeof(void))]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Contact(EmailFromModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("sand32hus@gmail.com"));  // replace with valid value 
                message.From = new MailAddress(model.FromEmail);  // replace with valid value
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        // UserName = "ag-mam@live.dk",  // replace with valid value
                        // Password = "1april1427"  // replace with valid value

                         UserName = "sand32hus@gmail.com",
                         Password = "12a12a12a"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return Ok();
                }
            }
            return BadRequest("Not sent");
        }

      



    }
}
