Imports Travel.Web.Enums

Public Class admin_blog
    Inherits BasePage

    Private Property BlogID As Integer
        Get
            Return ViewState("BlogID")
        End Get
        Set(value As Integer)
            ViewState("BlogID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle = "Blog Entries"

        If Not IsPostBack Then

            ResetForm()

            If LoadFormFromQueryString() = False Then
                ShowEditGrid()
            End If

        End If
    End Sub

    Private Function LoadFormFromQueryString() As Boolean
        If Not BlankLibrary.IsBlank(Request.QueryString("blogid")) Then

            BlogID = Request.QueryString("blogid")
            ''load the blog from query string
            Dim row As vBlog = Business.Blog.GetBlogItem(BlogID)
            LoadForm(FormMode.Edit, row)

            Return True
        End If

        Return False
    End Function

    Private Sub LoadForm(Mode As FormMode, Row As vBlog)

        grpSubmitMsg.Style.Add("display", "none")

        If Row IsNot Nothing Then BlogID = Row.BlogID


        LoadDetailsDropDowns()

        If Mode = FormMode.NewForm Then

            lblDetailsFormTitle.Text = "Add New"

            txtContent.Text = ""
            txtTitle.Text = ""
            txtSeoTitle.Text = ""
            txtSeoDescription.Text = ""
            txtNiceDescription.Text = ""
            ddlAuthor.SelectedValue = Nothing
            ddlCategories.SelectedValue = Nothing
            Try
                ddlCountrys.Items.FindByText("NA").Selected = True
            Catch ex As Exception
                ddlCountrys = Nothing
            End Try

            txtDate.Text = Date.Now
            chkShowPhotos.Checked = True

            ControlBinder.ClearDropDownItems(ddlImages)
            ControlBinder.ClearDropDownItems(ddlCategoryImages)

            imgAssociated.Visible = False
            imgCategory.Visible = False
            txtImageURL.Text = ""

            btnSubmitEditImages.Style.Add("display", "none")

        ElseIf Mode = FormMode.Edit Then

            lblDetailsFormTitle.Text = "Edit"

            txtContent.Text = Row.Content
            txtTitle.Text = Row.Title
            txtDate.Text = Row.Date
            txtSeoTitle.Text = BlankLibrary.NoBlank(Row.SEOTitle, "")
            txtSeoDescription.Text = BlankLibrary.NoBlank(Row.SEODescription, "")
            txtNiceDescription.Text = BlankLibrary.NoBlank(Row.NiceDescription, "")

            Try
                ddlAuthor.SelectedValue = BlankLibrary.NoNull(Row.AuthorID, Nothing)
                ddlCategories.SelectedValue = BlankLibrary.NoNull(Row.CategoryID, Nothing)
                ddlCountrys.SelectedValue = BlankLibrary.NoNull(Row.CountryID, Nothing)
                ddlCategoryImages.SelectedValue = BlankLibrary.NoNull(Row.CategoryImageID, Nothing)
            Catch ex As Exception

            End Try

            chkIsVisible.Checked = BlankLibrary.NoNull(Row.IsVisible, False)
            chkIsFullScreen.Checked = BlankLibrary.NoNull(Row.IsFullScreen, False)
            chkShowPhotos.Checked = Row.ShowPhotos

            btnSubmitEditImages.Style.Add("display", "block")

            GetDropdownImageURL()
            GetCategoryImageURL()

        End If

        ShowDetailsForm()

    End Sub

    Private Sub LoadDetailsDropDowns()

        ControlBinder.BindDropDown(Business.Country.SelectCountries(), "CountryID", "CountryDescription", ddlCountrys)
        ControlBinder.BindDropDown(Business.Author.GetAuthors.ToList, "AuthorID", "AuthorName", ddlAuthor)
        ControlBinder.BindDropDown(Business.Category.SelectCategories, "CategoryID", "CategoryDescription", ddlCategories)

        ''Linkd
        ControlBinder.BindDropDown(Business.ImageUpload.SelectImagesByBlog(BlogID), "ImageID", "ImageDescription", ddlImages)
        ControlBinder.BindDropDown(Business.ImageUpload.SelectImagesByBlog(BlogID), "ImageID", "ImageDescription", ddlCategoryImages)
      

    End Sub

    Private Sub ResetForm()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.NewClicked
        LoadForm(FormMode.NewForm, Nothing)
        HideSubmitMessage()
    End Sub

    Private Sub ShowDetailsForm()
        divDetails.Attributes.CssStyle.Add("display", "block")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub btnCategoryDetails_Click(sender As Object, e As EventArgs) Handles btnCategoryDetails.Click
        Response.Redirect(ResolveUrl("~/Admin/admin_categories.aspx"))
    End Sub

    Private Sub btnCountryDetails_Click(sender As Object, e As EventArgs) Handles btnCountryDetails.Click
        Response.Redirect(ResolveUrl("~/Admin/admin_countries.aspx"))
    End Sub

    Private Sub ddlImages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlImages.SelectedIndexChanged
        GetDropdownImageURL()
    End Sub

    Private Sub GetDropdownImageURL()
        If ddlImages.Items.Count > 0 Then
            txtImageURL.Text = ResolveUrl("~/images/uploads/medium/") + ddlImages.SelectedItem.Text
            imgAssociated.ImageUrl = ResolveUrl("~/images/uploads/thumbnail/") + ddlImages.SelectedItem.Text

            imgAssociated.Visible = True
        Else
            txtImageURL.Text = ""
            imgAssociated.Visible = False
        End If

    End Sub

    Private Sub ddlCategoryImages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoryImages.SelectedIndexChanged
        GetCategoryImageURL()
    End Sub

    Private Sub GetCategoryImageURL()
        imgCategory.ImageUrl = ResolveUrl("~/images/uploads/thumbnail/") + ddlCategoryImages.SelectedItem.Text

        If ddlCategoryImages.SelectedValue > 0 Then
            imgCategory.Visible = True
        Else
            imgCategory.Visible = False
        End If

    End Sub

#Region "Save"

    Private Function IsEdit() As Boolean
        If lblDetailsFormTitle.Text = "Edit" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SaveForm()
    End Sub

    Private Sub btnSubmitAddImages_Click(sender As Object, e As EventArgs) Handles btnSubmitAddImages.Click
        SaveForm()
        If IsEdit() Then
            Response.Redirect(ResolveUrl("~/admin/uploads/imageupload.aspx?AddBlogID=" & BlogID))
        Else
            Response.Redirect(ResolveUrl("~/admin/uploads/imageupload.aspx?AddBlogID=" & Business.Blog.GetMostRecent(True).BlogID))
        End If
    End Sub

    Private Sub btnSubmiteditImages_Click(sender As Object, e As EventArgs) Handles btnSubmitEditImages.Click
        SaveForm()
        Response.Redirect(ResolveUrl("/admin/uploads/imageupload.aspx?EditBlogID=" & BlogID))
    End Sub

    Private Sub SaveForm()

        If IsEdit() Then
            ''Update
            Business.Blog.Update(BlogID, txtTitle.Text, txtContent.Text, ddlAuthor.SelectedValue, ddlCountrys.SelectedValue,
                                 ddlCategories.SelectedValue, txtDate.Text, ddlCategoryImages.SelectedValue, chkIsVisible.Checked,
                                 chkIsFullScreen.Checked, txtSeoTitle.Text, txtSeoDescription.Text, txtNiceDescription.Text, chkShowPhotos.Checked)

        Else

            ''Insert
            Business.Blog.Insert(txtTitle.Text, ddlAuthor.SelectedValue, ddlCountrys.SelectedValue,
                            ddlCategories.SelectedValue, txtContent.Text, txtDate.Text, ddlCategoryImages.SelectedValue,
                            chkIsVisible.Checked, chkIsFullScreen.Checked, txtSeoTitle.Text, txtSeoDescription.Text, txtNiceDescription.Text, chkShowPhotos.Checked)

        End If

        ''Clear page, update cache
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        ResetForm()

        ShowSubmitMessage(IsEdit)

    End Sub

    Private Sub ShowSubmitMessage(IsEdit As Boolean)

        Dim Message As New StringBuilder
        Message.Append("Blog entry has been ")

        If IsEdit Then
            ''Display Message
            Message.Append("updated - ")
            Message.Append(Business.Blog.GetBlogItem(BlogID).Title)

            lnkBlog.NavigateUrl = ResolveUrl("~/blog/" + Business.Blog.GetBlogItem(BlogID).TitleURL)

            lnkEditBlog.NavigateUrl = ResolveUrl("~/admin/admin_blog.aspx?blogid=" & BlogID)

            lnkuploadImagesEdit.NavigateUrl = ResolveUrl("~/admin/uploads/imageupload.aspx?EditBlogID=" & BlogID)
            liImageEdit.Style.Add("display", "block")

            lnkuploadImagesAdd.NavigateUrl = ResolveUrl("~/admin/uploads/imageupload.aspx?AddBlogID=" & BlogID)
        Else
            Message.Append("created - ")
            Message.Append(Business.Blog.GetMostRecent(True).Title)
            lnkBlog.NavigateUrl = ResolveUrl("~/blog/" + Business.Blog.GetMostRecent(True).TitleURL)
            lnkEditBlog.NavigateUrl = ResolveUrl("~/admin/admin_blog.aspx?blogid=" & Business.Blog.GetMostRecent(True).BlogID)
            liImageEdit.Style.Add("display", "none")
            lnkuploadImagesAdd.NavigateUrl = ResolveUrl("~/admin/uploads/imageupload.aspx?AddBlogID=" & Business.Blog.GetMostRecent(True).BlogID)
        End If

        ''Display Message
        lblSubmitMessage.Text = Message.ToString
        ShowSubmitMessage()

    End Sub

    Private Sub HideSubmitMessage()
        grpSubmitMsg.Style.Add("display", "none")
    End Sub

    Private Sub ShowSubmitMessage()
        grpSubmitMsg.Style.Add("display", "block")
    End Sub
#End Region





#Region "Edit Grid"

    Private Sub grdBlogs_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdBlogs.PageIndexChanging
        grdBlogs.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Private Sub grdBlogs_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdBlogs.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim db As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)
            db.OnClientClick = "return confirm('Are you certain you want to delete this blog?');"
        End If

    End Sub

    Private Sub grdBlogs_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdBlogs.RowDeleting

        Try

            BlogID = grdBlogs.DataKeys(e.RowIndex).Value()
            Business.Blog.Delete(BlogID)
            LoadGrid()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub grdBlogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdBlogs.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As vBlog = Business.Blog.GetBlogItem(grdBlogs.DataKeys(grdBlogs.SelectedIndex).Value())
        LoadForm(FormMode.Edit, row)

        BlogID = grdBlogs.DataKeys(grdBlogs.SelectedIndex).Value()

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.EditClicked
        ShowEditGrid()
        HideSubmitMessage()
    End Sub

    Private Sub ShowEditGrid()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "block")
        LoadGrid()
        LoadFilterDropDowns()
    End Sub

    Private Sub LoadGrid()
        Dim Keys As Array = {"BlogID"}
        ControlBinder.BindGrid(Business.Blog.SelectBlogs(False, True), grdBlogs, Keys)
    End Sub


    Private Sub LoadFilterDropDowns()
        ControlBinder.BindDropDown(Business.Country.SelectCountries(), "CountryID", "CountryDescription", ddlCountryFilter)
        ControlBinder.BindDropDown(Business.Category.SelectCategories, "CategoryID", "CategoryDescription", ddlCategoryFilter)
    End Sub

    Private Sub ddlCategoryFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoryFilter.SelectedIndexChanged
        Dim Keys As Array = {"BlogID"}
        ControlBinder.BindGrid(Business.Blog.SelectBlogsByCategory(ddlCategoryFilter.SelectedValue, False, True), grdBlogs, Keys)

        ddlCountryFilter.SelectedValue = Nothing
    End Sub

    Private Sub ddlCountryFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountryFilter.SelectedIndexChanged
        Dim Keys As Array = {"BlogID"}
        ControlBinder.BindGrid(Business.Blog.SelectBlogsByCountry(ddlCountryFilter.SelectedValue, True), grdBlogs, Keys)

        ddlCategoryFilter.SelectedValue = Nothing
    End Sub

    Public Function BlogURL(ByVal URL As String) As String
        Return URLCreator.BlogURL(URL, False, False)
    End Function

    Public Sub ChangeVisibility(ByVal sender As Object, ByVal e As EventArgs)
 
        Dim box As CheckBox = DirectCast(sender, CheckBox)
        Dim row = DirectCast(box.Parent.Parent, GridViewRow)

        Dim BlogID As Integer = grdBlogs.DataKeys(row.RowIndex).Value


        Business.Blog.SetBlogVisibilty(BlogID, box.Checked)

        LoadGrid()

    End Sub

#End Region


End Class