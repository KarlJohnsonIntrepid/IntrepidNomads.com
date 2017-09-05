Public Class Continent

#Region "Cache"

    Public Shared Property MyCache As List(Of DAL.Continent)
        Get
            Return HttpContext.Current.Cache("Continent")
        End Get
        Set(value As List(Of DAL.Continent))
            HttpContext.Current.Cache("Continent") = value
        End Set
    End Property

    Private Shared Sub UpdateCache()
        SelectContinents(True)
    End Sub

#End Region


    Public Shared Function SelectContinents(Optional ByVal ClearCache As Boolean = False) As List(Of DAL.Continent)

        If MyCache Is Nothing Or ClearCache Then
            Using Travel As New TravelEntities
                Dim q = From a In Travel.Continents
                        Order By a.ContinentDescription
                MyCache = q.ToList
            End Using

        End If
        Return MyCache

    End Function

    Public Shared Function GetContinentRow(ContinentID) As DAL.Continent

        Try
            Dim query = From b In SelectContinents()
                  Where b.ContinentID = ContinentID

            Return query.First
        Catch ex As Exception
            Return Nothing
        End Try
      
    End Function

    Public Shared Sub Update()

    End Sub

    Public Shared Sub Insert()

    End Sub

    Public Shared Sub Delete()

    End Sub


End Class
