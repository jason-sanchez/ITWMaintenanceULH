Imports System.Data.SqlClient

Namespace Interventions

    Namespace Utilities

        <Serializable()> _
        Public Class iPathBuilder
            Inherits CommandBase

            Private _Level2intVntID As Integer
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

            Public Sub New(ByVal Level2intVntID As Integer)
                Me._Level2intVntID = Level2intVntID
            End Sub

            Public Function BuildiPaths() As Boolean
                Dim result As iPathBuilder
                ' This will call the DataPortal_Execute() command on the current object (Me)
                result = DataPortal.Execute(Of iPathBuilder)(Me)

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
                            Dim level1ID As Integer

                            ' 1. Validate that we are starting with a Level 2 record
                            sql = "SELECT intVntLevel, ParentID "
                            sql &= "FROM [110IntVnt] "
                            sql &= "WHERE intVntID = " & Me._Level2intVntID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If Not dr.Read() Then
                                    Throw New Exception("Unable to locate Intervention ID #" & Me._Level2intVntID.ToString())
                                Else
                                    If dr.IsDBNull("intVntLevel") OrElse dr.GetInt32("intVntLevel") <> 2 Then
                                        Throw New Exception("Intervention ID #" & Me._Level2intVntID.ToString() & " is not a Level 2 record.")
                                    End If
                                End If

                                ' No errors, so read the parent
                                level1ID = dr.GetInt32("ParentID")
                            End Using


                            ' 2. Update the level 1 (this IntVnt's parent) first
                            sql = "UPDATE [110IntVnt] SET "
                            sql &= "iPath = iName "
                            sql &= "WHERE intVntID = " & level1ID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()


                            ' 3. Drill into the IntVnt level by level
                            '       and build the iPath as the Parent's iPath
                            '       plus the child's iName.
                            ' Start with the level 2 record (must include the ", ").
                            childIDs = level1ID.ToString() & ", "
                            While Not String.IsNullOrEmpty(childIDs)
                                ' Trim off the last ", "
                                childIDs = Left(childIDs, Len(childIDs) - 2)

                                ' 4. Update this set of children
                                sql = "UPDATE [110IntVnt] "
                                sql &= "SET iPath = LEFT(iPaths.NewiPath, 200) "
                                sql &= "FROM ("
                                ' The inner query builds the new short name for each form
                                sql &= "	SELECT c.intVntID, p.iPath + ', ' + c.iName as NewiPath"
                                sql &= "	FROM [110IntVnt] c "
                                sql &= "	INNER JOIN [110IntVnt] p ON c.ParentID = p.intVntID "
                                sql &= "	WHERE c.ParentID in (" & childIDs & ") "
                                sql &= ") AS iPaths "
                                sql &= "WHERE [110IntVnt].intVntID = iPaths.intVntID "
                                sql &= ""
                                ' Return the IDs of the children here so we can combine database trips
                                sql &= "SELECT intVntID "
                                sql &= "FROM [110IntVnt] "
                                sql &= "WHERE ParentID in (" & childIDs & ") "

                                cmd.CommandType = CommandType.Text
                                cmd.CommandText = sql

                                ' 5. Get the IDs of the child rows
                                Using dr As New SafeDataReader(cmd.ExecuteReader)
                                    childIDs = ""
                                    While dr.Read()
                                        childIDs &= dr.GetInt32("intVntID").ToString() & ", "
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