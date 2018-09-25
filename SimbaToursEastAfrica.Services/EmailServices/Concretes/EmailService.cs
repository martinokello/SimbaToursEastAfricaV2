using SimbaToursEastAfrica.Services.EmailServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Services.EmailServices.Concretes
{
    public class EmailService : IMailService
    {
        public string GetTemplate(EmailTemplate template)
        {
            throw new NotImplementedException();
        }

        public void SendEmail(MailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
