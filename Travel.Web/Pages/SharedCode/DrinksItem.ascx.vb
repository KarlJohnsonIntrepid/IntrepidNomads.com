Public Class DrinksItem
    Inherits System.Web.UI.UserControl

    Public Property ImageURL As String
    Public Property Title As String
    Public Property Country As String
    Public Property NavigateURL As String
    Public Property Number As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgDrink.ImageUrl = ImageURL
        lblTitle.Text = Title
        lblCountry.Text = Country
        lnkDrink.NavigateUrl = NavigateURL
        lblNumber.Text = Number
    End Sub

    Public Shared Function Create() As DrinksItem
        Dim di As DrinksItem = BasePage.CurrentPage.LoadControl("~/Pages/SharedCode/DrinksItem.ascx")
        Return di
    End Function

End Class