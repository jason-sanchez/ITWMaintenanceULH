Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class FacilityList
        Inherits NameValueListBase(Of Integer, String)

        Private Shared _Facilities As FacilityList

#Region " Business Methods "

        Public Shared Function GetDefaultFacility() As Integer
            Dim list As FacilityList

            list = GetFacilities()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No facilities available; default facility cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal FacilityID As Integer) As Boolean
            For Each facility In Me
                If facility.Key = FacilityID Then
                    Return True
                End If
            Next

            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetFacilities() As FacilityList
            Return DataPortal.Fetch(Of FacilityList)()
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

                    sql = "SELECT [ID], [Name] "
                    sql &= "FROM [115NetworkFac] "
                    sql &= "ORDER BY [Name] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetInt32("ID"), .GetString("Name")))
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