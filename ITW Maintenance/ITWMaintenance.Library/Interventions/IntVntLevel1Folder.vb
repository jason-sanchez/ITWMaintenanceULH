Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Lookup
Imports ITWMaintenance.Library.Interventions.Utilities

Namespace Interventions

    <Serializable()> _
    Public Class IntVntLevel1Folder
        Inherits BusinessBase(Of IntVntLevel1Folder)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property intVntID() As Integer
            Get
                Return GetProperty(Of Integer)(intVntIDProperty)
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
        Public Property Discipline() As Integer
            Get
                Return GetProperty(Of Integer)(DisciplineProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(DisciplineProperty, value)
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

            ValidationRules.AddRule(Of IntVntLevel1Folder)(AddressOf ValidDiscipline(Of IntVntLevel1Folder), DisciplineProperty)

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))
        End Sub

        Private Shared Function ValidDiscipline(Of T As IntVntLevel1Folder)(ByVal target As T, _
            ByVal e As Validation.RuleArgs) As Boolean

            ' Validate to ensure the user has selected a valid discipline.
            ' Change this to DisciplineList.GetTherapyDisciplines if the discipline
            ' must be a therapy discipline.
            If DisciplineList.GetAllDisciplines.ContainsKey(target.Discipline) Then
                Return True
            Else
                e.Description = "This Level 1 Folder has not been linked to a valid discipline."
                Return False
            End If
        End Function

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

        Public Shared Function NewIntVntLevel1Folder() As IntVntLevel1Folder
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a Level 1 Intervention Folder")
            End If
            Return DataPortal.Create(Of IntVntLevel1Folder)()
        End Function

        Public Shared Function GetIntVntLevel1Folder(ByVal intVntID As Integer) As IntVntLevel1Folder
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a Level 1 Intervention Folder")
            End If
            Return DataPortal.Fetch(Of IntVntLevel1Folder)(New Criteria(intVntID))
        End Function

        Public Shared Sub DeleteIntVntLevel1Folder(ByVal intVntID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a Level 1 Intervention Folder")
            End If
            DataPortal.Delete(New Criteria(intVntID))
        End Sub

        Public Overloads Function Save() As IntVntLevel1Folder
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a Level 1 Intervention Folder")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a Level 1 Intervention Folder")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a Level 1 Intervention Folder")
            End If

            Return MyBase.Save()
        End Function

        Private Sub New()
            ' Require use of Factory methods
        End Sub

#End Region

#Region " Data Access "

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

        Private Overloads Sub DataPortal_Create()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Get the next displayOrder
                    sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                    sql &= "FROM [110IntVnt] "
                    sql &= "WHERE intVntLevel = 1 "

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

                    sql = "SELECT [ID], intVntID, intVntLevel, iName, Discipline, "
                    sql &= "dOrder, inactive "
                    sql &= "FROM [110IntVnt] "
                    sql &= "WHERE intVntID = " & Criteria.intVntID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            If .GetInt32("intVntLevel") = 1 Then
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                                LoadProperty(Of String)(FolderNameProperty, .GetString("iName"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("inactive"))
                            Else
                                Throw New Exception("Intervention ID #" & Criteria.intVntID & " is not a Level 1 Intervention Folder")
                            End If
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

                    ' Set the intVntID
                    SetProperty(Of Integer)(intVntIDProperty, IntVntHelper.GetNextintVntID(cmd))

                    ' Insert the new folder
                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [110IntVnt] (intVntID, ParentID, intVntLevel, iPath, "
                    sql &= "iName, ShortName, iFinal, DOrder, Level2, Required, "
                    sql &= "discipline, inactive) VALUES ("
                    sql &= ReadProperty(Of Integer)(intVntIDProperty) & ", "
                    sql &= "0, "
                    sql &= "1, "
                    sql &= "'" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                    sql &= "0, "
                    sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                    sql &= "0, "
                    sql &= "0, "
                    sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
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

                        sql = "UPDATE [110IntVnt] SET "
                        'sql &= "intVntID = " & Me._intVntID & ", "
                        sql &= "iPath = '" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                        sql &= "iName = '" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                        sql &= "ShortName = '" & Replace(ReadProperty(Of String)(FolderNameProperty), "'", "''") & "', "
                        sql &= "Discipline = " & ReadProperty(Of Integer)(DisciplineProperty) & ", "
                        sql &= "DOrder = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        If ReadProperty(Of Boolean)(InactiveProperty) Then
                            sql &= "Inactive = 1 "
                        Else
                            sql &= "Inactive = 0 "
                        End If
                        'sql &= "WHERE [ID] = " & Me._ID & " "
                        sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(intVntIDProperty) & " "

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
