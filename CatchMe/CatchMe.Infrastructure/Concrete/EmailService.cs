using System.Net;
using System.Net.Mail;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Models;

namespace CatchMe.Infrastructure.Concrete
{
    public class EmailService : IEmailService
    {        
        #region IEmailService

        public void Send(EmailMessage messageModel)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {                
                MailAddress mailAddress = new MailAddress(((NetworkCredential)smtpClient.Credentials).UserName, "CatchMe");
                MailMessage mailMessage = new MailMessage(mailAddress.Address,
                    messageModel.Destination,
                    messageModel.Subject,
                    messageModel.Message);

                smtpClient.Send(mailMessage);
            }
        }

        #endregion
    }
}
