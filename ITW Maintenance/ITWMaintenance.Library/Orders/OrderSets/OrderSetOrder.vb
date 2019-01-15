Imports System.Data.SqlClient

Namespace Orders

    Namespace OrderSets

        <Serializable()> _
        Public Class OrderSetOrder
            Inherits BusinessBase(Of OrderSetOrder)

#Region " Business Methods "

            Private Shared OrderIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("OrderID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property OrderID() As Integer
                Get
                    Return GetProperty(Of Integer)(OrderIDProperty)
                End Get
            End Property

            Private Shared PathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Path"))
            Public ReadOnly Property Path() As String
                Get
                    Return GetProperty(Of String)(PathProperty)
                End Get
            End Property

            Private Shared SequenceProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Sequence"))
            Public Property Sequence() As Integer
                Get
                    Return GetProperty(Of Integer)(SequenceProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(SequenceProperty, value)
                End Set
            End Property

            'Protected Overrides Function GetIdValue() As Object
            '    Return GetProperty(Of Integer)(OrderIDProperty)
            'End Function

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

            Friend Shared Function NewOrderSetOrder(ByVal OrderID As Integer, ByVal NewSequence As Integer) As OrderSetOrder
                Return DataPortal.CreateChild(Of OrderSetOrder)(OrderID, NewSequence)
            End Function

            Friend Shared Function GetOrderSetOrder(ByVal dr As SafeDataReader) As OrderSetOrder
                Return DataPortal.FetchChild(Of OrderSetOrder)(dr)
            End Function

#End Region

#Region " Data Access "

            Private Overloads Sub Child_Create(ByVal OrderID As Integer, ByVal NewSequence As Integer)
                LoadProperty(Of Integer)(OrderIDProperty, OrderID)
                LoadProperty(Of String)(PathProperty, ReadOnlyOrder.GetOrderInfo(OrderID).Path)
                LoadProperty(Of Integer)(SequenceProperty, NewSequence)
                ValidationRules.CheckRules()
            End Sub

            Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
                With dr
                    LoadProperty(Of Integer)(OrderIDProperty, .GetInt32("OrderID"))
                    LoadProperty(Of String)(PathProperty, .GetString("Path"))
                    LoadProperty(Of Integer)(SequenceProperty, .GetInt32("Sequence"))
                End With
                MarkOld()
            End Sub

            Private Sub Child_Insert(ByVal TheSet As OrderSet)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "INSERT INTO [109OrderSetOrder] ([OrderSetID], [OrderID], [Sequence]) "
                        sql &= "VALUES (@OrderSetID, @OrderID, @Sequence) "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@OrderSetID", TheSet.ID)
                        cmd.Parameters.AddWithValue("@OrderID", ReadProperty(Of Integer)(OrderIDProperty))
                        cmd.Parameters.AddWithValue("@Sequence", ReadProperty(Of Integer)(SequenceProperty))

                        cmd.ExecuteNonQuery()
                    End Using

                    MarkOld()
                End Using
            End Sub

            Private Sub Child_Update(ByVal TheSet As OrderSet)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "UPDATE [109OrderSetOrder] SET "
                        sql &= "[Sequence] = @Sequence "
                        sql &= "WHERE [OrderSetID] = @OrderSetID "
                        sql &= "AND [OrderID] = @OrderID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@OrderSetID", TheSet.ID)
                        cmd.Parameters.AddWithValue("@OrderID", ReadProperty(Of Integer)(OrderIDProperty))
                        cmd.Parameters.AddWithValue("@Sequence", ReadProperty(Of Integer)(SequenceProperty))
                        cmd.ExecuteNonQuery()
                    End Using

                    MarkOld()
                End Using
            End Sub

            Private Sub Child_DeleteSelf(ByVal TheSet As OrderSet)
                ' If we're not dirty then don't update the database
                If Not Me.IsDirty Then Exit Sub

                ' If we're new then don't update the database
                If Me.IsNew Then Exit Sub

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "DELETE FROM [109OrderSetOrder] "
                        sql &= "WHERE [OrderSetID] = @OrderSetID "
                        sql &= "AND [OrderID] = @OrderID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@OrderSetID", TheSet.ID)
                        cmd.Parameters.AddWithValue("@OrderID", ReadProperty(Of Integer)(OrderIDProperty))
                        cmd.ExecuteNonQuery()
                    End Using

                    MarkNew()
                End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace