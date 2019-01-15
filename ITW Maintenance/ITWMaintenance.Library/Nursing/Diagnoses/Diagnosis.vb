Imports System.Data.SqlClient

Namespace Nursing

    Namespace Diagnoses

        <Serializable()> _
        Public Class Diagnosis
            Inherits BusinessBase(Of Diagnosis)

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

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(InactiveProperty, value)
                End Set
            End Property

            Private Shared LinkedFormsProperty As PropertyInfo(Of DiagnosisFormList) = RegisterProperty(New PropertyInfo(Of DiagnosisFormList)("LinkedForms", "Linked Forms"))
            Public Property LinkedForms() As DiagnosisFormList
                Get
                    Return GetProperty(Of DiagnosisFormList)(LinkedFormsProperty)
                End Get
                Set(ByVal value As DiagnosisFormList)
                    SetProperty(Of DiagnosisFormList)(LinkedFormsProperty, value)
                End Set
            End Property

            Protected Overrides Function GetIdValue() As Object
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

            Public Shared Function NewDiagnosis() As Diagnosis
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Diagnosis")
                End If
                Return DataPortal.Create(Of Diagnosis)()
            End Function

            Public Shared Function GetDiagnosis(ByVal ID As Integer) As Diagnosis
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view a Diagnosis")
                End If
                Return DataPortal.Fetch(Of Diagnosis)(New Criteria(ID))
            End Function

            Public Shared Sub DeleteDiagnosis(ByVal ID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Diagnosis")
                End If
                DataPortal.Delete(New Criteria(ID))
            End Sub

            Public Overloads Function Save() As Diagnosis
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Diagnosis")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Diagnosis")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update a Diagnosis")
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
                LoadProperty(Of DiagnosisFormList)(LinkedFormsProperty, DiagnosisFormList.NewDiagnosisFormList())
                ValidationRules.CheckRules()
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], [Description], [Inactive] "
                        sql &= "FROM [120NursingDiagnosis] "
                        sql &= "WHERE [ID] = " & Criteria.ID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                            End With
                        End Using
                    End Using
                End Using

                ' Load Children
                LoadProperty(Of DiagnosisFormList)(LinkedFormsProperty, DiagnosisFormList.GetDiagnosisFormList(ReadProperty(Of Integer)(IDProperty)))
            End Sub

            Protected Overrides Sub DataPortal_Insert()
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        ' Insert the new Set
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [120NursingDiagnosis] ([Description], [Inactive]) "
                        sql &= "VALUES ("
                        sql &= "'" & Replace(ReadProperty(Of String)(DescriptionProperty), "'", "''") & "', "
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
                FieldManager.UpdateChildren(Me)
            End Sub

            Protected Overrides Sub DataPortal_Update()
                If MyBase.IsDirty Then
                    Using conn As New SqlConnection(Database.ITWConnection)
                        conn.Open()
                        Using cmd As SqlCommand = conn.CreateCommand
                            Dim sql As String

                            sql = "UPDATE [120NursingDiagnosis] SET "
                            sql &= "[Description] = '" & Replace(ReadProperty(Of String)(DescriptionProperty), "'", "''") & "', "
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
                FieldManager.UpdateChildren(Me)
            End Sub

            Protected Overrides Sub DataPortal_DeleteSelf()
                DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
            End Sub

            Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "DELETE FROM [120NDBridge] "
                        sql &= "WHERE [NursingDiagID] = " & criteria.ID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()

                        sql = "DELETE FROM [120NursingDiagnosis] "
                        sql &= "WHERE [ID] = " & criteria.ID & " "

                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                SetProperty(Of DiagnosisFormList)(LinkedFormsProperty, DiagnosisFormList.NewDiagnosisFormList())
            End Sub

#End Region

        End Class

    End Namespace

End Namespace