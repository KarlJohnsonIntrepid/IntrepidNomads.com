Imports System.Net

Public Class Blog

    Public Shared Property CurrentBlogID As Integer
        Get
            Return HttpContext.Current.Session("SelectedBlogID")
        End Get
        Set(value As Integer)
            HttpContext.Current.Session("SelectedBlogID") = value
        End Set
    End Property

#Region "Cache"

    Public Shared Property MyCache As List(Of vBlog)
        Get
            Return HttpContext.Current.Cache("Blog")
        End Get
        Set(value As List(Of vBlog))
            HttpContext.Current.Cache("Blog") = value
        End Set
    End Property

    Private Shared Sub UpdateCache()
        SelectBlogs(True)
    End Sub

#End Region

#Region "Select"
    Public Shared Function SelectBlogs(Optional ByVal ClearCache As Boolean = False, Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)

        If MyCache Is Nothing Or ClearCache Then
            Using Travel As New TravelEntities
                Dim query = From Blog In Travel.vBlogs
                            Order By Blog.Date Descending

                Dim blogs = query.ToList
                ''split and encode the url, then put back together
                For Each b In blogs

                    If (b.Title IsNot Nothing) Then
                        Dim array = b.TitleURL.Split("/")
                        b.TitleURL = ""

                        For Each item In array
                            b.TitleURL += WebUtility.UrlEncode(item) & "/"
                        Next

                        b.TitleURL = b.TitleURL.Substring(0, b.TitleURL.Length - 1)
                    End If

                Next

                MyCache = blogs
            End Using
        End If

        If Not IncludeHidden And MyCache.Count > 1 Then

            Dim visible = MyCache.Where(Function(row) row.IsVisible)
            If visible.Count > 1 Then
                Return MyCache.Where(Function(row) row.IsVisible = True).ToList
            End If

        End If

        Return MyCache

    End Function

    Public Shared Function CountOfBlogs()
        Return SelectBlogs.Count
    End Function


    Public Shared Function GetBlogItem(BlogID As Integer) As vBlog

        Dim query = From b In SelectBlogs(False, True)
                    Where b.BlogID = BlogID


        If query.Count > 0 Then
            Return query.First
        Else : Return Nothing
        End If

    End Function

    Public Shared Function SelectBlogsByCategory(CategoryID As String, Optional ReverseDateOrder As Boolean = False, Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)

        If ReverseDateOrder Then

            Dim query = From b In SelectBlogs(False, IncludeHidden)
          Where BlankLibrary.NoBlank(b.CategoryID, 0) = CategoryID Order By b.Date Ascending
            If query.Count > 0 Then
                Return query.ToList
            Else : Return New List(Of vBlog)
            End If
        Else

            Dim query = From b In SelectBlogs(False, IncludeHidden)
            Where BlankLibrary.NoBlank(b.CategoryID, 0) = CategoryID
            If query.Count > 0 Then
                Return query.ToList
            Else : Return New List(Of vBlog)
            End If
        End If

    End Function

    Public Shared Function SelectBlogsByCountry(CountryID As String, Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)

        Dim query = From b In SelectBlogs(False, IncludeHidden)
                    Where BlankLibrary.NoBlank(b.CountryID, 0) = CountryID

        If query.Count > 0 Then
            Return query.ToList
        Else : Return New List(Of vBlog)
        End If

    End Function


    Public Shared Function SelectBlogsByTitle(Destination As String, Category As String, Title As String, Optional ByVal IncludeHidden As Boolean = False) As vBlog

        Dim TitleURL = Destination + "/" + Category + "/" + HttpUtility.UrlDecode(Title)

        Dim query = From b In SelectBlogs(False, IncludeHidden)
                    Where BlankLibrary.NoBlank(HttpUtility.UrlDecode(b.TitleURL).ToLower, 0) = TitleURL.ToLower

        If query.Count > 0 Then
            Return query.First
        Else : Return Nothing
        End If

    End Function

    Public Shared Function SelectBlogsForDrinks(Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)
        Return Business.Blog.SelectBlogsByCategory(11)
    End Function

    Public Shared Function GetBlogMinimal(Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)
        ''Does not work needss to a deep copy!!!
        Dim query = From b In SelectBlogs(False, IncludeHidden)
        Dim Blogs As List(Of vBlog) = query.ToList
        For Each b In Blogs
            b.Content = ""
        Next
        Return query.ToList

    End Function

    Public Shared Function GetRecentBlogs(ByVal NumberOfBlogs As Integer, Optional ByVal IncludeHidden As Boolean = False) As List(Of vBlog)

        Dim query = From b In SelectBlogs(False, IncludeHidden)
        Take NumberOfBlogs

        Return query.ToList

    End Function

    Public Shared Function GetMostRecent(Optional ByVal IncludeHidden As Boolean = False) As vBlog
        Dim query = From b In SelectBlogs(False, IncludeHidden)
        Take 1

        Return query.First
    End Function

