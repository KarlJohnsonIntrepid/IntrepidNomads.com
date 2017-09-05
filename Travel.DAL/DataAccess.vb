Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Travel.Common

Public Class DataAccess
    Private m_dbCommand As SqlCommand
    Private m_dbConnection As SqlConnection

    Public Sub New()
        'Set up the command and connection
        Connection = New SqlConnection(ConnectionString.GetDBConnectionString)
        Command = New SqlCommand
        'Set the Command and connection timeouts the same 
        Command.CommandTimeout = 30
        Command.Connection = Connection
    End Sub

#Region "Properties"

    Public Property SQL() As String
        Get
            Return Command.CommandText
        End Get

        Set(ByVal Value As String)
            Command.CommandText = Value
        End Set
    End Property

    Public Property ProcedureName() As String
        Get
            Return Command.CommandText
        End Get
        Set(ByVal Value As String)
            Command.CommandText = Value
            Command.CommandType = CommandType.StoredProcedure
        End Set
    End Property

    Protected Property Connection() As SqlConnection
        Get
            Return m_dbConnection
        End Get
        Set(ByVal Value As SqlConnection)
            m_dbConnection = Value
        End Set
    End Property

    Public Property Command() As SqlCommand
        Get
            Return m_dbCommand
        End Get
        Set(ByVal Value As SqlCommand)
            m_dbCommand = Value

        End Set
    End Property

