Imports System.Web.Optimization

Public Class SiteMaster
    Inherits BaseMaster

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not BlankLibrary.IsBlank(MyPage.Description) Then
            Dim Description As New HtmlMeta
            Description.Name = "description"
            Description.Content = MyPage.Description

            plcMetaDescription.Controls.Add(Description)
        End If
      
    End Sub
End Class