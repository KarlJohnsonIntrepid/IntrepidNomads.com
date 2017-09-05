Public Class admin_countries
    Inherits BasePage

    Private Property CountryID As Integer
        Get
            Return ViewState("CountryID")
        End Get
        Set(value As Integer)
            ViewState("CountryID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle = "Destination"

        If Not IsPostBack Then
            LoadForm("New", Nothing)
            LoadDropDowns()

            ShowEditGrid()
            LoadGrid()
        End If
    End Sub

    Private Sub ResetForm()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub


    Private Sub LoadDropDowns()
        ControlBinder.BindDropDown(Business.Continent.SelectContinents(), "ContinentID", "ContinentDescription", ddlContinent)
    End Sub

    Private Sub LoadForm(Mode As String, Row As vCountry)

        If Mode = "New" Then

            lblDetailsFormTitle.Text = "Add New"
            divDetails.Attributes("class") = "panel panel-primary"

            txtSeoTitle.Text = ""
            txtSeoDescription.Text = ""

            txtCountry.Text = ""
            txtContent.Text = ""
            ddlContinent.SelectedValue = Nothing
            txtCountryImage.Text = ""
        Else

            lblDetailsFormTitle.Text = "Edit"
            divDetails.Attributes("class") = "panel panel-default"

            txtSeoTitle.Text = BlankLibrary.NoBlank(Row.SEOTitle, "")
            txtSeoDescription.Text = BlankLibrary.NoBlank(Row.SEODescription, "")
            txtCountry.Text = Row.CountryDescription
            txtCountryImage.Text = BlankLibrary.NoBlank(Row.CountryImageID, "")
            txtContent.Text = BlankLibrary.NoBlank(Row.CountryInformation, "")
            ddlContinent.SelectedValue = Row.ContinentID

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
            Business.Country.Update(CountryID, txtCountry.Text, ddlContinent.SelectedValue, txtContent.Text, txtSeoTitle.Text, txtSeoDescription.Text, txtCountryImage.Text)
        Else

            Business.Country.Insert(txtCountry.Text, ddlContinent.SelectedValue, txtContent.Text, txtSeoTitle.Text, txtSeoDescription.Text, txtCountryImage.Text)
        End If

        RouteConfig.RegisterRoutes(RouteTable.Routes)
    End Sub


    Private Sub LoadImagePreview(ImageDescription As String)
        imgCountryImage.ImageUrl = ResolveUrl("~/images/uploads/thumbnail/") + ImageDescription
    End Sub

    Private Sub btnCheckImageID_Click(sender As Object, e As EventArgs) Handles btnCheckImageID.Click
        Save()
        ' Get the currently selected row using the SelectedRow property.
        Dim row As vCountry = Business.Country.GetCountryRow(CountryID)
        LoadForm("Edit", row)
    End Sub

#Region "Edit Grid"

    Private Sub grdBlogs_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCountries.PageIndexChanging
        grdCountries.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Private Sub grdBlogs_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdCountries.RowDeleting

        Try
            CountryID = grdCountries.DataKeys(e.RowIndex).Value()
            Business.Country.Delete(CountryID)

            LoadGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

      
    End Sub

    Private Sub grdBlogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCountries.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As vCountry = Business.Country.GetCountryRow(grdCountries.DataKeys(grdCountries.SelectedIndex).Value())
        LoadForm("Edit", row)

        CountryID = grdCountries.DataKeys(grdCountries.SelectedIndex).Value()

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.EditClicked
        ShowEditGrid()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim Keys As Array = {"CountryID"}
        ControlBinder.BindGrid(Business.Country.SelectCountries, grdCountries, Keys)
    End Sub

    Private Sub ShowEditGrid()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "block")
    End Sub

#End Region


End Class