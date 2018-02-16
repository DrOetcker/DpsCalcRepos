using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Oetcker.Data
{
    public static class XmlSerializer<T>
    {

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

        public static T GetContent(string fileName)
        {
            if (!File.Exists(@"Data\fileName.xml"))
                return null;

            var reader = _xmlResourceService.GetXmlReader(SiriusCertifiedArticleXmlFile, SiriusCertifiedArticlePath);
            if (null == reader)
                throw new Exception("No SiriusCertifiedArticle.xml found.");
            var serializer = new XmlSerializer(typeof(T));
            var siriusCertifiedArticles = (T)serializer.Deserialize(reader);
            return siriusCertifiedArticles;

        }
    }
}
