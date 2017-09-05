Public Class Category
    Inherits BasePage

#Region "Properties"

    ReadOnly Property IsCountry As Boolean
        Get
            Return BlankLibrary.NoBlank(Page.RouteData.Values("IsCountry"), False)
        End Get
    End Property

    ReadOnly Property CategoryID As Integer
        Get
            Return BlankLibrary.NoBlank(Page.RouteData.Values("id"), 0)
        End Get
    End Property

   
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If CategoryID > 0 Then
                LoadCategoryList()
                LoadHeader()

            Else
                Response.Redirect("~/")
            End If

    End Sub

    Private Sub LoadHeader()

        Dim Title As String = ""
        Dim Information As String = ""

        Dim CountryID = 0

        If CategoryID > 0 Then
            If IsCountry Then
                Dim row As vCountry = Business.Country.GetCountryRow(CategoryID)
                Title = BlankLibrary.NoBlank(row.CountryDescription, "")
                Information = BlankLibrary.NoBlank(row.CountryInformation, "")
                CountryID = row.CountryID
                Me.Title = BlankLibrary.NoBlank(row.CountryDescription, "")
                Me.Title = BlankLibrary.NoBlank(row.SEOTitle, row.CountryDescription)
                Me.Description = BlankLibrary.NoBlank(row.SEODescription, "")
            Else
                Dim row As vCategory = Business.Category.GetCategoryRow(CategoryID)
                Title = BlankLibrary.NoBlank(row.CategoryDescription, "")
                Information = BlankLibrary.NoBlank(row.CategoryInformation, "")

                Me.Title = BlankLibrary.NoBlank(row.SEOTitle, row.CategoryDescription)
                Me.Description = BlankLibrary.NoBlank(row.SEODescription, "")
            End If
        End If

        lblTitle.Text = Title

        If Not IsPostBack Then
            litInformation.Text = Information

            ''Load linked features
            Dim ChildCategories As List(Of vCategory) = Business.Category.SelectChildCategories(CategoryID, CountryID)
            rptLinkedCategories.DataSource = ChildCategories
            rptLinkedCategories.DataBind()
        End If
       

    End Sub

    Private Sub LoadCategoryList()


        If CategoryID > 0 Then

            Dim ListSize As Integer = 10

            Dim Datasource As IEnumerable(Of Object)
            If IsCountry Then
                ''Load all for country, allows google to see all blogs...
                Datasource = Business.Blog.SelectBlogsByCountry(CategoryID)
                ListSize = 50
            Else
                Datasource = Business.Blog.SelectBlogsByCategory(CategoryID, ShouldReverseOrder)
            End If

            Datasource = pager.LoadPager(Datasource, ListSize)
            CategoryList1.BindList(Datasource, Not IsCountry, CategoryID)

        End If

    End Sub

    Private Function ShouldReverseOrder() As Boolean

        ''Should category be reversed for feature
        Dim row As vCategory = Business.Category.GetCategoryRow(CategoryID)
        Return CBool(BlankLibrary.NoBlank(row.ReverseDateOrder, False))

    End Function

    Private Sub lnkNext_Click(sender As Object, e As EventArgs) Handles pager.Next_Clicked
        pager.CurrentPage = pager.CurrentPage + 1
        LoadCategoryList()
    End Sub

    Private Sub lnkPrevious_Click(sender As Object, e As EventArgs) Handles pager.Prev_Clicked
        pager.CurrentPage = pager.CurrentPage - 1
        LoadCategoryList()
    End Sub
End Class