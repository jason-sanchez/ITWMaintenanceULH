Imports System.Data.SqlClient

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class DisciplinesWithNursingNotesList
            Inherits NameValueListBase(Of Integer, String)

#Region " Business Methods "

            Public Overloads Function Contains(ByVal DisciplineID As Integer) As Boolean
                For Each discipline In Me
                    If discipline.Key = DisciplineID Then
                        Return True
                    End If
                Next

                Return False
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function GetDisciplines() As DisciplinesWithNursingNotesList
                Return DataPortal.Fetch(Of DisciplinesWithNursingNotesList)()
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

                        sql = "SELECT d.DisID, d.DisName "
                        sql &= "FROM [100CrNt] c "
                        sql &= "INNER JOIN [116Discipline] d ON c.discipline = d.DisID "
                        sql &= "GROUP BY d.DisID, d.DisName "
                        sql &= "ORDER BY d.DisID, d.DisName "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            IsReadOnly = False
                            With dr
                                While .Read()
                                    Me.Add(New NameValuePair(.GetInt32("DisID"), .GetString("DisName")))
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

End Namespace