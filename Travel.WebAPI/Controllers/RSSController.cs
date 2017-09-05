using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Business;
using Travel.Common;
using System.Xml;
using System.Text;
using System.IO;

namespace Travel.WebAPI.Controllers
{
    public class RSSController : Controller
    {
        // GET: RSS
        public ActionResult Feed()
        {

            MemoryStream outputStream = new MemoryStream();

            XmlTextWriter xml = new XmlTextWriter(outputStream, Encoding.UTF8);
            xml.Formatting = Formatting.Indented;

            xml.WriteStartDocument();
            xml.WriteStartElement("rss");
            xml.WriteAttributeString("version", "2.0");
            xml.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");

            //create feed header section
            xml.WriteStartElement("channel");

            xml.WriteStartElement("atom:link");
            xml.WriteAttributeString("href", "http://intrepidnomads.com/rss.ashx");
            xml.WriteAttributeString("rel", "self");
            xml.WriteAttributeString("type", "application/rss+xml");
            xml.WriteEndElement();


            xml.WriteElementString("title", "Intrepid Nomads - Budget backpacking as a couple");
            xml.WriteElementString("link", "http://intrepidnomads.com");
            xml.WriteElementString("description", "Intrepid Nomads - Budget backpacking as a couple");
            xml.WriteElementString("language", "en-gb");
            xml.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"));
            xml.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"));
            xml.WriteElementString("managingEditor", "nomads@intrepidnomads.com (Karl Johnson)");
            xml.WriteElementString("webMaster", "nomads@intrepidnomads.com (Karl Johnson)");
            xml.WriteElementString("ttl", "60");

            //add stories to feed
            foreach (var row_loopVariable in Blog.GetRecentBlogs(15))
            {
                var row = row_loopVariable;
                xml.WriteStartElement("item");
                xml.WriteElementString("title", row.Title);

                xml.WriteElementString("link", "http://intrepidnomads.com/blog" + VirtualPathUtility.ToAbsolute("~/" + row.TitleURL));

                //output story as cdata
                xml.WriteStartElement("description");

                int MaxLength = row.Content.Length - 1;
                if (MaxLength > 250)
                    MaxLength = 250;

                xml.WriteCData(HTMLParser.ParseHTML(row.Content.Substring(0, MaxLength)));
                xml.WriteEndElement();

                xml.WriteElementString("pubDate", ((DateTime)row.Date).ToString("r"));

                xml.WriteElementString("guid", "http://intrepidnomads.com/blog" + VirtualPathUtility.ToAbsolute("~/" + row.TitleURL));
                xml.WriteEndElement();
            }

            xml.WriteEndElement();
            //close rss
            xml.WriteEndElement();
            //close document
            xml.WriteEndDocument();
            xml.Flush();
            //   xml.Close();

            xml.BaseStream.Position = 0;
    
            return new FileStreamResult(xml.BaseStream, "application/xml");
        }
    }
}