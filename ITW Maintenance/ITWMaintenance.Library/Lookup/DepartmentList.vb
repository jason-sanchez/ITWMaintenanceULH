Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class DepartmentList
        Inherits NameValueListBase(Of String, String)

        Private Shared _Departments As DepartmentList

#Region " Business Methods "

        Public Shared Function GetDefaultDepartment(ByVal IntakeFacility As Integer) As String
            Dim list As DepartmentList

            list = GetDepartments()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No departments available; default department cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal Department As String) As Boolean
            For Each dept In Me
                If dept.Key = Department Then
                    Return True
                End If
            Next

            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetDepartments() As DepartmentList
            Return DataPortal.Fetch(Of DepartmentList)(New FilteredCriteria())
        End Function

        Public Shared Function GetDepartments(ByVal IntakeFacility As Integer) As DepartmentList
            Return DataPortal.Fetch(Of DepartmentList)(New FilteredCriteria(IntakeFacility))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Class FilteredCriteria
            Private _IntakeFacility As Integer = 0

            Public ReadOnly Property IntakeFacility() As Integer
                Get
                    Return Me._IntakeFacility
                End Get
            End Property

            Public Sub New()
                Me._IntakeFacility = 0
            End Sub

            Public Sub New(ByVal IntakeFacility As Integer)
                Me._IntakeFacility = IntakeFacility
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Me.RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [Department], [Description] "
                    sql &= "FROM [130Department] "
                    If criteria.IntakeFacility > 0 Then
                        sql &= "WHERE [IntakeFacility] = " & criteria.IntakeFacility & " "
                    End If
                    sql &= "GROUP BY [Department], [Description] "
                    sql &= "ORDER BY [Description] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        With dr
                            While .Read()
                                Me.Add(New NameValuePair(.GetString("Department"), .GetString("Description")))
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