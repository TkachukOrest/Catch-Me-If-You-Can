using System.Threading.Tasks;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.ServiceLocation;

namespace CatchMe.Security.Services
{
    public class IdentityEmailService: IIdentityMessageService
    {
        private readonly IEmailService _emailService;

        public IdentityEmailService()
        {
            _emailService = ServiceLocator.Current.GetInstance<IEmailService>();
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Factory.StartNew(() =>
            {
                var emailMessage = new EmailMessage();
                emailMessage.Destination = message.Destination;
                emailMessage.Subject = message.Subject;
                emailMessage.Message = message.Body;   
                             
                _emailService.Send(emailMessage);
            });
        }
    }
}
