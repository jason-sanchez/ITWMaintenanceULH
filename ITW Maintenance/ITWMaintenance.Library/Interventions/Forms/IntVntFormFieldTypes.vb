Namespace Interventions

    Namespace Forms

        <Serializable()> _
        Public Class IntVntFormFieldTypes
            Inherits NameValueListBase(Of String, String)

            Private Shared _IntVntFormFieldTypes As IntVntFormFieldTypes

#Region " Business Methods "

            Public Shared Function GetDefaultFieldType() As String
                Dim list As IntVntFormFieldTypes = GetIntVntFormFieldTypes()

                If list.Count > 0 Then
                    Return list.Items(0).Key
                Else
                    Throw New NullReferenceException("No Intervention Form Field Types available; default Type cannot be returned")
                End If
            End Function

            Public Overloads Shared Function Contains(ByVal FieldType As String) As Boolean
                For Each type As NameValuePair In GetIntVntFormFieldTypes()
                    If type.Key = FieldType Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Shared Function GetValueByKey(ByVal Key As String) As String
                For Each Type As NameValuePair In GetIntVntFormFieldTypes()
                    If Type.Key = Key Then
                        Return Type.Value
                    End If
                Next

                ' Couldn't find the Value because this key wasn't in the list,
                ' so just return the key.
                Return Key
            End Function

            ''' <summary>
            ''' Clears the in-memory list cache
            ''' so the list of roles is reloaded on
            ''' next request.
            ''' </summary>
            Public Shared Sub InvalidateCache()
                _IntVntFormFieldTypes = Nothing
            End Sub

#End Region

#Region " Factory Methods "

            Public Shared Function GetIntVntFormFieldTypes() As IntVntFormFieldTypes
                If _IntVntFormFieldTypes Is Nothing Then
                    _IntVntFormFieldTypes = DataPortal.Fetch(Of IntVntFormFieldTypes)()
                End If

                Return _IntVntFormFieldTypes
            End Function

            Private Sub New()
                ' require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            <RunLocal()> _
            Private Overloads Sub DataPortal_Fetch()
                Me.RaiseListChangedEvents = False
                IsReadOnly = False

                Me.Add(New NameValuePair("", ""))
                Me.Add(New NameValuePair("text", "Text"))
                Me.Add(New NameValuePair("checkbox", "Checkbox"))
                Me.Add(New NameValuePair("combobox", "Combobox"))
                Me.Add(New NameValuePair("radio", "Radio"))
                Me.Add(New NameValuePair("memo", "Memo"))
                Me.Add(New NameValuePair("rptDesc", "Report Description"))
                Me.Add(New NameValuePair("scriblet", "Scriblet Memo"))
                Me.Add(New NameValuePair("date", "Date"))
                Me.Add(New NameValuePair("prepopulated memo", "Pre-Populated Memo"))

                IsReadOnly = True
                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace
