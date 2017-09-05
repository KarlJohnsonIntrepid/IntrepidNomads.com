
Public Class EnvironmentLibrary

#Region " Http Objects"

    ''' <summary>CCC:
    ''' The current Response object
    ''' </summary>
    Public Shared ReadOnly Property Response() As HttpResponse
        Get
            Return HttpContext.Current.Response
        End Get
    End Property

    ''' <summary>CCC:
    ''' The current Request object
    ''' </summary>
    Public Shared ReadOnly Property Request() As HttpRequest
        Get
            Return HttpContext.Current.Request
        End Get
    End Property

    ''' <summary>CCC:
    ''' The current Server object
    ''' </summary>
    Public Shared ReadOnly Property Server() As HttpServerUtility
        Get
            Return HttpContext.Current.Server
        End Get
    End Property

    ''' <summary>CCC:
    ''' The current Session object
    ''' </summary>
    Public Shared ReadOnly Property Session() As SessionState.HttpSessionState
        Get
            Return HttpContext.Current.Session
        End Get
    End Property

    ''' <summary>CCC:
    ''' The current Page object
    ''' </summary>
    Public Shared ReadOnly Property CurrentPage() As Page
        Get
            Return TryCast(HttpContext.Current.Handler, Page)
        End Get
    End Property

    ''' <summary>CCC:
    ''' Dictionary of items associated with the current page, this is held for the duration of a request
    ''' </summary>
    Public Shared ReadOnly Property CurrentItems() As IDictionary
        Get
            Return TryCast(HttpContext.Current.Items, IDictionary)
        End Get
    End Property

    Public Shared ReadOnly Property IsServerTransfer() As Boolean
        Get
            Return HttpContext.Current.CurrentHandler.Equals(HttpContext.Current.Handler)
        End Get
    End Property

    'Public Shared ReadOnly Property IsPartialPostback As Boolean
    '    Get
    '        Return ScriptManager.GetCurrent(EnvironmentLibrary.CurrentPage).IsInAsyncPostBack
    '    End Get
    'End Property


#End Region

#Region " Application Values"

    ''' <summary>CCC:
    ''' Returns the root directory of the website e.g. '/MyWebSite'
    ''' </summary>
    Public Shared ReadOnly Property ApplicationVirtualPath() As String
        Get
            Return HttpRuntime.AppDomainAppVirtualPath
        End Get
    End Property

    ''' <summary>CCC:
    ''' Returns the root directory of the website without the forward slash eg 'MyWebSite'
    ''' </summary>
    Public Shared ReadOnly Property ApplicationRoot() As String
        Get
            Return Replace(ApplicationVirtualPath, "/", "")
        End Get
    End Property

    ''' <summary>CCC:
    ''' Returns the file path of the root directory of the website e.g. 'C:\inetpub\wwwroot\MyWebSite'
    ''' </summary>
    Public Shared ReadOnly Property ApplicationPhysicalPath() As String
        Get
            Return HttpRuntime.AppDomainAppPath
        End Get
    End Property

    ''' <summary>CCC:
    ''' Returns the version of IIS in which the application is hosted
    ''' </summary>
    Public Shared ReadOnly Property IISVersion() As String
        Get
            Return HttpContext.Current.Request.ServerVariables("SERVER_SOFTWARE")
        End Get
    End Property

#End Region

#Region " Page Values"

    ''' <summary>CCC:
    ''' Returns the page name from the file path e.g. 'MyPage.aspx'
    ''' </summary>
    Public Shared ReadOnly Property PageName() As String
        Get
            Dim arrPath() As String = Split(Request.FilePath, "/")
            Return arrPath(arrPath.GetUpperBound(0))
        End Get
    End Property

    ''' <summary>CCC:
    ''' Returns the current page name without the ".aspx"
    ''' </summary>
    Public Shared ReadOnly Property PageNameWithoutExtension() As String
        Get
            Return PageName.Split("."c)(0)
        End Get
    End Property

    ''' <summary>CCC:
    ''' Returns the folder of the current page e.g. '/MyWebSite/my_folder/'
    ''' </summary>
    Public Shared ReadOnly Property PageFolderPath() As String
        Get
            Return Left(Request.Path, Request.Path.LastIndexOf("/") + 1)
        End Get
    End Property

#End Region

End Class

