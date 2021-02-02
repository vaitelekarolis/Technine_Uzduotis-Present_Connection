using System;

namespace TechnineUzduotis.Models
{
    public class Invoice
    {
        public Provider Provider { get; private set; }
        public Client Client { get; private set; }
        public double PriceBeforeTaxes { get; private set; }
        public double Taxes { get; private set; }
        public double PriceAfterTaxes { get; private set; }
        public DateTime Date { get; private set; }
        public Invoice(Provider provider, Client client, double priceBeforeTaxes, double taxes)
        {
            Provider = provider;
            Client = client;
            PriceBeforeTaxes = priceBeforeTaxes;
            Taxes = taxes;
            PriceAfterTaxes = priceBeforeTaxes + taxes;
            Date = DateTime.Now;
        }
    }
}
