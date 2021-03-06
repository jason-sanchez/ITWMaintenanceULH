﻿Namespace Orders

    <Serializable()> _
    Public Class OrderFormFieldTypes
        Inherits NameValueListBase(Of String, String)

        Private Shared _OrderFormFieldTypes As OrderFormFieldTypes

#Region " Business Methods "

        Public Shared Function GetDefaultFieldType() As String
            Dim list As OrderFormFieldTypes = GetOrderFormFieldTypes()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No Order Form Field Types available; default Type cannot be returned")
            End If
        End Function

        Public Overloads Shared Function Contains(ByVal FieldType As String) As Boolean
            For Each type As NameValuePair In GetOrderFormFieldTypes()
                If type.Key = FieldType Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Shared Function GetValueByKey(ByVal Key As String) As String
            For Each Type As NameValuePair In GetOrderFormFieldTypes()
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
            _OrderFormFieldTypes = Nothing
        End Sub

#End Region

#Region " Factory Methods "

        Public Shared Function GetOrderFormFieldTypes() As OrderFormFieldTypes
            If _OrderFormFieldTypes Is Nothing Then
                _OrderFormFieldTypes = DataPortal.Fetch(Of OrderFormFieldTypes)()
            End If

            Return _OrderFormFieldTypes
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

            Me.Add(New NameValuePair("label", "Label"))
            Me.Add(New NameValuePair("text", "Text"))
            Me.Add(New NameValuePair("checkbox", "Checkbox"))
            Me.Add(New NameValuePair("combobox", "Combobox"))
            Me.Add(New NameValuePair("radio", "Radio"))
            Me.Add(New NameValuePair("memo", "Memo"))
            Me.Add(New NameValuePair("scriblet", "Scriblet Memo"))
            Me.Add(New NameValuePair("date", "Date"))
            Me.Add(New NameValuePair("prepopulated memo", "Pre-Populated Memo"))

            IsReadOnly = True
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace
