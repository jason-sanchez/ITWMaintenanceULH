Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Interventions.Utilities

Namespace Interventions

    Namespace Folders

        <Serializable()> _
        Public Class IntVntFolder
            Inherits BusinessBase(Of IntVntFolder)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            'Public ReadOnly Property ID() As Integer
            '    Get
            '        Return GetProperty(Of Integer)(IDProperty)
            '    End Get
            'End Property

            Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property intVntID() As Integer
                Get
                    Return GetProperty(Of Integer)(intVntIDProperty)
                End Get
            End Property

            Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
            Public ReadOnly Property ParentID() As Integer
                Get
                    Return GetProperty(Of Integer)(ParentIDProperty)
                End Get
            End Property

            Private Shared Level2intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Level2intVntID"))
            Public ReadOnly Property Level2intVntID() As Integer
                Get
                    Return GetProperty(Of Integer)(Level2intVntIDProperty)
                End Get
            End Property

            Private Shared IntVntLevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntLevel"))
            Public ReadOnly Property IntVntLevel() As Integer
                Get
                    Return GetProperty(Of Integer)(IntVntLevelProperty)
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

            Private Shared iPathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("iPath"))
            Public Property iPath() As String
                Get
                    Return GetProperty(Of String)(iPathProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(iPathProperty, value)
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

            ''Private Shared iGroupOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("iGroupOrder", "iGroup Order"))
            ''Public Property iGroupOrder() As Integer
            ''    Get
            ''        Return GetProperty(Of Integer)(iGroupOrderProperty)
            ''    End Get
            ''   Set(ByVal value As Integer)
            ''        SetProperty(Of Integer)(iGroupOrderProperty, value)
            ''    End Set
            ''End Property

            ''Private Shared iGroupProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("iGroup"))
            ''Public Property iGroup() As String
            ''    Get
            ''        Return GetProperty(Of String)(iGroupProperty)
            ''    End Get
            ''    Set(ByVal value As String)
            ''        SetProperty(Of String)(iGroupProperty, value)
            ''    End Set
            ''End Property

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
                Return GetProperty(Of Integer)(intVntIDProperty)
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

            Public Shared Function NewIntVntFolder(ByVal ParentID As Integer) As IntVntFolder
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Intervention Folder")
                End If
                Return DataPortal.Create(Of IntVntFolder)(New CreateCriteria(ParentID))
            End Function

            Public Shared Function GetIntVntFolder(ByVal intVntID As Integer) As IntVntFolder
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Intervention Folder")
                End If
                Return DataPortal.Fetch(Of IntVntFolder)(New Criteria(intVntID))
            End Function

            Public Shared Sub DeleteIntVntFolder(ByVal intVntID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Intervention Folder")
                End If
                DataPortal.Delete(New Criteria(intVntID))
            End Sub

            Public Overloads Function Save() As IntVntFolder
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Intervention Folder")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Intervention Folder")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update an Intervention Folder")
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
                Private _ParentID As Integer

                Public ReadOnly Property ParentID() As Integer
                    Get
                        Return Me._ParentID
                    End Get
                End Property

                Public Sub New(ByVal ParentID As Integer)
                    Me._ParentID = ParentID
                End Sub
            End Class

            <Serializable()> _
            Private Class Criteria
                Private _intVntID As Integer

                Public ReadOnly Property intVntID() As Integer
                    Get
                        Return Me._intVntID
                    End Get
                End Property

                Public Sub New(ByVal intVntID As Integer)
                    Me._intVntID = intVntID
                End Sub
            End Class

            Private Overloads Sub DataPortal_Create(ByVal Criteria As CreateCriteria)
                ' Set the parent ID
                SetProperty(Of Integer)(ParentIDProperty, Criteria.ParentID)

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        ' First, figure out what level we're on from the ParentID.
                        ' Also, set this folder's level 2 to be the same as its parent's Level2.
                        ' Set the iGroup/iGroupOrder as well.
                        '     The iGroupOrder matches the dOrder from level3 and is the same for all children (regardless of the level)
                        '     The iGroup matches the iName for level3;
                        '       it matches the parent's iGroup plus the current iName if it's a folder for levels 4+;
                        '       and it matches the parent's iGroup for forms for levels 4+
                        '     Example:
                        '       OPPT
                        '         Back IntVnt
                        '           Subjective                              iGroupOrder: 1      iGroup: Subjective
                        '             Test Form                             iGroupOrder: 1      iGroup: Subjective
                        '             Patient Chief Complaints (folder)     iGroupOrder: 1      iGroup: Subjective, Patient Chief Complaints
                        '               Another Form                        iGroupOrder: 1      iGroup: Subjective, Patient Chief Complaints
                        '             Surgical Procedure                    iGroupOrder: 1      iGroup: Subjective, Surgical Procedure
                        sql = "SELECT intVntLevel + 1 as ThisIntVntLevel, Level2 " '', "
                        ''sql &= "iGroupOrder, iGroup "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE intVntID = " & Criteria.ParentID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(IntVntLevelProperty, dr.GetInt32("ThisIntVntLevel"))
                                SetProperty(Of Integer)(Level2intVntIDProperty, dr.GetInt32("Level2"))

                                If ReadProperty(Of Integer)(IntVntLevelProperty) = 3 Then
                                    ' This is a new folder on level 3.
                                    ' The iGroupOrder should be set to 0 so we know to set it to the dOrder
                                    ' The iGroup should be set to an empty string so it will be set to the folder name on Insert
                                    ''SetProperty(Of Integer)(iGroupOrderProperty, 0)
                                    ''SetProperty(Of String)(iGroupProperty, "")
                                Else
                                    ' This is a new form on level 4+
                                    ' Set the iGroup to the parent's iGroup but append this folder's name on Insert
                                    Try
                                        ''SetProperty(Of Integer)(iGroupOrderProperty, dr.GetInt32("iGroupOrder"))
                                        ''If Not IsDBNull(dr.GetValue("iGroup")) Then
                                        ''SetProperty(Of String)(iGroupProperty, dr.GetString("iGroup"))
                                        ''Else
                                        ''SetProperty(Of String)(iGroupProperty, "")
                                        ''End If
                                    Catch ex As Exception
                                        ''SetProperty(Of Integer)(iGroupOrderProperty, 0)
                                        ''SetProperty(Of String)(iGroupProperty, "")
                                    End Try
                                End If
                            End If
                        End Using

                        ' Get the next displayOrder
                        sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDOrder"))

                                ' Set the iGroupOrder if it's 0
                                ''If ReadProperty(Of Integer)(iGroupOrderProperty) = 0 Then
                                ''SetProperty(Of Integer)(iGroupOrderProperty, dr.GetInt32("NextDOrder"))
                                ''End If
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

                        sql = "SELECT [ID], intVntID, ParentID, intVntLevel, iName, Discipline, "
                        sql &= "Level2, ShortName, iPath, dOrder, "
                        ''iGroupOrder, iGroup, 
                        sql &= "inactive "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE intVntID = " & Criteria.intVntID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of Integer)(Level2intVntIDProperty, .GetInt32("Level2"))
                                LoadProperty(Of Integer)(IntVntLevelProperty, .GetInt32("intVntLevel"))
                                LoadProperty(Of String)(FolderNameProperty, .GetString("iName"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of String)(ShortNameProperty, .GetString("ShortName"))
                                LoadProperty(Of String)(iPathProperty, .GetString("iPath"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                ''LoadProperty(Of Integer)(iGroupOrderProperty, .GetInt32("iGroupOrder"))
                                ''LoadProperty(Of String)(iGroupProperty, .GetString("iGroup"))
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

                        ' Set the intVntID
                        SetProperty(Of Integer)(intVntIDProperty, IntVntHelper.GetNextintVntID(cmd))


                        ' Get the Parent's Path and discipline
                        sql = "SELECT iPath, Discipline "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ParentPath = dr.GetString("iPath")
                                SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                            End If
                        End Using


                        ' Set this folder's iPath (this is used to determine the ShortName)
                        SetProperty(Of String)(iPathProperty, ParentPath & ", " & ReadProperty(Of String)(FolderNameProperty))

                        ' Get the Intervention's Path and remove it
                        ' to form this folder's ShortName
                        sql = "SELECT iPath "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(Level2intVntIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ' My ShortName = My Path from level 3 and down
                                ' (my Path minus my Interventions's Path)
                                SetProperty(Of String)(ShortNameProperty, Left(Trim(Replace(ReadProperty(Of String)(iPathProperty), dr.GetString("iPath") & ", ", "")), 200))
                            End If
                        End Using


                        ' If we haven't set an iGroup (if this form is on level 3), 
                        ' set the iGroup to the name of the form
                        ''If String.IsNullOrEmpty(ReadProperty(Of String)(iGroupProperty)) Then
                        ''SetProperty(Of String)(iGroupProperty, ReadProperty(Of String)(FolderNameProperty))
                        ''Else
                        ''SetProperty(Of String)(iGroupProperty, ReadProperty(Of String)(iGroupProperty) & ", " & ReadProperty(Of String)(FolderNameProperty))
                        ''End If


                        ' Insert the new folder
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [110IntVnt] (intVntID, ParentID, intVntLevel, iPath, "
                        sql &= "iName, ShortName, iFinal, Discipline, DOrder, Level2, Required, "
                        ''sql &= "iGroupOrder, iGroup, 
                        sql &= "inactive) VALUES ("
                        sql &= ReadProperty(Of Integer)(intVntIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(IntVntLevelProperty) & ", "
                        ' The path can't exceed 200 characters
                        sql &= "'" & Left(Replace(ReadProperty(Of String)(iPathProperty), "'", "''"), 200) & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(ShortNameProperty), "'", "''") & "', "
                        sql &= "0, "
                        sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
                        sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        If ReadProperty(Of Integer)(IntVntLevelProperty) > 3 Then
                            sql &= ReadProperty(Of Integer)(Level2intVntIDProperty) & ", "
                        ElseIf ReadProperty(Of Integer)(IntVntLevelProperty) = 2 Then
                            sql &= ReadProperty(Of Integer)(intVntIDProperty) & ", "
                        Else
                            sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                        End If
                        sql &= "0, "
                        ''sql &= ReadProperty(Of Integer)(iGroupOrderProperty) & ", "
                        ''sql &= "'" & Left(Replace(ReadProperty(Of String)(iGroupProperty), "'", "''"), 200) & "', "
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
                            sql = "SELECT iPath "
                            sql &= "FROM [110IntVnt] "
                            sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If dr.Read() Then
                                    ParentPath = dr.GetString("iPath")
                                End If
                            End Using

                            SetProperty(Of String)(iPathProperty, ParentPath & ", " & ReadProperty(Of String)(FolderNameProperty))


                            ' Update the folder
                            sql = "UPDATE [110IntVnt] SET "
                            'sql &= "intVntID = " & ReadProperty(Of Integer)(intVntIDProperty) & ", "
                            'sql &= "ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & ", "
                            sql &= "iPath = '" & Left(Replace(ReadProperty(Of String)(iPathProperty), "'", "''"), 200) & "', "
                            sql &= "iName = '" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                            sql &= "ShortName = '" & Replace(ReadProperty(Of String)(ShortNameProperty), "'", "''") & "', "
                            sql &= "DOrder = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                            ''sql &= "iGroupOrder = " & ReadProperty(Of Integer)(iGroupOrderProperty) & ", "
                            ''sql &= "iGroup = '" & Left(Replace(ReadProperty(Of String)(iGroupProperty), "'", "''"), 200) & "', "
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
                DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(intVntIDProperty)))
            End Sub

            Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
                ' Detele is not allowed by the authorization rules above, but
                ' don't even code the delete for security.

                'Using conn As New SqlConnection(Database.ITWConnection)
                '    conn.Open()
                '    Using cmd As SqlCommand = conn.CreateCommand
                '        Dim sql As String

                '        sql = "DELETE FROM [110IntVnt] "
                '        sql &= "WHERE intVntID = " & criteria.intVntID & " "

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