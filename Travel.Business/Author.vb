Public Class Author

    Public Shared Function GetAuthors() As List(Of DAL.Author)

        Using Travel As New TravelEntities
            Dim q = From a In Travel.Authors
                    Order By a.AuthorName
            Return q.ToList
        End Using

    End Function

End Class
