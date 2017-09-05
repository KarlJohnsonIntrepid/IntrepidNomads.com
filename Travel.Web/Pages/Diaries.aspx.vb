Public Class Diaries
    Inherits BasePage

    Private Sub Home_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.MyMaster, blog1).HideSideBar()
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Title = "Diaries - Intrepid Nomads - Budget BackPacking As A Couple"
        Me.Description = "Ever thought of trekking to Everest Base Camp or Hiking the Atlas Mountains? See what life is really like by reading our day-by-day diaries of our more unique trips."

        LoadDiaries()
    End Sub

    Private Sub LoadDiaries()
        rptDiaries.DataSource = Business.Category.SelectCategoriesFeatured
        rptDiaries.DataBind()
    End Sub

End Class