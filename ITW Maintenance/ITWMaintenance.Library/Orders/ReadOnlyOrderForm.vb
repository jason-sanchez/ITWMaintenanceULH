Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Lookup

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderForm
        Inherits ReadOnlyBase(Of ReadOnlyOrderForm)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
        Public ReadOnly Property Name() As String
            Get
                Return GetProperty(Of String)(NameProperty)
            End Get
        End Property

        Private Shared PathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Path"))
        Public ReadOnly Property Path() As String
            Get
                Return GetProperty(Of String)(PathProperty)
            End Get
        End Property

        Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
        Public ReadOnly Property DisplayOrder() As Integer
            Get
                Return GetProperty(Of Integer)(DisplayOrderProperty)
            End Get
        End Property

        Private Shared FromDisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FromDiscipline", "From Discipline"))
        Public ReadOnly Property FromDiscipline() As String
            Get
                Return GetProperty(Of String)(FromDisciplineProperty)
            End Get
        End Property

        Private Shared ToDisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ToDiscipline", "To Discipline"))
        Public ReadOnly Property ToDiscipline() As String
            Get
                Return GetProperty(Of String)(ToDisciplineProperty)
            End Get
        End Property

        Private Shared QuickPickProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("QuickPick", "Quick Pick"))
        Public ReadOnly Property QuickPick() As Boolean
            Get
                Return GetProperty(Of Boolean)(QuickPickProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Private Shared FieldsProperty As PropertyInfo(Of ReadOnlyOrderFormFieldList) = RegisterProperty(New PropertyInfo(Of ReadOnlyOrderFormFieldList)("Fields"))
        Public ReadOnly Property Fields() As ReadOnlyOrderFormFieldList
            Get
                Return GetProperty(Of ReadOnlyOrderFormFieldList)(FieldsProperty)
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

        Public Shared Function GetOrderFormInfo(ByVal ID As Integer) As ReadOnlyOrderForm
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Order Form")
            End If
            Return DataPortal.Fetch(Of ReadOnlyOrderForm)(New Criteria(ID))
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

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT o.[ID], o.[Name], o.[Path], o.[DisplayOrder], "
                    sql &= "dFrom.[DisName] AS FromDiscipline, dTo.[DisName] AS ToDiscipline, "
                    sql &= "o.[QuickPick], o.[Inactive] "
                    sql &= "FROM [109Order] o "
                    sql &= "LEFT JOIN [116Discipline] dFrom ON o.[FromDiscipline] = dFrom.[disID] "
                    sql &= "LEFT JOIN [116Discipline] dTo ON o.[ToDiscipline] = dTo.[disID] "
                    sql &= "WHERE o.[ID] = " & Criteria.ID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(NameProperty, .GetString("Name"))
                            LoadProperty(Of String)(PathProperty, .GetString("Path"))
                            LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                            LoadProperty(Of String)(FromDisciplineProperty, .GetString("FromDiscipline"))
                            LoadProperty(Of String)(ToDisciplineProperty, .GetString("ToDiscipline"))
                            LoadProperty(Of Boolean)(QuickPickProperty, .GetBoolean("QuickPick"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                        End With
                    End Using
                End Using
            End Using

            LoadProperty(Of ReadOnlyOrderFormFieldList)(FieldsProperty, ReadOnlyOrderFormFieldList.GetOrderFormFieldList(Criteria.ID))
        End Sub

#End Region

    End Class

End Namespace
