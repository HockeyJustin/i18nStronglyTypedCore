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
    /// Reource Provider created in line with this guide 
    /// http://afana.me/archive/2013/11/01/aspnet-mvc-internationalization-store-strings-in-database-or-xml.aspx/
    /// </summary>
    public class XmlResourceProvider : BaseResourceProvider
    {
        // File path
        private static string filePath = null;

        public XmlResourceProvider() { }
        public XmlResourceProvider(string filePath)
        {
            XmlResourceProvider.filePath = filePath;

            if (!File.Exists(filePath)) throw new FileNotFoundException(string.Format("XML Resource file {0} was not found", filePath));
        }

        internal override IList<ResourceEntry> ReadResources()
        {

            // Parse the XML file
            return XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value
                }).ToList();
        }
        protected override ResourceEntry ReadResource(string name, string culture)
        {
            // Parse the XML file
            return XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Where(e => e.Attribute("name").Value == name && e.Attribute("culture").Value == culture)
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value
                }).FirstOrDefault();
        }
    }
}