#End Region

    Public Overloads Sub AddParameter(ByVal Name As String, ByVal Value As Object, ByVal Type As SqlDbType)
        AddParameter(Name, Value, Type, ParameterDirection.Input)
    End Sub

    Public Overloads Sub AddParameter(ByVal Name As String, ByVal Value As Object)
        'This one doesn't select the datatype of the parameter but lets .NET work it out for itself
        'Seems to work better for datetime parameters!
        Dim dbParameter As New SqlParameter
        'Add a parameter to the command
        dbParameter.ParameterName = Name
        dbParameter.Value = Value
        Command.Parameters.Add(dbParameter)
    End Sub

    Public Overloads Sub AddParameter(ByVal Name As String, ByVal Value As Object, ByVal Type As SqlDbType, ByVal Direction As ParameterDirection)
        Dim dbParameter As New SqlParameter
        'Add a parameter to the command
        dbParameter.ParameterName = Name
        dbParameter.Value = Value
        dbParameter.DbType = CType(Type, DbType)
        dbParameter.Direction = Direction
        Command.Parameters.Add(dbParameter)
    End Sub

    Public Function Parameter(ByVal Name As String) As SqlParameter
        Dim dbParameter As New SqlParameter
        dbParameter = Command.Parameters(Name)
        Return dbParameter
    End Function

    Public Sub GetData(ByRef DataStore As Object)
        Dim dataAdapter As New SqlDataAdapter

        'Use a data adapter to fill the datastore
        dataAdapter.SelectCommand = Command

        Try
            dataAdapter.Fill(DataStore)
        Catch ex As SqlException
            Throw ex
        End Try

    End Sub

    Public Sub GetData(ByRef DataStore As Object, ByVal CacheName As String, ReloadFromDataBase As Boolean)
        GetData(DataStore, CacheName, ReloadFromDataBase, 3600)
    End Sub

    Public Sub GetData(ByRef DataStore As Object, ByVal CacheName As String, ReloadFromDataBase As Boolean, CacheTimeInSeconds As Integer)
        Dim page = EnvironmentLibrary.CurrentPage

        Dim UseCache As Boolean

        If page Is Nothing Then
            UseCache = False
        End If

        If page IsNot Nothing AndAlso page.Cache(CacheName) IsNot Nothing Then
            UseCache = True
        End If

        If ReloadFromDataBase Then
            UseCache = False
        End If

        If UseCache Then
            DataStore = page.Cache(CacheName)
        Else
            Dim dataAdapter As New SqlDataAdapter

            'Use a data adapter to fill the datastore
            dataAdapter.SelectCommand = Command

            Try
                dataAdapter.Fill(DataStore)

                If page IsNot Nothing Then
                    page.Cache.Insert(CacheName, DataStore, Nothing,
                    DateTime.Now.AddSeconds(CacheTimeInSeconds),
                        TimeSpan.Zero)
                End If
            Catch ex As SqlException
                Throw ex
            End Try
        End If

    End Sub

    Public Function GetDataset() As DataSet

        Dim dataSet As DataSet = New DataSet

        Me.GetData(dataSet)
        Return dataSet

    End Function

    Public Function GetDataset(ByVal StartRecord As Integer, ByVal MaxRecord As Integer) As DataSet
        Dim dataAdapter As New SqlDataAdapter
        Dim dataSet As DataSet = New DataSet

        'Use a data adapter to fill the datastore
        dataAdapter.SelectCommand = Command

        Try
            dataAdapter.Fill(dataSet, StartRecord, MaxRecord, "Table")
        Catch ex As SqlException
            Throw ex
        End Try

        Return dataSet
    End Function

    Public Function GetDataset(ByVal ProcedureName As String) As DataSet
        'NB Dont put parameters in sp name (eg not spName @param1)
        'Add them as separate parameters and the command will pick them up

        'Set the SQL
        SQL = ProcedureName

        'Set the command so it will run an SP
        Command.CommandType = CommandType.StoredProcedure

        'Return the dataset
        Return GetDataset()

    End Function

    Public Overloads Function RunStoredProcedure() As Integer

        'NB Dont put parameters in sp name (eg not spName @param1)
        'Add them as separate parameters and the command will pick them up

        'Set the command so it will run an SP
        Command.CommandType = CommandType.StoredProcedure

        'Add a param to collect a value (can only get an integer)
        AddParameter("@ReturnValue", Nothing, SqlDbType.Int, ParameterDirection.ReturnValue)

        'Run the SP
        RunSQL()

        'Return the value of the param
        Return Command.Parameters.Item("@ReturnValue").Value
    End Function

    Public Overloads Function RunStoredProcedure(ByVal ProcedureName As String) As Integer
        'Set the SQL
        SQL = ProcedureName

        Return RunStoredProcedure()

    End Function

    Public Overloads Sub RunSQL()
        'Execute the SQL
        Connection.Open()
        Try
            Command.ExecuteNonQuery()
        Catch ex As SqlException
            Throw ex
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Overloads Sub RunSQL(ByVal SQL As String)
        Command.CommandText = SQL
        RunSQL()
    End Sub

    Public Function GetSingleValue() As Object
        'Execute the SQL and return a single value (first column of first row)
        Connection.Open()
        Try
            Return Command.ExecuteScalar()
        Catch ex As SqlException
            Throw ex
        Finally
            Connection.Close()
        End Try

    End Function

    Public Function GetSingleValue(ByVal SQL As String) As Object
        Command.CommandText = SQL
        Return GetSingleValue()
    End Function

    ''' <summary>
    ''' Used to Create an array of strings from the contents of a datacolumn
    ''' </summary>
    Public Function CreateStringArray(ByVal tbl As DataTable, ByVal ColumnName As String) As String()

        Dim objArray() As String = Nothing
        Dim upperBound As Integer = tbl.Rows.Count - 1

        If upperBound >= 0 Then
            ReDim objArray(upperBound)
            For i As Integer = 0 To upperBound
                objArray(i) = tbl.Rows(i).Item(ColumnName).ToString
            Next
        End If

        Return objArray
    End Function

    ''' <summary>
    ''' Used to Create an array of strings from the contents of 2 datacolumns
    ''' </summary>
    Public Function Create2DStringArray(ByVal tbl As DataTable, ByVal ColumnName As String, ByVal ColumnName2 As String) As String(,)

        Dim objArray(,) As String = Nothing
        Dim upperBound As Integer = tbl.Rows.Count - 1

        If upperBound >= 0 Then
            ReDim objArray(upperBound, 1)
            For i As Integer = 0 To upperBound
                objArray(i, 0) = tbl.Rows(i).Item(ColumnName).ToString
                objArray(i, 1) = tbl.Rows(i).Item(ColumnName2).ToString
            Next
        End If

        Return objArray
    End Function

End Class

Public Class IntegerNullable
    'Returns integer value or DBNull

    Dim m_intValue As Integer
    Dim m_blnHasValue As Boolean = True 'Default to true

    Public Property Value() As Object
        Get
            If HasValue Then
                Return m_intValue
            Else
                Return DBNull.Value
            End If
        End Get
        Set(ByVal Value As Object)
            If BlankLibrary.IsBlank(Value) Then
                HasValue = False
            Else
                m_intValue = Value
                HasValue = True
            End If
        End Set
    End Property

    Public Property HasValue() As Boolean
        Get
            Return m_blnHasValue
        End Get
        Set(ByVal Value As Boolean)
            m_blnHasValue = Value
        End Set
    End Property

End Class

