Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class UserRoleList
        Inherits NameValueListBase(Of String, String)

        Private Shared _UserRoles As UserRoleList

#Region " Business Methods "

        Public Shared Function GetDefaultRole() As String
            Dim list As UserRoleList

            list = GetUserRoles()

            If list.Count > 0 Then
                Return list.Items(0).Key
            Else
                Throw New NullReferenceException("No user roles available; default role cannot be returned")
            End If
        End Function

        Public Overloads Function Contains(ByVal UserRole As String) As Boolean
            For Each role In Me
                If role.Key = UserRole Then
                    Return True
                End If
            Next

            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetUserRoles() As UserRoleList
            Return DataPortal.Fetch(Of UserRoleList)()
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

            Me.Add(New NameValuePair("PT", "PT"))
            Me.Add(New NameValuePair("PTA", "PTA"))
            Me.Add(New NameValuePair("PC", "PPS Coordinator"))
            Me.Add(New NameValuePair("Student", "Student"))
            Me.Add(New NameValuePair("RN", "RN"))
            Me.Add(New NameValuePair("OT", "OT"))
            Me.Add(New NameValuePair("COTA/L", "COTA/L"))
            Me.Add(New NameValuePair("SP", "SP"))
            Me.Add(New NameValuePair("CNA", "CNA"))
            Me.Add(New NameValuePair("OC", "OC"))
            Me.Add(New NameValuePair("MD", "MD"))
            Me.Add(New NameValuePair("Resident MD", "Resident MD"))
            Me.Add(New NameValuePair("PSY", "PSY"))
            Me.Add(New NameValuePair("REC", "REC"))
            Me.Add(New NameValuePair("PUL", "PUL"))
            Me.Add(New NameValuePair("CFY", "CFY"))
            Me.Add(New NameValuePair("PSYL", "PSYL"))
            Me.Add(New NameValuePair("Practitioner", "Practitioner"))
            Me.Add(New NameValuePair("US", "Unit Secretary"))

            IsReadOnly = True
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace