Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntervention
        Inherits ReadOnlyBase(Of ReadOnlyIntervention)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property intVntID() As Integer
            Get
                Return GetProperty(Of Integer)(intVntIDProperty)
            End Get
        End Property

        Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
        Public ReadOnly Property ParentID() As Integer
            Get
                Return GetProperty(Of Integer)(ParentIDProperty)
            End Get
        End Property

        Private Shared IntVntNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("IntVntName", "Intervention Name"))
        Public ReadOnly Property IntVntName() As String
            Get
                Return GetProperty(Of String)(IntVntNameProperty)
            End Get
        End Property

        Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
        Public ReadOnly Property DisplayOrder() As Integer
            Get
                Return GetProperty(Of Integer)(DisplayOrderProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Private Shared OneTimeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("OneTime", "One Time"))
        Public ReadOnly Property OneTime() As Boolean
            Get
                Return GetProperty(Of Boolean)(OneTimeProperty)
            End Get
        End Property

        Private Shared BillingProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Billing", "Billing"))
        Public ReadOnly Property Billing() As Boolean
            Get
                Return GetProperty(Of Boolean)(BillingProperty)
            End Get
        End Property

        Private Shared IsFormProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("IsForm", "Is Form"))
        Public ReadOnly Property IsForm() As Boolean
            Get
                Return GetProperty(Of Boolean)(IsFormProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(intVntIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetIntVntInfo(ByVal intVntID As Integer) As ReadOnlyIntervention
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Intervention")
            End If
            Return DataPortal.Fetch(Of ReadOnlyIntervention)(New Criteria(intVntID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                LoadProperty(Of String)(IntVntNameProperty, .GetString("IntVntName"))
                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                LoadProperty(Of Boolean)(BillingProperty, .GetBoolean("Billing"))
                LoadProperty(Of Boolean)(IsFormProperty, .GetBoolean("iFinal"))
            End With
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _intVntID As Integer

            Public ReadOnly Property intVntID() As Integer
                Get
                    Return Me._intVntID
                End Get
            End Property

            Public Sub New(ByVal intVntID As Integer)
                Me._intVntID = intVntID
            End Sub
        End Class

        '<RunLocal()> _
        'Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
        '    Dim c As Criteria = DirectCast(criteria, Criteria)

        '    If c.intVntID > 0 Then
        '        LoadProperty(Of Integer)(intVntIDProperty, c.intVntID)
        '    End If
        'End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT i.[ID], i.[intVntID], i.[ParentID], i.[iName] AS [IntVntName], i.[intVntLevel], "
                    sql &= "i.[dOrder] AS [DisplayOrder], i.[Inactive], i.[OneTime], i.[Billing], i.[iFinal] "
                    sql &= "FROM [110IntVnt] i "
                    sql &= "WHERE i.[intVntID] = " & criteria.intVntID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            Try
                                If .GetInt32("intVntLevel") = 2 Then
                                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                    LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                                    LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                    LoadProperty(Of String)(IntVntNameProperty, .GetString("IntVntName"))
                                    LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                                    LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                                    LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                                    LoadProperty(Of Boolean)(BillingProperty, .GetBoolean("Billing"))
                                    LoadProperty(Of Boolean)(IsFormProperty, .GetBoolean("iFinal"))
                                Else
                                    Throw New Exception("Intervention ID #" & criteria.intVntID & " is not an Intervention")
                                End If
                            Catch
                            End Try

                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace