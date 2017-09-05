Public Class blog1
    Inherits BaseMaster

    Public Property MainDiv As HtmlGenericControl
        Get
            Return divMain
        End Get
        Set(value As HtmlGenericControl)

        End Set
    End Property

    Private Property SideBarVisible As Boolean = True


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        CreateMenu()

        Dim link As New HtmlLink()
        link.Href = "~/content/blog.css"
        link.Attributes.Add("rel", "stylesheet")
        link.Attributes.Add("type", "text/css")
        Page.Header.Controls.Add(link)

        LoadSideBar()

    End Sub

    Private Sub LoadSideBar()
        If SideBarVisible Then
            Dim SideBar As SideBar = Page.LoadControl("~/Pages/SideBar.ascx")
            SideBar.ID = "SideBar"
            plcSideBar.Controls.Add(SideBar)
        Else
            divMain.Attributes.Add("class", "col-sm-12 blog-main")
        End If
    End Sub

    Private Sub CreateMenu()

        litCategoryMenu.Mode = LiteralMode.PassThrough
        litCategoryMenu.Text = ""
        litCategoryMenu.Text = Menu.CategoryMenuItems

    End Sub

    Public Sub HideSideBar()
        divSideBar.Attributes.CssStyle.Add("display", "none")
        SideBarVisible = False
    End Sub

    'Private Sub LoadDaysDeparture()
    '    Dim DateOfDeparture = New Date(2015, 5, 2)
    '    lblDays.Text = DateDiff(DateInterval.Day, Date.Now, DateOfDeparture).ToString
    'End Sub



End Class