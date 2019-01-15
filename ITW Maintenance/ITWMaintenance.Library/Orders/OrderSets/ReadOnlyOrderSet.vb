Imports System.Data.SqlClient

Namespace Orders

    Namespace OrderSets

        <Serializable()> _
        Public Class ReadOnlyOrderSet
            Inherits ReadOnlyBase(Of ReadOnlyOrderSet)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            Public ReadOnly Property ID() As Integer
                Get
                    Return GetProperty(Of Integer)(IDProperty)
                End Get
            End Property

            Private Shared DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
            Public ReadOnly Property Description() As String
                Get
                    Return GetProperty(Of String)(DescriptionProperty)
                End Get
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public ReadOnly Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
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

            Public Shared Function GetOrderSetInfo(ByVal ID As Integer) As ReadOnlyOrderSet
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Order Set")
                End If
                Return DataPortal.Fetch(Of ReadOnlyOrderSet)(New Criteria(ID))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

            Friend Sub New(ByRef dr As SafeDataReader)
                With dr
                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                    LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                    LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                End With
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

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], [Description], [Inactive] "
                        sql &= "FROM [109OrderSet] "
                        sql &= "WHERE [ID] = " & criteria.ID & " "

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
            End Sub

#End Region

        End Class

    End Namespace

End Namespace