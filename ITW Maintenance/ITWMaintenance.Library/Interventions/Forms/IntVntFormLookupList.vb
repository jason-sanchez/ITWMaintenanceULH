Imports System.Data.SqlClient

Namespace Interventions

    Namespace Forms

        ''' <summary>
        ''' This class is used by the IntVntForm class to adjust the lookup
        ''' values as the form fields change.  So, for instance, if a form has
        ''' field 5 linked with the "Example" Global Lookup value and field 1
        ''' is removed, that lookup should shift to field 4 with the form field.
        ''' </summary>
        ''' <remarks></remarks>

        <Serializable()> _
        Public Class IntVntFormLookupList
            Inherits BusinessListBase(Of IntVntFormLookupList, IntVntFormLookup)

#Region " Authorization Rules "

            Public Shared Function CanAddObject() As Boolean
                Return True 'We must allow add so we can copy lookup values when we paste a copied IntVnt form
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function NewIntVntFormLookupList() As IntVntFormLookupList
                Return DataPortal.Create(Of IntVntFormLookupList)()
            End Function

            Public Shared Function GetIntVntFormLookupList(ByVal intVntID As Integer) As IntVntFormLookupList
                Return DataPortal.Fetch(Of IntVntFormLookupList)(New Criteria(intVntID))
            End Function

            Private Sub New()
                Me.AllowNew = True
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

            Public Overrides Function Save() As IntVntFormLookupList
                ' See if save is allowed
                If Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to modify Intervention Form Lookup values.")
                End If

                ' Do the save
                Dim result As IntVntFormLookupList
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
                        sql &= "WHERE [Type] = 'IntVnt' "
                        sql &= "AND [FormNumber] = " & criteria.intVntID & " "
                        sql &= "ORDER BY [ID] DESC "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    Me.Add(IntVntFormLookup.GetIntVntFormLookup(dr))
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
                    For Each item As IntVntFormLookup In DeletedList
                        item.DeleteSelf(Conn)
                    Next
                    DeletedList.Clear()

                    For Each item As IntVntFormLookup In Me
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