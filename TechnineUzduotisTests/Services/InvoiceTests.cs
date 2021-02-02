using NSubstitute;
using TechnineUzduotis.Models;
using TechnineUzduotis.Services;
using Xunit;

namespace TechnineUzduotisTests
{
    public class InvoiceTests
    {
        private readonly IInvoiceService _invoiceService = Substitute.For<InvoiceService>();

        [Theory]
        // Kai paslaugu tiekejas nera PVM moketojas - PVM mokestis nuo uzsakymo sumos nera skaiciuojamas.
        [InlineData("Karolis Vaitele", false, "Lithuania", false, "Present Connection", false, "Netherlands", 1000, 0)]
        // Kai paslaugu tiekejas yra PVM moketojas, o klientas Uz EU (Europos sajungos) ribu - PVM taikomas 0%
        [InlineData("Karolis Vaitele", false, "Canada", false, "Present Connection", true, "Netherlands", 1000, 0)]
        /* Kai paslaugu tiekejas yra PVM moketojas, o klientas gyvena EU, yra ne PVM moketojas, 
           bet gyvena skirtingoje salyje nei paslaugu tiekejas, taikomas PVM x%, kur x - toje salyje taikomas PVM procentas. */
        [InlineData("Karolis Vaitele", false, "Slovakia", false, "Present Connection", true, "Netherlands", 1000, 200)]
        /* Kai paslaugu tiekejas yra PVM moketojas, o klientas gyvena EU, yra PVM moketojas,
           bet gyvena skirtingoje salyse nei paslaugu tiekejas. Taikomas 0% pagal atvirkstini apmokestinima. */
        [InlineData("Karolis Vaitele", true, "Lithuania", false, "Present Connection", true, "Netherlands", 1000, 0)]
        // Kai paslaugu tiekejas yra PVM moketojas, o klientas nera PVM moketojas, kai uzsakovas ir paslaugu tiekejas gyvena toje pacioje salyje - visada taikomas PVM
        [InlineData("Karolis Vaitele", false, "Lithuania", false, "Present Connection", true, "Lithuania", 1000, 210)]
        // Kai paslaugu tiekejas yra PVM moketojas, o klientas yra PVM moketojas, kai uzsakovas ir paslaugu tiekejas gyvena toje pacioje salyje - visada taikomas PVM
        [InlineData("Karolis Vaitele", true, "Lithuania", false, "Present Connection", true, "Lithuania", 1000, 210)]
        public void TaxesAreCalculatedCorrectly(string clientName, bool clientTaxPayer, string clientCountry, bool clientJuridical,
            string providerName, bool providerTaxPayer, string providerCountry, double price, double expectedResult)
        {
            var client = new Client(clientName, clientTaxPayer, clientCountry, clientJuridical);
            var provider = new Provider(providerName, providerTaxPayer, providerCountry);
            var order = new Order(client, provider, price);

            var result = _invoiceService.CalculateOrderTax(order);

            Assert.Equal(expectedResult, result);
        }
    }
}
