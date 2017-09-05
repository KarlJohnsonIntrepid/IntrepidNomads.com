Public Class admin_locations
    Inherits BasePage

    Private Property LocationID As Integer
        Get
            Return ViewState("LocationID")
        End Get
        Set(value As Integer)
            ViewState("LocationID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageTitle = "Locations"


        If Not IsPostBack Then
            LoadForm("New", Nothing)

            ShowEditGrid()
            LoadGrid()
        End If

    End Sub

    Private Sub ResetForm()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "none")
    End Sub

    Private Sub LoadForm(Mode As String, Row As DAL.Location)

        If Mode = "New" Then

            lblDetailsFormTitle.Text = "Add New"
            divDetails.Attributes("class") = "panel panel-primary"

            txtDate.Text = ""
            txtLocation.Text = ""
            txtLatitude.Text = ""
            txtLongitude.Text = ""
        Else

            lblDetailsFormTitle.Text = "Edit"
            divDetails.Attributes("class") = "panel panel-default"

            txtDate.Text = BlankLibrary.NoBlank(Row.Date, "")
            txtLocation.Text = BlankLibrary.NoBlank(Row.Location1, "")
            txtLatitude.Text = BlankLibrary.NoBlank(Row.Latitude, "")
            txtLongitude.Text = BlankLibrary.NoBlank(Row.Longitude, "")
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

        If lblDetailsFormTitle.Text = "Edit" Then
            Business.Location.Update(LocationID, txtDate.Text, txtLatitude.Text, txtLongitude.Text, txtLocation.Text)
        Else

            Business.Location.Insert(txtDate.Text, txtLatitude.Text, txtLongitude.Text, txtLocation.Text)
        End If

        RouteConfig.RegisterRoutes(RouteTable.Routes)

        LoadForm("New", Nothing)

        ResetForm()

    End Sub

#Region "Edit Grid"

    Private Sub grdBlogs_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdLocations.PageIndexChanging
        grdLocations.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Private Sub grdLocations_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdLocations.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim db As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)
            db.OnClientClick = "return confirm('Are you certain you want to delete this location?');"
        End If
    End Sub

    Private Sub grdBlogs_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdLocations.RowDeleting

        Try
            LocationID = grdLocations.DataKeys(e.RowIndex).Value()
            Business.Location.Delete(LocationID)

            LoadGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub grdBlogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdLocations.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As DAL.Location = Business.Location.SelectLocation(grdLocations.DataKeys(grdLocations.SelectedIndex).Value())
        LoadForm("Edit", row)

        LocationID = grdLocations.DataKeys(grdLocations.SelectedIndex).Value()

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnAddNewButtons.EditClicked
        ShowEditGrid()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim Keys As Array = {"LocationID"}
        ControlBinder.BindGrid(Business.Location.SelectLocations, grdLocations, Keys)
    End Sub

    Private Sub ShowEditGrid()
        divDetails.Attributes.CssStyle.Add("display", "none")
        divEditGrid.Attributes.CssStyle.Add("display", "block")
    End Sub

#End Region

  


End Class