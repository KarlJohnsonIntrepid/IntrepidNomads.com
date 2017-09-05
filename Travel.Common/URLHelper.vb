Public Class URLHelper

    Public Shared Function ConvertRelativeUrlToAbsoluteUrl(relativeUrl As String) As String
        Return String.Format("http{0}://{1}{2}", If((HttpContext.Current.Request.IsSecureConnection), "s", ""), HttpContext.Current.Request.Url.Host, CType(HttpContext.Current.Handler, Page).ResolveUrl(relativeUrl))
    End Function

End Class
