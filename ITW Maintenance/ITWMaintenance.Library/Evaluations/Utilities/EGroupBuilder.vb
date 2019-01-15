Imports System.Data.SqlClient

Namespace Evaluations

    Namespace Utilities

        <Serializable()> _
        Public Class EGroupBuilder
            Inherits CommandBase

            Private _Level2EvalID As Integer
            Private _CompletedSuccessfully As Boolean = False
            Private _Error As Exception

            Public ReadOnly Property CompletedSuccessfully() As Boolean
                Get
                    Return Me._CompletedSuccessfully
                End Get
            End Property

            Public ReadOnly Property TheError() As Exception
                Get
                    Return Me._Error
                End Get
            End Property

            Public Sub New(ByVal Level2EvalID As Integer)
                Me._Level2EvalID = Level2EvalID
            End Sub

            Public Function BuildEGroups() As Boolean
                Dim result As EGroupBuilder
                ' This will call the DataPortal_Execute() command on the current object (Me)
                result = DataPortal.Execute(Of EGroupBuilder)(Me)

                ' If we encountered an error, throw it
                If Not IsNothing(result.TheError) Then
                    Throw result.TheError
                End If

                Return result.CompletedSuccessfully
            End Function

            Protected Overrides Sub DataPortal_Execute()
                Using conn As New SqlConnection(Database.ITWConnection)
                    Try
                        conn.Open()

                        Using cmd As SqlCommand = conn.CreateCommand
                            Dim sql As String
                            Dim childIDs As String = ""

                            ' 1. Validate that we are starting with a Level 2 record
                            sql = "SELECT EvalLevel "
                            sql &= "FROM [100Eval] "
                            sql &= "WHERE EvalID = " & Me._Level2EvalID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If Not dr.Read() Then
                                    Throw New Exception("Unable to locate Eval ID #" & Me._Level2EvalID.ToString())
                                Else
                                    If dr.IsDBNull("EvalLevel") OrElse dr.GetInt32("EvalLevel") <> 2 Then
                                        Throw New Exception("Eval ID #" & Me._Level2EvalID.ToString() & " is not a Level 2 record.")
                                    End If
                                End If
                            End Using


                            ' 2. Update all children (level 3 records) and set the eGroup to their EName
                            sql = "UPDATE [100Eval] SET "
                            sql &= "eGroup = EName "
                            sql &= "WHERE ParentID = " & Me._Level2EvalID.ToString() & " "
                            ' Return the IDs of the children here so we can combine database trips
                            sql &= "SELECT EvalID "
                            sql &= "FROM [100Eval] "
                            sql &= "WHERE ParentID = " & Me._Level2EvalID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            ' 3. Get the IDs of the child rows
                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                While dr.Read()
                                    childIDs &= dr.GetInt32("EvalID").ToString() & ", "
                                End While
                            End Using


                            ' 4. Keep drilling into the eval level by level
                            '       and build the ShortName as the Parent's ShortName
                            '       plus the child's EName.
                            While Not String.IsNullOrEmpty(childIDs)
                                ' Trim off the last ", "
                                childIDs = Left(childIDs, Len(childIDs) - 2)

                                ' 5. Update the child forms first
                                '       For forms, the eGroup is the parent's eGroup.
                                sql = "UPDATE [100Eval] "
                                sql &= "SET eGroup = LEFT(eGroups.NewEGroup, 200) "
                                sql &= "FROM ("
                                ' The inner query builds the new eGroup for each form
                                sql &= "	SELECT c.EvalID, p.eGroup as NewEGroup"
                                sql &= "	FROM [100Eval] c "
                                sql &= "	INNER JOIN [100Eval] p ON c.ParentID = p.EvalID "
                                sql &= "	WHERE c.ParentID in (" & childIDs & ") "
                                sql &= "    AND c.EFinal = 1 "
                                sql &= ") AS eGroups "
                                sql &= "WHERE [100Eval].EvalID = eGroups.EvalID "

                                cmd.CommandType = CommandType.Text
                                cmd.CommandText = sql

                                ' No reason to return the child form IDs since we can't have
                                ' a form below a form.
                                cmd.ExecuteNonQuery()


                                ' 6. Update the child folders now
                                '       For folders, the eGroup is the parent's eGroup plus the folder's EName.
                                sql = "UPDATE [100Eval] "
                                sql &= "SET eGroup = LEFT(eGroups.NewEGroup, 200) "
                                sql &= "FROM ("
                                ' The inner query builds the new eGroup for each form
                                sql &= "	SELECT c.EvalID, p.eGroup + ', ' + c.EName as NewEGroup"
                                sql &= "	FROM [100Eval] c "
                                sql &= "	INNER JOIN [100Eval] p ON c.ParentID = p.EvalID "
                                sql &= "	WHERE c.ParentID in (" & childIDs & ") "
                                sql &= "    AND c.EFinal = 0 "
                                sql &= ") AS eGroups "
                                sql &= "WHERE [100Eval].EvalID = eGroups.EvalID "
                                sql &= ""
                                ' Return the IDs of the children here so we can combine database trips
                                sql &= "SELECT EvalID "
                                sql &= "FROM [100Eval] "
                                sql &= "WHERE ParentID in (" & childIDs & ") "

                                cmd.CommandType = CommandType.Text
                                cmd.CommandText = sql

                                ' 7. Get the IDs of the child rows
                                Using dr As New SafeDataReader(cmd.ExecuteReader)
                                    childIDs = ""
                                    While dr.Read()
                                        childIDs &= dr.GetInt32("EvalID").ToString() & ", "
                                    End While
                                End Using
                            End While
                        End Using

                        Me._CompletedSuccessfully = True
                    Catch ex As Exception
                        Me._CompletedSuccessfully = False
                        Me._Error = ex
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Sub

        End Class

    End Namespace

End Namespace