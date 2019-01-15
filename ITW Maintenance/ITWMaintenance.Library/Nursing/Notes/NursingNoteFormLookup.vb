Imports System.Data.SqlClient

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class NursingNoteFormLookup
            Inherits BusinessBase(Of NursingNoteFormLookup)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            <System.ComponentModel.DataObjectField(True, True)> _
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

            Private Shared FormNumberProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormNumber", "Form Number"))
            Public Property FormNumber() As Integer
                Get
                    Return GetProperty(Of Integer)(FormNumberProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(FormNumberProperty, value)
                End Set
            End Property

            Private Shared FormFieldProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormField", "Form Field"))
            Public Property FormField() As Integer
                Get
                    Return GetProperty(Of Integer)(FormFieldProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(FormFieldProperty, value)
                End Set
            End Property

            Protected Overrides Function GetIdValue() As Object
                Return GetProperty(Of Integer)(IDProperty)
            End Function

#End Region

#Region " Validation Rules "

            Protected Overrides Sub AddBusinessRules()
                ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                                        New Validation.CommonRules.IntegerMinValueRuleArgs(FormFieldProperty, 1))
                ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMaxValue, _
                                        New Validation.CommonRules.IntegerMaxValueRuleArgs(FormFieldProperty, 20))
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
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

#End Region

#Region " Factory Methods "

            Friend Shared Function NewNursingNoteFormLookup() As NursingNoteFormLookup
                Return New NursingNoteFormLookup()
            End Function

            Friend Shared Function GetNursingNoteFormLookup(ByVal dr As SafeDataReader) As NursingNoteFormLookup
                Return New NursingNoteFormLookup(dr)
            End Function

            Private Sub New()
                MarkAsChild()
                ValidationRules.CheckRules()
            End Sub

            Private Sub New(ByVal dr As SafeDataReader)
                MarkAsChild()
                Fetch(dr)
            End Sub

#End Region

#Region " Data Access "

            Private Sub Fetch(ByVal dr As Csla.Data.SafeDataReader)
                With dr
                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                    LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                    LoadProperty(Of Integer)(FormNumberProperty, .GetInt32("FormNumber"))
                    LoadProperty(Of Integer)(FormFieldProperty, .GetInt32("FormField"))
                End With
                MarkOld()
            End Sub

            Friend Sub Insert(ByRef Conn As SqlConnection)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    ' Only update the field this lookup is linked to...
                    sql = "INSERT INTO [100GlobalLookup] ([Type], [Description], [FormNumber], [FormField]) VALUES "
                    sql &= "('NursingNote', @Description, @FormNumber, @FormField) "
                    sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                    sql &= "SET NOCOUNT OFF "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                    cmd.Parameters.AddWithValue("@FormNumber", ReadProperty(Of Integer)(FormNumberProperty))
                    cmd.Parameters.AddWithValue("@FormField", ReadProperty(Of Integer)(FormFieldProperty))

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using

                MarkOld()
            End Sub

            Friend Sub Update(ByRef Conn As SqlConnection)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    ' Only update the field this lookup is linked to...
                    sql = "UPDATE [100GlobalLookup] SET "
                    sql &= "[Description] = @Description, "
                    sql &= "[FormNumber] = @FormNumber, "
                    sql &= "[FormField] = @FormField "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                    cmd.Parameters.AddWithValue("@FormNumber", ReadProperty(Of Integer)(FormNumberProperty))
                    cmd.Parameters.AddWithValue("@FormField", ReadProperty(Of Integer)(FormFieldProperty))
                    cmd.ExecuteNonQuery()
                End Using

                MarkOld()
            End Sub

            Friend Sub DeleteSelf(ByRef Conn As SqlConnection)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                ' If we're new then don't update the database
                If Me.IsNew Then Exit Sub

                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "DELETE FROM [100GlobalLookup] "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", Me.ID)
                    cmd.ExecuteNonQuery()
                End Using

                MarkNew()
            End Sub

#End Region

        End Class

    End Namespace

End Namespace