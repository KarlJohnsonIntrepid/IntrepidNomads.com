Public Class CategoryList
    Inherits System.Web.UI.UserControl

#Region "Properties"
    Public Property IsCategoryPage As Boolean
        Get
            Return BlankLibrary.NoBlank(ViewState("IsCategory"), False)
        End Get
        Set(value As Boolean)
            ViewState("IsCategory") = value
        End Set
    End Property

    Public Property CategoryID As String
        Get
            Return ViewState("CategoryID")
        End Get
        Set(value As String)
            ViewState("CategoryID") = value
        End Set
    End Property
#End Region


    Public Sub BindList(ByVal DataSource As Object, ByVal IsCategoryList As Boolean, ByVal pCategoryID As String)

        IsCategoryPage = IsCategoryList
        CategoryID = pCategoryID

        BindList(DataSource)
    End Sub

    Public Sub BindList(ByVal DataSource As Object)
        If DataSource.Count > 0 Then
            lstCategory.DataSource = DataSource
            lstCategory.DataBind()
        End If
    End Sub

    Public Function BlogURL(ByVal URL As String) As String

        Return URLCreator.BlogURL(URL, IsCategoryPage, CategoryID)
    End Function

    Public Shared Function LoadPreview(ByVal HTML As String, NiceDescription As String) As String

        If BlankLibrary.IsBlank(NiceDescription) Then
            Return HTMLParser.ParseHTML(HTML)
        Else
            Return NiceDescription
        End If

    End Function

    Public Shared Function ParseHTML(ByVal HTML As String) As String
        Return HTMLParser.ParseHTML(HTML)
    End Function

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        Dim script As String = " loadDotDotDot('.wrap')"
        Dim s As ClientScriptManager = BasePage.CurrentPage.ClientScript
        s.RegisterStartupScript(BasePage.CurrentPage.GetType, "DotDotCategory", script, True)
    End Sub
End Class