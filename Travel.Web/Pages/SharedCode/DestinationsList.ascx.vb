Public Class DestinationsList
    Inherits System.Web.UI.UserControl

    Public Property Continent As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblContinentTitle.Text = Continent
    End Sub

    Public Sub LoadCountries(ByVal Datasource As List(Of DAL.vCountry))

        If Datasource.Count = 0 Then
            Me.Visible = False
        End If


        rptCountries.DataSource = Datasource
        rptCountries.DataBind()
    End Sub

End Class