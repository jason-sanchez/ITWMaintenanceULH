Imports System.Data.SqlClient

Namespace Orders

    Namespace OrderSets

        <Serializable()> _
        Public Class OrderSetOrderList
            Inherits BusinessListBase(Of OrderSetOrderList, OrderSetOrder)

#Region " Factory Methods "

            Public Shared Function NewOrderSetOrderList() As OrderSetOrderList
                Return DataPortal.CreateChild(Of OrderSetOrderList)()
            End Function

            Public Shared Function GetOrderSetOrderList(ByVal OrderSetID As Integer) As OrderSetOrderList
                Return DataPortal.FetchChild(Of OrderSetOrderList)(New Criteria(OrderSetID))
            End Function

            Public Overloads Function Contains(ByVal OrderID As Integer) As Boolean
                For Each order As OrderSetOrder In Me
                    If order.OrderID = OrderID Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Overloads Sub AddNew(ByVal OrderID As Integer)
                If Not Me.Contains(OrderID) Then
                    ' Get next sequence automatically
                    Dim sequence As Integer = 1

                    For Each order As OrderSetOrder In Me
                        If order.Sequence >= sequence Then
                            sequence += 1
                        End If
                    Next

                    Me.Add(OrderSetOrder.NewOrderSetOrder(OrderID, sequence))
                Else
                    Throw New InvalidOperationException("That Order is already linked to this set.")
                End If
            End Sub

            Public Overloads Sub AddNew(ByVal OrderID As Integer, ByVal Sequence As Integer)
                If Not Me.Contains(OrderID) Then
                    Me.Add(OrderSetOrder.NewOrderSetOrder(OrderID, Sequence))
                Else
                    Throw New InvalidOperationException("That Order is already linked to this set.")
                End If
            End Sub

            Public Overloads Sub Remove(ByVal Index As Integer)
                ' Remove the order
                Me.RemoveAt(Index)

                ' Next, rebuild the sequence
                Dim sequence As Integer = 0

                For Each order As OrderSetOrder In Me
                    sequence += 1
                    order.Sequence = sequence
                Next
            End Sub

            Private Sub New()
                Me.AllowNew = True
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _OrderSetID As Integer

                Public ReadOnly Property OrderSetID() As Integer
                    Get
                        Return Me._OrderSetID
                    End Get
                End Property

                Public Sub New(ByVal OrderSetID As Integer)
                    Me._OrderSetID = OrderSetID
                End Sub
            End Class

            Private Sub Child_Fetch(ByVal criteria As Criteria)
                Me.RaiseListChangedEvents = False
                Using Conn As New SqlConnection(Database.ITWConnection)
                    Conn.Open()
                    Using cmd As SqlCommand = Conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT oso.[OrderSetID], oso.[OrderID], oso.[Sequence], o.[Path] "
                        sql &= "FROM [109OrderSetOrder] oso "
                        sql &= "INNER JOIN [109Order] o ON oso.[OrderID] = o.[ID] "
                        sql &= "WHERE oso.[OrderSetID] = " & criteria.OrderSetID & " "
                        sql &= "ORDER BY oso.[Sequence] "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    Me.Add(OrderSetOrder.GetOrderSetOrder(dr))
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

End Namespace