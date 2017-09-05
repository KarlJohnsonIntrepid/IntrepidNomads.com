Public Class SideBar
    Inherits System.Web.UI.UserControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        LoadSlideshow()
        LoadDaysOnTheRoad()

        LoadRelated()

        SetLastKnownLocation()
    End Sub

    Private Sub LoadDaysOnTheRoad()
        Dim DateOfDeparture = New Date(2015, 5, 2)
        lblDays.Text = DateDiff(DateInterval.Day, DateOfDeparture, Date.Now).ToString
    End Sub

    Private Sub LoadSlideshow()

        Dim count As Integer = 1
        Dim Blogs As List(Of vBlog) = Business.Blog.SelectBlogs.AsEnumerable.Where(Function(x) x.ShowInSlideShow = True).ToList

        For Each Row As vBlog In Blogs
            If count = 7 Then Exit For

            ''Create circles.
            Dim li As New HtmlGenericControl("li")
            li.Attributes.Add(" data-slide-to", count - 1)
            If count = 1 Then li.Attributes.Add("class", "active")

            carouselcircles.Controls.Add(li)

            ''Add Images
            Dim Item As CarouselItem = CarouselItem.Create()
            Item.LoadSlideshowItem(Row)

            If count = 1 Then Item.SetActive()
            CarouselItems.Controls.Add(Item)

            count += 1
        Next

        If Blogs.Count = 0 Then ControlHelper.HideGenericHTMLControl(divCarousel)

    End Sub

    Private Sub LoadRelated()
        rptRecent.DataSource = Business.Blog.GetRecentBlogs(10)
        rptRecent.DataBind()

        ' ''If there is no blog selected just show recent.
        'Dim Related = Business.Blog.SelectRelatedBlogs(Business.Blog.CurrentBlogID, 10)
        'If Related.Count > 0 Then
        '    rptRelated.DataSource = Business.Blog.SelectRelatedBlogs(Business.Blog.CurrentBlogID, 10)
        'Else
        '    rptRelated.DataSource = rptRecent.DataSource
        'End If
        'rptRelated.DataBind()


        'rptRelatedPictureList1.DataSource = CType(rptRelated.DataSource, List(Of vBlog)).Take(5)
        'rptRelatedPictureList1.DataBind()

        'rptRelatedPictureList2.DataSource = CType(rptRelated.DataSource, List(Of vBlog)).Skip(5).Take(5)
        'rptRelatedPictureList2.DataBind()

    End Sub

    Private Sub bntSubscribe_Click(sender As Object, e As EventArgs) Handles bntSubscribe.Click
        Response.Redirect("~/Pages/SharedCode/MailChimp.aspx?email=" & txtSubscribe.Text)
    End Sub

    Private Sub SetLastKnownLocation()
        lblLastKnownLocation.Text = Business.Location.SetLastKnownLocation
    End Sub


End Class