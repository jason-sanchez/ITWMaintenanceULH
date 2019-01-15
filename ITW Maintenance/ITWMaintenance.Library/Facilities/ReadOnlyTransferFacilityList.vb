Imports System.Data.SqlClient

Namespace Facilities

    <Serializable()> _
    Public Class ReadOnlyTransferFacilityList
        Inherits ReadOnlyListBase(Of ReadOnlyTransferFacilityList, ReadOnlyTransferFacility)

#Region " Factory Methods "

        Public Shared Function GetFacilityList(ByVal FacilityType As FacilityType, ByVal ActiveOnly As Boolean) As ReadOnlyTransferFacilityList
            Return DataPortal.Fetch(Of ReadOnlyTransferFacilityList)(New Criteria(FacilityType, ActiveOnly, Nothing))
        End Function

        Public Shared Function GetFacilityList(ByVal SearchText As String) As ReadOnlyTransferFacilityList
            Return DataPortal.Fetch(Of ReadOnlyTransferFacilityList)(New Criteria(Nothing, False, SearchText))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Public Overloads Function Contains(ByVal FacilityID As Integer) As Boolean
            For Each item As ReadOnlyTransferFacility In Me
                If item.ID = FacilityID Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Data Access "

        Public Enum FacilityType
            Transfer = 1
            OPTherapy = 2
            Acute = 3
            All = 4
        End Enum

        <Serializable()> _
        Private Class Criteria
            Private _FacilityType As FacilityType = Nothing
            Private _ActiveOnly As Boolean
            Private _SearchText As String

            Public ReadOnly Property FacilityType() As FacilityType
                Get
                    Return Me._FacilityType
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public ReadOnly Property SearchText() As String
                Get
                    Return Me._SearchText
                End Get
            End Property

            Public Sub New(ByVal FacilityType As FacilityType, ByVal ActiveOnly As Boolean, ByVal SearchText As String)
                Me._FacilityType = FacilityType
                Me._ActiveOnly = ActiveOnly
                Me._SearchText = SearchText
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Facility], [Contact], [Phone], [TransferFacility], "
                    sql &= "[opTherapy], [AcuteFacility], [Inactive] "
                    sql &= "FROM [111TranFac] "
                    sql &= "WHERE (1=1) "

                    If Not String.IsNullOrEmpty(criteria.SearchText) Then
                        sql &= "AND [Facility] LIKE '" & Trim(criteria.SearchText) & "%' "
                    End If

                    If Not IsNothing(criteria.FacilityType) Then
                        Select Case criteria.FacilityType
                            Case FacilityType.Transfer
                                sql &= "AND [TransferFacility] = 1 "
                            Case FacilityType.OPTherapy
                                sql &= "AND [opTherapy] = 1 "
                            Case FacilityType.Acute
                                sql &= "AND [AcuteFacility] = 1 "
                            Case FacilityType.All
                                ' No need to do anything
                        End Select
                    End If

                    If criteria.ActiveOnly Then
                        sql &= "AND [Inactive] = 0 "
                    End If

                    sql &= "ORDER BY [Facility] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyTransferFacility(dr))
                        End While
                        IsReadOnly = True
                    End Using
                End Using
            End Using

            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace