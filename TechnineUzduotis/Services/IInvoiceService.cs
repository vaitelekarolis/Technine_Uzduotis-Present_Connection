using TechnineUzduotis.Models;

namespace TechnineUzduotis.Services
{
    public interface IInvoiceService
    {
        public Invoice GetInvoice(Order order);
        public double CalculateOrderTax(Order order);
        public void PrintToConsole(Invoice invoice);
    }
}
