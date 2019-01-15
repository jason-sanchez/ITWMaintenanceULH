Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Lookup

Namespace Orders

    <Serializable()> _
    Public Class OrderFacilityActionItem
        Inherits BusinessBase(Of OrderFacilityActionItem)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        'Private Shared AliasProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Alias"))
        'Public ReadOnly Property [Alias]() As String
        '    Get
        '        Return GetProperty(Of String)(AliasProperty)
        '    End Get
        'End Property

        Private Shared IntakeFacilityProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("IntakeFacility", "Intake Facility"))
        Public Property IntakeFacility() As Integer
            Get
                Return GetProperty(Of Integer)(IntakeFacilityProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(IntakeFacilityProperty, value)
            End Set
        End Property

        Private Shared IntakeFacilityDescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("IntakeFacilityDescription", "Intake Facility Description"))
        Public ReadOnly Property IntakeFacilityDescription() As String
            Get
                Return GetProperty(Of String)(IntakeFacilityDescriptionProperty)
            End Get
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

        Private Shared DisciplineDescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("DisciplineDescription", "Discipline Description"))
        Public ReadOnly Property DisciplineDescription() As String
            Get
                Return GetProperty(Of String)(DisciplineDescriptionProperty)
            End Get
        End Property

        Private Shared ActionCodeProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ActionCode", "Action Code"))
        Public Property ActionCode() As Integer
            Get
                Return GetProperty(Of Integer)(ActionCodeProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(ActionCodeProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(Of OrderFacilityActionItem)(AddressOf ValidDiscipline(Of OrderFacilityActionItem), DisciplineProperty)
        End Sub

        Private Shared Function ValidDiscipline(Of T As OrderFacilityActionItem)(ByVal target As T, _
            ByVal e As Validation.RuleArgs) As Boolean

            ' Validate to ensure the user has selected a valid discipline.
            ' Change this to DisciplineList.GetTherapyDisciplines if the discipline
            ' must be a therapy discipline.
            If DisciplineList.GetAllDisciplines.ContainsKey(target.Discipline) Then
                Return True
            Else
                e.Description = "Order Facility Action item is not linked to a valid discipline."
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

        Friend Shared Function NewOrderFacilityActionItem(ByVal IntakeFacility As Integer, ByVal Discipline As Integer, ByVal ActionCode As Integer) As OrderFacilityActionItem
            Return DataPortal.CreateChild(Of OrderFacilityActionItem)(IntakeFacility, Discipline, ActionCode)
        End Function

        Friend Shared Function GetOrderFacilityActionItem(ByVal dr As SafeDataReader) As OrderFacilityActionItem
            Return DataPortal.FetchChild(Of OrderFacilityActionItem)(dr)
        End Function

        Private Sub New()
            ' Force use of Factory methods
        End Sub

#End Region

#Region " Data Access "

        Private Overloads Sub Child_Create(ByVal IntakeFacility As Integer, ByVal Discipline As Integer, ByVal ActionCode As Integer)
            'LoadProperty(Of String)(AliasProperty, [Alias])
            LoadProperty(Of Integer)(IntakeFacilityProperty, IntakeFacility)
            LoadProperty(Of Integer)(DisciplineProperty, Discipline)
            LoadProperty(Of Integer)(ActionCodeProperty, ActionCode)
            ValidationRules.CheckRules()
        End Sub

        Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                'LoadProperty(Of String)(AliasProperty, .GetString("Alias"))
                LoadProperty(Of Integer)(IntakeFacilityProperty, .GetInt32("IntakeFacility"))
                LoadProperty(Of String)(IntakeFacilityDescriptionProperty, .GetString("IntakeFacilityDescription"))
                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                LoadProperty(Of String)(DisciplineDescriptionProperty, .GetString("DisciplineDescription"))
                LoadProperty(Of Integer)(ActionCodeProperty, .GetInt32("ActionCode"))
            End With
            MarkOld()
        End Sub

        Private Sub Child_Insert(ByVal OrderCatalogItem As OrderCatalogItem)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using Conn As New SqlConnection(Database.ITWCernerConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [OrderFacilityAction] ([Alias], [IntakeFacility], [Discipline], [ActionCode]) "
                    sql &= "VALUES(@Alias, @IntakeFacility, @Discipline, @ActionCode) "
                    sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                    sql &= "SET NOCOUNT OFF "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@Alias", OrderCatalogItem.Alias) 'ReadProperty(Of String)(AliasProperty))
                    cmd.Parameters.AddWithValue("@IntakeFacility", ReadProperty(Of Integer)(IntakeFacilityProperty))
                    cmd.Parameters.AddWithValue("@Discipline", ReadProperty(Of Integer)(DisciplineProperty))
                    cmd.Parameters.AddWithValue("@ActionCode", ReadProperty(Of Integer)(ActionCodeProperty))

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using
            End Using

            MarkOld()
        End Sub

        Private Sub Child_Update(ByVal OrderCatalogItem As OrderCatalogItem)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            Using Conn As New SqlConnection(Database.ITWCernerConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "UPDATE [OrderFacilityAction] SET "
                    sql &= "[IntakeFacility] = @IntakeFacility, "
                    sql &= "[Discipline] = @Discipline, "
                    sql &= "[ActionCode] = @ActionCode "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                    cmd.Parameters.AddWithValue("@IntakeFacility", ReadProperty(Of Integer)(IntakeFacilityProperty))
                    cmd.Parameters.AddWithValue("@Discipline", ReadProperty(Of Integer)(DisciplineProperty))
                    cmd.Parameters.AddWithValue("@ActionCode", ReadProperty(Of Integer)(ActionCodeProperty))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MarkOld()
        End Sub

        Private Sub Child_DeleteSelf(ByVal OrderCatalogItem As OrderCatalogItem)
            ' If we're not dirty then don't update the database
            If Not Me.IsDirty Then Exit Sub

            ' If we're new then don't update the database
            If Me.IsNew Then Exit Sub

            Using Conn As New SqlConnection(Database.ITWCernerConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "DELETE FROM [OrderFacilityAction] "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", Me.ID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MarkNew()
        End Sub

#End Region

    End Class

End Namespace