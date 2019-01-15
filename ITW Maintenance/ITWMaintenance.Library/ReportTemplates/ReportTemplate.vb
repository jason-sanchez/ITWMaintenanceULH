Imports System.Data.SqlClient

Namespace ReportTemplates

    <Serializable()> _
    Public Class ReportTemplate
        Inherits BusinessBase(Of ReportTemplate)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
        Public Property Description() As String
            Get
                Return GetProperty(Of String)(DescriptionProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(DescriptionProperty, value)
            End Set
        End Property

        Private Shared DisciplineIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisciplineID"))
        Public Property DisciplineID() As Integer
            Get
                Return GetProperty(Of Integer)(DisciplineIDProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(DisciplineIDProperty, value)
            End Set
        End Property
        Private Shared DisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Discipline"))
        Public ReadOnly Property Discipline() As String
            Get
                Return GetProperty(Of String)(DisciplineProperty)
            End Get
        End Property

        Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder"))
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
            Set(value As Boolean)
                SetProperty(Of Boolean)(InactiveProperty, value)
            End Set
        End Property

        'Private Shared ReportTemplateItemProperty As PropertyInfo(Of ReadOnlyReportTemplateList) = RegisterProperty(New PropertyInfo(Of ReadOnlyReportTemplateList)("LinkedForms", "Linked Forms"))
        'Public Property ReportTemplateItem() As ReadOnlyReportTemplateItem
        '    Get
        '        Return GetProperty(Of DiagnosisFormList)(LinkedFormsProperty)
        '    End Get
        '    Set(ByVal value As ReadOnlyReportTemplateItem)
        '        SetProperty(Of DiagnosisFormList)(LinkedFormsProperty, value)
        '    End Set
        'End Property

        Protected Overrides Function GetIDValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, DescriptionProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(DescriptionProperty, 55))
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
            Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewReportTemplate() As ReportTemplate
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a ReportTemplate")
            End If
            Return DataPortal.Create(Of ReportTemplate)()
        End Function

        Public Shared Function GetReportTemplate(ByVal ID As Integer) As ReportTemplate
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a ReportTemplate")
            End If
            Return DataPortal.Fetch(Of ReportTemplate)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteReportTemplate(ByVal ID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a ReportTemplate")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overloads Function Save() As ReportTemplate
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a ReportTemplate")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a ReportTemplate")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a ReportTemplate")
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

        <RunLocal()> _
        Private Overloads Sub DataPortal_Create()
            'LoadProperty(Of ReportTemplateSectionList)(ReportTemplateSectionItem, ReportTemplateSectionList.NewReportTemplateSectionList())
            'ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[documents].[ReportTemplate_Retrieve]"
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                            LoadProperty(Of Integer)(DisciplineIDProperty, .GetInt32("DisciplineID"))
                            LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                            LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                        End With
                    End Using
                End Using
            End Using

            ' Load Children
            'LoadProperty(Of ReadOnlyReportTemplateList)(IDProperty, ReadOnlyReportTemplateList.GetReportTemplateList(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[documents].[ReportTemplate_Insert]"

                    cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                    cmd.Parameters.AddWithValue("@DisciplineID", ReadProperty(Of Integer)(DisciplineIDProperty))
                    cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                    cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using
            End Using

            ' Update child objects
            'FieldManager.UpdateChildren(Me)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand


                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "[documents].[ReportTemplate_Update]"

                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                        cmd.Parameters.AddWithValue("@DisciplineID", ReadProperty(Of Integer)(DisciplineIDProperty))
                        cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                        cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If

            ' Update child objects
            'FieldManager.UpdateChildren(Me)
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        'Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
        '   Using conn As New SqlConnection(Database.ITWConnection)
        '      conn.Open()
        '     Using cmd As SqlCommand = conn.CreateCommand
        'Dim sql As String

        'sql = "DELETE FROM [120NDBridge] "
        'sql &= "WHERE [NursingDiagID] = " & criteria.rptTypeId & " "

        '           cmd.CommandType = CommandType.Text
        '          cmd.CommandText = sql
        '         cmd.ExecuteNonQuery()
        '
        '           sql = "DELETE FROM [105rptType] "
        '          sql &= "WHERE [rptTypeId] = " & criteria.rptTypeId & " "
        '
        '           cmd.CommandText = sql
        '          cmd.ExecuteNonQuery()
        '     End Using
        'End Using

        '    SetProperty(Of ReadOnlyReportTemplateList)(ReportTemplate, ReadOnlyReportTemplateList.GetReportTemplateList())
        'End Sub

#End Region

    End Class

End Namespace