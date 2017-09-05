''' <summary>CCC:
''' Library class of methods to work with Blank (Empty String, Nothing or DBNull) values.<br/>
''' Using the shared methods from this class will typically save you a line or 2 of code especially when working with values where you don't care about the difference between "" (Empty String), Nothing (null) and DBNull.Value.
''' </summary>
''' <remarks>Featured:</remarks>
Public Class BlankLibrary

    Private Sub New()
        'Shared methods only, no need to create this class
    End Sub

    ''' <summary>CCC:
    ''' Function looks to see if value is blank (DBNull, Nothing and "" all equate to blank)
    ''' </summary>
    ''' <remarks>NOTE: From the users perspective, DBNull, Nothing and "" are all the same when displayed in a textbox! </remarks>
    Public Shared Function IsBlank(ByVal Value As Object) As Boolean
        If IsNull(Value) Then
            Return True
        Else
            Dim strValue As String = TryCast(Value, String)
            If strValue IsNot Nothing AndAlso strValue.Length = 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>CCC:
    ''' Function looks to see if value is blank (DBNull, Nothing, "" and "  " all equate to blank)
    ''' </summary>
    ''' <remarks>NOTE: From the users perspective, DBNull, Nothing and "" are all the same when displayed in a textbox! This function counts spaces as blank when doing the check</remarks>
    Public Shared Function IsBlankIgnoreSpaces(ByVal Value As Object) As Boolean
        If IsNull(Value) Then
            Return True
        Else
            Dim strValue As String = TryCast(Value, String)
            If strValue IsNot Nothing AndAlso strValue.Trim.Length = 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>CCC:
    ''' If Value is Blank (Nothing or "") then return ValueIfBlank, otherwise return Value
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ValueIfBlank"></param>
    ''' <returns></returns>
    ''' <remarks>NOTE: A Bit like NZ from VBA or IsNull from SQL</remarks>
    Public Shared Function NoBlank(ByVal Value As Object, ByVal ValueIfBlank As Object) As Object
        If IsBlank(Value) Then
            Return ValueIfBlank
        Else
            Return Value
        End If
    End Function

    ''' <summary>CCC:
    ''' If Value is Blank (Nothing or "") then return ValueIfBlank, otherwise return Value
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ValueIfBlank"></param>
    ''' <returns></returns>
    ''' <remarks>NOTE: A Bit like NZ from VBA or IsNull from SQL</remarks>
    Public Shared Function NoBlank(ByVal Value As String, ByVal ValueIfBlank As String) As String
        If IsBlank(Value) Then
            Return ValueIfBlank
        Else
            Return Value
        End If
    End Function

    ''' <summary>CCC:
    ''' If Value is Null (Nothing or DBNull) then return ValueIfNull, otherwise return Value
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ValueIfNull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NoNull(ByVal Value As Object, ByVal ValueIfNull As Object) As Object
        If IsNull(Value) Then
            Return ValueIfNull
        Else
            Return Value
        End If
    End Function

    ''' <summary>CCC:
    ''' If Value is Null (Nothing or DBNull) then return ValueIfDBNull, otherwise return Value
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ValueIfNull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NoNull(ByVal Value As String, ByVal ValueIfNull As String) As String
        If IsNull(Value) Then
            Return ValueIfNull
        Else
            Return Value
        End If
    End Function

    ''' <summary>CCC:
    ''' Return whether value is Null (Nothing or DBNull) 
    ''' </summary>
    ''' <param name="Value"></param>
    Public Shared Function IsNull(Value As Object) As Boolean
#If CCCPORTABLE Then
        If Value Is Nothing Then
            Return True
        ElseIf Value.GetType.Name = "DBNull" Then
            Return True
        Else
            Return False
        End If
#Else
        If Value Is Nothing OrElse Value Is DBNull.Value Then
            Return True
        Else
            Return False
        End If
#End If
    End Function

End Class
