using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class EmailConfig : IEmailConfig
    {
        public bool SendEmail(EmailB emailBody)
        {
            var status = true;
         
            MailMessage mm = new MailMessage("firoz.sabath@cud.ac.ae", emailBody.Emailto); // Message Body Initialisation.
            mm.Subject = emailBody.Subject; //Adding Subject To the Mail/
                                        // mm.Body = body;       // Adding the Message Body.
            mm.Body = emailBody.Emailbody;       // Adding the Message Body.            

            foreach(var cc in emailBody.Emailcc)
            {
                mm.Bcc.Add(cc);
            }
            // mm.Attachments.Add(new Attachment(path)); //The Code is used to Attach the pdf to the mail.
            mm.IsBodyHtml = true;
            mm.BodyEncoding = Encoding.UTF8;
            SmtpClient smtp = new SmtpClient("emailServer", 587); // Initialising the SMTP
                                                                //smtp.Host = "CUDSMTP01.cud.ae";            
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "emailUser";
            NetworkCred.Password = "emailPassword";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            try
            {
                smtp.Send(mm); // Forwarding the Created mail to the recepient.
                
            }
            catch (Exception ex)
            {
                //_logger.LogError($"{ex.Message} - {ex.InnerException}");
                return false;
            }

            return status;
        }
    }
}
