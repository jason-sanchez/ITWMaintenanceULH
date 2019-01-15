Imports System.Data.SqlClient

Namespace Classifications

    <Serializable()> _
    Public Class DiagnosisClassification
        Inherits BusinessBase(Of DiagnosisClassification)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared CategoryProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Category"))
        Public Property Category() As String
            Get
                Return GetProperty(Of String)(CategoryProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(CategoryProperty, value)
            End Set
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

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, CategoryProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(CategoryProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, DescriptionProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(DescriptionProperty, 100))
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
            ' Use the inactive switch
            Return False 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewDiagnosisClassification() As DiagnosisClassification
            Return DataPortal.CreateChild(Of DiagnosisClassification)()
        End Function

        Friend Shared Function GetDiagnosisClassification(ByVal dr As SafeDataReader) As DiagnosisClassification
            Return DataPortal.FetchChild(Of DiagnosisClassification)(dr)
        End Function

#End Region

#Region " Data Access "

        <RunLocal()> _
        Private Overloads Sub Child_Create()
            LoadProperty(Of Boolean)(InactiveProperty, False)
            ValidationRules.CheckRules()
        End Sub

        Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(CategoryProperty, .GetString("Category"))
                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
            End With
            MarkOld()
        End Sub

        Private Sub Child_Insert(ByVal Conn As SqlConnection)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using cmd As SqlCommand = Conn.CreateCommand
                Dim sql As String

                sql = "SET NOCOUNT ON "
                sql &= "INSERT INTO [111DiagnosisClass] ([Category], [Description], [Inactive]) "
                sql &= "VALUES (@Category, @Description, @Inactive) "
                sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                sql &= "SET NOCOUNT OFF "

                cmd.CommandType = CommandType.Text
                cmd.CommandText = sql

                cmd.Parameters.AddWithValue("@Category", ReadProperty(Of String)(CategoryProperty))
                cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                ' Save the new ID that was added
                LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
            End Using

            MarkOld()
        End Sub

        Private Sub Child_Update(ByVal Conn As SqlConnection)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using cmd As SqlCommand = Conn.CreateCommand
                Dim sql As String

                sql = "UPDATE [111DiagnosisClass] SET "
                sql &= "[Category] = @Category, "
                sql &= "[Description] = @Description, "
                sql &= "[Inactive] = @Inactive "
                sql &= "WHERE [ID] = @ID "

                cmd.CommandType = CommandType.Text
                cmd.CommandText = sql

                cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                cmd.Parameters.AddWithValue("@Category", ReadProperty(Of String)(CategoryProperty))
                cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                cmd.ExecuteNonQuery()
            End Using

            MarkOld()
        End Sub

        Private Sub Child_DeleteSelf(ByVal Conn As SqlConnection)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' If we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Using cmd As SqlCommand = Conn.CreateCommand
                Dim sql As String

                sql = "DELETE FROM [111DiagnosisClass] "
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