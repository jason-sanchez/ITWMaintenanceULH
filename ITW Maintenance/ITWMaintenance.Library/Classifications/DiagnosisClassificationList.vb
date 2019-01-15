Imports System.Data.SqlClient

Namespace Classifications

    <Serializable()> _
    Public Class DiagnosisClassificationList
        Inherits BusinessListBase(Of DiagnosisClassificationList, DiagnosisClassification)

#Region " Business Methods "

        Public Overloads Sub Remove(ByVal ID As Integer)
            For Each item As DiagnosisClassification In Me
                If item.ID = ID Then
                    Remove(item)
                    Exit For
                End If
            Next
        End Sub

        Public Function GetDiagnosisClassificationByID(ByVal ID As Integer) As DiagnosisClassification
            For Each item As DiagnosisClassification In Me
                If item.ID = ID Then
                    Return item
                End If
            Next
            Return Nothing
        End Function

        Protected Overrides Function AddNewCore() As Object
            Dim item As DiagnosisClassification = DiagnosisClassification.NewDiagnosisClassification()
            Add(item)
            Return item
        End Function

        Public Function GetChildRuleDescriptions() As String()
            Return DiagnosisClassification.NewDiagnosisClassification().GetRuleDescriptions()
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanAddObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return False 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetList(ByVal ActiveOnly As Boolean) As DiagnosisClassificationList
            Return DataPortal.Fetch(Of DiagnosisClassificationList)(New Criteria(ActiveOnly))
        End Function

        Private Sub New()
            Me.AllowNew = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ActiveOnly As Boolean)
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Public Overrides Function Save() As DiagnosisClassificationList
            ' See if save is allowed
            If Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to modify Diagnosis Classifications")
            End If

            ' Do the save
            Dim result As DiagnosisClassificationList
            result = MyBase.Save()

            Return result
        End Function

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False
            Using Conn As New SqlConnection(Database.ITWConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Category], [Description], [Inactive] "
                    sql &= "FROM [111DiagnosisClass] "
                    If criteria.ActiveOnly Then
                        sql &= "WHERE [Inactive] = 0 "
                    End If
                    sql &= "ORDER BY [Description] "

                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        With dr
                            While .Read()
                                Me.Add(DiagnosisClassification.GetDiagnosisClassification(dr))
                            End While
                        End With
                    End Using
                End Using
            End Using
            RaiseListChangedEvents = True
        End Sub

        Protected Overrides Sub DataPortal_Update()
            Me.RaiseListChangedEvents = False
            Using Conn As New SqlConnection(Database.ITWConnection)
                Conn.Open()
                Me.Child_Update(Conn)
            End Using
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace