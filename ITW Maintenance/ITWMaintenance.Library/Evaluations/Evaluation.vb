Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Evaluations.Utilities

Namespace Evaluations

    <Serializable()> _
    Public Class Evaluation
        Inherits BusinessBase(Of Evaluation)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property EvalID() As Integer
            Get
                Return GetProperty(Of Integer)(EvalIDProperty)
            End Get
        End Property

        Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
        Public ReadOnly Property ParentID() As Integer
            Get
                Return GetProperty(Of Integer)(ParentIDProperty)
            End Get
        End Property

        Private Shared EvalNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EvalName", "Eval Name"))
        Public Property EvalName() As String
            Get
                Return GetProperty(Of String)(EvalNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(EvalNameProperty, value)
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

        Private Shared PostDischargeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("PostDischarge", "Post Discharge"))
        Public Property PostDischarge() As Boolean
            Get
                Return GetProperty(Of Boolean)(PostDischargeProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(PostDischargeProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(EvalIDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, EvalNameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                New Validation.CommonRules.MaxLengthRuleArgs(EvalNameProperty, 200))

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

        Public Shared Function NewEvaluation(ByVal ParentEvalID As Integer) As Evaluation
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Evaluation")
            End If
            Return DataPortal.Create(Of Evaluation)(New InsertCriteria(ParentEvalID))
        End Function

        Public Shared Function GetEvaluation(ByVal EvalID As Integer) As Evaluation
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Evaluation")
            End If
            Return DataPortal.Fetch(Of Evaluation)(New Criteria(EvalID))
        End Function

        Public Shared Sub DeleteEvaluation(ByVal EvalID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Evaluation")
            End If
            DataPortal.Delete(New Criteria(EvalID))
        End Sub

        Public Overloads Function Save() As Evaluation
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove an Evaluation")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Evaluation")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update an Evaluation")
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
            Private _ParentEvalID As Integer

            Public ReadOnly Property ParentEvalID() As Integer
                Get
                    Return Me._ParentEvalID
                End Get
            End Property

            Public Sub New(ByVal ParentEvalID As Integer)
                Me._ParentEvalID = ParentEvalID
            End Sub
        End Class

        <Serializable()> _
        Private Class Criteria
            Private _EvalID As Integer

            Public ReadOnly Property EvalID() As Integer
                Get
                    Return Me._EvalID
                End Get
            End Property

            Public Sub New(ByVal EvalID As Integer)
                Me._EvalID = EvalID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Create(ByVal Criteria As InsertCriteria)
            ' Set the parent ID
            SetProperty(Of Integer)(ParentIDProperty, Criteria.ParentEvalID)

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Get the next displayOrder
                    sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                    sql &= "FROM [100Eval] "
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

                    sql = "SELECT [ID], EvalID, ParentID, EvalLevel, EName, dOrder, "
                    sql &= "discipline, inactive, OneTime, postDC "
                    sql &= "FROM [100Eval] "
                    sql &= "WHERE EvalID = " & Criteria.EvalID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            If .GetInt32("EvalLevel") = 2 Then
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of String)(EvalNameProperty, .GetString("EName"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("inactive"))
                                LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                                LoadProperty(Of Boolean)(PostDischargeProperty, .GetBoolean("postDC"))
                            Else
                                Throw New Exception("Eval ID #" & Criteria.EvalID & " is not an Evaluation on Level 2")
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

                    ' Set the EvalID
                    SetProperty(Of Integer)(EvalIDProperty, EvalHelper.GetNextEvalID(cmd))

                    ' Get the Parent's Path and discipline
                    sql = "SELECT EPath, Discipline "
                    sql &= "FROM [100Eval] "
                    sql &= "WHERE EvalID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            ParentPath = dr.GetString("EPath")
                            SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                        End If
                    End Using


                    ' Insert the new folder
                    sql = "SET NOCOUNT ON "
                    sql &= "INSERT INTO [100Eval] (EvalID, ParentID, EvalLevel, EPath, "
                    sql &= "EName, ShortName, EFinal, Discipline, DOrder, Level2, Required, "
                    sql &= "inactive, OneTime, postDC) VALUES ("
                    sql &= ReadProperty(Of Integer)(EvalIDProperty) & ", "
                    sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                    sql &= "2, "
                    ' The path can't exceed 200 characters
                    sql &= "'" & Left(Replace(ParentPath & ", " & ReadProperty(Of String)(EvalNameProperty), "'", "''"), 200) & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(EvalNameProperty), "'", "''") & "', "
                    sql &= "'" & Replace(ReadProperty(Of String)(EvalNameProperty), "'", "''") & "', "
                    sql &= "0, "
                    sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
                    sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                    sql &= ReadProperty(Of Integer)(EvalIDProperty) & ", "
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
                    If ReadProperty(Of Boolean)(PostDischargeProperty) Then
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
                        sql = "SELECT EPath "
                        sql &= "FROM [100Eval] "
                        sql &= "WHERE EvalID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                ParentPath = dr.GetString("EPath")
                            End If
                        End Using


                        sql = "UPDATE [100Eval] SET "
                        'sql &= "EvalID = " & Me._EvalID & ", "
                        'sql &= "ParentID = " & Me._ParentID & ", "
                        ' The path can't exceed 200 characters
                        sql &= "EPath = '" & Left(Replace(ParentPath & ", " & ReadProperty(Of String)(EvalNameProperty), "'", "''"), 200) & "', "
                        sql &= "EName = '" & Replace(ReadProperty(Of String)(EvalNameProperty), "'", "''") & "', "
                        sql &= "ShortName = '" & Replace(ReadProperty(Of String)(EvalNameProperty), "'", "''") & "', "
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
                        If ReadProperty(Of Boolean)(PostDischargeProperty) Then
                            sql &= "postDC = 1 "
                        Else
                            sql &= "postDC = 0 "
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
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(EvalIDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            ' Detele is not allowed by the authorization rules above, but
            ' don't even code the delete for security.

            'Using conn As New SqlConnection(Database.ITWConnection)
            '    conn.Open()
            '    Using cmd As SqlCommand = conn.CreateCommand
            '        Dim sql As String

            '        sql = "DELETE FROM [100Eval] "
            '        sql &= "WHERE EvalID = " & criteria.EvalID & " "

            '        cmd.CommandType = CommandType.Text
            '        cmd.CommandText = sql
            '        cmd.ExecuteNonQuery()
            '    End Using
            'End Using
        End Sub

#End Region

    End Class

End Namespace
