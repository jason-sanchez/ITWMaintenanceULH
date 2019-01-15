Imports System.Data.SqlClient

Namespace Orders

    Namespace OrderSets

        <Serializable()> _
        Public Class OrderSet
            Inherits BusinessBase(Of OrderSet)

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

            Private Shared OrdersProperty As PropertyInfo(Of OrderSetOrderList) = RegisterProperty(New PropertyInfo(Of OrderSetOrderList)("Orders"))
            Public Property Orders() As OrderSetOrderList
                Get
                    Return GetProperty(Of OrderSetOrderList)(OrdersProperty)
                End Get
                Set(ByVal value As OrderSetOrderList)
                    SetProperty(Of OrderSetOrderList)(OrdersProperty, value)
                End Set
            End Property

            Private Shared ServiceTypeProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ServiceType", "Service Type"))
            Public Property ServiceType() As Integer
                Get
                    Return GetProperty(Of Integer)(ServiceTypeProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(ServiceTypeProperty, value)
                End Set
            End Property

            Private Shared IntakeFacilityProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("IntakeFacility", "Intake Facility"))
            Public Property IntakeFacility() As Integer
                Get
                    Return GetProperty(Of Integer)(IntakeFacilityProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(IntakeFacilityProperty, value)
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

            Public Shared Function NewOrderSet() As OrderSet
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Order Set")
                End If
                Return DataPortal.Create(Of OrderSet)()
            End Function

            Public Shared Function GetOrderSet(ByVal ID As Integer) As OrderSet
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Order Set")
                End If
                Return DataPortal.Fetch(Of OrderSet)(New Criteria(ID))
            End Function

            Public Shared Sub DeleteOrderSet(ByVal ID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Order Set")
                End If
                DataPortal.Delete(New Criteria(ID))
            End Sub

            Public Overloads Function Save() As OrderSet
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove an Order Set")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add an Order Set")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update an Order Set")
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
                LoadProperty(Of OrderSetOrderList)(OrdersProperty, OrderSetOrderList.NewOrderSetOrderList())
                ValidationRules.CheckRules()
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], [Description], [Inactive], [SvcType], [IntakeFacility] "
                        sql &= "FROM [109OrderSet] "
                        sql &= "WHERE [ID] = " & Criteria.ID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                                LoadProperty(Of Integer)(ServiceTypeProperty, .GetInt32("SvcType"))
                                LoadProperty(Of Integer)(IntakeFacilityProperty, .GetInt32("IntakeFacility"))
                            End With
                        End Using
                    End Using
                End Using

                ' Load Children
                LoadProperty(Of OrderSetOrderList)(OrdersProperty, OrderSetOrderList.GetOrderSetOrderList(ReadProperty(Of Integer)(IDProperty)))
            End Sub

            Protected Overrides Sub DataPortal_Insert()
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        ' Insert the new Set
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [109OrderSet] ([Description], [Inactive], "
                        sql &= "[SvcType], [IntakeFacility]) "
                        sql &= "VALUES (@Description, @Inactive, @ServiceType, @IntakeFacility) "
                        sql &= "SELECT @@IDENTITY AS NewID "
                        sql &= "SET NOCOUNT OFF "

                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                        cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                        cmd.Parameters.AddWithValue("@ServiceType", ReadProperty(Of Integer)(ServiceTypeProperty))
                        cmd.Parameters.AddWithValue("@IntakeFacility", ReadProperty(Of Integer)(IntakeFacilityProperty))

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

                            sql = "UPDATE [109OrderSet] SET "
                            sql &= "[Description] = @Description, "
                            sql &= "[Inactive] = @Inactive, "
                            sql &= "[SvcType] = @ServiceType, "
                            sql &= "[IntakeFacility] = @IntakeFacility "
                            sql &= "WHERE [ID] = @ID "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql

                            cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                            cmd.Parameters.AddWithValue("@Description", ReadProperty(Of String)(DescriptionProperty))
                            cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                            If ReadProperty(Of Integer)(ServiceTypeProperty) = 0 Then
                                cmd.Parameters.AddWithValue("ServiceType", DBNull.Value)
                            Else
                                cmd.Parameters.AddWithValue("@ServiceType", ReadProperty(Of Integer)(ServiceTypeProperty))
                            End If

                            If ReadProperty(Of Integer)(IntakeFacilityProperty) = 0 Then
                                cmd.Parameters.AddWithValue("IntakeFacility", DBNull.Value)
                            Else
                                cmd.Parameters.AddWithValue("@IntakeFacility", ReadProperty(Of Integer)(IntakeFacilityProperty))
                            End If

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

                        sql = "DELETE FROM [109OrderSetOrder] "
                        sql &= "WHERE [OrderSetID] = " & criteria.ID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()

                        sql = "DELETE FROM [109OrderSet] "
                        sql &= "WHERE [ID] = " & criteria.ID & " "

                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' Reset the children
                SetProperty(Of OrderSetOrderList)(OrdersProperty, OrderSetOrderList.NewOrderSetOrderList())
            End Sub

#End Region

        End Class

    End Namespace

End Namespace