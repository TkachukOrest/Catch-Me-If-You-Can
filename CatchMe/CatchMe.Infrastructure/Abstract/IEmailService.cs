using CatchMe.Infrastructure.Models;

namespace CatchMe.Infrastructure.Abstract
{
    public interface IEmailService
    {
        void Send(EmailMessage messageModel);
    }
}
