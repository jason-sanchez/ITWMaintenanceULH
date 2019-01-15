Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvalLevel1FolderList
        Inherits ReadOnlyListBase(Of ReadOnlyEvalLevel1FolderList, ReadOnlyEvalLevel1Folder)

#Region " Factory Methods "

        Public Shared Function GetEvalLevel1FolderList(ByVal ActiveOnly As Boolean) As ReadOnlyEvalLevel1FolderList
            Return DataPortal.Fetch(Of ReadOnlyEvalLevel1FolderList)(New Criteria(ActiveOnly))
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

                    sql = "SELECT e.[ID], e.EvalID, e.EName as FolderName, "
                    sql &= "d.disName as Discipline, e.DOrder as DisplayOrder, e.Inactive "
                    sql &= "FROM [100Eval] e "
                    sql &= "INNER JOIN [116Discipline] d on e.discipline = d.disID "
                    sql &= "WHERE e.EvalLevel = 1 "
                    If criteria.ActiveOnly Then
                        sql &= "AND e.Inactive = 0 "
                    End If
                    sql &= "ORDER BY e.DOrder "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyEvalLevel1Folder(dr))
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