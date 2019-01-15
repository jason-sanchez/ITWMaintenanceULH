Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderCatalogItem
        Inherits ReadOnlyBase(Of ReadOnlyOrderCatalogItem)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared AliasProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Alias"))
        Public ReadOnly Property [Alias]() As String
            Get
                Return GetProperty(Of String)(AliasProperty)
            End Get
        End Property

        Private Shared DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
        Public ReadOnly Property Description() As String
            Get
                Return GetProperty(Of String)(DescriptionProperty)
            End Get
        End Property

        Private Shared OrderGroupProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("OrderGroup", "Order Group"))
        Public ReadOnly Property OrderGroup() As String
            Get
                Return GetProperty(Of String)(OrderGroupProperty)
            End Get
        End Property

        Private Shared LabCollectProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("LabCollect", "Lab Collect"))
        Public ReadOnly Property LabCollect() As Boolean
            Get
                Return GetProperty(Of Boolean)(LabCollectProperty)
            End Get
        End Property

        Private Shared AutoExpandDetailsProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("AutoExpandDetails", "Auto Expand Details"))
        Public ReadOnly Property AutoExpandDetails() As Boolean
            Get
                Return GetProperty(Of Boolean)(AutoExpandDetailsProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetOrderCatalogInfo(ByVal [Alias] As String) As ReadOnlyOrderCatalogItem
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Order Catalog Item")
            End If
            Return DataPortal.Fetch(Of ReadOnlyOrderCatalogItem)(New Criteria([Alias]))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(AliasProperty, .GetString("Alias"))
                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                LoadProperty(Of String)(OrderGroupProperty, .GetString("OrderGroup"))
                LoadProperty(Of Boolean)(LabCollectProperty, .GetBoolean("LabCollect"))
                LoadProperty(Of Boolean)(AutoExpandDetailsProperty, .GetBoolean("AutoExpandDetails"))
            End With
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _Alias As Integer

            Public ReadOnly Property OrderAlias() As String
                Get
                    Return Me._Alias
                End Get
            End Property

            Public Sub New(ByVal OrderAlias As Integer)
                Me._Alias = OrderAlias
            End Sub
        End Class

        '<RunLocal()> _
        'Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
        '    Dim c As Criteria = DirectCast(criteria, Criteria)

        '    If c.EvalID > 0 Then
        '        LoadProperty(Of Integer)(EvalIDProperty, c.EvalID)
        '    End If
        'End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Alias], [Description], [OrderGroup], [LabCollect], [AutoExpandDetails] "
                    sql &= "FROM [OrderCatalog] "
                    sql &= "WHERE [Alias] = @Alias "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@Alias", criteria.OrderAlias)

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
        End Sub

#End Region

    End Class

End Namespace