Public Class DateNullable
    'Returns Date or DBNull

    Dim m_dtValue As Date
    Dim m_blnHasValue As Boolean = True 'Default to true

    Public Property Value() As Object
        Get
            If HasValue Then
                Return m_dtValue
            Else
                Return DBNull.Value
            End If
        End Get
        Set(ByVal Value As Object)
            If BlankLibrary.IsBlank(Value) Then
                HasValue = False
            Else
                m_dtValue = Value
            End If
        End Set
    End Property

    Public Property HasValue() As Boolean
        Get
            Return m_blnHasValue
        End Get
        Set(ByVal Value As Boolean)
            m_blnHasValue = Value
        End Set
    End Property

End Class

Public Class DataRowGenerator

    Private _TableName As String
    Private _KeyField As New List(Of String)
    Private _Row As DataRow
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private da As SqlDataAdapter
    Private bldr As SqlCommandBuilder
    Private ds As DataSet

    Private ReadOnly Property Table() As DataTable
        Get
            Return ds.Tables(0)
        End Get
    End Property


    ''' <summary>
    ''' KeyColumn will generated by TableNameID
    ''' </summary>
    ''' <param name="TableName">The name of the table to work with</param>
    Public Sub New(ByVal TableName As String)
        _TableName = TableName
        _KeyField.Add(TableName & "ID")
        InitialiseObjects()
    End Sub

    ''' <param name="TableName">The name of the table to work with</param>
    ''' <param name="KeyField">The field in the table that uniquely identifies the row. If nothing entered this will be constructed as TableName + "ID"</param>
    Public Sub New(ByVal TableName As String, ByVal KeyField As String)
        _TableName = TableName
        _KeyField.Add(KeyField)
        InitialiseObjects()
    End Sub

    Public Sub New(ByVal TableName As String, ByVal KeyFields As List(Of String))
        _TableName = TableName
        _KeyField = KeyFields
        InitialiseObjects()
    End Sub

    Private Sub InitialiseObjects()
        conn = New SqlConnection
        conn.ConnectionString = ConnectionString.GetDBConnectionString
        cmd = conn.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM " & _TableName & " WHERE 1=1" & BuildWHEREClause()
        da = New SqlDataAdapter(cmd)
        bldr = New SqlCommandBuilder(da)
        ds = New DataSet
    End Sub

    Private Function BuildWHEREClause() As String
        Dim strWHERE As String = ""
        For Each KeyColumn As String In _KeyField
            strWHERE &= " AND " & KeyColumn & " = @" & KeyColumn
        Next
        Return strWHERE
    End Function

    Private Overloads Sub AddParameter(ByVal value As Object)
        'Assume one one keyfield has been added
        Dim KeyField As String = _KeyField.Item(0)

        AddParameter(value, KeyField)

    End Sub

    Private Overloads Sub AddParameter(ByVal value As Object, ByVal KeyField As String)

        If Not cmd.Parameters.Contains("@" & KeyField) Then
            Dim param As SqlParameter = cmd.CreateParameter
            param.ParameterName = "@" & KeyField
            param.Value = value
            cmd.Parameters.Add(param)
        Else
            cmd.Parameters.Item("@" & KeyField).Value = value
        End If

    End Sub

    Private Sub AddParameters(ByVal IDValue As List(Of Integer))
        'Assume that there are the same number of ID values as there
        ' are parameters and that they are added in the same order!
        For i As Integer = 0 To _KeyField.Count - 1
            AddParameter(IDValue.Item(i), _KeyField.Item(i))
        Next

    End Sub

    Private Sub AddDummyParameters()
        For Each KeyField As String In _KeyField
            AddParameter(0, KeyField)
        Next

    End Sub


    Public Overloads ReadOnly Property Row(ByVal IDValue As Integer) As DataRow
        Get
            Dim values As New List(Of Integer)(New Integer() {IDValue})
            Return Row(values)

        End Get
    End Property

    ''' <param name="IDValues">The values of the primary key field to identify the row</param>
    Public Overloads ReadOnly Property Row(ByVal IDValues As List(Of Integer)) As DataRow
        Get
            If _Row Is Nothing Then
                AddParameters(IDValues)
                da.Fill(ds, _TableName)
                If Table.Rows.Count > 0 Then
                    _Row = ds.Tables(0).Rows(0)
                End If
            End If

            Return _Row

        End Get
    End Property

    Public ReadOnly Property NewRow() As DataRow
        Get
            Dim _NewRow As DataRow

            AddDummyParameters() 'Put in a dummy parameter to always return no data
            da.Fill(ds, _TableName)
            _NewRow = Table.NewRow
            Table.Rows.Add(_NewRow)

            Return _NewRow

        End Get
    End Property

    Public Sub Update()
        'Save the data back to the database
        da.Update(ds, _TableName)
    End Sub


End Class



