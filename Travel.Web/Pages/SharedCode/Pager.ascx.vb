Public Class pager
    Inherits System.Web.UI.UserControl

    Public Event Next_Clicked(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Prev_Clicked(ByVal sender As Object, ByVal e As System.EventArgs)

#Region "Properties"

    Public Property CurrentPage As Integer
        Get
            Return BlankLibrary.NoBlank(ViewState("CurrentPage"), 1)
        End Get
        Set(value As Integer)
            ViewState("CurrentPage") = value
        End Set
    End Property

    Private Property PageSize As Integer
        Get
            Return BlankLibrary.NoBlank(ViewState("PageSize"), 15)
        End Get
        Set(value As Integer)
            ViewState("PageSize") = value
        End Set
    End Property

#End Region


    Public Sub HideNext()
        lnkNext.Visible = False
    End Sub

    Public Sub ShowNext()
        lnkNext.Visible = True
    End Sub

    Public Sub HidePrevious()
        lnkPrevious.Visible = False
    End Sub

    Public Sub ShowPrevious()
        lnkPrevious.Visible = True
    End Sub

    Public Sub PreviousNavigateTo(NavigateURL)
        lnkPrevious.PostBackUrl = NavigateURL
    End Sub

    Public Sub NextNavigateTo(NavigateURL)
        lnkNext.PostBackUrl = NavigateURL
    End Sub
    Private Sub lnkNext_Click(sender As Object, e As EventArgs) Handles lnkNext.Click
        RaiseEvent Next_Clicked(sender, e)
    End Sub

    Private Sub lnkPrevious_Click(sender As Object, e As EventArgs) Handles lnkPrevious.Click
        RaiseEvent Prev_Clicked(sender, e)
    End Sub

    Public Function LoadPager(Datasource As IEnumerable(Of Object), PageSize As Integer) As IEnumerable(Of Object)

        Me.PageSize = PageSize

        Dim TotalrowCount = Datasource.Count

        If TotalrowCount <= (Me.PageSize * Me.CurrentPage) Then
            Me.HideNext()
        Else
            Me.ShowNext()
        End If

        If Me.CurrentPage = 1 Then
            Me.HidePrevious()
        Else
            Me.ShowPrevious()
        End If

        If TotalrowCount > 0 Then
            Return Datasource.Skip(Me.PageSize * (Me.CurrentPage - 1)).Take(Me.PageSize).ToList
        Else
            Return Datasource
        End If
    End Function
End Class