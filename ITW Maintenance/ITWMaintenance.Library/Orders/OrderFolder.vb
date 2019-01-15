Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Lookup

Namespace Orders

    <Serializable()> _
    Public Class OrderFolder
        Inherits BusinessBase(Of OrderFolder)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
        Public ReadOnly Property ParentID() As Integer
            Get
                Return GetProperty(Of Integer)(ParentIDProperty)
            End Get
        End Property

        Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
        Public Property Name() As String
            Get
                Return GetProperty(Of String)(NameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(NameProperty, value)
            End Set
        End Property

        Private Shared PathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Path"))
        Public ReadOnly Property Path() As String
            Get
                Return GetProperty(Of String)(PathProperty)
            End Get
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

        Private Shared FromDisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FromDiscipline", "From Discipline"))
        Public Property FromDiscipline() As Integer
            Get
                Return GetProperty(Of Integer)(FromDisciplineProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(FromDisciplineProperty, value)
            End Set
        End Property

        Private Shared ToDisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ToDiscipline", "To Discipline"))
        Public Property ToDiscipline() As Integer
            Get
                Return GetProperty(Of Integer)(ToDisciplineProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(ToDisciplineProperty, value)
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
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, NameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(NameProperty, 200))

            ValidationRules.AddRule(Of OrderFolder)(AddressOf ValidFromDiscipline(Of OrderFolder), FromDisciplineProperty)
            ValidationRules.AddRule(Of OrderFolder)(AddressOf ValidToDiscipline(Of OrderFolder), ToDisciplineProperty)

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))
        End Sub

        Private Shared Function ValidFromDiscipline(Of T As OrderFolder)(ByVal target As T, _
            ByVal e As Validation.RuleArgs) As Boolean

            ' Validate to ensure the user has selected a valid discipline.
            ' Change this to DisciplineList.GetTherapyDisciplines if the discipline
            ' must be a therapy discipline.
            If DisciplineList.GetAllDisciplines.ContainsKey(target.FromDiscipline) Then
                Return True
            Else
                e.Description = "The " & e.PropertyFriendlyName & " is not a valid discipline."
                Return False
            End If
        End Function

        Private Shared Function ValidToDiscipline(Of T As OrderFolder)(ByVal target As T, _
            ByVal e As Validation.RuleArgs) As Boolean

            ' Validate to ensure the user has selected a valid discipline.
            ' Change this to DisciplineList.GetTherapyDisciplines if the discipline
            ' must be a therapy discipline.
            If DisciplineList.GetAllDisciplines.ContainsKey(target.FromDiscipline) Then
                Return True
            Else
                e.Description = "The " & e.PropertyFriendlyName & " is not a valid discipline."
                Return False
            End If
        End Function

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

        Public Shared Function NewOrderFolder(ByVal ParentID As Integer) As OrderFolder
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Order Folder")
            End If
            Return DataPortal.Create(Of OrderFolder)(New CreateCriteria(ParentID))
        End Function

        Public Shared Function GetOrderFolder(ByVal ID As Integer) As OrderFolder
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Order Folder")
            End If
            Return DataPortal.Fetch(Of OrderFolder)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteOrderFolder(ByVal ID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Order Folder")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overloads Function Save() As OrderFolder
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Order Folder")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Order Folder")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update an Order Folder")
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
            Private _ID As Integer

            Public ReadOnly Property ID() As Integer
                Get
                    Return Me._ID
                End Get
            End Property

            Public Sub New(ByVal ID As Integer)
                Me._ID = ID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Create(ByVal criteria As CreateCriteria)
            ' Set the ParentID
            SetProperty(Of Integer)(ParentIDProperty, criteria.ParentID)

            ' Set the DisplayOrder
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Get the next displayOrder
                    sql = "SELECT ISNULL(MAX([DisplayOrder]), 0) + 1 as NextDisplayOrder "
                    sql &= "FROM [109Order] "
                    sql &= "WHERE [ParentID] = " & criteria.ParentID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDisplayOrder"))
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

                    sql = "SELECT [ID], [ParentID], [Name], [Path], [DisplayOrder], "
                    sql &= "[FromDiscipline], [ToDiscipline], [Inactive] "
                    sql &= "FROM [109Order] "
                    sql &= "WHERE [ID] = " & Criteria.ID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                            LoadProperty(Of String)(NameProperty, .GetString("Name"))
                            LoadProperty(Of String)(PathProperty, .GetString("Path"))
                            LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                            LoadProperty(Of Integer)(FromDisciplineProperty, .GetInt32("FromDiscipline"))
                            LoadProperty(Of Integer)(ToDisciplineProperty, .GetInt32("ToDiscipline"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
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

                    ' Get the level and Path from the parent
                    Dim orderLevel As Integer = 1
                    If ReadProperty(Of Integer)(ParentIDProperty) > 0 Then
                        sql = "SELECT [Level] + 1 AS OrderLevel, [Path] "
                        sql &= "FROM [109Order] "
                        sql &= "WHERE [ID] = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                orderLevel = .GetInt32("OrderLevel")
                                If Not IsDBNull(.GetString("Path")) AndAlso Not String.IsNullOrEmpty(.GetString("Path")) Then
                                    SetProperty(Of String)(PathProperty, .GetString("Path") & ", " & ReadProperty(Of String)(NameProperty))
                                Else
                                    SetProperty(Of String)(PathProperty, ReadProperty(Of String)(NameProperty))
                                End If
                            End With
                        End Using
                    Else
                        SetProperty(Of String)(PathProperty, ReadProperty(Of String)(NameProperty))
                    End If

                    ' Insert the new folder
                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [109Order] ([ParentID], [Level], [Name], [Path], "
                    sql &= "[DisplayOrder], [FromDiscipline], [ToDiscipline], [Inactive]) "
                    sql &= "VALUES ("
                    sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                    sql &= orderLevel & ", "
                    sql &= "'" & Replace(ReadProperty(Of String)(NameProperty), "'", "''") & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(PathProperty), "'", "''") & "', "
                    sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                    sql &= ReadProperty(Of Integer)(FromDisciplineProperty) & ", "
                    sql &= ReadProperty(Of Integer)(ToDisciplineProperty) & ", "
                    If ReadProperty(Of Boolean)(InactiveProperty) Then
                        sql &= "1) "
                    Else
                        sql &= "0) "
                    End If
                    sql &= "SELECT @@IDENTITY AS NewID "
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

                        ' Rebuild the Path
                        If ReadProperty(Of Integer)(ParentIDProperty) > 0 Then
                            sql = "SELECT [Path] "
                            sql &= "FROM [109Order] "
                            sql &= "WHERE [ID] = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            Using dr As New SafeDataReader(cmd.ExecuteReader)
                                dr.Read()
                                With dr
                                    If Not IsDBNull(.GetString("Path")) AndAlso Not String.IsNullOrEmpty(.GetString("Path")) Then
                                        SetProperty(Of String)(PathProperty, .GetString("Path") & ", " & ReadProperty(Of String)(NameProperty))
                                    Else
                                        SetProperty(Of String)(PathProperty, ReadProperty(Of String)(NameProperty))
                                    End If
                                End With
                            End Using
                        Else
                            SetProperty(Of String)(PathProperty, ReadProperty(Of String)(NameProperty))
                        End If

                        sql = "UPDATE [109Order] SET "
                        sql &= "[Name] = '" & Replace(ReadProperty(Of String)(NameProperty), "'", "''") & "', "
                        sql &= "[Path] = '" & Replace(ReadProperty(Of String)(PathProperty), "'", "''") & "', "
                        sql &= "[DisplayOrder] = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        sql &= "[FromDiscipline] = " & ReadProperty(Of Integer)(FromDisciplineProperty) & ", "
                        sql &= "[ToDiscipline] = " & ReadProperty(Of Integer)(ToDisciplineProperty) & ", "
                        If ReadProperty(Of Boolean)(InactiveProperty) Then
                            sql &= "[Inactive] = 1 "
                        Else
                            sql &= "[Inactive] = 0 "
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
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            ' Detele is not allowed by the authorization rules above, but
            ' don't even code the delete for security.

            'Using conn As New SqlConnection(Database.ITWConnection)
            '    conn.Open()
            '    Using cmd As SqlCommand = conn.CreateCommand
            '        Dim sql As String

            '        sql = "DELETE FROM [109Order] "
            '        sql &= "WHERE [ID] = " & criteria.ID & " "

            '        cmd.CommandType = CommandType.Text
            '        cmd.CommandText = sql
            '        cmd.ExecuteNonQuery()
            '    End Using
            'End Using
        End Sub

#End Region

    End Class

End Namespace
