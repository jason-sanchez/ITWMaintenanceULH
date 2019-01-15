Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class FormValidationList
        Inherits NameValueListBase(Of Integer, String)

        Private Shared _FormValidationList As FormValidationList

#Region " Business Methods "

        Public Shared Function GetDefaultValidationCode() As Integer
            Dim list As FormValidationList

            list = GetValidationCodes()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No validation codes available; default validation code cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal ValidationCodeID As Integer) As Boolean
            For Each code In Me
                If code.Key = ValidationCodeID Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Shared Function GetValueByKey(ByVal Key As String) As String
            For Each Type As NameValuePair In GetValidationCodes()
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
            _FormValidationList = Nothing
        End Sub

#End Region

#Region " Factory Methods "

        Public Shared Function GetValidationCodes() As FormValidationList
            Return DataPortal.Fetch(Of FormValidationList)()
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch()
            Me.RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ColumnFormatCode], [ColumnFormatDesc] "
                    sql &= "FROM [190FormFieldValidation] "
                    sql &= "ORDER BY [ColumnFormatDesc] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetInt32("ColumnFormatCode"), .GetString("ColumnFormatDesc") & " [" & .GetInt32("ColumnFormatCode").ToString() & "]"))
                            End While
                        End With
                        IsReadOnly = True
                    End Using
                End Using
            End Using

            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace