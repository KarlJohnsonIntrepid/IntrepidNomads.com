Public Class Locations
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "Last Known Locations - Intrepid Nomads"

        SetLastKnownLocation()
    End Sub

    Private Sub SetLastKnownLocation()
        lblLastKnownLocation.Text = Business.Location.SetLastKnownLocation
    End Sub

End Class