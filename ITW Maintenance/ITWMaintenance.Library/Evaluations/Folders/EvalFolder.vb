Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Evaluations.Utilities

Namespace Evaluations

    Namespace Folders

        <Serializable()> _
        Public Class EvalFolder
            Inherits BusinessBase(Of EvalFolder)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            'Public ReadOnly Property ID() As Integer
            '    Get
            '        Return GetProperty(Of Integer)(IDProperty)
            '    End Get
            'End Property

            Private Shared EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property EvalID() As Integer
                Get
                    Return GetProperty(Of Integer)(EvalIDProperty)
                End Get
            End Property

            Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
            Public ReadOnly Property ParentID() As Integer
                Get
                    Return GetProperty(Of Integer)(ParentIDProperty)
                End Get
            End Property

            Private Shared Level2EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Level2EvalID"))
            Public ReadOnly Property Level2EvalID() As Integer
                Get
                    Return GetProperty(Of Integer)(Level2EvalIDProperty)
                End Get
            End Property

            Private Shared EvalLevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalLevel"))
            Public ReadOnly Property EvalLevel() As Integer
                Get
                    Return GetProperty(Of Integer)(EvalLevelProperty)
                End Get
            End Property

            Private Shared FolderNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FolderName", "Folder Name"))
            Public Property FolderName() As String
                Get
                    Return GetProperty(Of String)(FolderNameProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(FolderNameProperty, value)
                End Set
            End Property

            Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline"))
            Public ReadOnly Property Discipline() As Integer
                Get
                    Return GetProperty(Of Integer)(DisciplineProperty)
                End Get
            End Property

            Private Shared ShortNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ShortName", "Short Name"))
            Public Property ShortName() As String
                Get
                    Return GetProperty(Of String)(ShortNameProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(ShortNameProperty, value)
                End Set
            End Property

            Private Shared EPathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EPath"))
            Public Property EPath() As String
                Get
                    Return GetProperty(Of String)(EPathProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(EPathProperty, value)
                End Set
            End Property

            Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
            Public Property DisplayOrder() As Integer
                Get
                    Return GetProperty(Of Integer)(DisplayOrderProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(DisplayOrderProperty, value)
                End Set
            End Property

            Private Shared EGroupOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EGroupOrder", "EGroup Order"))
            Public Property EGroupOrder() As Integer
                Get
                    Return GetProperty(Of Integer)(EGroupOrderProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(EGroupOrderProperty, value)
                End Set
            End Property

            Private Shared EGroupProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EGroup"))
            Public Property EGroup() As String
                Get
                    Return GetProperty(Of String)(EGroupProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(EGroupProperty, value)
                End Set
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(InactiveProperty, value)
                End Set
            End Property

            Protected Overrides Function GetIdValue() As Object
                Return GetProperty(Of Integer)(EvalIDProperty)
            End Function

#End Region

#Region " Validation Rules "

            Protected Overrides Sub AddBusinessRules()
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, FolderNameProperty)
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                    New Validation.CommonRules.MaxLengthRuleArgs(FolderNameProperty, 200))

                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                    New Validation.CommonRules.MaxLengthRuleArgs(ShortNameProperty, 200))

                ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                    New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))
            End Sub

#End Region

#Region " Authorization Rules "

            Protected Overrides Sub AddAuthorizationRules()
                ' Add AuthorizationRules here
            End Sub

            Public Shared Function CanAddObject() As Boolean
                Return Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function NewEvalFolder(ByVal ParentEvalID As Integer) As EvalFolder
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Eval Folder")
                End If
                Return DataPortal.Create(Of EvalFolder)(New CreateCriteria(ParentEvalID))
            End Function

            Public Shared Function GetEvalFolder(ByVal EvalID As Integer) As EvalFolder
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Eval Folder")
                End If
                Return DataPortal.Fetch(Of EvalFolder)(New Criteria(EvalID))
            End Function

            Public Shared Sub DeleteEvalFolder(ByVal EvalID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Eval Folder")
                End If
                DataPortal.Delete(New Criteria(EvalID))
            End Sub

            Public Overloads Function Save() As EvalFolder
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Eval Folder")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Eval Folder")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update an Eval Folder")
                End If

                Return MyBase.Save()
            End Function

            Private Sub New()
                ' Require use of Factory methods
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class CreateCriteria
                Private _ParentEvalID As Integer

                Public ReadOnly Property ParentEvalID() As Integer
                    Get
                        Return Me._ParentEvalID
                    End Get
                End Property

                Public Sub New(ByVal ParentEvalID As Integer)
                    Me._ParentEvalID = ParentEvalID
                End Sub
            End Class

            <Serializable()> _
            Private Class Criteria
                Private _EvalID As Integer

                Public ReadOnly Property EvalID() As Integer
                    Get
                        Return Me._EvalID
                    End Get
                End Property

                Public Sub New(ByVal EvalID As Integer)
                    Me._EvalID = EvalID
                End Sub
            End Class

            Private Overloads Sub DataPortal_Create(ByVal Criteria As CreateCriteria)
                ' Set the parent ID
                SetProperty(Of Integer)(ParentIDProperty, Criteria.ParentEvalID)

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        ' First, figure out what level we're on from the ParentID.
                        ' Also, set this folder's level 2 to be the same as its parent's Level2.
                        ' Set the eGroup/eGroupOrder as well.
                        '     The eGroupOrder matches the dOrder from level3 and is the same for all children (regardless of the level)
                        '     The eGroup matches the EName for level3;
                        '       it matches the parent's eGroup plus the current EName if it's a folder for levels 4+;
                        '       and it matches the parent's eGroup for forms for levels 4+
                        '     Example:
                        '       OPPT
                        '         Back Eval
                        '           Subjective                              eGroupOrder: 1      eGroup: Subjective
                        '             Test Form                             eGroupOrder: 1      eGroup: Subjective
                        '             Patient Chief Complaints (folder)     eGroupOrder: 1      eGroup: Subjective, Patient Chief Complaints
                        '               Another Form                        eGroupOrder: 1      eGroup: Subjective, Patient Chief Complaints
                        '             Surgical Procedure                    eGroupOrder: 1      eGroup: Subjective, Surgical Procedure
                        sql = "SELECT EvalLevel + 1 as ThisEvalLevel, Level2, "
                        sql &= "eGroupOrder, eGroup "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE EvalID = " & Criteria.ParentEvalID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(EvalLevelProperty, dr.GetInt32("ThisEvalLevel"))
                                SetProperty(Of Integer)(Level2EvalIDProperty, dr.GetInt32("Level2"))

                                If ReadProperty(Of Integer)(EvalLevelProperty) = 3 Then
                                    ' This is a new folder on level 3.
                                    ' The eGroupOrder should be set to 0 so we know to set it to the dOrder
                                    ' The eGroup should be set to an empty string so it will be set to the folder name on Insert
                                    SetProperty(Of Integer)(EGroupOrderProperty, 0)
                                    SetProperty(Of String)(EGroupProperty, "")
                                Else
                                    ' This is a new form on level 4+
                                    ' Set the eGroup to the parent's eGroup but append this folder's name on Insert
                                    Try
                                        SetProperty(Of Integer)(EGroupOrderProperty, dr.GetInt32("eGroupOrder"))
                                        If Not IsDBNull(dr.GetValue("eGroup")) Then
                                            SetProperty(Of String)(EGroupProperty, dr.GetString("eGroup"))
                                        Else
                                            SetProperty(Of String)(EGroupProperty, "")
                                        End If
                                    Catch ex As Exception
                                        SetProperty(Of Integer)(EGroupOrderProperty, 0)
                                        SetProperty(Of String)(EGroupProperty, "")
                                    End Try
                                End If
                            End If
                        End Using

                        ' Get the next displayOrder
                        sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDOrder"))

                                ' Set the eGroupOrder if it's 0
                                If ReadProperty(Of Integer)(EGroupOrderProperty) = 0 Then
                                    SetProperty(Of Integer)(EGroupOrderProperty, dr.GetInt32("NextDOrder"))
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], EvalID, ParentID, EvalLevel, EName, Discipline, "
                        sql &= "Level2, ShortName, EPath, dOrder, eGroupOrder, eGroup, inactive "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE EvalID = " & Criteria.EvalID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of Integer)(Level2EvalIDProperty, .GetInt32("Level2"))
                                LoadProperty(Of Integer)(EvalLevelProperty, .GetInt32("EvalLevel"))
                                LoadProperty(Of String)(FolderNameProperty, .GetString("EName"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of String)(ShortNameProperty, .GetString("ShortName"))
                                LoadProperty(Of String)(EPathProperty, .GetString("EPath"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Integer)(EGroupOrderProperty, .GetInt32("eGroupOrder"))
                                LoadProperty(Of String)(EGroupProperty, .GetString("eGroup"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("inactive"))
                            End With
                        End Using
                    End Using
                End Using
            End Sub

            Protected Overrides Sub DataPortal_Insert()
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String
                        Dim ParentPath As String = ""

                        ' Set the EvalID
                        SetProperty(Of Integer)(EvalIDProperty, EvalHelper.GetNextEvalID(cmd))


                        ' Get the Parent's Path and discipline
                        sql = "SELECT EPath, Discipline "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE EvalID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ParentPath = dr.GetString("EPath")
                                SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                            End If
                        End Using


                        ' Set this folder's EPath (this is used to determine the ShortName)
                        SetProperty(Of String)(EPathProperty, ParentPath & ", " & ReadProperty(Of String)(FolderNameProperty))

                        ' Get the Evaluation's Path and remove it
                        ' to form this folder's ShortName
                        sql = "SELECT EPath "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE EvalID = " & ReadProperty(Of Integer)(Level2EvalIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ' My ShortName = My Path from level 3 and down
                                ' (my Path minus my Eval's Path)
                                SetProperty(Of String)(ShortNameProperty, Left(Trim(Replace(ReadProperty(Of String)(EPathProperty), dr.GetString("EPath") & ", ", "")), 200))
                            End If
                        End Using


                        ' If we haven't set an eGroup (if this form is on level 3), 
                        ' set the eGroup to the name of the form
                        If String.IsNullOrEmpty(ReadProperty(Of String)(EGroupProperty)) Then
                            SetProperty(Of String)(EGroupProperty, ReadProperty(Of String)(FolderNameProperty))
                        Else
                            SetProperty(Of String)(EGroupProperty, ReadProperty(Of String)(EGroupProperty) & ", " & ReadProperty(Of String)(FolderNameProperty))
                        End If


                        ' Insert the new folder
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [100Eval] (EvalID, ParentID, EvalLevel, EPath, "
                        sql &= "EName, ShortName, EFinal, Discipline, DOrder, Level2, Required, "
                        sql &= "eGroupOrder, eGroup, inactive) VALUES ("
                        sql &= ReadProperty(Of Integer)(EvalIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(EvalLevelProperty) & ", "
                        ' The path can't exceed 200 characters
                        sql &= "'" & Left(Replace(ReadProperty(Of String)(EPathProperty), "'", "''"), 200) & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(ShortNameProperty), "'", "''") & "', "
                        sql &= "0, "
                        sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
                        sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        sql &= ReadProperty(Of Integer)(Level2EvalIDProperty) & ", "
                        sql &= "0, "
                        sql &= ReadProperty(Of Integer)(EGroupOrderProperty) & ", "
                        sql &= "'" & Left(Replace(ReadProperty(Of String)(EGroupProperty), "'", "''"), 200) & "', "
                        If ReadProperty(Of Boolean)(InactiveProperty) Then
                            sql &= "1) "
                        Else
                            sql &= "0) "
                        End If
                        sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                        sql &= "SET NOCOUNT OFF "

                        cmd.CommandText = sql

                        ' Save the new ID that was added
                        LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                    End Using
                End Using

                ' Update child objects
            End Sub

            Protected Overrides Sub DataPortal_Update()
                If MyBase.IsDirty Then
                    Using conn As New SqlConnection(Database.ITWConnection)
                        conn.Open()
                        Using cmd As SqlCommand = conn.CreateCommand
                            Dim sql As String
                            Dim ParentPath As String = ""

                            ' Get the Parent's Path
                            sql = "SELECT EPath "
                            sql &= "FROM [100Eval] "
                            sql &= "WHERE EvalID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If dr.Read() Then
                                    ParentPath = dr.GetString("EPath")
                                End If
                            End Using

                            SetProperty(Of String)(EPathProperty, ParentPath & ", " & ReadProperty(Of String)(FolderNameProperty))


                            ' Update the folder
                            sql = "UPDATE [100Eval] SET "
                            'sql &= "EvalID = " & ReadProperty(Of Integer)(EvalIDProperty) & ", "
                            'sql &= "ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & ", "
                            sql &= "EPath = '" & Left(Replace(ReadProperty(Of String)(EPathProperty), "'", "''"), 200) & "', "
                            sql &= "EName = '" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                            sql &= "ShortName = '" & Replace(ReadProperty(Of String)(ShortNameProperty), "'", "''") & "', "
                            sql &= "DOrder = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                            sql &= "eGroupOrder = " & ReadProperty(Of Integer)(EGroupOrderProperty) & ", "
                            sql &= "eGroup = '" & Left(Replace(ReadProperty(Of String)(EGroupProperty), "'", "''"), 200) & "', "
                            If ReadProperty(Of Boolean)(InactiveProperty) Then
                                sql &= "Inactive = 1 "
                            Else
                                sql &= "Inactive = 0 "
                            End If
                            sql &= "WHERE [ID] = " & ReadProperty(Of Integer)(IDProperty) & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End If

                ' Update child objects
            End Sub

            Protected Overrides Sub DataPortal_DeleteSelf()
                DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(EvalIDProperty)))
            End Sub

            Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
                ' Detele is not allowed by the authorization rules above, but
                ' don't even code the delete for security.

                'Using conn As New SqlConnection(Database.ITWConnection)
                '    conn.Open()
                '    Using cmd As SqlCommand = conn.CreateCommand
                '        Dim sql As String

                '        sql = "DELETE FROM [100Eval] "
                '        sql &= "WHERE EvalID = " & criteria.EvalID & " "

                '        cmd.CommandType = CommandType.Text
                '        cmd.CommandText = sql
                '        cmd.ExecuteNonQuery()
                '    End Using
                'End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace