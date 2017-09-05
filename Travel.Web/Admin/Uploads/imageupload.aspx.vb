Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class imageupload
    Inherits BasePage

    Private Property ImageID As Integer
        Get
            Return ViewState("ImageID")
        End Get
        Set(value As Integer)
            ViewState("ImageID") = value
        End Set
    End Property

    Private Property SelectedBlogID As Integer?
        Get
            Return Session("SelectedBlogID")
        End Get
        Set(value As Integer?)
            Session("SelectedBlogID") = value
        End Set
    End Property

    Property OriginalPath As String = Server.MapPath("~/images/uploads/original/")
    Property ThumbNailPath As String = Server.MapPath("~/images/uploads/thumbnail/")
    Property MediumPath As String = Server.MapPath("~/images/uploads/medium/")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PageTitle = "Upload Images"

        If Not IsPostBack Then
            ResetForm()

            LoadDropDowns()
            If TryLoadFromQueryString() = False Then
                LoadForm("EditGrid", Nothing)
            End If
        End If
    End Sub

    Private Function TryLoadFromQueryString() As Boolean
        If Not BlankLibrary.IsBlank(Request.QueryString("EditBlogID")) Then

            ddlBlogFilter.SelectedValue = Request.QueryString("EditBlogID")
            LoadForm("EditGrid", Nothing)
            ShowGoToBlogLink()

            Return True
        End If

        If Not BlankLibrary.IsBlank(Request.QueryString("AddBlogID")) Then

            ddlAssoicatedBlogID.SelectedValue = Request.QueryString("AddBlogID")
            SelectedBlogID = Request.QueryString("AddBlogID")
            LoadForm("New", Nothing)
            ShowGoToBlogLinkNew()
            Return True
        End If

        Return False
    End Function

    Private Sub ResetForm()
        divUploadImages.Attributes.CssStyle.Add("display", "none")
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub LoadForm(Mode As String, Row As vUploadedImage)

        If Mode = "New" Then

            lblDetailsFormTitle.Text = "Upload New Images"
            ShowUploadImages()

        ElseIf Mode = "Edit" Then

            lblDetailsFormTitle.Text = "Edit"

            If Row IsNot Nothing Then
                txtCaption.Text = Row.ImageCaption
                txtImageDescription.Text = Row.ImageDescription
                ddlBlog.SelectedValue = Row.BlogID
                imgPreview.ImageUrl = ResolveUrl("~/images/uploads/original/" + Row.ImageDescription)
            End If

            ShowDetailsForm()

        Else
            ShowEditGrid()
            LoadGrid()
        End If

    End Sub

    Private Sub LoadDropDowns()

        ControlBinder.BindDropDown(Business.Blog.SelectBlogs(False, True), "BlogID", "Title", ddlBlogFilter)
        ControlBinder.BindDropDown(Business.Blog.SelectBlogs(False, True), "BlogID", "Title", ddlBlog)
        ControlBinder.BindDropDown(Business.Blog.SelectBlogs(False, True), "BlogID", "Title", ddlAssoicatedBlogID)

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles admin_addnewbuttons.NewClicked
        LoadForm("New", Nothing)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles admin_addnewbuttons.EditClicked
        LoadForm("EditGrid", Nothing)
    End Sub

    Private Sub ShowDetailsForm()
        divUploadImages.Attributes.CssStyle.Add("display", "none")
        divDetails.Attributes.CssStyle.Add("display", "block")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub ShowEditGrid()
        divUploadImages.Attributes.CssStyle.Add("display", "none")
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "block")
    End Sub

    Private Sub ShowUploadImages()
        divUploadImages.Attributes.CssStyle.Add("display", "block")
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Business.ImageUpload.Update(ImageID, txtImageDescription.Text, txtCaption.Text, ddlBlog.SelectedValue)

        Dim row As vUploadedImage = Business.ImageUpload.GetImageRow(grdBlog.DataKeys(grdBlog.SelectedIndex).Value())

        Dim ImagePath As String = OriginalPath + row.ImageDescription
        Dim OriginalThumbNailPath As String = ThumbNailPath + row.ImageDescription
        Dim MediumImagePath As String = MediumPath + row.ImageDescription

        Dim NewPath As String = OriginalPath + txtImageDescription.Text
        Dim NewThumbNailPath As String = ThumbNailPath + txtImageDescription.Text
        Dim NewMediumPath As String = MediumPath + txtImageDescription.Text


        If ImagePath <> NewPath Then
            File.Move(ImagePath, NewPath)
            File.Move(OriginalThumbNailPath, NewThumbNailPath)
            File.Move(MediumImagePath, NewMediumPath)
        End If

        Business.ImageUpload.UpdateCache()

        LoadForm("EditGrid", Nothing)

    End Sub

