Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class OrderFormFieldList
        Inherits BusinessListBase(Of OrderFormFieldList, OrderFormField)

#Region " Business Methods "

        Public Overloads Sub AddNew()
            Me.Add(OrderFormField.NewOrderFormField(Me.Count + 1))
        End Sub

        Public Sub InsertField(ByVal Index As Integer)
            For Each field As OrderFormField In Me
                If field.DisplayOrder >= Index Then
                    field.DisplayOrder += 1
                End If
            Next
            Dim newField As OrderFormField = OrderFormField.NewOrderFormField(Index)
            Me.Insert(Index - 1, newField)
        End Sub

        Public Sub RemoveField(ByVal Index As Integer)
            Me.RemoveAt(Index - 1)
            For Each field As OrderFormField In Me
                If field.DisplayOrder >= Index Then
                    field.DisplayOrder -= 1
                End If
            Next
        End Sub

        Public Sub ClearField(ByVal Index As Integer)
            Dim field As OrderFormField = Me(Index - 1)
            field.Label = ""
            field.Type = OrderFormFieldTypes.GetDefaultFieldType()
            field.Options = ""
            field.HelpText = ""
        End Sub

        Public Function GetChildRuleDescriptions() As String()
            Return OrderFormField.NewOrderFormField(0).GetRuleDescriptions()
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewOrderFormFieldList() As OrderFormFieldList
            Return DataPortal.CreateChild(Of OrderFormFieldList)()
        End Function

        Public Shared Function GetOrderFormFieldList(ByVal OrderID As Integer) As OrderFormFieldList
            Return DataPortal.FetchChild(Of OrderFormFieldList)(New Criteria(OrderID))
        End Function

        Private Sub New()
            Me.AllowNew = True
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
                                Me.Add(OrderFormField.GetOrderFormField(dr))
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