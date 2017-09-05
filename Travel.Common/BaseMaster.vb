Public Class BaseMaster
    Inherits MasterPage
    Public Property PageTitle As String = ""

    Public ReadOnly Property MyPage As BasePage
        Get
            Return CType(Me.Page, BasePage)
        End Get
    End Property



End Class
