using System;
using System.Configuration;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ReportsService.Models;
using MongoDB.Driver;

namespace ReportsService.Reports
{
    public class ReportsManager
    {
        private static readonly ReportsManager _instance = new ReportsManager();

        private ReportsManager()
        {
            
        }
        
        /// <summary>
        /// Function in charge of connecting to MongoDB
        /// </summary>
        public void GenerateReport(string healthCenterId)
        {
            
        }
        
        /// <summary>
        /// Function in charge of exporting a pdf
        /// </summary>
        /// <param name="filename">
        /// Name of the file
        /// </param>
        /// <param name="destination">
        /// Destination of the dile
        /// </param>
        /// <param name="crReport">
        /// Report
        /// </param>
        public static void ExportPdf(string filename, string destination,  ReportClass crReport)
        {
            var dest = new DiskFileDestinationOptions {DiskFileName = GetPath(filename, destination)};

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

        public static ReportsManager Instance => _instance;
    }
}