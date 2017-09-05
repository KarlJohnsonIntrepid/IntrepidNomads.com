Public Class admin_home
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadSlideshow()
        End If
    End Sub

    Private Sub LoadSlideshow()

        ''Bind All dropdowns 
        For x = 1 To 6
            ControlBinder.BindDropDown(Business.Blog.SelectBlogs, "BlogID", "Title", GetImageDropDown(x))
        Next

        ''Set values
        Dim Count = 1
        For Each Row As vBlog In Business.Blog.SelectBlogs.AsEnumerable.Where(Function(x) x.ShowInSlideShow = True)
            GetImageDropDown(Count).SelectedValue = Row.BlogID
            Count += 1
        Next

    End Sub

    Private Function GetImageDropDown(Id As Integer) As DropDownList
        Select Case Id
            Case 1
                Return ddlSlideShow1
            Case 2
                Return ddlSlideShow2
            Case 3
                Return ddlSlideShow3
            Case 4
                Return ddlSlideShow4
            Case 5
                Return ddlSlideShow5
            Case 6
                Return ddlSlideShow6
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub SaveSlideShow()

        ''Set all in slide show to be false
        Business.Blog.SetSlideShowFalse()

        ''Update based on the dropdown
        Dim DDLS() As DropDownList = {ddlSlideShow1, ddlSlideShow2, ddlSlideShow3, ddlSlideShow4, ddlSlideShow5, ddlSlideShow6}

        For Each DropDown As DropDownList In DDLS
            If Not BlankLibrary.IsBlank(DropDown.SelectedValue) Then
                Business.Blog.SetSlideShowTrue(DropDown.SelectedValue)
            End If
        Next

    End Sub


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SaveSlideShow()
    End Sub
End Class