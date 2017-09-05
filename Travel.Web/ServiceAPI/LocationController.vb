Imports System.Net
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports System.Net.Http

Public Class LocationController
    Inherits ApiController

    Private jss As New JavaScriptSerializer()

    ' GET api/<controller>
    Public Function GetValues() As String

        Dim googleDataTable = New Bortosky.Google.Visualization.GoogleDataTable(Business.Location.SelectLocationsForMap)
        Dim Json As String = googleDataTable.GetJson
        Return Json
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String

        If id = 0 Then
            Dim googleDataTable = New Bortosky.Google.Visualization.GoogleDataTable(Business.Location.SelectLastKnownLocationForMap)
            Dim Json As String = googleDataTable.GetJson

            Return Json
        End If

        Return "Not Configured"
    End Function

    ' POST api/<controller>
    <Authorize(Roles:="Administrators")>
    Public Function PostValue(<FromBody()> ByVal value As String)

        Try
            Dim Location As DAL.Location = jss.Deserialize(value, GetType(DAL.Location))
            Location.Date = Date.Now

            If Not Business.Location.EntryExistsForToday(Location) Then Business.Location.Insert(Location)

        Catch ex As Exception
            Return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message)
        End Try

        Return "Location Logged"

    End Function

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class
