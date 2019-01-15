Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class ServiceTypeList
        Inherits NameValueListBase(Of Integer, String)

        Private Shared _ServiceTypes As ServiceTypeList

#Region " Business Methods "

        Public Shared Function GetDefaultServiceType() As Integer
            Dim list As ServiceTypeList

            list = GetServiceTypes()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No service types available; default type cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal ServiceTypeID As Integer) As Boolean
            For Each type In Me
                If type.Key = ServiceTypeID Then
                    Return True
                End If
            Next

            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetServiceTypes() As ServiceTypeList
            Return DataPortal.Fetch(Of ServiceTypeList)()
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

                    sql = "SELECT [svcType], [Description] "
                    sql &= "FROM [130ServiceType] "
                    sql &= "ORDER BY [Description] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetInt32("svcType"), .GetString("Description")))
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