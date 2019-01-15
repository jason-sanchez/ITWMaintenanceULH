Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class DisciplineList
        Inherits NameValueListBase(Of Integer, String)

        Private Shared _Disciplines As DisciplineList

#Region " Business Methods "

        Public Shared Function GetDefaultDiscipline(ByVal TherapyDisciplinesOnly As Boolean) As Integer
            Dim list As DisciplineList

            If TherapyDisciplinesOnly Then
                list = GetTherapyDisciplines()
            Else
                list = GetAllDisciplines()
            End If

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No disciplines available; default discipline cannot be returned")
            End If
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetAllDisciplines() As DisciplineList
            Return DataPortal.Fetch(Of DisciplineList)(New FilteredCriteria(GetType(DisciplineList), True))
        End Function

        Public Shared Function GetTherapyDisciplines() As DisciplineList
            Return DataPortal.Fetch(Of DisciplineList)(New FilteredCriteria(GetType(DisciplineList), False))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Class FilteredCriteria
            Private _List As Object
            Private _IncludeReferenceOnly As Boolean = True

            Public ReadOnly Property List() As Object
                Get
                    Return Me._List
                End Get
            End Property

            Public ReadOnly Property IncludeReferenceOnly() As Boolean
                Get
                    Return Me._IncludeReferenceOnly
                End Get
            End Property

            Public Sub New(ByVal list As Object, ByVal IncludeReferenceOnly As Boolean)
                Me._List = list
                Me._IncludeReferenceOnly = IncludeReferenceOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As FilteredCriteria)
            Me.RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT DisID, DisName "
                    sql &= "FROM [116Discipline] "
                    If Not criteria.IncludeReferenceOnly Then
                        sql &= "WHERE referenceOnly = 0 "
                    End If
                    sql &= "ORDER BY DisName "

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
