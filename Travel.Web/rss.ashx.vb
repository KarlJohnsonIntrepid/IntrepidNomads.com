Imports System.Web
Imports System.Web.Services
Imports System.Web.Http
Imports System
Imports System.Xml
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net

Public Class rss
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest


        context.Response.ContentType = "application/xml"
        context.Response.ContentEncoding = System.Text.Encoding.UTF8
        context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(3600))
        context.Response.Cache.SetCacheability(HttpCacheability.Public)

        Dim xml As New XmlTextWriter(context.Response.OutputStream, Encoding.UTF8)
        xml.Formatting = Formatting.Indented

        xml.WriteStartDocument()
        xml.WriteStartElement("rss")
        xml.WriteAttributeString("version", "2.0")
        xml.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom")

        'create feed header section
        xml.WriteStartElement("channel")

        xml.WriteStartElement("atom:link")
        xml.WriteAttributeString("href", "http://intrepidnomads.com/rss.ashx")
        xml.WriteAttributeString("rel", "self")
        xml.WriteAttributeString("type", "application/rss+xml")
        xml.WriteEndElement()


        xml.WriteElementString("title", "Intrepid Nomads - Budget backpacking as a couple")
        xml.WriteElementString("link", "http://intrepidnomads.com")
        xml.WriteElementString("description", "Intrepid Nomads - Budget backpacking as a couple")
        xml.WriteElementString("language", "en-gb")
        xml.WriteElementString("pubDate", DateTime.UtcNow.ToString("r"))
        xml.WriteElementString("lastBuildDate", DateTime.UtcNow.ToString("r"))
        xml.WriteElementString("managingEditor", "nomads@intrepidnomads.com (Karl Johnson)")
        xml.WriteElementString("webMaster", "nomads@intrepidnomads.com (Karl Johnson)")
        xml.WriteElementString("ttl", "60")

        'add stories to feed
        For Each row In Business.Blog.GetRecentBlogs(15)
            xml.WriteStartElement("item")
            xml.WriteElementString("title", row.Title)

            xml.WriteElementString("link", "http://intrepidnomads.com/blog" & VirtualPathUtility.ToAbsolute("~/" & row.TitleURL))

            'output story as cdata
            xml.WriteStartElement("description")

            Dim MaxLength As Integer = row.Content.Length - 1
            If MaxLength > 250 Then MaxLength = 250

            xml.WriteCData(HTMLParser.ParseHTML(row.Content.Substring(0, MaxLength)))
            xml.WriteEndElement()

            xml.WriteElementString("pubDate", CType(row.Date, DateTime).ToString("r"))

            xml.WriteElementString("guid", "http://intrepidnomads.com/blog" & VirtualPathUtility.ToAbsolute("~/" & row.TitleURL))
            xml.WriteEndElement()
        Next

        xml.WriteEndElement()
        'close rss
        xml.WriteEndElement()
        'close document
        xml.WriteEndDocument()
        xml.Flush()
        xml.Close()

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class