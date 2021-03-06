Imports System.Data.SqlClient

Namespace Interventions

    Namespace Utilities

        <Serializable()> _
        Public Class ShortNameBuilder
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

            Public Function BuildShortNames() As Boolean
                Dim result As ShortNameBuilder
                ' This will call the DataPortal_Execute() command on the current object (Me)
                result = DataPortal.Execute(Of ShortNameBuilder)(Me)

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
                            sql = "SELECT IntVntLevel "
                            sql &= "FROM [110IntVnt] "
                            sql &= "WHERE intVntID = " & Me._Level2intVntID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If Not dr.Read() Then
                                    Throw New Exception("Unable to locate Interventions ID #" & Me._Level2intVntID.ToString())
                                Else
                                    If dr.IsDBNull("intVntLevel") OrElse dr.GetInt32("intVntLevel") <> 2 Then
                                        Throw New Exception("Intervention ID #" & Me._Level2intVntID.ToString() & " is not a Level 2 record.")
                                    End If
                                End If
                            End Using


                            ' 2. Update all children (level 3 records) and set the ShortName to their iName
                            sql = "UPDATE [110IntVnt] SET "
                            sql &= "ShortName = iName "
                            sql &= "WHERE ParentID = " & Me._Level2intVntID.ToString() & " "
                            ' Return the IDs of the children here so we can combine database trips
                            sql &= "SELECT intVntID "
                            sql &= "FROM [110IntVnt] "
                            sql &= "WHERE ParentID = " & Me._Level2intVntID.ToString() & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            ' 3. Get the IDs of the child rows
                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                While dr.Read()
                                    childIDs &= dr.GetInt32("intVntID").ToString() & ", "
                                End While
                            End Using


                            ' 4. Keep drilling into the IntVnt level by level
                            '       and build the ShortName as the Parent's ShortName
                            '       plus the child's iName.
                            While Not String.IsNullOrEmpty(childIDs)
                                ' Trim off the last ", "
                                childIDs = Left(childIDs, Len(childIDs) - 2)

                                ' 5. Update this set of children
                                sql = "UPDATE [110IntVnt] "
                                sql &= "SET ShortName = LEFT(shortNames.NewShortName, 200) "
                                sql &= "FROM ("
                                ' The inner query builds the new short name for each form
                                sql &= "	SELECT c.intVntID, p.ShortName + ', ' + c.iName as NewShortName"
                                sql &= "	FROM [110IntVnt] c "
                                sql &= "	INNER JOIN [110IntVnt] p ON c.ParentID = p.intVntID "
                                sql &= "	WHERE c.ParentID in (" & childIDs & ") "
                                sql &= ") AS shortNames "
                                sql &= "WHERE [110IntVnt].intVntID = shortNames.intVntID "
                                sql &= ""
                                ' Return the IDs of the children here so we can combine database trips
                                sql &= "SELECT intVntID "
                                sql &= "FROM [110IntVnt] "
                                sql &= "WHERE ParentID in (" & childIDs & ") "

                                cmd.CommandType = CommandType.Text
                                cmd.CommandText = sql

                                ' 6. Get the IDs of the child rows
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