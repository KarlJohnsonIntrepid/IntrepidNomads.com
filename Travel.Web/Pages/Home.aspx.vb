Public Class Home
    Inherits BasePage

    Private Sub Home_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.MyMaster, blog1).HideSideBar()
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Title = "Intrepid Nomads - Budget BackPacking As A Couple"
        Me.Description = "It is our aim to use this blog as a way to share our experiences of a couple travelling on a budget, and hopefully will help to inspire other fellow adventurers to go out and travel. After all, there is a whole planet out there to explore. "
        LoadSlideShow()
    End Sub

    Private Sub LoadSlideShow()

        Dim numArticles As Int16 = 20
        rptRelatedPictureList1.DataSource = Business.Blog.GetRecentBlogs(numArticles)
        rptRelatedPictureList1.DataBind()


        Dim RandomCountries = Business.Country.GetCountries(numArticles).OrderBy(Function(x) Guid.NewGuid()).Take(numArticles)
        Dim ExcludedCountries = New List(Of String) From {"Italy", "Scotland", "Jordan"}

        RandomCountries = RandomCountries.Where(Function(x) Not ExcludedCountries.Contains(x.CountryDescription))


        rptDestinations.DataSource = RandomCountries
        rptDestinations.DataBind()

        rptDiaries.DataSource = Business.Category.SelectCategoriesFeatured.Take(numArticles)
        rptDiaries.DataBind()

    End Sub
End Class