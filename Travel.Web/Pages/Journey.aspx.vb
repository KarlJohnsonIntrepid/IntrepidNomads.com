Public Class Journey
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PageTitle = "Intrepid Nomads Journey"
        CType(MyMaster, blog1).HideSideBar()
    End Sub

End Class