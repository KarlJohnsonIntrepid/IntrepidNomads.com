Public Class admin
    Inherits BaseMaster

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblPageTitle.InnerHtml = Me.PageTitle
        MyPage.Title = "Administration"
        If Not IsPostBack Then
            SetMenu()
        End If

    End Sub

    Private Sub SetMenu()

        Select Case EnvironmentLibrary.PageName
            Case "admin.aspx"
                mnuOverview.Attributes.Add("class", "active")

            Case "admin_blog.aspx"
                mnuBlog.Attributes.Add("class", "active")

            Case "imageupload.aspx"
                mnuImages.Attributes.Add("class", "active")

            Case "admin_countries.aspx"
                mnuCountries.Attributes.Add("class", "active")

            Case "admin_categories.aspx"
                mnuCategories.Attributes.Add("class", "active")
        End Select

    End Sub

End Class