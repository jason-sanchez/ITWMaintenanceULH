Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class PhysicianGroupList
        Inherits NameValueListBase(Of Integer, String)

        Private Shared _PhysicianGroups As PhysicianGroupList

#Region " Business Methods "

        Public Shared Function GetDefaultGroup() As Integer
            Dim list As PhysicianGroupList

            list = GetPhysicianGroups()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No physician groups available; default group cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal GroupID As Integer) As Boolean
            For Each group In Me
                If group.Key = GroupID Then
                    Return True
                End If
            Next

            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetPhysicianGroups() As PhysicianGroupList
            Return DataPortal.Fetch(Of PhysicianGroupList)()
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

                    sql = "SELECT [ID], [Description] "
                    sql &= "FROM [114PhysGroup] "
                    sql &= "ORDER BY [Description] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetInt32("ID"), .GetString("Description")))
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