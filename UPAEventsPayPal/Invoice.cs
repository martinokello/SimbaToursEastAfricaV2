using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace UPAEventsPayPal
{
    public class Invoice
    {
        private int invoiceNo;
        private List<dynamic> products;
        private string buyerEmail;
        private decimal amount;
        private decimal amountVAT;
        private const int beginGenerate = 1003;
        static private Random randomGenerator = new Random(DateTime.Now.Millisecond);

        public Invoice(List<dynamic> products, decimal amount, string buyerEmail)
        {
            this.products = products;
            this.amount = amount;
            this.buyerEmail = buyerEmail;
        }

        public long GenerateUniqueInvoiceNo()
        {
            invoiceNo = randomGenerator.Next(beginGenerate);
            return (long)invoiceNo;
        }

        public int InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        public string BuyerEmail
        {
            get { return buyerEmail; }
            set { buyerEmail = value; }
        }


        public List<dynamic> Products
        {
            get { return products; }
            set { products = value; }
        }

        public decimal Ammount
        {
            get {
                decimal amount = 0.00M;
                foreach(var prod in Products)
                {
                    amount += prod.Amount;
                }
                return amount;
            }
            set { amount = value; }
        }

        public decimal AmountVAT
        {
            get { return amountVAT; }
            set { amountVAT = value; }
        }
        
        public IEnumerator<dynamic> GetEnumerator()
        {
            return products.GetEnumerator();
        }
    }
}
