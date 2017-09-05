Public Class Country

#Region "Cache"

    Public Shared Property MyCache As List(Of vCountry)
        Get
            Return HttpContext.Current.Cache("Country")
        End Get
        Set(value As List(Of vCountry))
            HttpContext.Current.Cache("Country") = value
        End Set
    End Property

    Private Shared Sub UpdateCache()
        SelectCountries(True)
    End Sub

#End Region

#Region "Select"
    ''' <summary>
    ''' This is not used yet, everything needs converted and cached
    ''' </summary>
    Public Shared Function SelectCountries(Optional ByVal ClearCache As Boolean = False) As List(Of vCountry)

        If MyCache Is Nothing Or ClearCache Then
            Using Travel As New TravelEntities
                Dim query = From Country In Travel.vCountries
                            Order By Country.CountryDescription

                MyCache = query.ToList
            End Using
        End If
        Return MyCache
    End Function

    Public Shared Function SelectCountriesNotNA() As List(Of vCountry)
        Return SelectCountries.Where(Function(x) x.CountryDescription <> "NA").ToList
    End Function

    Public Shared Function SelectCountriesOrderByContinent() As List(Of vCountry)
        Dim query = From c In SelectCountries()
                    Order By c.ContinentDescription Ascending, c.CountryDescription Ascending

        Return query.ToList

    End Function

    Public Shared Function GetCountryRow(CountryID) As vCountry
        Return (From b In SelectCountries()
                Where b.CountryID = CountryID).FirstOrDefault
    End Function

    Public Shared Function GetCountryByDescription(Description) As vCountry
        Return (From b In SelectCountries()
                Where b.CountryDescription = Description).FirstOrDefault
    End Function

    Public Shared Function GetCountries(ByVal NumberOfBlogs As Integer) As List(Of vCountry)

        Dim query = From b In SelectCountriesNotNA()
        Take NumberOfBlogs

        Return query.ToList

    End Function

    Public Shared Function GetCountriesWithInfo(ByVal NumberOfBlogs As Integer) As List(Of vCountry)

        Dim query = From b In SelectCountriesNotNA().Where(Function(x) x.CountryInformation IsNot Nothing)
        Take NumberOfBlogs

        Return query.ToList

    End Function

#End Region

#Region "Update\Insert\Delete"


    Public Shared Sub Insert(Country As DAL.Country)
        Using context As New TravelEntities
            context.Countries.Add(Country)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Insert(CountryDescription As String, ContinentID As Nullable(Of Integer), CountryInformation As String, SEOTitle As String, SEODescription As String, CountryImageID As Nullable(Of Integer))

        Dim Country As New DAL.Country
        With Country
            .CountryDescription = CountryDescription
            .ContinentID = ContinentID
            .CountryInformation = CountryInformation
            .SEOTitle = SEOTitle
            .SEODescription = SEODescription
            .CountryImageID = CountryImageID
        End With

        Using context As New TravelEntities
            context.Countries.Add(Country)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub


    Public Shared Sub Delete(CountryID As Integer)

        Using Travel As New TravelEntities
            Dim c = (From Country In Travel.Countries
                       Where Country.CountryID = CountryID).First

            Travel.Countries.Remove(c)
            Travel.SaveChanges()
        End Using

        UpdateCache()
    End Sub

    Public Shared Sub Update(CountryID As String, CountryDescription As String, ContinentID As Integer, CountryInformation As String, SEOTitle As String,
                             SEODescription As String, CountryImageID As Integer)


        Using Travel As New TravelEntities
            Dim c = (From Country In Travel.Countries
                     Where Country.CountryID = CountryID).First

            c.CountryDescription = CountryDescription
            c.ContinentID = ContinentID
            c.CountryInformation = CountryInformation
            c.SEOTitle = SEOTitle
            c.SEODescription = SEODescription
            c.CountryImageID = CountryImageID

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Update(Country As DAL.Country, Id As Integer)

        Using Travel As New TravelEntities
            Dim updated = (From c In Travel.Countries
                           Where c.CountryID = Id).First


            Travel.Entry(updated).CurrentValues.SetValues(Country)

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

#End Region

End Class
