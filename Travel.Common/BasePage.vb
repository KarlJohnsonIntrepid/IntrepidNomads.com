Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Net
Imports System.Text.RegularExpressions

Public Class BasePage
    Inherits Page

    ReadOnly Property MyMaster As BaseMaster
        Get
            Return CType(Me.Master, BaseMaster)
        End Get
    End Property

    ''' <summary>
    ''' This is the page title in the admin application
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageTitle As String
        Get
            Return MyMaster.PageTitle
        End Get
        Set(value As String)
            MyMaster.PageTitle = value
        End Set
    End Property

    ''' <summary>
    ''' This is the page title in the browser tab
    ''' </summary>
    Public Shadows Property Title As String
        Get
            If BlankLibrary.IsBlank(mTitle) Then Return "Intrepid Nomads - Budget Backpacking Couple"
            If mTitle.Length >= 65 Then
                Return mTitle.Substring(0, 65)
            Else
                Return mTitle
            End If
        End Get
        Set(value As String)
            mTitle = value
        End Set
    End Property
    Private Property mTitle As String

    ''' <summary>
    ''' This is the Meta deascription used for SEO
    ''' </summary>
    Public Shadows Property Description As String
        Get
            Return mDescription
        End Get
        Set(value As String)
            mDescription = value
        End Set
    End Property
    Private Property mDescription As String

    Public Shared Property CurrentPage As BasePage
        Get
            Return HttpContext.Current.Handler
        End Get
        Set(value As BasePage)

        End Set
    End Property

    Public Sub AddOpenGraphMetaTag(_Property As String, Content As String)
        Dim tag As HtmlMeta = New HtmlMeta()
        tag.Attributes.Add("property", _Property)
        tag.Content = Content '' // don't HtmlEncode() string. HtmlMeta already escapes characters.
        Page.Header.Controls.Add(tag)
    End Sub

    ''' <summary>CCC:
    ''' Get the control that caused this post back
    ''' </summary>
    Public Shared Function GetPostBackControl() As Control

        Dim PostBackControl As Control = Nothing

        'Get the control name from the hidden input tag __EVENTTARGET
        'Note: names of buttons clicked don't go into __EVENTTARGET
        Dim controlName As String = CurrentPage.Request.Params.Get("__EVENTTARGET")

        If Not BlankLibrary.IsBlank(controlName) Then
            PostBackControl = CurrentPage.FindControl(controlName)
        Else
            'Button clicks cause a page submit and therefore the button
            'must instead be retrieved from the form collection
            For Each ctrl As String In CurrentPage.Request.Form

                'This If block deals with ImageButton controls
                If ctrl.EndsWith(".x") Or ctrl.EndsWith(".y") Then
                    ctrl = ctrl.Substring(0, ctrl.Length - 2)
                End If

                Dim formControl As Control = CurrentPage.FindControl(ctrl)

                If TypeOf formControl Is Button Then
                    PostBackControl = formControl
                    Exit For
                End If

            Next

        End If

        Return PostBackControl

    End Function

    Public Shared Function RemoveHTMLTags(Text As String)
        Return WebUtility.HtmlDecode(Regex.Replace(Text, "<[^>]*(>|$)", String.Empty))
    End Function


End Class
