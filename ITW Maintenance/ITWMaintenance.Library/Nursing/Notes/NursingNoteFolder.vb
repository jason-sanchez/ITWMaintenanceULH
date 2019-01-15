Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Evaluations.Utilities

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class NursingNoteFolder
            Inherits BusinessBase(Of NursingNoteFolder)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            'Public ReadOnly Property ID() As Integer
            '    Get
            '        Return GetProperty(Of Integer)(IDProperty)
            '    End Get
            'End Property

            Private Shared FolderIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FolderID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property FolderID() As Integer
                Get
                    Return GetProperty(Of Integer)(FolderIDProperty)
                End Get
            End Property

            Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID", "Parent ID", 0))
            Public ReadOnly Property ParentID() As Integer
                Get
                    Return GetProperty(Of Integer)(ParentIDProperty)
                End Get
            End Property

            Private Shared ParentNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ParentName", "Parent Name"))
            Public ReadOnly Property ParentName() As String
                Get
                    Return GetProperty(Of String)(ParentNameProperty)
                End Get
            End Property

            Private Shared LevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Level", "Level", 1))
            Public ReadOnly Property Level() As Integer
                Get
                    Return GetProperty(Of Integer)(LevelProperty)
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

            Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline", "Discipline", 0))
            Public ReadOnly Property Discipline() As Integer
                Get
                    Return GetProperty(Of Integer)(DisciplineProperty)
                End Get
            End Property

            Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order", 0))
            Public Property DisplayOrder() As Integer
                Get
                    Return GetProperty(Of Integer)(DisplayOrderProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(DisplayOrderProperty, value)
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
                Return GetProperty(Of Integer)(FolderIDProperty)
            End Function

#End Region

#Region " Validation Rules "

            Protected Overrides Sub AddBusinessRules()
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, FolderNameProperty)
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                    New Validation.CommonRules.MaxLengthRuleArgs(FolderNameProperty, 200))

                ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                    New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))
            End Sub

            Public Function GetRuleDescriptions() As String()
                Return ValidationRules.GetRuleDescriptions()
            End Function

#End Region

