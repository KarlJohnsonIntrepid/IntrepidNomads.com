
Public Class ConnectionString

    Public Shared Function GetDBConnectionString() As String

        Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    End Function

End Class


