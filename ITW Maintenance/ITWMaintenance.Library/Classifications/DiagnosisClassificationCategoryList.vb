Imports System.Data.SqlClient

Namespace Classifications

    <Serializable()> _
    Public Class DiagnosisClassificationCategoryList
        Inherits NameValueListBase(Of String, String)

        Private Shared _CategoryList As DiagnosisClassificationCategoryList

#Region " Business Methods "

        Public Shared Function GetDefaultCategory() As String
            Dim list As DiagnosisClassificationCategoryList = GetCategories()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No Diagnosis Classification Categories available; default category cannot be returned")
            End If
        End Function

        Public Overloads Shared Function Contains(ByVal Category As String) As Boolean
            For Each type As NameValuePair In GetCategories()
                If type.Key = Category Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Shared Function GetValueByKey(ByVal Key As String) As String
            For Each Type As NameValuePair In GetCategories()
                If Type.Key = Key Then
                    Return Type.Value
                End If
            Next

            ' Couldn't find the Value because this key wasn't in the list,
            ' so just return the key.
            Return Key
        End Function

        ''' <summary>
        ''' Clears the in-memory list cache
        ''' so the list of roles is reloaded on
        ''' next request.
        ''' </summary>
        Public Shared Sub InvalidateCache()
            _CategoryList = Nothing
        End Sub

#End Region

#Region " Factory Methods "

        Public Shared Function GetCategories() As DiagnosisClassificationCategoryList
            If _CategoryList Is Nothing Then
                _CategoryList = DataPortal.Fetch(Of DiagnosisClassificationCategoryList)()
            End If

            Return _CategoryList
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch()
            Me.RaiseListChangedEvents = False
            IsReadOnly = False

            Using Conn As New SqlConnection(Database.ITWConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [Category] "
                    sql &= "FROM [111DiagnosisClass] "
                    sql &= "GROUP BY [Category] "
                    sql &= "ORDER BY [Category] "

                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetString("Category"), .GetString("Category")))
                            End While
                        End With
                    End Using
                End Using
            End Using

            IsReadOnly = True
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace