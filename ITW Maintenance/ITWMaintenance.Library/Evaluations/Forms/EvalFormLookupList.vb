Imports System.Data.SqlClient

Namespace Evaluations

    Namespace Forms

        ''' <summary>
        ''' This class is used by the EvalForm class to adjust the lookup
        ''' values as the form fields change.  So, for instance, if a form has
        ''' field 5 linked with the "Example" Global Lookup value and field 1
        ''' is removed, that lookup should shift to field 4 with the form field.
        ''' </summary>
        ''' <remarks></remarks>

        <Serializable()> _
        Public Class EvalFormLookupList
            Inherits BusinessListBase(Of EvalFormLookupList, EvalFormLookup)

#Region " Authorization Rules "

            Public Shared Function CanAddObject() As Boolean
                Return Csla.ApplicationContext.User.IsInRole("Administrator") 'We must allow add so we can copy lookup values when we paste a copied eval form
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

            Public Shared Function NewEvalFormLookupList() As EvalFormLookupList
                Return DataPortal.Create(Of EvalFormLookupList)()
            End Function

            Public Shared Function GetEvalFormLookupList(ByVal EvalID As Integer) As EvalFormLookupList
                Return DataPortal.Fetch(Of EvalFormLookupList)(New Criteria(EvalID))
            End Function

            Private Sub New()
                Me.AllowNew = True
            End Sub

#End Region

#Region " Data Access "

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

            Public Overrides Function Save() As EvalFormLookupList
                ' See if save is allowed
                If Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to modify Eval Form Lookup values.")
                End If

                ' Do the save
                Dim result As EvalFormLookupList
                result = MyBase.Save()

                Return result
            End Function

            <RunLocal()> _
            Private Overloads Sub DataPortal_Create()
                'Me.AllowNew = True
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Me.RaiseListChangedEvents = False
                Using Conn As New SqlConnection(Database.ITWConnection)
                    Conn.Open()
                    Using cmd As SqlCommand = Conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [ID], [Description], [FormNumber], [FormField] "
                        sql &= "FROM [100GlobalLookup] "
                        sql &= "WHERE [Type] = 'eval' "
                        sql &= "AND [FormNumber] = " & criteria.EvalID & " "
                        sql &= "ORDER BY [ID] DESC "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    Me.Add(EvalFormLookup.GetEvalFormLookup(dr))
                                End While
                            End With
                        End Using
                    End Using
                End Using
                Me.RaiseListChangedEvents = True
            End Sub

            Protected Overrides Sub DataPortal_Update()
                Me.RaiseListChangedEvents = False
                Using Conn As New SqlConnection(Database.ITWConnection)
                    Conn.Open()
                    For Each item As EvalFormLookup In DeletedList
                        item.DeleteSelf(Conn)
                    Next
                    DeletedList.Clear()

                    For Each item As EvalFormLookup In Me
                        If item.IsNew Then
                            item.Insert(Conn)
                        Else
                            item.Update(Conn)
                        End If
                    Next
                End Using
                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace