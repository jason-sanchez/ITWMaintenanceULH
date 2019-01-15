Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class OrderFacilityActionItemList
        Inherits BusinessListBase(Of OrderFacilityActionItemList, OrderFacilityActionItem)

#Region " Factory Methods "

        Public Shared Function NewOrderFacilityActionItemList() As OrderFacilityActionItemList
            Return DataPortal.CreateChild(Of OrderFacilityActionItemList)()
        End Function

        Public Shared Function GetOrderFacilityActionItems(ByVal OrderAlias As String) As OrderFacilityActionItemList
            Return DataPortal.FetchChild(Of OrderFacilityActionItemList)(New Criteria(OrderAlias))
        End Function

        Public Overloads Function ContainsFacility(ByVal FacilityID As Integer) As Boolean
            For Each item As OrderFacilityActionItem In Me
                If item.IntakeFacility = FacilityID Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Overloads Function IndexOf(ByVal FacilityID As Integer) As Integer
            For i As Integer = 0 To Me.Count
                If Me(i).IntakeFacility = FacilityID Then
                    Return i
                End If
            Next

            Return -1
        End Function

        Public Overloads Sub AddNew(ByVal FacilityID As Integer, ByVal Discipline As Integer, ByVal ActionCode As Integer)
            Me.Add(OrderFacilityActionItem.NewOrderFacilityActionItem(FacilityID, Discipline, ActionCode))
        End Sub

        Public Overloads Sub Remove(ByVal ID As Integer)
            For Each item As OrderFacilityActionItem In Me
                If item.ID = ID Then
                    Me.Remove(item)
                    Exit Sub
                End If
            Next
        End Sub

        Private Sub New()
            Me.AllowNew = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _OrderAlias As String

            Public ReadOnly Property OrderAlias() As String
                Get
                    Return _OrderAlias
                End Get
            End Property

            Public Sub New(ByVal OrderAlias As String)
                _OrderAlias = OrderAlias
            End Sub
        End Class

        Private Sub Child_Fetch(ByVal criteria As Criteria)
            Me.RaiseListChangedEvents = False
            Using Conn As New SqlConnection(Database.ITWCernerConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT ofa.[ID], ofa.[IntakeFacility], ISNULL(f.[Name], 'Unknown Facility') AS [IntakeFacilityDescription], "
                    sql &= "ofa.[Discipline], ISNULL(d.[disName], 'Unknown Discipline') AS [DisciplineDescription], ofa.[ActionCode] "
                    sql &= "FROM [OrderFacilityAction] ofa "
                    sql &= "LEFT JOIN [115NetworkFac] f ON ofa.[IntakeFacility] = f.[ID] "
                    sql &= "LEFT JOIN [116Discipline] d ON ofa.[Discipline] = d.[disID] "
                    sql &= "WHERE ofa.[Alias] = @OrderAlias "
                    sql &= "ORDER BY ofa.[IntakeFacility] "

                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@OrderAlias", criteria.OrderAlias)

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        With dr
                            While .Read()
                                Me.Add(OrderFacilityActionItem.GetOrderFacilityActionItem(dr))
                            End While
                        End With
                    End Using
                End Using
            End Using
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace