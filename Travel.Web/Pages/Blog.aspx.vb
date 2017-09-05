Public Class Blog
    Inherits BasePage

#Region "Properties"

    Public ReadOnly Property BlogID As Integer
        Get
            Return BlankLibrary.NoBlank(Page.RouteData.Values("id"), 0)
        End Get

    End Property

    Private Property BlogRow As vBlog
        Get
            If mBlogRow Is Nothing And BlogID > 0 Then
                mBlogRow = Business.Blog.GetBlogItem(BlogID)
            End If
            Return mBlogRow
        End Get
        Set(value As vBlog)
            mBlogRow = value
        End Set
    End Property
    Private mBlogRow As vBlog = Nothing

    Private ReadOnly Property IsWithinFeatureDiary As Boolean
        Get
            Return Request.QueryString("IsFeature") = 1
        End Get
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If BlogID > 0 Then
                Business.Blog.CurrentBlogID = BlogID
                LoadBlog(BlogID)
                LoadPaging()

            Else
                Response.Redirect("~/")
            End If

        End If

        SetFacebookLikeBox()

        AddOpenGraphTags()

        CommentCounter.BlogId = BlogID
        CommentCounter.SetBlogURL(Request.Url.GetLeftPart(UriPartial.Path))
    End Sub

    Private Sub LoadBlog(ByVal ID As Integer)

        If ID > 0 Then

            Me.Title = BlankLibrary.NoBlank(BlogRow.SEOTitle, BlogRow.Title)
            Me.Description = BlankLibrary.NoBlank(BlogRow.SEODescription, "")

            ''Load page for this blog ID
            lblTitle.Text = BlogRow.Title
            lblAuthor.Text = BlogRow.AuthorName
            lblDate.Text = CDate(BlogRow.Date).ToString("MMMM dd, yyyy")

            If CBool(BlankLibrary.NoBlank(BlogRow.IsFeature, False)) Then
                lblSubTitle.Text = "Featured In - " + BlogRow.CategoryDescription.ToString
            End If

            litBlogContent.Text = BlogRow.Content
            Dim t = BlogRow.Title.ToString + "-" + BlogRow.CountryDescription.ToString + "-" + BlogRow.CategoryDescription.ToString

            If BlogRow.IsFullScreen Then
                CType(MyMaster, blog1).MainDiv.Attributes.Add("class", "col-sm-12 blog-main")
                CType(MyMaster, blog1).HideSideBar()
            End If

            If BlogRow.ShowPhotos Then
                LoadImages()
            Else
                pnlPhotos.Attributes.CssStyle.Add("display", "none")
            End If
        End If
    End Sub

    Private Sub LoadPaging()
        Dim dt As List( Of vBlog)
        If IsWithinFeatureDiary Then
            dt = Business.Blog.SelectBlogsByCategory(BlogRow.CategoryID, True)

        Else
            ''Normal Paging of all blogs
            dt = Business.Blog.SelectBlogs()
        End If

        ''Load page for this blog ID
        Dim nextBlogIndex As Integer = 0
        Dim prevBlogIndex As Integer = 0

        For index = 0 To dt.Count - 1
            If CInt(dt.Item(index).BlogID) = CInt(BlogID) Then
                Pager.CurrentPage = index
                Exit For
            End If

        Next

        ''Ensure paging only pages the feaure diary
        Dim QueryString As String = ""
        If IsWithinFeatureDiary Then
            QueryString = "?IsFeature=1"
        End If

        ''Previous Pager
        If Pager.CurrentPage = 0 Then
            Pager.HidePrevious()
        Else
            Pager.ShowPrevious()

            prevBlogIndex = Pager.CurrentPage - 1
            Dim drPrev = dt.Item(prevBlogIndex)
            Pager.PreviousNavigateTo("~/blog/" + drPrev.TitleURL + QueryString)

        End If

        ''Next pager
        If Pager.CurrentPage = dt.Count - 1 Then
            Pager.HideNext()
        Else
            Pager.ShowNext()

            nextBlogIndex = Pager.CurrentPage + 1
            Dim drNext = dt.Item(nextBlogIndex)
            Pager.NextNavigateTo("~/blog/" + drNext.TitleURL + QueryString)
        End If

    End Sub

    Private Sub LoadImages()
        rptImages.DataSource = Business.ImageUpload.SelectImagesByBlog(BlogID)
        rptImages.DataBind()
    End Sub

    Private Sub AddOpenGraphTags()

        AddOpenGraphMetaTag("og:title", BlogRow.Title)
        AddOpenGraphMetaTag("og:site_name", "Intrepid Nomads")
        AddOpenGraphMetaTag("og:url", Request.Url.AbsoluteUri.ToLower)
        AddOpenGraphMetaTag("og:description", BlogRow.SEODescription)
        AddOpenGraphMetaTag("og:image", URLHelper.ConvertRelativeUrlToAbsoluteUrl(ResolveUrl("~/images/uploads/original/" + BlogRow.CategoryImageDescription)))
        AddOpenGraphMetaTag("fb:app_id", "404772936329803")

    End Sub

    'Public Sub LoadRelatedItems()
    '    If Not IsWithinFeatureDiary Then
    '        rptRelated.DataSource = Business.Blog.SelectRelatedBlogs(BlogID, 4)
    '        rptRelated.DataBind()
    '    End If
    'End Sub

    Private Sub bntSubscribe_Click(sender As Object, e As EventArgs) Handles bntSubscribe.Click
        Response.Redirect("~/Pages/SharedCode/MailChimp.aspx?email=" & txtSubscribe.Text)
    End Sub

    Public Sub SetFacebookLikeBox()

        Dim uri = New Uri(Request.Url.AbsoluteUri)

        divFacebook.Attributes.Add("data-href", uri.GetLeftPart(UriPartial.Path))

    End Sub
End Class