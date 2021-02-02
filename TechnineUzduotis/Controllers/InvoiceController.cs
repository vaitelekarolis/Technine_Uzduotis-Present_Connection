using Microsoft.AspNetCore.Mvc;
using TechnineUzduotis.Models;
using TechnineUzduotis.Services;

namespace TechnineUzduotis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<Invoice> GetInvoice([FromQuery] Order order)
        {
            Invoice invoice = _invoiceService.GetInvoice(order);
            _invoiceService.PrintToConsole(invoice); // Just because front-end is not implemented.
            return Ok(invoice);
        }
    }
}
