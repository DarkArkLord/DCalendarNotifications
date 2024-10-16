using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DCalendarNotifications.Core.Config
{
    public static class ConfigParser
    {
        public static XDocument ReadXml(string path)
        {
            var file = File.ReadAllText(path);
            var xDocument = XDocument.Parse(file);
            return xDocument;
        }

        public static ConfigData Parse(XDocument xml)
        {
            var config = new ConfigData();

            //

            return config;
        }
    }
}
