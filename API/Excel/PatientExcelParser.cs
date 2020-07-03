﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using API.Source.Entities;
using ExcelLibrary.SpreadSheet;
using String = API.Util.String;

namespace API.Excel
{
    public static class PatientExcelParser
    {
        public static IEnumerable<Patient> LoadPatients(byte[] buffer) => ReadData(ReadBuffer(buffer));
        
        public static IEnumerable<Patient> LoadPatients(string filepath) => ReadData(OpenXls(filepath));
        
        
        // Loads xls from memory
        private static Worksheet ReadBuffer(byte[] buffer)
        {
            var memStream = new MemoryStream(buffer);
            var book = Workbook.Load(memStream);
            var sheet = book.Worksheets[0];
            return sheet;
        }

        // Open xls file from path
        private static Worksheet OpenXls(string fileName)
        {
            const string subFolder = "Excel";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subFolder, fileName);
            var book = Workbook.Load(path);
            var sheet = book.Worksheets[0];
            return sheet;
        }
        
        private static IEnumerable<Patient> ReadData(Worksheet data)
        {
            var firstRow = data.Cells.GetRow(data.Cells.FirstRowIndex);
            var patients = new List<Patient>();
            // Starts reading from the second row
            for (var rowIndex = data.Cells.FirstRowIndex + 1; rowIndex <= data.Cells.LastRowIndex; rowIndex++)
            {
                var row = data.Cells.GetRow(rowIndex);
                var patient = ParsePatient(row, firstRow);
                patients.Add(patient);
            }

            return patients;
        }

        private static Patient ParsePatient(Row row, Row firstRow)
        {
            var patient = new Patient();
            // Reads each cell in the row
            for (var colIndex = row.FirstColIndex; colIndex <= row.LastColIndex; colIndex++)
            {
                var cell = row.GetCell(colIndex);
                var colName = firstRow.GetCell(colIndex).StringValue.ToLower();
                
                // Sets each property of the patient
                switch (colName)
                {
                    case "ssn":
                        patient.ssn = ParseSsn(cell.StringValue);
                        break;
                    case "name":
                        var (firstName, lastName) = ParseName(cell.StringValue);
                        patient.firstName = firstName;
                        patient.lastName = lastName;
                        break;
                    case "firstname":
                        patient.firstName = cell.StringValue;
                        break;
                    case "lastname":
                        patient.lastName = cell.StringValue;
                        break;
                    case "country":
                        patient.country = cell.StringValue;
                        break;
                    case "nationality":
                        patient.nationality = cell.StringValue;
                        break;
                    case "birthdate":
                        patient.birthDate = ParseBirthDate(cell).ToString();
                        break;
                    case "state":
                        patient.state = cell.StringValue;
                        break;
                    default:
                        Console.Error.WriteLine("Patient column undefined");
                        break;
                }
            }
            return patient;
        }

        // Removes empty spaces and converts to int
        private static string ParseSsn(string ssn)
        {
            var charsToRemove = new[] {" ", "-", "_"};
            var newSsn = String.RemoveChars(charsToRemove, ssn);
            return newSsn;
        }

        private static (string, string) ParseName(string name)
        {
            // Removes all extra characters and split by spaces
            var charsToRemove = new[] {",", "."};
            name = String.RemoveChars(charsToRemove, name);
            var names = name.Split(' ');
            
            // Removes the Middle Name
            names = names.Where(value => value.Length > 1).ToArray();
            
            // The first of names is the lastname
            var lastName = names.First();
            // The last of names is the firstname
            var firstName = names.Last();
            
            return (firstName, lastName);
        }

        private static DateTime ParseBirthDate(Cell cell)
        {
            DateTime date;
            // Standard date format MM-dd-yyyy
            try
            {
                date = cell.DateTimeValue;
            }
            catch (Exception e)
            {
                // de-DE date format dd-MM-yyyy
                try
                {
                    var cultureInfo = new CultureInfo("de-DE");
                    date = DateTime.Parse(cell.StringValue, cultureInfo);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(exception);
                    throw;
                }
            }

            return date;
        }
    }
}