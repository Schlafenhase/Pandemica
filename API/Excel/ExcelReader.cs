using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using ExcelLibrary.SpreadSheet;

namespace API.Excel
{
    public class ExcelReader
    {
        public static void Main()
        {
            OpenXls("Patients.xls");
        }
        
        public static Worksheet OpenXls(string fileName)
        {
            // open xls file Workbook
            const string subFolder = "Excel";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subFolder, fileName);
            var book = Workbook.Load(path);
            var sheet = book.Worksheets[0];

            return sheet;
        }
    }
}