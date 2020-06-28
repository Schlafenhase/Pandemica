using System;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace API.Reports
{
    public static class ReportGenerator
    {
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

        private static string GetPath(string filename, string destination)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, destination, filename);
        }
    }
}