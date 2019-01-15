Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class OrderFormField
        Inherits BusinessBase(Of OrderFormField)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Label", "Field Label"))
        Public Property Label() As String
            Get
                Return GetProperty(Of String)(LabelProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(LabelProperty, value)
            End Set
        End Property

        Private Shared TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Type"))
        Public Property Type() As String
            Get
                Return GetProperty(Of String)(TypeProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(TypeProperty, value)
            End Set
        End Property

        Private Shared OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Options"))
        Public Property Options() As String
            Get
                Return GetProperty(Of String)(OptionsProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(OptionsProperty, value)
            End Set
        End Property

        Private Shared HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("HelpText"))
        Public Property HelpText() As String
            Get
                Return GetProperty(Of String)(HelpTextProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(HelpTextProperty, value)
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

        Private Shared DefaultValueProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("DefaultValue", "Default Value"))
        Public Property DefaultValue() As String
            Get
                Return GetProperty(Of String)(DefaultValueProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(DefaultValueProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, LabelProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(LabelProperty, 75))

            ValidationRules.AddRule(Of OrderFormField)(AddressOf ValidType(Of OrderFormField), TypeProperty)

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(DefaultValueProperty, 200))
        End Sub

        Private Shared Function ValidType(Of T As OrderFormField)(ByVal target As T, _
                                                                  ByVal e As Validation.RuleArgs) As Boolean
            ' Make sure the type is in the list
            If Not OrderFormFieldTypes.Contains(target.Type) Then
                e.Description = "This Order Field is not an approved type of field."
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
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewOrderFormField(ByVal NewDisplayOrder As Integer) As OrderFormField
            Return DataPortal.CreateChild(Of OrderFormField)(NewDisplayOrder, OrderFormFieldTypes.GetDefaultFieldType())
        End Function

        Friend Shared Function GetOrderFormField(ByVal dr As SafeDataReader) As OrderFormField
            Return DataPortal.FetchChild(Of OrderFormField)(dr)
        End Function

#End Region

#Region " Data Access "

        Private Overloads Sub Child_Create(ByVal NewDisplayOrder As Integer, ByVal NewType As String)
            LoadProperty(Of Integer)(DisplayOrderProperty, NewDisplayOrder)
            LoadProperty(Of String)(TypeProperty, NewType)
            ValidationRules.CheckRules()
        End Sub

        Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(LabelProperty, .GetString("Label"))
                LoadProperty(Of String)(TypeProperty, .GetString("Type"))
                LoadProperty(Of String)(OptionsProperty, .GetString("Options"))
                LoadProperty(Of String)(HelpTextProperty, .GetString("HelpText"))
                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                LoadProperty(Of String)(DefaultValueProperty, .GetString("DefaultValue"))
            End With
            MarkOld()
        End Sub

        Private Sub Child_Insert(ByVal Order As OrderForm)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [109OrderFormField] ([OrderID], [Label], [Type], "
                    sql &= "[Options], [HelpText], [DisplayOrder], [DefaultValue]) VALUES ("
                    sql &= "@OrderID, @Label, @Type, @Options, @HelpText, @DisplayOrder, @DefaultValue) "
                    sql &= "SELECT @@IDENTITY AS NewID "
                    sql &= "SET NOCOUNT OFF "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@OrderID", Order.ID)
                    cmd.Parameters.AddWithValue("@Label", ReadProperty(Of String)(LabelProperty))
                    cmd.Parameters.AddWithValue("@Type", ReadProperty(Of String)(TypeProperty))

                    If Not String.IsNullOrEmpty(ReadProperty(Of String)(OptionsProperty)) Then
                        ' Trim each option
                        Dim arrOptions() As String = ReadProperty(Of String)(OptionsProperty).Split(vbCrLf)
                        For i As Int16 = 0 To UBound(arrOptions)
                            ' Note that for some reason, splitting on the vbCrLf doesn't remove it
                            arrOptions(i) = Trim(Replace(Replace(Replace(arrOptions(i), vbCrLf, ""), vbCr, ""), vbLf, ""))
                        Next
                        cmd.Parameters.AddWithValue("@Options", Join(arrOptions, vbCrLf))
                    Else
                        cmd.Parameters.AddWithValue("@Options", ReadProperty(Of String)(OptionsProperty))
                    End If

                    cmd.Parameters.AddWithValue("@HelpText", ReadProperty(Of String)(HelpTextProperty))
                    cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                    cmd.Parameters.AddWithValue("@DefaultValue", Trim(ReadProperty(Of String)(DefaultValueProperty)))

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using

                MarkOld()
            End Using
        End Sub

        Private Sub Child_Update(ByVal Order As OrderForm)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "UPDATE [109OrderFormField] SET "
                    sql &= "[Label] = @Label, "
                    sql &= "[Type] = @Type, "
                    sql &= "[Options] = @Options, "
                    sql &= "[HelpText] = @HelpText, "
                    sql &= "[DisplayOrder] = @DisplayOrder, "
                    sql &= "[DefaultValue] = @DefaultValue "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                    cmd.Parameters.AddWithValue("@Label", ReadProperty(Of String)(LabelProperty))
                    cmd.Parameters.AddWithValue("@Type", ReadProperty(Of String)(TypeProperty))

                    If Not String.IsNullOrEmpty(ReadProperty(Of String)(OptionsProperty)) Then
                        ' Trim each option
                        Dim arrOptions() As String = ReadProperty(Of String)(OptionsProperty).Split(vbCrLf)
                        For i As Int16 = 0 To UBound(arrOptions)
                            ' Note that for some reason, splitting on the vbCrLf doesn't remove it
                            arrOptions(i) = Trim(Replace(Replace(Replace(arrOptions(i), vbCrLf, ""), vbCr, ""), vbLf, ""))
                        Next
                        cmd.Parameters.AddWithValue("@Options", Join(arrOptions, vbCrLf))
                    Else
                        cmd.Parameters.AddWithValue("@Options", ReadProperty(Of String)(OptionsProperty))
                    End If

                    cmd.Parameters.AddWithValue("@HelpText", ReadProperty(Of String)(HelpTextProperty))
                    cmd.Parameters.AddWithValue("@DisplayOrder", ReadProperty(Of Integer)(DisplayOrderProperty))
                    cmd.Parameters.AddWithValue("@DefaultValue", Trim(ReadProperty(Of String)(DefaultValueProperty)))
                    cmd.ExecuteNonQuery()
                End Using

                MarkOld()
            End Using
        End Sub

        Private Sub Child_DeleteSelf(ByVal Order As OrderForm)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' If we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "DELETE FROM [109OrderFormField] "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", Me.ID)
                    cmd.ExecuteNonQuery()
                End Using

                MarkNew()
            End Using
        End Sub

#End Region

    End Class

End Namespace