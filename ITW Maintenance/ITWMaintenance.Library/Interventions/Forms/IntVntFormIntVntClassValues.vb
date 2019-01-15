Namespace Interventions

    Namespace Forms

        <Serializable()> _
        Public Class IntVntFormIntVntClassValues
            Inherits NameValueListBase(Of Integer, String)

            Private Shared _IntVntClassList As IntVntFormIntVntClassValues

#Region " Business Methods "

            Public Shared Function GetDefaultIntVntClass() As Integer
                Dim list As IntVntFormIntVntClassValues = GetIntVntFormIntVntClassValues()

                If list.Count > 0 Then
                    Return list.Items(0).Key
                Else
                    Throw New NullReferenceException("No IntVntClass values available; default IntVntClass cannot be returned")
                End If
            End Function

            ''' <summary>
            ''' Clears the in-memory RoleList cache
            ''' so the list of roles is reloaded on
            ''' next request.
            ''' </summary>
            Public Shared Sub InvalidateCache()
                _IntVntClassList = Nothing
            End Sub

#End Region

#Region " Factory Methods "

            Public Shared Function GetIntVntFormIntVntClassValues() As IntVntFormIntVntClassValues
                If _IntVntClassList Is Nothing Then
                    _IntVntClassList = DataPortal.Fetch(Of IntVntFormIntVntClassValues)()
                End If

                Return _IntVntClassList
            End Function

            Private Sub New()
                ' require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            Private Overloads Sub DataPortal_Fetch()
                Me.RaiseListChangedEvents = False
                IsReadOnly = False

                Me.Add(New NameValuePair(0, "Objective"))
                Me.Add(New NameValuePair(1, "Subjective"))
                Me.Add(New NameValuePair(2, "Goal"))
                Me.Add(New NameValuePair(3, "Assessment"))
                Me.Add(New NameValuePair(4, "Plan"))
                Me.Add(New NameValuePair(5, "Patient Goal"))
                Me.Add(New NameValuePair(6, "History & Environment"))

                IsReadOnly = True
                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace
