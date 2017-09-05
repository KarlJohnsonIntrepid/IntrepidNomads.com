Public Class Drinks
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "Intrepid Nomads - Around the world in 80 drinks"
        If Not IsPostBack Then
            LoadDrinks()
        End If
    End Sub

    Private Sub LoadDrinks()

        Dim Drinks As List(Of vBlog) = Business.Blog.SelectBlogsByCategory(11, True)
        ' Dim PagedDrinks As IEnumerable(Of Object) = pager.LoadPager(Drinks, 9)

        Dim index = 1
        For Each blog As vBlog In Drinks

            Dim Drink As DrinksItem = DrinksItem.Create
            Drink.ID = "drink-" & index

            Drink.ImageURL = ResolveUrl("~/images/uploads/medium/") + blog.CategoryImageDescription
            Drink.Title = blog.Title
            Drink.Country = blog.CountryDescription
            Drink.NavigateURL = ResolveUrl("~/blog/" + blog.TitleURL) + "/"
            Drink.Number = index

            If index Mod 2 <> 0 Then
                ''odd number
                pnlDrinks1.Controls.Add(Drink)
            Else
                pnlDrinks2.Controls.Add(Drink)
            End If

            index += 1
        Next

    End Sub


    'Private Sub lnkNext_Click(sender As Object, e As EventArgs) Handles pager.Next_Clicked
    '    pager.CurrentPage = pager.CurrentPage + 1
    '    loadDrinks()
    'End Sub

    'Private Sub lnkPrevious_Click(sender As Object, e As EventArgs) Handles pager.Prev_Clicked
    '    pager.CurrentPage = pager.CurrentPage - 1
    '    loadDrinks()
    'End Sub
End Class