Public Class default_1
    Inherits BasePage

    Private Property PageSize As Integer = 15

    Public Property LoadedPageSize As String
        Get
            Return BlankLibrary.NoBlank(ViewState("LoadPageSize"), PageSize)
        End Get
        Set(value As String)
            ViewState("LoadPageSize") = value
        End Set
    End Property

    Public Property SelectedCategory As String
        Get
            Return ViewState("SelectedCategory")
        End Get
        Set(value As String)
            ViewState("SelectedCategory") = value
        End Set
    End Property

    Public Property CountOfBlogs As String
        Get
            Return ViewState("CountOfBlogs")
        End Get
        Set(value As String)
            ViewState("CountOfBlogs") = value
        End Set
    End Property

    Public ReadOnly Property NumberOfPages As Integer
        Get
            Return Math.Ceiling(CountOfBlogs / PageSize)
        End Get
    End Property

    Public Property CurrentPagerPage As Integer
        Get
            Return BlankLibrary.NoBlank(ViewState("CurrentPage"), 1)
        End Get
        Set(value As Integer)
            ViewState("CurrentPage") = value
        End Set
    End Property

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ''run this after the load more click
            LoadList()
        End If

        LoadDropdownList()

        LoadPager()

    End Sub

    Private Sub btnLoadMore_Click(sender As Object, e As EventArgs) Handles btnLoadMore.Click
        LoadedPageSize = LoadedPageSize + 10


        If SelectedCategory IsNot Nothing Then
            LoadList(Business.Blog.SelectBlogsByCategory(SelectedCategory))
        Else
            LoadList()
        End If

    End Sub

    Private Sub LoadList(Optional ByVal OptionalDatasource As List(Of vBlog) = Nothing)

        Dim Datasource As List(Of vBlog)
        If Business.Blog.SelectBlogs.Count > 0 Then


            If OptionalDatasource Is Nothing Then
                countOfBlogs = Business.Blog.CountOfBlogs
                Datasource = Business.Blog.SelectBlogs.AsEnumerable.Take(LoadedPageSize).ToList
            Else
                countOfBlogs = OptionalDatasource.Count
                Datasource = OptionalDatasource.AsEnumerable.Take(LoadedPageSize).ToList
            End If

            If Datasource.Count > 0 Then
                CategoryList1.BindList(Datasource)
            End If
            If LoadedPageSize < 100 AndAlso LoadedPageSize >= countOfBlogs Then
                Common.ControlHelper.HideControl(btnLoadMore)
            Else
                Common.ControlHelper.ShowControl(btnLoadMore, "inline-block")
            End If
        End If

    End Sub

    Private Sub LoadDropdownList()

        ''Load Categorys

        Dim Categorys = Business.Category.SelectCategoriesBlogFilter
        If Categorys.Count > 0 Then
            For Each c In Categorys
                Dim li As New HtmlGenericControl("li")
                li.Attributes.Add("role", "presentation")
                Dim a As New LinkButton
                a.Text = c.CategoryDescription
                a.CommandName = "category"
                a.CommandArgument = c.CategoryID
                a.CssClass = "categorydropdown"
                li.Controls.Add(a)
                plcCategorys.Controls.Add(li)

            Next
        End If

        ' ''Load Countrys
        'Dim Countries = Business.Country.SelectCountries
        'If Countries.Count > 0 Then
        '    For Each c In Countries
        '        Dim li As New HtmlGenericControl("li")
        '        li.Attributes.Add("role", "presentation")
        '        Dim a As New LinkButton
        '        a.Text = c.CountryDescription
        '        a.CommandName = "country"
        '        a.CommandArgument = c.CountryID

        '        li.Controls.Add(a)
        '        plcCountries.Controls.Add(li)
        '    Next
        'End If


        DropdownClicked()
    End Sub

    Private Sub DropdownClicked()
        Dim control As LinkButton = TryCast(GetPostBackControl(), LinkButton)

        If control IsNot Nothing AndAlso (control.ID = "lnkViewAll" Or control.CssClass = "categorydropdown") Then
            If control.CommandArgument IsNot Nothing Then
                If control.CommandName = "category" Then
                    LoadList(Business.Blog.SelectBlogsByCategory(control.CommandArgument))
                    SelectedCategory = control.CommandArgument

                    CurrentPagerPage = 1

                    'ElseIf control.CommandName = "country" Then
                    '    LoadList(Business.Blog.SelectBlogsByCountry(control.CommandArgument))
                End If

                litSearchText.Text = BlankLibrary.NoBlank(control.Text, "Search By")
            End If
        End If
    End Sub

    Private Sub lnkViewAll_Click(sender As Object, e As EventArgs) Handles lnkViewAll.Click
        LoadList()
    End Sub

    Private Sub LoadPager()
        plcPager.Controls.Clear()
        If NumberOfPages > 0 Then
            For index = 1 To NumberOfPages
                AddPagerItem(index)
            Next
        End If

        SetPrevNext()
        SetSelectedPage()
    End Sub

    Private Sub AddPagerItem(pageNumber As Integer)
        Dim li As New HtmlGenericControl("li")
        Dim a As New LinkButton

        a.Text = pageNumber
        a.CommandArgument = a.Text

        AddHandler a.Command, AddressOf PageClicked

        li.Controls.Add(a)
        plcPager.Controls.Add(li)
    End Sub

    Protected Sub PageClicked(sender As Object, e As CommandEventArgs)

        CurrentPagerPage = e.CommandArgument
        ReloadForPager()

    End Sub

    Private Sub ReloadForPager()
        Dim Datasource As List(Of vBlog)
        If Business.Blog.SelectBlogs.Count > 0 Then


            If SelectedCategory IsNot Nothing Then
                Datasource = Business.Blog.SelectBlogsByCategory(SelectedCategory)
            Else
                Datasource = Business.Blog.SelectBlogs
            End If

            Datasource = Datasource.AsEnumerable.Skip((CurrentPagerPage * PageSize) - PageSize).Take(PageSize).ToList

            If Datasource.Count > 0 Then
                CategoryList1.BindList(Datasource)
            End If
        End If

        LoadPager()
        SetPrevNext()
        SetSelectedPage()
    End Sub

    Private Sub SetSelectedPage()
        Dim count As Integer = 1
        For Each Control As HtmlGenericControl In plcPager.Controls
            If count = CurrentPagerPage Then
                Control.Attributes.Add("class", "active")
            Else
                Control.Attributes.Clear()
            End If
            count += 1
        Next

    End Sub

    Private Sub SetPrevNext()
        If CurrentPagerPage = NumberOfPages Then
            liNext.Attributes.Add("class", "disabled")
            lnkPageNext.Enabled = False
        Else
            liNext.Attributes.Remove("class")
            lnkPageNext.Enabled = True
        End If

        If CurrentPagerPage = 1 Then
            liPrev.Attributes.Add("class", "disabled")
            lnkPagePrev.Enabled = False
        Else
            liPrev.Attributes.Remove("class")
            lnkPagePrev.Enabled = True
        End If
    End Sub

    Private Sub lnkPageNext_Click(sender As Object, e As EventArgs) Handles lnkPageNext.Click
        If CurrentPagerPage < NumberOfPages Then
            CurrentPagerPage += 1
            ReloadForPager()
        End If
    End Sub

    Private Sub lnkPagePrev_Click(sender As Object, e As EventArgs) Handles lnkPagePrev.Click
        If CurrentPagerPage > 1 Then
            CurrentPagerPage = CurrentPagerPage - 1
            ReloadForPager()
        End If
    End Sub
End Class