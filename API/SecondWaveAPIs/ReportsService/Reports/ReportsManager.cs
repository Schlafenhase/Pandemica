using System;
using System.Collections.Generic;
using System.Configuration;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using Syncfusion.Pdf.Grid;
using System.Drawing;
using System.Net.Http;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ReportsService.Models;
using ReportsService.Source.Util;

namespace ReportsService.Reports
{
    public class ReportsManager
    {
        public static ReportsManager Instance { get; } = new ReportsManager();
        private readonly string _subFolder = ConfigurationManager.AppSettings["PdfPath"];
        private readonly string _reportName = ConfigurationManager.AppSettings["ReportName"];
        private readonly string _reportXml = ConfigurationManager.AppSettings["ReportXml"];
        private readonly string _reportTittle = ConfigurationManager.AppSettings["ReportTittle"];
        
        private ReportsManager() { }

        /// <summary>
        /// Function in charge of creating the report a pdf
        /// </summary>
        /// <param name="reports">
        /// List of reports from Mongo
        /// </param>
        public StreamContent GenerateReport(List<Feedback> reports)
        {
            XmlGenerator.GenerateXml(reports, _reportXml, _subFolder);
            ExportPdf(new FeedbackReport());
            return GetReportPdf();
        }
        
        /// <summary>
        /// Function in charge of creating the report a pdf manually using Syncfusion
        /// </summary>
        /// <param name="reports">
        /// List of reports from Mongo
        /// </param>
        public StreamContent ForcePdf(IEnumerable<Feedback> reports)
        {
            //Create a new PDF document.
            var doc = new PdfDocument();

            //Add a page.
            var page = doc.Pages.Add();
            var graphics = page.Graphics;
            
            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            graphics.DrawString(_reportTittle, font, PdfBrushes.Black, new PointF(150, 40));
            
            //Create a PdfGrid.
            var pdfGrid = new PdfGrid();

            //Add list to IEnumerable
            IEnumerable<object> dataTable = reports;

            //Assign data source.
            pdfGrid.DataSource = dataTable;

            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
            
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 110));
            
            //Creating the stream object
            var stream = new MemoryStream();

            //Save the document as stream
            doc.Save(stream);

            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;

            //Close the document.
            doc.Close(true);

            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return new StreamContent(stream);
        }
        
        /// <summary>
        /// Function in charge of exporting a pdf
        /// </summary>
        /// <param name="crReport">
        /// Report
        /// </param>
        private void ExportPdf(ReportClass crReport)
        {
            var dest = new DiskFileDestinationOptions {DiskFileName = GetPath(_reportName, _subFolder)};

            var formatOpt = new PdfFormatOptions
            {
                FirstPageNumber = 0, LastPageNumber = 0, UsePageRange = false, CreateBookmarksFromGroupTree = false
            };

            var ex = new ExportOptions
            {
                ExportDestinationType = ExportDestinationType.DiskFile,
                ExportDestinationOptions = dest,
                ExportFormatType = ExportFormatType.PortableDocFormat,
                ExportFormatOptions = formatOpt
            };

            crReport.Export(ex);
        }
        
        /// <summary>
        /// Function in charge of finding the pdf
        /// </summary>
        private StreamContent GetReportPdf()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _subFolder, _reportName);
            var pdf = File.OpenRead(path);
            return new StreamContent(pdf);
        }

        /// <summary>
        /// Function in charge of getting the path of a report
        /// </summary>
        /// <param name="filename">
        /// Filename
        /// </param>
        /// <param name="destination">
        /// Destination
        /// </param>
        /// <returns>
        /// String with the path
        /// </returns>
        private static string GetPath(string filename, string destination)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, destination, filename);
        }
    }
}