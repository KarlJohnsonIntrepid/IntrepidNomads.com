Public Class admin_categories
    Inherits BasePage


    Private Property CategoryID As Integer
        Get
            Return ViewState("CategoryID")
        End Get
        Set(value As Integer)
            ViewState("CategoryID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle = "Categories"

        If Not IsPostBack Then
            LoadForm("New", Nothing)
            LoadDropDowns()

            ShowEditGrid()
            LoadGrid()

        End If
    End Sub


    Private Sub LoadDropDowns()

        ControlBinder.BindDropDown(Business.Category.SelectCategories, "CategoryID", "CategoryDescription", ddlShowFeature)
        ControlBinder.BindDropDown(Business.Country.SelectCountries, "CountryID", "CountryDescription", ddlShowFeatureCountry)

    End Sub

    Private Sub ResetForm()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub LoadForm(Mode As String, Row As vCategory)

        If Mode = "New" Then

            lblDetailsFormTitle.Text = "Add New"


            txtCategory.Text = ""
            txtContent.Text = ""
            txtSeoTitle.Text = ""
            txtSeoDescription.Text = ""
            chkFeature.Checked = False
            chkReverseOrder.Checked = False
            chkShowInMenu.Checked = False
            ddlShowFeature.SelectedValue = 0
            ddlShowFeatureCountry.SelectedValue = 0
            txtCategoryImage.Text = ""
        Else

            lblDetailsFormTitle.Text = "Edit"

            txtCategory.Text = Row.CategoryDescription
            txtContent.Text = BlankLibrary.NoBlank(Row.CategoryInformation, "")
            txtSeoTitle.Text = BlankLibrary.NoBlank(Row.SEOTitle, "")
            txtSeoDescription.Text = BlankLibrary.NoBlank(Row.SEODescription, "")
            txtCategoryImage.Text = BlankLibrary.NoBlank(Row.CategoryImageID, "")

            chkFeature.Checked = BlankLibrary.NoBlank(Row.IsFeature, False)
            chkReverseOrder.Checked = BlankLibrary.NoBlank(Row.ReverseDateOrder, False)
            chkShowInMenu.Checked = BlankLibrary.NoBlank(Row.ShowInMenu, False)
            ddlShowFeature.SelectedValue = BlankLibrary.NoBlank(Row.ParentCategoryID, 0)
            ddlShowFeatureCountry.SelectedValue = BlankLibrary.NoBlank(Row.ParentCountryID, 0)

            LoadImagePreview(Row.ImageDescription)
        End If

        ShowDetailsForm()

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.NewClicked
        LoadForm("New", Nothing)
    End Sub

    Private Sub ShowDetailsForm()
        divDetails.Attributes.CssStyle.Add("display", "block")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Save()

        LoadForm("New", Nothing)
        ResetForm()
    End Sub

    Private Sub Save()

        If lblDetailsFormTitle.Text = "Edit" Then
            Business.Category.Update(CategoryID, txtCategory.Text, txtContent.Text, chkReverseOrder.Checked, chkFeature.Checked, _
                                     ddlShowFeature.SelectedValue, ddlShowFeatureCountry.SelectedValue, txtSeoTitle.Text, txtSeoDescription.Text, chkShowInMenu.Checked, txtCategoryImage.Text)
        Else

            Business.Category.Insert(txtCategory.Text, txtContent.Text, chkReverseOrder.Checked, chkFeature.Checked, _
                                     ddlShowFeature.SelectedValue, ddlShowFeatureCountry.SelectedValue, txtSeoTitle.Text, txtSeoDescription.Text, chkShowInMenu.Checked, txtCategoryImage.Text)
        End If

        RouteConfig.RegisterRoutes(RouteTable.Routes)

    End Sub

    Private Sub LoadImagePreview(ImageDescription As String)
        imgCategoryImage.ImageUrl = ResolveUrl("~/images/uploads/thumbnail/") + ImageDescription
    End Sub

    Private Sub btnCheckImageID_Click(sender As Object, e As EventArgs) Handles btnCheckImageID.Click
        Save()
        Dim row As vCategory = Business.Category.GetCategoryRow(CategoryID)
        LoadForm("Edit", row)
    End Sub

#Region "Edit Grid"

    Private Sub grdBlogs_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCategory.PageIndexChanging
        grdCategory.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Private Sub grdBlogs_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdCategory.RowDeleting
        Try
            CategoryID = grdCategory.DataKeys(e.RowIndex).Value()
            Business.Category.Delete(CategoryID)
            LoadGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub grdBlogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCategory.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As vCategory = Business.Category.GetCategoryRow(grdCategory.DataKeys(grdCategory.SelectedIndex).Value())
        LoadForm("Edit", row)

        CategoryID = grdCategory.DataKeys(grdCategory.SelectedIndex).Value()

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.EditClicked
        ShowEditGrid()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim Keys As Array = {"CategoryID"}
        ControlBinder.BindGrid(Business.Category.SelectCategories, grdCategory, Keys)
    End Sub

    Private Sub ShowEditGrid()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "block")
    End Sub

#End Region

End Class