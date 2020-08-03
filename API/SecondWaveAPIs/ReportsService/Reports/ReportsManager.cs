using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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