#End Region


#Region "Update/Insert/Delete"

    Public Shared Sub Insert(Title As String, AuthorID As String, CountryID As Integer?, CategoryID As Integer?, Content As String, BlogDate As Date,
                           CategoryImageID As Integer?, IsVisible As Boolean, IsFullScreen As Boolean, SEOTitle As String, SEODescription As String, NiceDescription As String,
                           ShowPhotos As Boolean)

        Dim Blog As New DAL.Blog
        With Blog
            .Title = Title
            .AuthorID = AuthorID
            .CountryID = CountryID
            .CategoryID = CategoryID
            .Content = Content
            .Date = BlogDate
            .DateCreated = Now
            .CategoryImageID = CategoryImageID
            .IsVisible = IsVisible
            .IsFullScreen = IsFullScreen
            .SEOTitle = SEOTitle
            .SEODescription = SEODescription
            .NiceDescription = NiceDescription
            .ShowPhotos = ShowPhotos
        End With

        Using context As New TravelEntities
            context.Blogs.Add(Blog)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Insert(Blog As DAL.Blog)

        Using context As New TravelEntities
            context.Blogs.Add(Blog)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub


    Public Shared Sub Delete(BlogID As Integer)

        Using Travel As New TravelEntities
                Dim b = (From Blog In Travel.Blogs
                         Where Blog.BlogID = BlogID).First


                Travel.Blogs.Remove(b)
                Travel.SaveChanges()
            End Using

        UpdateCache()

    End Sub

    Public Shared Sub Update(blog As DAL.Blog, id As Integer)

        Using Travel As New TravelEntities
            Dim updated = (From b In Travel.Blogs
                           Where b.BlogID = id).First

            Travel.Entry(updated).CurrentValues.SetValues(blog)

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub


    Public Shared Sub Update(BlogID As Integer, Title As String, Content As String, AuthorID As String, CountryID As String, CategoryId As String,
                             BlogDate As Date, CategoryImageID As Integer, IsVisible As Boolean, IsFullScreen As Boolean,
                             SEOTitle As String, SEODescription As String, NiceDescription As String, ShowPhotos As Boolean)

        Using Travel As New TravelEntities
            Dim c = (From Blog In Travel.Blogs
                       Where Blog.BlogID = BlogID).First

            c.Title = Title
            c.Content = Content
            c.AuthorID = AuthorID
            c.CountryID = CountryID
            c.CategoryID = CategoryId
            c.Date = BlogDate
            c.CategoryImageID = CategoryImageID
            c.IsVisible = IsVisible
            c.IsFullScreen = IsFullScreen
            c.SEOTitle = SEOTitle
            c.SEODescription = SEODescription
            c.NiceDescription = NiceDescription
            c.ShowPhotos = ShowPhotos

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub SetSlideShowFalse()
        Using Travel As New TravelEntities
            Travel.Database.ExecuteSqlCommand("UPDATE [dbo].[Blog] SET [ShowInSlideShow] = 0")
        End Using
        UpdateCache()
    End Sub

    Public Shared Sub SetSlideShowTrue(BlogID As Integer)

        If BlogID > 0 Then
            Using Travel As New TravelEntities
                Dim c = (From Blog In Travel.Blogs
                           Where Blog.BlogID = BlogID).First

                c.ShowInSlideShow = True

                Travel.SaveChanges()
            End Using

            UpdateCache()
        End If
    End Sub

    Public Shared Sub SetBlogVisibilty(ByVal BlogID As Integer, ByVal IsVisible As Boolean)
        Using Travel As New TravelEntities
            Dim c = (From Blog In Travel.Blogs
                       Where Blog.BlogID = BlogID).First

            c.IsVisible = IsVisible

            Travel.SaveChanges()
        End Using

        UpdateCache()
    End Sub

    Public Shared Function SelectRelatedBlogs(BlogId As Integer, NumberOfBlogs As Integer) As List(Of vBlog)

        Dim Blogs As List(Of vBlog)
        Using Travel As New TravelEntities
            Blogs = Travel.spTypedRelatedBlogs(BlogId).ToList

        End Using

        Dim query = From b In Blogs
            Take NumberOfBlogs

        Return query.ToList

    End Function
#End Region





End Class