#Region "Edit Grid"

    Private Sub ddlBlogFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlogFilter.SelectedIndexChanged
        LoadGrid()
        ShowGoToBlogLink()
    End Sub

    Private Sub grdBlogs_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdBlog.PageIndexChanging
        grdBlog.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Private Sub grdBlog_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdBlog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim db As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)
            db.OnClientClick = "return confirm('Are you certain you want to delete this image?');"
        End If
    End Sub

    Private Sub grdBlogs_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdBlog.RowDeleting

        ImageID = grdBlog.DataKeys(e.RowIndex).Value()

        Dim row As vUploadedImage = Business.ImageUpload.GetImageRow(ImageID)

        Dim ImagePath As String = Me.OriginalPath + row.ImageDescription
        Dim ThumbNailPath As String = Me.ThumbNailPath + row.ImageDescription
        Dim MediumImagePath As String = Me.MediumPath + row.ImageDescription

        File.Delete(ImagePath)
        File.Delete(ThumbNailPath)
        File.Delete(MediumImagePath)

        Business.ImageUpload.Delete(ImageID)

        LoadGrid()
    End Sub

    Private Sub grdBlogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdBlog.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As vUploadedImage = Business.ImageUpload.GetImageRow(grdBlog.DataKeys(grdBlog.SelectedIndex).Value())
        LoadForm("Edit", row)

        ImageID = grdBlog.DataKeys(grdBlog.SelectedIndex).Value()

    End Sub


    Private Sub LoadGrid()
        Dim Keys As Array = {"ImageID"}

        'filter for blog
        If Not BlankLibrary.IsBlank(ddlBlogFilter.SelectedValue) AndAlso ddlBlogFilter.SelectedValue > 0 Then
            ControlBinder.BindGrid(Business.ImageUpload.SelectImagesByBlog(ddlBlogFilter.SelectedValue), grdBlog, Keys)
        Else
            ControlBinder.BindGrid(Business.ImageUpload.SelectImages, grdBlog, Keys)
        End If

    End Sub

    Private Sub ShowGoToBlogLink()

        Dim QueryString As String = ""
        If Not BlankLibrary.IsBlank(ddlBlogFilter.SelectedValue) Then
            QueryString = "?blogid=" + ddlBlogFilter.SelectedValue
            lnkGoToBlog.Visible = True
        Else
            lnkGoToBlog.Visible = False
        End If

        lnkGoToBlog.NavigateUrl = "~/Admin/admin_blog.aspx" + QueryString
    End Sub

#End Region

#Region "File Upload"

    Private Sub ddlAssoicatedBlogID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssoicatedBlogID.SelectedIndexChanged
        ''Store the selected blog Id in session so it can be accessein the upload handler
        SelectedBlogID = ddlAssoicatedBlogID.SelectedValue

        ShowGoToBlogLinkNew()
    End Sub

    Private Sub ShowGoToBlogLinkNew()

        Dim QueryString As String = ""
        If Not BlankLibrary.IsBlank(ddlAssoicatedBlogID.SelectedValue) Then
            QueryString = "?blogid=" + ddlAssoicatedBlogID.SelectedValue
            lnkGoToBlogNew.Visible = True
        Else
            lnkGoToBlogNew.Visible = False
        End If

        lnkGoToBlogNew.NavigateUrl = "~/Admin/admin_blog.aspx" + QueryString
    End Sub


    Private Sub fileUplloadTest_UploadComplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles actFileUpload.UploadComplete
        Dim FileName As String = e.FileName.Replace(" ", "-")

        Dim ImagePath As String = OriginalPath + FileName
        Dim ThumbNail As String = ThumbNailPath + FileName
        Dim MediumImagePath As String = MediumPath + FileName

        Dim msImage As MemoryStream = New MemoryStream(e.GetContents)
        Dim ThumbImage As Image = Image.FromStream(msImage)
        Dim MediumImage As Image = Image.FromStream(msImage)

        ''Save the original file
        actFileUpload.SaveAs(ImagePath)

        'Create Thumbnail
        ImageCompressor.CompressAndShrinkImage(ThumbNail, ThumbImage, 120, 80, 25)

        'Create Medium Size Image
        ImageCompressor.SaveJpeg(MediumImagePath, MediumImage, 50)

        ThumbImage.Dispose()
        MediumImage.Dispose()
        msImage.Dispose()

        ''Save link to the file here in a database table so we can manage the pictures
        ''Save files to a database here so we can display the files in a grid.

        Dim BlogID As String = BlankLibrary.NoBlank(SelectedBlogID, "")
        Business.ImageUpload.Insert(FileName, Nothing, BlogID)


    End Sub

    Private Sub fileUplloadTest_UploadCompleteAll(sender As Object, e As AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs) Handles actFileUpload.UploadCompleteAll
        ''Reload the grid
        Business.ImageUpload.UpdateCache()
    End Sub


#End Region


   
   
  
   
   
   
End Class