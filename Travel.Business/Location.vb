Public Class Location

    Public Shared Property LastLocation As String
        Get
            Return HttpContext.Current.Cache("LastKnownLocation")
        End Get
        Set(value As String)
            HttpContext.Current.Cache.Add("LastKnownLocation", value, Nothing, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, Nothing)
        End Set
    End Property

    Public Shared Function SelectLocationsForMap() As DataTable
        Using Travel As New TravelEntities
            Dim q = From a In Travel.Locations
                    Order By a.Date Descending

            Dim dt = New DataTable()
            dt.Columns.Add("Lat", GetType(Decimal)).Caption = "Lat"
            dt.Columns.Add("Long", GetType(Decimal)).Caption = "Long"
            dt.Columns.Add("Name", GetType(String)).Caption = "Name"

            For Each Location As DAL.Location In q
                Dim row As DataRow = dt.NewRow
                row("Lat") = CDec(CStr(BlankLibrary.NoBlank(Location.Latitude, "0")))
                row("Long") = CDec(CStr(BlankLibrary.NoBlank(Location.Longitude, "0")))
                row("Name") = Location.Location1
                dt.Rows.Add(row)
            Next

            Return dt
        End Using

    End Function

    Public Shared Function SelectLastKnownLocationForMap() As DataTable
        Using Travel As New TravelEntities
            Dim q = From a In Travel.Locations
                    Order By a.Date Descending

            Dim dt = New DataTable()
            dt.Columns.Add("Lat", GetType(Decimal)).Caption = "Lat"
            dt.Columns.Add("Long", GetType(Decimal)).Caption = "Long"
            dt.Columns.Add("Name", GetType(String)).Caption = "Name"

            Dim Location As DAL.Location = q.FirstOrDefault

            Dim row As DataRow = dt.NewRow
            row("Lat") = CDec(CStr(Location.Latitude))
            row("Long") = CDec(CStr(Location.Longitude))
            row("Name") = Location.Location1
            dt.Rows.Add(row)

            Return dt
        End Using

    End Function

    Public Shared Function SelectLocations() As List(Of DAL.Location)
        Using Travel As New TravelEntities
            Dim q = From a In Travel.Locations
                    Order By a.Date Descending
            Return q.ToList
        End Using
    End Function

    Public Shared Function SetLastKnownLocation() As String

        Try
            If LastLocation IsNot Nothing Then Return LastLocation

            Using Travel As New TravelEntities
                Dim q = From a In Travel.Locations
                        Order By a.Date Descending
                LastLocation = q.First.Location1
                Return LastLocation
            End Using
        Catch ex As Exception
            Return "No Locations Set"
        End Try

    End Function

    Public Shared Function SelectLocation(ID As Integer) As DAL.Location
        Using Travel As New TravelEntities
            Dim q = From a In Travel.Locations
                    Where a.LocationID = ID
                    Order By a.Date Descending
            Return q.SingleOrDefault
        End Using
    End Function

    Public Shared Function EntryExistsForToday(ByVal Location As DAL.Location) As Boolean

        Dim Locations As List(Of DAL.Location) = Business.Location.SelectLocations

        Dim Exists As Boolean = False
        For Each L As DAL.Location In Locations

            If (L.Date IsNot Nothing) Then
                If ((CDate(L.Date).DayOfYear = Date.Now.DayOfYear) And (CDate(L.Date).Year = Date.Now.Year)) And L.Location1 = Location.Location1 Then
                    Exists = True
                End If
            End If

        Next

        Return Exists
    End Function

    Public Shared Sub Delete(LocationID As Integer)
        Using Travel As New TravelEntities
            Dim c = (From Location In Travel.Locations
                       Where Location.LocationID = LocationID).First

            Travel.Locations.Remove(c)
            Travel.SaveChanges()
        End Using

        SetLastKnownLocation()
    End Sub

    Public Shared Sub Insert(Location As DAL.Location)
        Using context As New TravelEntities
            context.Locations.Add(Location)
            context.SaveChanges()
        End Using
    End Sub

    Public Shared Sub Insert(CreatedDate As Date, Latitude As String, Longitude As String, Location As String)

        Dim L As New DAL.Location
        With L
            .Date = CreatedDate
            .Latitude = Latitude
            .Longitude = Longitude
            .Location1 = Location
        End With

        Using context As New TravelEntities
            context.Locations.Add(L)
            context.SaveChanges()
        End Using

        SetLastKnownLocation()



    End Sub

    Public Shared Sub Update(Location As DAL.Location, LocationID As Integer)

        Using Travel As New TravelEntities
            Dim c = (From L In Travel.Locations
                     Where L.LocationID = LocationID).First

            Travel.Entry(c).CurrentValues.SetValues(Location)
            Travel.SaveChanges()
        End Using

        SetLastKnownLocation()
    End Sub


    Public Shared Sub Update(LocationID As Integer, CreatedDate As Date, Latitude As String, Longitude As String, Location As String)

        Using Travel As New TravelEntities
            Dim c = (From L In Travel.Locations
                       Where L.LocationID = L.LocationID).First

            c.Date = CreatedDate
            c.Latitude = Latitude
            c.Longitude = Longitude
            c.Location1 = Location

            Travel.SaveChanges()
        End Using

        SetLastKnownLocation()
    End Sub


End Class
