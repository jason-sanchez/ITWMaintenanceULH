Imports System.Data.SqlClient

Namespace Nursing

    Namespace GlobalLookup

        <Serializable()> _
        Public Class NursingNoteGlobalLookupList
            Inherits BusinessListBase(Of NursingNoteGlobalLookupList, NursingNoteGlobalLookup)

#Region " Business Methods "

            Public Overloads Sub Remove(ByVal ID As Integer)
                For Each lookup As NursingNoteGlobalLookup In Me
                    If lookup.ID = ID Then
                        Remove(lookup)
                        Exit For
                    End If
                Next
            End Sub

            Public Function GetGlobalLookupByID(ByVal ID As Integer) As NursingNoteGlobalLookup
                For Each lookup As NursingNoteGlobalLookup In Me
                    If lookup.ID = ID Then
                        Return lookup
                    End If
                Next
                Return Nothing
            End Function

            Protected Overrides Function AddNewCore() As Object
                Dim lookup As NursingNoteGlobalLookup = NursingNoteGlobalLookup.NewGlobalLookup()
                Add(lookup)
                Return lookup
            End Function

            Public Function GetChildRuleDescriptions() As String()
                Return NursingNoteGlobalLookup.NewGlobalLookup().GetRuleDescriptions()
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
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function GetGlobalLookupList() As NursingNoteGlobalLookupList
                Return DataPortal.Fetch(Of NursingNoteGlobalLookupList)(New Criteria("", SearchField.Description))
            End Function

            Public Shared Function GetGlobalLookupList(ByVal SearchText As String, ByVal SearchField As SearchField) As NursingNoteGlobalLookupList
                Return DataPortal.Fetch(Of NursingNoteGlobalLookupList)(New Criteria(SearchText, SearchField))
            End Function

            Private Sub New()
                Me.AllowNew = True
            End Sub

#End Region

#Region " Data Access "

            Public Enum SearchField
                FormNumber = 1
                Description = 2
                FormField = 3
            End Enum

            <Serializable()> _
            Private Class Criteria
                Private _SearchText As String
                Private _SearchField As SearchField

                Public ReadOnly Property SearchText() As String
                    Get
                        Return Me._SearchText
                    End Get
                End Property

                Public ReadOnly Property SearchField() As SearchField
                    Get
                        Return Me._SearchField
                    End Get
                End Property

                Public Sub New(ByVal SearchText As String, ByVal SearchField As SearchField)
                    Me._SearchText = SearchText
                    Me._SearchField = SearchField
                End Sub
            End Class

            Public Overrides Function Save() As NursingNoteGlobalLookupList
                ' See if save is allowed
                If Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to modify Eval Global Lookup values.")
                End If

                ' Do the save
                Dim result As NursingNoteGlobalLookupList
                result = MyBase.Save()

                Return result
            End Function

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Me.RaiseListChangedEvents = False
                Using Conn As New SqlConnection(Database.ITWConnection)
                    Conn.Open()
                    Using cmd As SqlCommand = Conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT gl.[ID], gl.[Description], gl.[FormNumber], gl.[FormField], nn.[NName] AS [Path], "
                        sql &= "nnEl.[Label01], nnEl.[Label02], nnEl.[Label03], nnEl.[Label04], nnEl.[Label05], "
                        sql &= "nnEl.[Label06], nnEl.[Label07], nnEl.[Label08], nnEl.[Label09], nnEl.[Label10], "
                        sql &= "nnEl.[Label11], nnEl.[Label12], nnEl.[Label13], nnEl.[Label14], nnEl.[Label15], "
                        sql &= "nnEl.[Label16], nnEl.[Label17], nnEl.[Label18], nnEl.[Label19], nnEl.[Label20] "
                        sql &= "FROM [100GlobalLookup] gl "
                        sql &= "LEFT JOIN [100CrNt] nn ON gl.[FormNumber] = nn.[NsNtID] "
                        sql &= "LEFT JOIN [100CrNtEl] nnEl ON gl.[FormNumber] = nnEl.[NsNtID] "
                        sql &= "WHERE gl.[Type] = 'NursingNote' "
                        If Not String.IsNullOrEmpty(criteria.SearchText) Then
                            If criteria.SearchField = SearchField.FormNumber Then
                                sql &= "AND gl.[FormNumber] = " & criteria.SearchText & " "
                            ElseIf criteria.SearchField = SearchField.Description Then
                                sql &= "AND gl.[Description] LIKE '%" & criteria.SearchText & "%' "
                            End If
                        End If
                        sql &= "ORDER BY gl.[Description], gl.[FormNumber] DESC "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    If Not String.IsNullOrEmpty(criteria.SearchText) AndAlso criteria.SearchField = SearchField.FormField Then
                                        ' Since the query can't analyze the field associated with the lookup value
                                        ' (because we don't know which label to read until we have the record set),
                                        ' we need to do it here before adding the child object.
                                        Dim child As NursingNoteGlobalLookup = NursingNoteGlobalLookup.GetGlobalLookup(dr)
                                        If InStr(LCase(child.FieldLabel), LCase(criteria.SearchText)) > 0 Then
                                            ' This lookup field contains the search text, so add it to the result list
                                            Me.Add(child)
                                        End If
                                    Else
                                        Me.Add(NursingNoteGlobalLookup.GetGlobalLookup(dr))
                                    End If
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
                    For Each item As NursingNoteGlobalLookup In DeletedList
                        item.DeleteSelf(Conn)
                    Next
                    DeletedList.Clear()

                    For Each item As NursingNoteGlobalLookup In Me
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