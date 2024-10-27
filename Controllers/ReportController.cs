using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.UniversalAccessibility.Drawing;
using SerhendKumbara.Data.Entity;
using SerhendKumbara.Services;

namespace SerhendKumbara.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly PlacemarkService _placemarkService;

    public ReportController(PlacemarkService placemarkService)
    {
        _placemarkService = placemarkService;
    }

    [HttpGet]
    public FileResult GetAllPlacemarks()
    {
        var list = _placemarkService.GetAllPlacemarks().Result.OrderBy(o => o.Status).ToList();

        PdfDocument pdf = new PdfDocument();
        pdf.Info.Title = "KUMBARA RAPOR";
        PdfPage pdfPage = pdf.AddPage();
        XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        XFont font = new XFont("Verdana", 10);
        var yPoint = 40;
        var xPoint = 50;
        int counter = 1;
        graph.DrawString("KUMBARA LİSTESİ", new XFont("Verdana", 20), XBrushes.Black, new XRect(0, 10, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
        graph.DrawLine(new XPen(XColors.LightGray), 0, yPoint, 600, yPoint);

        foreach (var item in list)
        {
            graph.DrawString(counter.ToString(), font, item.Status == PlacemarkStatus.Active ? XBrushes.Black : XBrushes.DarkRed, new XRect(xPoint - 20, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(item.Name, font, item.Status == PlacemarkStatus.Active ? XBrushes.Black : XBrushes.DarkRed, new XRect(xPoint + 5, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            //graph.DrawString(item.Status == PlacemarkStatus.Active ? "Aktif" : "Pasif", font, item.Status == PlacemarkStatus.Active ? XBrushes.Black : XBrushes.DarkRed, new XRect(xPoint + 210, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            graph.DrawLine(new XPen(XColors.LightGray), 0, yPoint + 13, 600, yPoint + 13);
            yPoint = yPoint + 15;
            if (counter % 100 == 0)
            {
                graph.DrawLine(new XPen(XColors.LightGray), 300, 40, 300, 789);
                pdfPage = pdf.AddPage();
                graph = XGraphics.FromPdfPage(pdfPage);
                yPoint = 40;
                xPoint = 50;

                graph.DrawLine(new XPen(XColors.LightGray), 0, yPoint, 600, yPoint);
            }
            else if (counter % 50 == 0)
            {
                yPoint = 40;
                xPoint = 330;
            }
            counter++;
        }
        string pdfFilename = AppDomain.CurrentDomain.BaseDirectory + "KUMBARA_RAPOR.pdf";
        pdf.Save(pdfFilename);

        var stream = new FileStream(pdfFilename, FileMode.Open);
        return File(stream, "application/pdf");
    }

}
