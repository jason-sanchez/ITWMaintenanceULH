Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Interventions.Utilities

Namespace Interventions

    <Serializable()> _
    Public Class Intervention
        Inherits BusinessBase(Of Intervention)

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
        Public Property IntVntName() As String
            Get
                Return GetProperty(Of String)(IntVntNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(IntVntNameProperty, value)
            End Set
        End Property

        Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline"))
        Public ReadOnly Property Discipline() As Integer
            Get
                Return GetProperty(Of Integer)(DisciplineProperty)
            End Get
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

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(InactiveProperty, value)
            End Set
        End Property

        Private Shared OneTimeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("OneTime", "One Time"))
        Public Property OneTime() As Boolean
            Get
                Return GetProperty(Of Boolean)(OneTimeProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(OneTimeProperty, value)
            End Set
        End Property

        Private Shared BillingProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Billing", "Billing"))
        Public Property Billing() As Boolean
            Get
                Return GetProperty(Of Boolean)(BillingProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(BillingProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(intVntIDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, IntVntNameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(IntVntNameProperty, 200))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            ' Add AuthorizationRules here
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return Csla.ApplicationContext.User.IsInRole("Administrator")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return Csla.ApplicationContext.User.IsInRole("Administrator")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return Csla.ApplicationContext.User.IsInRole("Administrator")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewIntervention(ByVal ParentID As Integer) As Intervention
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Intervention")
            End If
            Return DataPortal.Create(Of Intervention)(New InsertCriteria(ParentID))
        End Function

        Public Shared Function GetIntervention(ByVal intVntID As Integer) As Intervention
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Intervention")
            End If
            Return DataPortal.Fetch(Of Intervention)(New Criteria(intVntID))
        End Function

        Public Shared Sub DeleteIntervention(ByVal intVntID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Intervention")
            End If
            DataPortal.Delete(New Criteria(intVntID))
        End Sub

        Public Overloads Function Save() As Intervention
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Intervention")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Intervention")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update an Intervention")
            End If

            Return MyBase.Save()
        End Function

        Private Sub New()
            ' Require use of Factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class InsertCriteria
            Private _ParentID As Integer

            Public ReadOnly Property ParentID() As Integer
                Get
                    Return Me._ParentID
                End Get
            End Property

            Public Sub New(ByVal ParentID As Integer)
                Me._ParentID = ParentID
            End Sub
        End Class

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

        Private Overloads Sub DataPortal_Create(ByVal Criteria As InsertCriteria)
            ' Set the parent ID
            SetProperty(Of Integer)(ParentIDProperty, Criteria.ParentID)

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Get the next displayOrder
                    sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                    sql &= "FROM [110IntVnt] "
                    sql &= "WHERE ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDOrder"))
                        End If
                    End Using
                End Using
            End Using
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], intVntID, ParentID, intVntLevel, iName, dOrder, "
                    sql &= "discipline, inactive, OneTime, Billing "
                    sql &= "FROM [110IntVnt] "
                    sql &= "WHERE intVntID = " & Criteria.intVntID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            If .GetInt32("IntVntLevel") = 2 Then
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of String)(IntVntNameProperty, .GetString("iName"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("inactive"))
                                LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                                LoadProperty(Of Boolean)(BillingProperty, .GetBoolean("Billing"))
                            Else
                                Throw New Exception("Intervention ID #" & Criteria.intVntID & " is not an Intervention on Level 2")
                            End If
                        End With
                    End Using
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String
                    Dim ParentPath As String = ""

                    ' Set the intVntID
                    SetProperty(Of Integer)(intVntIDProperty, IntVntHelper.GetNextintVntID(cmd))

                    ' Get the Parent's Path and discipline
                    sql = "SELECT iPath, Discipline "
                    sql &= "FROM [110IntVnt] "
                    sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            ParentPath = dr.GetString("iPath")
                            SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                        End If
                    End Using


                    ' Insert the new folder
                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [110IntVnt] (intVntID, ParentID, intVntLevel, iPath, "
                    sql &= "iName, ShortName, iFinal, Discipline, DOrder, Level2, Required, "
                    sql &= "inactive, OneTime, Billing) VALUES ("
                    sql &= ReadProperty(Of Integer)(intVntIDProperty) & ", "
                    sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                    sql &= "2, "
                    ' The path can't exceed 200 characters
                    sql &= "'" & Left(Replace(ParentPath & ", " & ReadProperty(Of String)(IntVntNameProperty), "'", "''"), 200) & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(IntVntNameProperty), "'", "''") & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(IntVntNameProperty), "'", "''") & "', "
                    sql &= "0, "
                    sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
                    sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                    sql &= ReadProperty(Of Integer)(intVntIDProperty) & ", "
                    sql &= "0, "
                    If ReadProperty(Of Boolean)(InactiveProperty) Then
                        sql &= "1, "
                    Else
                        sql &= "0, "
                    End If
                    If ReadProperty(Of Boolean)(OneTimeProperty) Then
                        sql &= "1, "
                    Else
                        sql &= "0, "
                    End If
                    If ReadProperty(Of Boolean)(BillingProperty) Then
                        sql &= "1) "
                    Else
                        sql &= "0) "
                    End If
                    sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                    sql &= "SET NOCOUNT OFF "

                    cmd.CommandText = sql

                    ' Save the new ID that was added
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using
            End Using

            ' Update child objects
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If MyBase.IsDirty Then
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String
                        Dim ParentPath As String = ""

                        ' Get the Parent's Path
                        sql = "SELECT iPath "
                        sql &= "FROM [110IntVnt] "
                        sql &= "WHERE intVntID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ParentPath = dr.GetString("iPath")
                            End If
                        End Using


                        sql = "UPDATE [110IntVnt] SET "
                        'sql &= "intVntID = " & Me._intVntID & ", "
                        'sql &= "ParentID = " & Me._ParentID & ", "
                        ' The path can't exceed 200 characters
                        sql &= "iPath = '" & Left(Replace(ParentPath & ", " & ReadProperty(Of String)(IntVntNameProperty), "'", "''"), 200) & "', "
                        sql &= "iName = '" & Replace(ReadProperty(Of String)(IntVntNameProperty), "'", "''") & "', "
                        sql &= "ShortName = '" & Replace(ReadProperty(Of String)(IntVntNameProperty), "'", "''") & "', "
                        sql &= "DOrder = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        If ReadProperty(Of Boolean)(InactiveProperty) Then
                            sql &= "Inactive = 1, "
                        Else
                            sql &= "Inactive = 0, "
                        End If
                        If ReadProperty(Of Boolean)(OneTimeProperty) Then
                            sql &= "OneTime = 1, "
                        Else
                            sql &= "OneTime = 0, "
                        End If
                        If ReadProperty(Of Boolean)(BillingProperty) Then
                            sql &= "Billing = 1 "
                        Else
                            sql &= "Billing = 0 "
                        End If
                        sql &= "WHERE [ID] = " & ReadProperty(Of Integer)(IDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If

            ' Update child objects
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(intVntIDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            ' Detele is not allowed by the authorization rules above, but
            ' don't even code the delete for security.

            'Using conn As New SqlConnection(Database.ITWConnection)
            '    conn.Open()
            '    Using cmd As SqlCommand = conn.CreateCommand
            '        Dim sql As String

            '        sql = "DELETE FROM [110IntVnt] "
            '        sql &= "WHERE intVntID = " & criteria.intVntID & " "

            '        cmd.CommandType = CommandType.Text
            '        cmd.CommandText = sql
            '        cmd.ExecuteNonQuery()
            '    End Using
            'End Using
        End Sub

#End Region

    End Class

End Namespace
