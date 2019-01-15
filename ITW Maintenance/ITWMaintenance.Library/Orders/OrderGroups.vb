Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class OrderGroups
        Inherits NameValueListBase(Of String, String)

        Private Shared _OrderGroups As OrderGroups

#Region " Business Methods "

        Public Shared Function GetDefaultOrderGroup() As String
            Dim list As OrderGroups = GetOrderGroups()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No Order Groups available; default Group cannot be returned")
            End If
        End Function

        Public Overloads Shared Function Contains(ByVal OrderGroup As String) As Boolean
            For Each group As NameValuePair In GetOrderGroups()
                If group.Key = OrderGroup Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Shared Function GetValueByKey(ByVal Key As String) As String
            For Each group As NameValuePair In GetOrderGroups()
                If group.Key = Key Then
                    Return group.Value
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
            _OrderGroups = Nothing
        End Sub

#End Region

#Region " Factory Methods "

        Public Shared Function GetOrderGroups() As OrderGroups
            If _OrderGroups Is Nothing Then
                _OrderGroups = DataPortal.Fetch(Of OrderGroups)()
            End If

            Return _OrderGroups
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch()
            Me.RaiseListChangedEvents = False
            IsReadOnly = False

            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [OrderGroup] "
                    sql &= "FROM [OrderCatalog] "
                    sql &= "GROUP BY [OrderGroup] "
                    sql &= "ORDER BY [OrderGroup] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetString("OrderGroup"), .GetString("OrderGroup")))
                            End While
                        End With
                        IsReadOnly = True
                    End Using
                End Using
            End Using

            IsReadOnly = True
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace