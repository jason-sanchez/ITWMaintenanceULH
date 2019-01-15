Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderFormFieldList
        Inherits ReadOnlyListBase(Of ReadOnlyOrderFormFieldList, ReadOnlyOrderFormField)

#Region " Factory Methods "

        Public Shared Function GetOrderFormFieldList(ByVal OrderID As Integer) As ReadOnlyOrderFormFieldList
            Return DataPortal.FetchChild(Of ReadOnlyOrderFormFieldList)(New Criteria(OrderID))
        End Function

        Private Sub New()
            ' Require use of Factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _OrderID As Integer

            Public ReadOnly Property OrderID() As Integer
                Get
                    Return Me._OrderID
                End Get
            End Property

            Public Sub New(ByVal OrderID As Integer)
                Me._OrderID = OrderID
            End Sub
        End Class

        Private Sub Child_Fetch(ByVal criteria As Criteria)
            Me.RaiseListChangedEvents = False
            IsReadOnly = False
            Using Conn As New SqlConnection(Database.ITWConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [OrderID], [Label], [Type], "
                    sql &= "[Options], [HelpText], [DisplayOrder], [DefaultValue] "
                    sql &= "FROM [109OrderFormField] "
                    sql &= "WHERE [OrderID] = " & criteria.OrderID & " "
                    sql &= "ORDER BY [DisplayOrder] "

                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        With dr
                            While .Read()
                                Me.Add(ReadOnlyOrderFormField.GetOrderFormFieldInfo(dr))
                            End While
                        End With
                    End Using
                End Using
            End Using
            IsReadOnly = True
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace