Imports System.Web.UI.WebControls

Public Class ControlBinder

    Public Shared Sub BindGrid(ByVal DataSource As Object, grid As GridView, DataKeys() As String)


        grid.DataKeyNames = DataKeys
        grid.DataSource = DataSource
        grid.DataBind()

    End Sub

    Public Shared Sub BindDropDown(ByVal DataSource As Object, DataValueField As String, DataTextField As String, DropDown As DropDownList)

        ClearDropDownItems(DropDown)

        DropDown.DataValueField = DataValueField
        DropDown.DataTextField = DataTextField
        DropDown.DataSource = DataSource
        DropDown.DataBind()

    End Sub


    Public Shared Sub ClearDropDownItems(DropDown As DropDownList)
        ''Keep the null value dropdown item if added
        Dim Item As ListItem = DropDown.Items.FindByValue("0")
        DropDown.Items.Clear()
        If Item IsNot Nothing Then DropDown.Items.Add(Item)

    End Sub


End Class
