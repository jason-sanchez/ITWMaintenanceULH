Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyInterventionList
        Inherits ReadOnlyListBase(Of ReadOnlyInterventionList, ReadOnlyIntervention)

#Region " Factory Methods "

        Public Shared Function GetInterventionList(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyInterventionList
            Return DataPortal.Fetch(Of ReadOnlyInterventionList)(New Criteria(ParentID, ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ParentID As Integer
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ParentID() As Integer
                Get
                    Return Me._ParentID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean)
                Me._ParentID = ParentID
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT i.[ID], i.[IntVntID], i.[ParentID], i.[iName] AS [IntVntName], "
                    sql &= "i.[dOrder] AS [DisplayOrder], i.[Inactive], i.[OneTime], i.[Billing], i.[iFinal] "
                    sql &= "FROM [110intVnt] i "
                    sql &= "WHERE i.[ParentID] = " & criteria.ParentID & " "
                    If criteria.ActiveOnly Then
                        sql &= "AND i.[Inactive] = 0 "
                    End If
                    sql &= "ORDER BY i.[dOrder] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyIntervention(dr))
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