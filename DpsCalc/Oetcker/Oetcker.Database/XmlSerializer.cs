using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Oetcker.Data
{
    public static class XmlSerializer<T>
    {
        #region Methods

        /// <summary>
        ///     Exportiert ausgelesene Daten in XML
        /// </summary>
        public static void ExportToXml(T data, string fileName)
        {
            if (!Directory.Exists(@"Data"))
                Directory.CreateDirectory(@"Data");
            var xmlserializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8
            };
            using (var writer = XmlWriter.Create($@"Data\{fileName}.xml", settings))
            {
                xmlserializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Diese Methode liefert den Inhalt einer XML-Datei zurück
        /// </summary>
        public static T GetContent(string fileName)
        {
            if (!File.Exists($@"Data\{fileName}.xml"))
                return default(T);

            using (var reader = XmlReader.Create($@"Data\{fileName}.xml"))
            {
                if (null == reader)
                    throw new Exception("No SiriusCertifiedArticle.xml found.");
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        #endregion
    }
}
