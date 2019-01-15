Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Nursing.Notes

Namespace Nursing

    Namespace Diagnoses

        <Serializable()> _
        Public Class DiagnosisForm
            Inherits BusinessBase(Of DiagnosisForm)

#Region " Business Methods "

            Private Shared FormIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormID", "Form ID"))
            Public ReadOnly Property FormID() As Integer
                Get
                    Return GetProperty(Of Integer)(FormIDProperty)
                End Get
            End Property

            Private Shared FormNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FormName", "Form Name"))
            Public ReadOnly Property FormName() As String
                Get
                    Return GetProperty(Of String)(FormNameProperty)
                End Get
            End Property

            'Protected Overrides Function GetIdValue() As Object
            '    Return GetProperty(Of Integer)(IDProperty)
            'End Function

#End Region

#Region " Validation Rules "

            Protected Overrides Sub AddBusinessRules()
                ValidationRules.AddRule(Of DiagnosisForm)(AddressOf ValidFormID(Of DiagnosisForm), FormIDProperty)
            End Sub

            Private Shared Function ValidFormID(Of T As DiagnosisForm)(ByVal target As T, _
                                                                      ByVal e As Validation.RuleArgs) As Boolean
                ' TODO - Make sure the type is in the list
                'If Not OrderFormFieldTypes.Contains(target.Type) Then

                If target.FormID = 0 Then
                    e.Description = "This Form Number is not a valid Form."
                    Return False
                End If

                Return True
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
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return False 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

#End Region

#Region " Factory Methods "

            Friend Shared Function NewDiagnosisForm(ByVal FormID As Integer) As DiagnosisForm
                Return DataPortal.CreateChild(Of DiagnosisForm)(FormID)
            End Function

            Friend Shared Function GetDiagnosisForm(ByVal dr As SafeDataReader) As DiagnosisForm
                Return DataPortal.FetchChild(Of DiagnosisForm)(dr)
            End Function

#End Region

#Region " Data Access "

            Private Overloads Sub Child_Create(ByVal FormID As Integer)
                LoadProperty(Of Integer)(FormIDProperty, FormID)
                LoadProperty(Of String)(FormNameProperty, ReadOnlyNursingNote.GetInfo(FormID).Name)
                ValidationRules.CheckRules()
            End Sub

            Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
                With dr
                    LoadProperty(Of Integer)(FormIDProperty, .GetInt32("FormID"))
                    LoadProperty(Of String)(FormNameProperty, .GetString("FormName"))
                End With
                MarkOld()
            End Sub

            Private Sub Child_Insert(ByVal Diagnosis As Diagnosis)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "INSERT INTO [120NDBridge] ([NursingDiagID], [Form]) VALUES ("
                        sql &= "@DiagnosisID, @FormID) "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@DiagnosisID", Diagnosis.ID)
                        cmd.Parameters.AddWithValue("@FormID", ReadProperty(Of Integer)(FormIDProperty))

                        cmd.ExecuteNonQuery()
                    End Using

                    MarkOld()
                End Using
            End Sub

            Private Sub Child_Update(ByVal Diagnosis As Diagnosis)
                '' If we're not dirty then don't update the database
                'If Not Me.IsDirty Then Exit Sub

                'Using conn As New SqlConnection(Database.ITWConnection)
                '    conn.Open()
                '    Using cmd As SqlCommand = conn.CreateCommand
                '        Dim sql As String

                '        sql = "UPDATE [120NDBridge] SET "
                '        sql &= "[Label] = @Label, "
                '        sql &= "[Type] = @Type, "
                '        sql &= "[Options] = @Options, "
                '        sql &= "[HelpText] = @HelpText, "
                '        sql &= "[DisplayOrder] = @DisplayOrder "
                '        sql &= "WHERE [ID] = @ID "

                '        cmd.CommandType = CommandType.Text
                '        cmd.CommandText = sql

                '        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                '        cmd.Parameters.AddWithValue("@Label", ReadProperty(Of String)(LabelProperty))
                '        cmd.Parameters.AddWithValue("@Type", ReadProperty(Of String)(TypeProperty))
                '        cmd.Parameters.AddWithValue("@Options", ReadProperty(Of String)(OptionsProperty))
                '        cmd.Parameters.AddWithValue("@HelpText", ReadProperty(Of String)(HelpTextProperty))
                '        cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                '        cmd.ExecuteNonQuery()
                '    End Using

                '    MarkOld()
                'End Using
            End Sub

            Private Sub Child_DeleteSelf(ByVal Diagnosis As Diagnosis)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                ' If we're new then don't update the database
                If Me.IsNew Then Exit Sub

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "DELETE FROM [120NDBridge] "
                        sql &= "WHERE [NursingDiagID] = @DiagnosisID "
                        sql &= "AND [Form] = @FormID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@DiagnosisID", Diagnosis.ID)
                        cmd.Parameters.AddWithValue("@FormID", ReadProperty(Of Integer)(FormIDProperty))
                        cmd.ExecuteNonQuery()
                    End Using

                    MarkNew()
                End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace