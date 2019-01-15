Imports System.Data.SqlClient

Namespace Nursing

    Namespace Diagnoses

        <Serializable()> _
        Public Class ReadOnlyDiagnosisList
            Inherits ReadOnlyListBase(Of ReadOnlyDiagnosisList, ReadOnlyDiagnosis)

#Region " Factory Methods "

            Public Shared Function GetDiagnosisList(ByVal ActiveOnly As Boolean) As ReadOnlyDiagnosisList
                Return DataPortal.Fetch(Of ReadOnlyDiagnosisList)(New Criteria(ActiveOnly))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _ActiveOnly As Boolean

                Public ReadOnly Property ActiveOnly() As Boolean
                    Get
                        Return Me._ActiveOnly
                    End Get
                End Property

                Public Sub New(ByVal ActiveOnly As Boolean)
                    Me._ActiveOnly = ActiveOnly
                End Sub
            End Class

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                RaiseListChangedEvents = False

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], [Description], [Inactive] "
                        sql &= "FROM [120NursingDiagnosis] "
                        If criteria.ActiveOnly Then
                            sql &= "WHERE [Inactive] = 0 "
                        End If
                        sql &= "ORDER BY [Description] "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            IsReadOnly = False
                            While dr.Read()
                                Me.Add(New ReadOnlyDiagnosis(dr))
                            End While
                            IsReadOnly = True
                        End Using
                    End Using
                End Using

                RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace