Imports System.Data.SqlClient
Imports System.Xml

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvalTree
        Inherits ReadOnlyBase(Of ReadOnlyEvalTree)

        'Private _EvalID As Integer
        'Private _TreeDataXML As String

#Region " Business Methods "

        Private Shared EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property EvalID() As Integer
            Get
                Return ReadProperty(Of Integer)(EvalIDProperty)
            End Get
        End Property

        Private Shared TreeDataXMLProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("TreeDataXML"))
        Public ReadOnly Property TreeDataXML() As String
            Get
                Return ReadProperty(Of String)(TreeDataXMLProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return ReadProperty(Of Integer)(EvalIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetEvalTree(ByVal EvalID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyEvalTree
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Evaluation Tree")
            End If
            Return DataPortal.Fetch(Of ReadOnlyEvalTree)(New Criteria(EvalID, ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _EvalID As Integer
            Private _ActiveOnly As Boolean

            Public ReadOnly Property EvalID() As Integer
                Get
                    Return Me._EvalID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal EvalID As Integer, ByVal ActiveOnly As Boolean)
                Me._EvalID = EvalID
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        <RunLocal()> _
        Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
            Dim c As Criteria = DirectCast(criteria, Criteria)

            If c.EvalID > 0 Then
                LoadProperty(Of Integer)(EvalIDProperty, c.EvalID)
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            LoadProperty(Of Integer)(EvalIDProperty, criteria.EvalID)

            Using conn As New SqlConnection(Database.ITWConnection)
                ' Use an SqlDataAdapter to populate the dataset with information
                Dim sql As String
                Dim da As SqlDataAdapter
                Dim ds As New DataSet()
                Dim relation As DataRelation

                sql = "SELECT e.EvalID, e.ParentID, e.EName as [Description], "
                sql &= "e.EvalLevel, e.EFinal "
                sql &= "FROM [100Eval] e "
                sql &= "WHERE e.Level2 = " & criteria.EvalID & " "
                If criteria.ActiveOnly Then
                    sql &= "AND (Inactive = 0 OR EvalLevel = 2) "
                End If
                sql &= "ORDER BY EvalLevel, DOrder "

                conn.Open()
                da = New SqlDataAdapter(sql, conn)
                da.Fill(ds, "TreeNode")

                ds.Tables("TreeNode").PrimaryKey = New DataColumn() {ds.Tables("TreeNode").Columns(0)}

                Dim removeEvalIDs As New List(Of Integer)

                For Each dr As DataRow In ds.Tables("TreeNode").Rows
                    If (dr.Item("EvalLevel") > 3) AndAlso (Not ds.Tables("TreeNode").Rows.Contains(dr.Item("ParentID")) OrElse removeEvalIDs.Contains(dr.Item("ParentID"))) Then 'OrElse removeRows.Rows.Contains(dr.Item("ParentID"))) Then
                        'Dim rowToAdd As DataRow = removeRows.NewRow()
                        'rowToAdd.Item("EvalID") = dr.Item("EvalID")
                        'removeRows.Rows.Add()
                        removeEvalIDs.Add(dr.Item("EvalID"))
                    End If
                Next

                ' Now remove all of the rows
                For Each evalID As Integer In removeEvalIDs
                    For Each dr As DataRow In ds.Tables("TreeNode").Rows
                        If dr.Item("EvalID") = evalID Then
                            ds.Tables("TreeNode").Rows.Remove(dr)
                            Exit For
                        End If
                    Next
                Next

                relation = New DataRelation("ParentChild", ds.Tables("TreeNode").Columns("EvalID"), _
                    ds.Tables("TreeNode").Columns("ParentID"), False)
                relation.Nested = True
                ds.Relations.Add(relation)

                For Each column As DataColumn In ds.Tables("TreeNode").Columns
                    column.ColumnMapping = MappingType.Attribute
                Next

                LoadProperty(Of String)(TreeDataXMLProperty, ds.GetXml())
            End Using
        End Sub

#End Region

    End Class

End Namespace
