using Foodi.Core.Services;
using Foodi.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;
using System.Security.Claims;

namespace Foodi.Controllers
{
    public class PdfController : Controller
    {
        private readonly PdfService _pdfService;
        private readonly IOrdersService _ordersService;

        public PdfController(PdfService pdfService , IOrdersService ordersService)
        {
            _pdfService = pdfService;
            _ordersService = ordersService;
        }

        
        public IActionResult GeneratePdf(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = _ordersService.GetUserOrderDetailsAsync(int.Parse(userId), id);

            if (result == null)
            {
                return BadRequest(new { Message = "there is no order with that id " });
            }

            
            var htmlContent = _pdfService.GetHTML(result);
            var pdfBytes = _pdfService.GeneratePdf(htmlContent);

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs", "TestPDF.pdf");
            //var directory = Path.GetDirectoryName(filePath);
            //if (!Directory.Exists(directory))
            //{
            //    Directory.CreateDirectory(directory);
            //}
            //System.IO.File.WriteAllBytes(filePath, pdfBytes);
            //return Ok(new { filePath = "/pdfs/TestPDF.pdf" });

            return File(pdfBytes, "application/pdf", "Invoice.pdf");

        }
    }

    
}
