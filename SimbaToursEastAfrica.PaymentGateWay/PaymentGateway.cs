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

        public PaymentGateway(string baseUrl, string businessEmail, string successUrl, string cancelUrl, string notifyUrl,string buyerEmail,HttpContext context) {
            _payPalPayments = new PayPalHandler(baseUrl, businessEmail, successUrl, cancelUrl, notifyUrl,buyerEmail);
            _payPalPayments._HttpContext = context;
        }

        public void MakePaymentByPaypal(List<dynamic> products)
        {
            _payPalPayments.RedirectToPayPal(products);
        }

        public void MakePaymentByCreditFacilities()
        {
            throw new NotImplementedException("Currently not supported!");
        }
    }
}
