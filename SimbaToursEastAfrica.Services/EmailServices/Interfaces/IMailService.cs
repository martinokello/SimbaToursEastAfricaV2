using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Services.EmailServices.Interfaces
{
    public enum EmailTemplate { InviteMessage, HotelBookingMessage, PackageBookingMessage, InvoiceMessage, WelcomeMessage, OffersMessage}
    public interface IMailService
    {
        void SendEmail(MailMessage message);
        string GetTemplate(EmailTemplate template);
    }
}
