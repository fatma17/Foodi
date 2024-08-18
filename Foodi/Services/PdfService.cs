using DinkToPdf;
using DinkToPdf.Contracts;
using Foodi.Core.ViewModels;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Text;

namespace Foodi.Services
{
     // <link href = ""https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"" rel=""stylesheet"">
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }
         
        public String GetHTML(OrderDetailsViewModel OrderDetails)
        {

            var sb = new StringBuilder();
            sb.AppendFormat(@"<html>
                        <head>
                           <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css'>
                        </head>
                        <body>
                          <div class='container'>
                           <h1 class='text-center'>Invoice</h1>
                           <div class='row mt-3 border'>
                             <div style=""background-color:#f9fafd; "">
                                <div style=""margin: 0px 0px 15px 15px;"">
                                    <h4 class=""mb-0 mt-1"">Order Details : {0} </h4>
                                    <p class=""fs-10 mb-2 "">{1}</p>
                                    <div class=""text-muted"">
                                        <div class=""mb-1""> <strong>Status : {2} </strong> </div>
                                        <div class=""mb-1""> <strong>Payment Method : {3} </strong> </div>
                                        <div class=""mb-1""> <strong>Total : {4} $</strong> </div>
                                    </div>
                                 </div>
                             </div>
                            <div class=""p-4"">
                                <table class=""table mb-4"">
                                        <tr style='text-align: left !important;'>
                                            <th>Items</th>
                                            <th>Quantity</th>
                                            <th>UnitPrice</th>
                                            <th>Total</th>
                                        </tr>
                                    <tbody>", OrderDetails.Id, OrderDetails.OrderDate,OrderDetails.Status,OrderDetails.PaymentMethod,OrderDetails.Total
            );

            foreach (var item in OrderDetails.OrderItems)
            {
                sb.AppendFormat(@"<tr> <td>{0}</td> <td>{1}</td> <td>{2}$</td> <td>{3}$</td> </tr>", 
                                item.ProductName, item.Quantity, item.Price, item.Quantity * item.Price);
            }

            sb.Append(@"</tbody> </table> </div> </div> </div> </body> </html>");                     
            return sb.ToString();
        }

       

        public byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Invoice"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                //WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "This is a footer" }

            };
       

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(pdf);
        }
    }
}
