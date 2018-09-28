using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPAEventsPayPal;

namespace SimbaToursEastAfrica.PaymentGateWay
{
    public class PaymentGateway
    {
        private PayPalHandler _payPalPayments;

        public PaymentGateway(string baseUrl, string businessEmail, string successUrl, string cancelUrl, string notifyUrl,string buyerEmail) {
            _payPalPayments = new PayPalHandler(baseUrl, businessEmail, successUrl, cancelUrl, notifyUrl,buyerEmail);
        }

        public string MakePaymentByPaypal(List<Product> products)
        {
            var paypalUrlRedirect = _payPalPayments.RedirectToPayPal(products);
            return paypalUrlRedirect;
        }

        public void MakePaymentByCreditFacilities()
        {
            throw new NotImplementedException("Currently not supported!");
        }
    }
}
