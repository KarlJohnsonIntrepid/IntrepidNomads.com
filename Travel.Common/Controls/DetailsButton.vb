Imports System.Web.UI.WebControls

Public Class DetailsButton
    Inherits Button


    Private Sub DetailsButton_Init(sender As Object, e As EventArgs) Handles Me.Init
        CssClass = "btn btn-info btn-xs"
        Text = "..."
    End Sub

End Class
