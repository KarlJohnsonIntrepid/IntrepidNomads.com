Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class ControlHelper

    Public Shared Sub HideControl(control As WebControl)
        control.Style.Add("display", "none")
    End Sub

    Public Shared Sub ShowControl(control As WebControl, Optional ByVal CSSClass As String = "block")
        control.Style.Add("display", CSSClass)
    End Sub


    Public Shared Sub HideGenericHTMLControl(control As HtmlGenericControl)
        control.Style.Add("display", "none")
    End Sub

    Public Shared Sub ShowGenericHTMLControl(control As HtmlGenericControl)
        control.Style.Add("display", "block")
    End Sub
End Class
