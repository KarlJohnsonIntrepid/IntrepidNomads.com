Public Class CommentCounter
    Inherits System.Web.UI.UserControl

    Public Property BlogURL As String = Nothing
    Public Property BlogID As String = ""

    Public Sub SetBlogURL(ByVal URL As String)
        lnkCounter.NavigateUrl = URL + "#disqus_thread"
        lnkCounter.Attributes.Add("data-disqus-identifier", BlogId)
    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If BlogURL IsNot Nothing Then
            SetBlogURL(BlogURL)
        End If
    End Sub
End Class