Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class NursingNoteRedirectValues
            Inherits NameValueListBase(Of String, String)

            Private Shared _NursingNoteRedirectValues As NursingNoteRedirectValues

#Region " Business Methods "

            Public Shared Function GetDefaultValue() As String
                Dim list As NursingNoteRedirectValues = GetNursingNoteRedirectValues()

                If list.Count > 0 Then
                    Return list.Items(0).Key
                Else
                    Throw New NullReferenceException("No Nursing Note Form Redirect Values available; default value cannot be returned")
                End If
            End Function

            Public Overloads Shared Function Contains(ByVal FieldType As String) As Boolean
                For Each type As NameValuePair In GetNursingNoteRedirectValues()
                    If type.Key = FieldType Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Shared Function GetValueByKey(ByVal Key As String) As String
                For Each Type As NameValuePair In GetNursingNoteRedirectValues()
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
                _NursingNoteRedirectValues = Nothing
            End Sub

#End Region

#Region " Factory Methods "

            Public Shared Function GetNursingNoteRedirectValues() As NursingNoteRedirectValues
                If _NursingNoteRedirectValues Is Nothing Then
                    _NursingNoteRedirectValues = DataPortal.Fetch(Of NursingNoteRedirectValues)()
                End If

                Return _NursingNoteRedirectValues
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
                Me.Add(New NameValuePair("graphics", "Graphics"))
                Me.Add(New NameValuePair("caloriecount", "Calorie Count"))

                IsReadOnly = True
                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace