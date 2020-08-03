using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ReportsService.Source.Util
{
    public static class XmlGenerator
    {
        public static void GenerateXml<T>(IReadOnlyCollection<T> serializable, string filename, string destination)
        {
            var rPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, destination, filename);
            var serializer = new XmlSerializer(typeof(List<T>));
            using TextWriter writer = new StreamWriter(rPath);
            serializer.Serialize(writer, serializable);
        }
    }
}