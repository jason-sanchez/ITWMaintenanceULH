Imports System.Data.SqlClient
Imports Csla.Data

Namespace Orders

    <Serializable()> _
    Public Class OrderCatalogItem
        Inherits BusinessBase(Of OrderCatalogItem)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared AliasProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Alias"))
        Public Property [Alias]() As String
            Get
                Return GetProperty(Of String)(AliasProperty)
            End Get
            Set(value As String)
                SetProperty(Of String)(AliasProperty, value)
            End Set
        End Property

        Private Shared DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
        Public Property Description() As String
            Get
                Return GetProperty(Of String)(DescriptionProperty)
            End Get
            Set(value As String)
                SetProperty(Of String)(DescriptionProperty, value)
            End Set
        End Property

        Private Shared OrderGroupProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("OrderGroup", "Order Group"))
        Public Property OrderGroup() As String
            Get
                Return GetProperty(Of String)(OrderGroupProperty)
            End Get
            Set(value As String)
                SetProperty(Of String)(OrderGroupProperty, value)
            End Set
        End Property

        Private Shared LabCollectProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("LabCollect", "Lab Collect"))
        Public Property LabCollect() As Boolean
            Get
                Return GetProperty(Of Boolean)(LabCollectProperty)
            End Get
            Set(value As Boolean)
                SetProperty(Of Boolean)(LabCollectProperty, value)
            End Set
        End Property

        Private Shared AutoExpandDetailsProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("AutoExpandDetails", "Auto Expand Details"))
        Public Property AutoExpandDetails() As Boolean
            Get
                Return GetProperty(Of Boolean)(AutoExpandDetailsProperty)
            End Get
            Set(value As Boolean)
                SetProperty(Of Boolean)(AutoExpandDetailsProperty, value)
            End Set
        End Property

        Private Shared OrderFacilityActionItemsProperty As PropertyInfo(Of OrderFacilityActionItemList) = RegisterProperty(New PropertyInfo(Of OrderFacilityActionItemList)("OrderFacilityActionItems", "Order Facility Action Items"))
        Public ReadOnly Property OrderFacilityActionItems() As OrderFacilityActionItemList
            Get
                Return GetProperty(Of OrderFacilityActionItemList)(OrderFacilityActionItemsProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, AliasProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, DescriptionProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, OrderGroupProperty)

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(AliasProperty, 25))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(DescriptionProperty, 100))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(OrderGroupProperty, 25))

            ValidationRules.AddRule(Of OrderCatalogItem)(AddressOf UniqueAlias(Of OrderCatalogItem), AliasProperty)
        End Sub

        Private Shared Function UniqueAlias(Of T As OrderCatalogItem)(ByVal target As T, _
            ByVal e As Validation.RuleArgs) As Boolean

            ' Validate to ensure the alias entered doesn't already exist in the catalog.
            If AliasExistsCommand.AliasAlreadyExists(target.Alias, target.ID) Then
                e.Description = "Alias '" & target.Alias & "' already exists in the Catalog."
                Return False
            Else
                Return True
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

        Public Shared Function NewOrderCatalogItem() As OrderCatalogItem
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an order to the catalog.")
            End If
            Return DataPortal.Create(Of OrderCatalogItem)()
        End Function

        Public Shared Function GetOrderCatalogItem(ByVal ID As Integer) As OrderCatalogItem
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an order.")
            End If
            Return DataPortal.Fetch(Of OrderCatalogItem)(New Criteria(ID))
        End Function

        Public Shared Sub DeleteOrderCatalogItem(ByVal ID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a Level 1 Eval Folder")
            End If
            DataPortal.Delete(New Criteria(ID))
        End Sub

        Public Overloads Function Save() As OrderCatalogItem
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an order.")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an order to the catalog.")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update an order.")
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
            SetProperty(Of OrderFacilityActionItemList)(OrderFacilityActionItemsProperty, OrderFacilityActionItemList.NewOrderFacilityActionItemList())
            ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Alias], [Description], [OrderGroup], [LabCollect], [AutoExpandDetails] "
                    sql &= "FROM [OrderCatalog] "
                    sql &= "WHERE [ID] = " & criteria.ID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(AliasProperty, .GetString("Alias"))
                            LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                            LoadProperty(Of String)(OrderGroupProperty, .GetString("OrderGroup"))
                            LoadProperty(Of Boolean)(LabCollectProperty, .GetBoolean("LabCollect"))
                            LoadProperty(Of Boolean)(AutoExpandDetailsProperty, .GetBoolean("AutoExpandDetails"))
                        End With
                    End Using
                End Using
            End Using

            ' Load the children
            LoadProperty(Of OrderFacilityActionItemList)(OrderFacilityActionItemsProperty, OrderFacilityActionItemList.GetOrderFacilityActionItems(ReadProperty(Of String)(AliasProperty)))
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Insert the new order
                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [OrderCatalog] ([Alias], [Description], [OrderGroup], [LabCollect], [AutoExpandDetails]) "
                    sql &= "VALUES (@Alias, @Description, @OrderGroup, @LabCollect, @AutoExpandDetails) "
                    sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                    sql &= "SET NOCOUNT OFF "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@Alias", ReadProperty(Of String)(AliasProperty))
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                    cmd.Parameters.AddWithValue("@OrderGroup", ReadProperty(Of String)(OrderGroupProperty))
                    cmd.Parameters.AddWithValue("@LabCollect", ReadProperty(Of Boolean)(LabCollectProperty))
                    cmd.Parameters.AddWithValue("@AutoExpandDetails", ReadProperty(Of Boolean)(AutoExpandDetailsProperty))

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))

                    ' Update child objects
                    FieldManager.UpdateChildren(Me)
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Using conn As New SqlConnection(Database.ITWCernerConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "UPDATE [OrderCatalog] SET "
                        sql &= "[Alias] = @Alias, "
                        sql &= "[Description] = @Description, "
                        sql &= "[OrderGroup] = @OrderGroup, "
                        sql &= "[LabCollect] = @LabCollect, "
                        sql &= "[AutoExpandDetails] = @AutoExpandDetails "
                        sql &= "WHERE [ID] = @ID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.Parameters.AddWithValue("@Alias", ReadProperty(Of String)(AliasProperty))
                        cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                        cmd.Parameters.AddWithValue("@OrderGroup", ReadProperty(Of String)(OrderGroupProperty))
                        cmd.Parameters.AddWithValue("@LabCollect", ReadProperty(Of Boolean)(LabCollectProperty))
                        cmd.Parameters.AddWithValue("@AutoExpandDetails", ReadProperty(Of Boolean)(AutoExpandDetailsProperty))

                        cmd.ExecuteNonQuery()

                        ' Update child objects
                        FieldManager.UpdateChildren(Me)
                    End Using
                End Using
            End If

            ' Update child objects
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' First remove the [OrderFacilityAction] records
                    sql = "DELETE FROM [OrderFacilityAction] "
                    sql &= "WHERE [Alias] = ("
                    sql &= "    SELECT [Alias] FROM [OrderCatalog] "
                    sql &= "    WHERE [ID] = @OrderID "
                    sql &= ") "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    cmd.Parameters.AddWithValue("@OrderID", ReadProperty(Of Integer)(IDProperty))
                    cmd.ExecuteNonQuery()

                    sql = "DELETE FROM [OrderCatalog] "
                    sql &= "WHERE [ID] = @OrderID "

                    cmd.CommandText = sql
                    cmd.Parameters.AddWithValue("@OrderID", ReadProperty(Of Integer)(IDProperty))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

        <Serializable()> _
        Private Class AliasExistsCommand
            Inherits CommandBase

            Private _Alias As String
            Private _IDtoIgnore As Integer = 0
            Private _Exists As Boolean

            Public ReadOnly Property Exists() As Boolean
                Get
                    Return _Exists
                End Get
            End Property

            Public Shared Function AliasAlreadyExists(ByVal [Alias] As String, ByVal IDtoIgnore As Integer) As Boolean
                Dim result As AliasExistsCommand
                result = DataPortal.Execute(Of AliasExistsCommand)(New AliasExistsCommand([Alias], IDtoIgnore))
                Return result.Exists
            End Function

            Public Sub New(ByVal [Alias] As String, ByVal IDtoIgnore As Integer)
                _Alias = [Alias]
                _IDtoIgnore = IDtoIgnore
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Using conn As New SqlConnection(Database.ITWCernerConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT COUNT([ID]) AS [Count] "
                        sql &= "FROM [OrderCatalog] "
                        sql &= "WHERE [Alias] = @Alias "
                        sql &= "AND [ID] <> @IDtoIgnore "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@Alias", _Alias)
                        cmd.Parameters.AddWithValue("@IDtoIgnore", _IDtoIgnore)

                        If cmd.ExecuteScalar() > 0 Then
                            _Exists = True
                        Else
                            _Exists = False
                        End If
                    End Using
                End Using
            End Sub
        End Class

    End Class

End Namespace
