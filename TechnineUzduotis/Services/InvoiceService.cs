using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechnineUzduotis.Models;

namespace TechnineUzduotis.Services
{
    public class InvoiceService : IInvoiceService
    {
        private static List<Country> DownloadEuCountries()
        {
            List<Country> countries = new List<Country>();
            using (WebClient web = new WebClient())
            {
                try
                {
                    var json = web.DownloadString("https://euvatrates.com/rates.json");
                    var deserialized = JObject.Parse(json);
                    var rates = deserialized["rates"].Children().ToList();
                    foreach (JToken rate in rates)
                    {
                        countries.Add(rate.Children().FirstOrDefault().ToObject<Country>());
                    }
                }
                catch
                {
                    throw new Exception("Failed to fetch and deserialize EU country data.");
                }
            }
            return countries;
        }
        public Invoice GetInvoice(Order order)
        {
            double tax = CalculateOrderTax(order);
            return new Invoice(order.Provider, order.Client, order.Price, tax);
        }
        public double CalculateOrderTax(Order order)
        {
            List<Country> _euCountries = DownloadEuCountries();

            var clientCountry = _euCountries.Where(x => string.Equals(x.Name, order.Client.Country, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (!order.Provider.TaxPayer
                || clientCountry == null
                || (order.Client.TaxPayer && !string.Equals(order.Client.Country, order.Provider.Country, StringComparison.OrdinalIgnoreCase)))
            {
                return 0;
            }
            return Math.Round(order.Price * clientCountry.Vat / 100, 2);
        }

        public void PrintToConsole(Invoice invoice)
        {
            try
            {
                Console.WriteLine("\nDate: {0}", invoice.Date.ToString("F"));
                Console.WriteLine("--------FROM--------");
                Console.WriteLine("Name: {0}", invoice.Provider.Name);
                Console.WriteLine("Country: {0}", invoice.Provider.Country);
                Console.WriteLine("---------TO---------");
                Console.WriteLine("Name: {0}", invoice.Client.Name);
                Console.WriteLine("Country: {0}", invoice.Client.Country);
                Console.WriteLine("Juridical?: {0}", invoice.Client.Juridical ? "Yes" : "No");
                Console.WriteLine("-------AMOUNT-------");
                Console.WriteLine("Subtotal: {0}", invoice.PriceBeforeTaxes);
                Console.WriteLine("Tax: {0}", invoice.Taxes);
                Console.WriteLine("Total: {0}\n", Math.Round(invoice.PriceAfterTaxes, 2));
            }
            catch
            {
                throw new Exception("Invoice is not fulfilled, could not print to console.");
            }
        }
    }
}