#Region " Authorization Rules "

            Protected Overrides Sub AddAuthorizationRules()
                ' Add AuthorizationRules here
            End Sub

            Public Shared Function CanAddObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return False 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function NewNursingNoteFolder(ByVal ParentID As Integer) As NursingNoteFolder
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Nursing Note Folder")
                End If
                Return DataPortal.Create(Of NursingNoteFolder)(New CreateCriteria(ParentID))
            End Function

            Public Shared Function NewNursingNoteFolder(ByVal ParentID As Integer, ByVal Discipline As Integer) As NursingNoteFolder
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Nursing Note Folder")
                End If
                Return DataPortal.Create(Of NursingNoteFolder)(New CreateCriteria(ParentID, Discipline))
            End Function

            Public Shared Function GetNursingNoteFolder(ByVal FolderID As Integer) As NursingNoteFolder
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view a Nursing Note Folder")
                End If
                Return DataPortal.Fetch(Of NursingNoteFolder)(New Criteria(FolderID))
            End Function

            Public Shared Sub DeleteNursingNoteFolder(ByVal FolderID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Nursing Note Folder")
                End If
                DataPortal.Delete(New Criteria(FolderID))
            End Sub

            Public Overloads Function Save() As NursingNoteFolder
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Nursing Note Folder")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Nursing Note Folder")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update a Nursing Note Folder")
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
                Private _Discipline As Integer

                Public ReadOnly Property ParentID() As Integer
                    Get
                        Return Me._ParentID
                    End Get
                End Property

                Public ReadOnly Property Discipline() As Integer
                    Get
                        Return Me._Discipline
                    End Get
                End Property

                Public Sub New(ByVal ParentID As Integer)
                    Me._ParentID = ParentID
                End Sub

                Public Sub New(ByVal ParentID As Integer, ByVal Discipline As Integer)
                    Me._ParentID = ParentID
                    Me._Discipline = Discipline
                End Sub
            End Class

            <Serializable()> _
            Private Class Criteria
                Private _FolderID As Integer

                Public ReadOnly Property FolderID() As Integer
                    Get
                        Return Me._FolderID
                    End Get
                End Property

                Public Sub New(ByVal FolderID As Integer)
                    Me._FolderID = FolderID
                End Sub
            End Class

            Private Overloads Sub DataPortal_Create(ByVal Criteria As CreateCriteria)
                ' Set the parent ID
                SetProperty(Of Integer)(ParentIDProperty, Criteria.ParentID)

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        If Criteria.ParentID > 0 Then
                            sql = "SELECT nsNtLevel + 1 as ThisFolderLevel, Discipline, NName "
                            sql &= "FROM [100CrNt] "
                            sql &= "WHERE nsNtID = " & Criteria.ParentID & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                If dr.Read() Then
                                    SetProperty(Of Integer)(LevelProperty, dr.GetInt32("ThisFolderLevel"))
                                    SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                                    SetProperty(Of String)(ParentNameProperty, dr.GetString("NName"))
                                End If
                            End Using
                        Else
                            ' ParentID is 0, so this folder is on level 1
                            SetProperty(Of Integer)(LevelProperty, 1)
                            SetProperty(Of Integer)(DisciplineProperty, Criteria.Discipline)
                        End If

                        ' Get the next displayOrder
                        sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                        sql &= "FROM [100CrNt] "
                        sql &= "WHERE ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "
                        sql &= "AND Discipline = " & ReadProperty(Of Integer)(DisciplineProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDOrder"))
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

                        sql = "SELECT n.[ID], n.NsNtID, n.ParentID, p.NName AS ParentName, "
                        sql &= "n.NsNtLevel, n.NName, n.Discipline, n.dOrder, n.nInactive "
                        sql &= "FROM [100CrNt] n "
                        sql &= "LEFT JOIN [100CrNt] p ON n.ParentID = p.nsNtID "
                        sql &= "WHERE n.NsNtID = " & Criteria.FolderID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(FolderIDProperty, .GetInt32("NsNtID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of String)(ParentNameProperty, .GetString("ParentName"))
                                LoadProperty(Of Integer)(LevelProperty, .GetInt32("NsNtLevel"))
                                LoadProperty(Of String)(FolderNameProperty, .GetString("NName"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("nInactive"))
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

                        ' Insert the new folder
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [100CrNt] (NsNtID, ParentID, NsNtLevel, NName, "
                        sql &= "    nFinal, Discipline, dOrder, nInactive) "
                        sql &= "VALUES (@NsNtID, @ParentID, @Level, @FolderName, "
                        sql &= "    0, @Discipline, @DisplayOrder, @Inactive) "
                        sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                        sql &= "SET NOCOUNT OFF "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        ' The NsNtID value is always the same as the ID value, so we'll need
                        ' to update it with an update query after the insert query.
                        cmd.Parameters.AddWithValue("@NsNtID", 0)
                        cmd.Parameters.AddWithValue("@ParentID", ReadProperty(Of Integer)(ParentIDProperty))
                        cmd.Parameters.AddWithValue("@Level", ReadProperty(Of Integer)(LevelProperty))
                        cmd.Parameters.AddWithValue("@FolderName", ReadProperty(Of String)(FolderNameProperty))
                        cmd.Parameters.AddWithValue("@Discipline", ReadProperty(Of Integer)(DisciplineProperty))
                        cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                        cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                        ' Save the new ID that was added
                        LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                        LoadProperty(Of Integer)(FolderIDProperty, ReadProperty(Of Integer)(IDProperty))

                        ' Update the NsNtID, which is always the same as the ID
                        sql = "UPDATE [100CrNt] SET "
                        sql &= "NsNtID = @ID "
                        sql &= "WHERE [ID] = @ID "

                        cmd.CommandText = sql
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.ExecuteNonQuery()
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

                            ' Update the folder
                            sql = "UPDATE [100CrNt] SET "
                            sql &= "NName = @FolderName, "
                            sql &= "DOrder = @DisplayOrder, "
                            sql &= "nInactive = @Inactive "
                            sql &= "WHERE [ID] = @ID "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            cmd.Parameters.AddWithValue("@FolderName", ReadProperty(Of String)(FolderNameProperty))
                            cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                            cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                            cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))

                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End If

                ' Update child objects
            End Sub

            Protected Overrides Sub DataPortal_DeleteSelf()
                DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(FolderIDProperty)))
            End Sub

            Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
                ' Detele is not allowed by the authorization rules above, but
                ' don't even code the delete for security.

                'Using conn As New SqlConnection(Database.ITWConnection)
                '    conn.Open()
                '    Using cmd As SqlCommand = conn.CreateCommand
                '        Dim sql As String

                '        sql = "DELETE FROM [100CrNt] "
                '        sql &= "WHERE [NsNtID] = " & criteria.FolderID & " "

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