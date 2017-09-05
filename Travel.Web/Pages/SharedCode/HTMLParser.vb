Imports System.Net

Public Class HTMLParser

    Public Shared Function ParseHTML(ByVal HTML As String) As String

        Return WebUtility.HtmlDecode(Regex.Replace(HTML, "<[^>]*(>|$)", String.Empty))

    End Function
End Class
