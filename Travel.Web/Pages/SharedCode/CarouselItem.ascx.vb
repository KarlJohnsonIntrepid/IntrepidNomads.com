Public Class CarouselItem
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub LoadSlideshowItem(ByVal row As vBlog)

        imgSlideShow.ImageUrl = ResolveUrl("~/images/uploads/medium/") + row.CategoryImageDescription
        txtHeader.Text = row.Title
        lnk.NavigateUrl = ResolveUrl("~/blog/" + row.TitleURL) + "/"

    End Sub

    Public Sub SetActive()
        item.Attributes.Add("class", "item active")
    End Sub

    Public Shared Function Create() As CarouselItem
        Dim c As CarouselItem = BasePage.CurrentPage.LoadControl("~/Pages/SharedCode/CarouselItem.ascx")
        Return c
    End Function

End Class