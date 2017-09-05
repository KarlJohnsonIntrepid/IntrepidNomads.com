Public Class admin_addnewbuttons
    Inherits System.Web.UI.UserControl

    Private Enum SelectedPage
        AddNew
        Edit
        NotSelected
    End Enum

    Private Property SelectedMode() As SelectedPage
        Get
            Return BlankLibrary.NoBlank(ViewState("SelectedPage"), SelectedPage.NotSelected)
        End Get
        Set(value As SelectedPage)
            ViewState("SelectedPage") = value
        End Set
    End Property

    Public Event NewClicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event EditClicked(ByVal sender As Object, ByVal e As System.EventArgs)


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        SelectedMode = SelectedPage.Edit
        RaiseEvent EditClicked(sender, e)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        SelectedMode = SelectedPage.AddNew
        RaiseEvent NewClicked(sender, e)
    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If SelectedMode = SelectedPage.AddNew Then
            btnNew.CssClass = "btn btn-primary btn-lg"
            btnEdit.CssClass = "btn btn-default btn-lg"
        ElseIf SelectedMode = SelectedPage.Edit Then
            btnEdit.CssClass = "btn btn-primary btn-lg"
            btnNew.CssClass = "btn btn-default btn-lg"
        ElseIf SelectedMode = SelectedPage.NotSelected Then
            btnEdit.CssClass = "btn btn-default btn-lg"
            btnNew.CssClass = "btn btn-default btn-lg"
        End If
    End Sub
End Class