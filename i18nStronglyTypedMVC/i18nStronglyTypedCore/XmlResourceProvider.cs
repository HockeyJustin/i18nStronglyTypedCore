using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace i18nStronglyTypedCore
{
    /// <summary>
    /// Created by i18n.ResourceProvider
    /// This is a .net core port from this blog.
    /// http://afana.me/archive/2013/11/01/aspnet-mvc-internationalization-store-strings-in-database-or-xml.aspx/
    /// </summary>
    public class XmlResourceProvider : BaseResourceProvider
    {
        // File path
        private static string[] _filePaths = null;

        public XmlResourceProvider() { }
        public XmlResourceProvider(string filePath): this(new string[] { filePath })
        {
        }

        public XmlResourceProvider(string[] filePaths)
        {
            XmlResourceProvider._filePaths = filePaths;

            foreach(var filePath in _filePaths)
            {
                if (!File.Exists(filePath)) throw new FileNotFoundException(string.Format("XML Resource file {0} was not found", filePath));
            }
        }

        internal override IList<ResourceEntry> ReadResources()
        {
            List<ResourceEntry> returnValue = new List<ResourceEntry>();

            foreach(var filePath in _filePaths)
            {
                var values = XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value
                }).ToList();

                returnValue.AddRange(values);
            }

            // Parse the XML file
            return returnValue;
        }
        protected override ResourceEntry ReadResource(string name, string culture)
        {
            // inefficient
            return ReadResources()
                .Where(_ => _.Name == name && _.Culture == culture)
                .FirstOrDefault();
        }
    }
}
