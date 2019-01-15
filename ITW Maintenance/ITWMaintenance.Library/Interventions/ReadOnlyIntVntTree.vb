Imports System.Data.SqlClient
Imports System.Xml

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntVntTree
        Inherits ReadOnlyBase(Of ReadOnlyIntVntTree)

        'Private _intVntID As Integer
        'Private _TreeDataXML As String

#Region " Business Methods "

        Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property intVntID() As Integer
            Get
                Return ReadProperty(Of Integer)(intVntIDProperty)
            End Get
        End Property

        Private Shared TreeDataXMLProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("TreeDataXML"))
        Public ReadOnly Property TreeDataXML() As String
            Get
                Return ReadProperty(Of String)(TreeDataXMLProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return ReadProperty(Of Integer)(intVntIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetIntVntTree(ByVal intVntID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyIntVntTree
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Intervention Tree")
            End If
            Return DataPortal.Fetch(Of ReadOnlyIntVntTree)(New Criteria(intVntID, ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _intVntID As Integer
            Private _ActiveOnly As Boolean

            Public ReadOnly Property intVntID() As Integer
                Get
                    Return Me._intVntID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal intVntID As Integer, ByVal ActiveOnly As Boolean)
                Me._intVntID = intVntID
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        <RunLocal()> _
        Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
            Dim c As Criteria = DirectCast(criteria, Criteria)

            If c.intVntID > 0 Then
                LoadProperty(Of Integer)(intVntIDProperty, c.intVntID)
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            LoadProperty(Of Integer)(intVntIDProperty, criteria.intVntID)

            Using conn As New SqlConnection(Database.ITWConnection)
                ' Use an SqlDataAdapter to populate the dataset with information
                Dim sql As String
                Dim da As SqlDataAdapter
                Dim ds As New DataSet()
                Dim relation As DataRelation

                'sql = "SELECT i.intVntID, i.ParentID, i.iName as [Description], "
                'sql &= "i.intVntLevel, i.iFinal "
                'sql &= "FROM [110IntVnt] i "
                'sql &= "WHERE i.Level2 = " & criteria.intVntID & " "

                'If criteria.ActiveOnly Then
                'sql &= "AND (i.Inactive = 0 OR i.intVntLevel = 2) "
                'End If
                'sql &= "ORDER BY i.intVntLevel, i.DOrder "

                'Query was altered from the above query so that the ParentID folder shows on the Landing page.
                'also the criteria.ActiveOnly was not working so I moved the if/else logic to it's current location
                'added
                sql = "SELECT i.intVntID, i.ParentID, i.iName as [Description], "
                sql &= "i.intVntLevel, i.iFinal "
                sql &= "FROM [110IntVnt] i "
                sql &= "WHERE "
                'selects active only except if level2 is inactive you need to see any that tree off of it
                If criteria.ActiveOnly Then
                    sql &= "(i.Inactive = 0 OR i.intVntLevel = 2) AND "
                End If
                'selects level 1
                sql &= "(i.intVntID in (SELECT ParentID "
                sql &= "FROM [110IntVnt]  "
                sql &= "WHERE intVntID = " & criteria.intVntID & ") "
                'selects level 2
                sql &= "or (i.intVntID = " & criteria.intVntID & ") "
                'selects level 3
                sql &= "or (i.Level2 = " & criteria.intVntID & " "
                sql &= "and i.parentID = " & criteria.intVntID & ") "
                'selects greater than level 3
                sql &= "or (i.Level2 = " & criteria.intVntID & " and i.intVntLevel > 3)) "
                'end of added
                sql &= "ORDER BY i.intVntLevel, i.DOrder "

                conn.Open()
                da = New SqlDataAdapter(sql, conn)
                da.Fill(ds, "TreeNode")

                ds.Tables("TreeNode").PrimaryKey = New DataColumn() {ds.Tables("TreeNode").Columns(0)}

                Dim removeintVntIDs As New List(Of Integer)

                For Each dr As DataRow In ds.Tables("TreeNode").Rows
                    If (dr.Item("intVntLevel") > 3) AndAlso (Not ds.Tables("TreeNode").Rows.Contains(dr.Item("ParentID")) OrElse removeintVntIDs.Contains(dr.Item("ParentID"))) Then 'OrElse removeRows.Rows.Contains(dr.Item("ParentID"))) Then
                        'Dim rowToAdd As DataRow = removeRows.NewRow()
                        'rowToAdd.Item("intVntID") = dr.Item("intVntID")
                        'removeRows.Rows.Add()
                        removeintVntIDs.Add(dr.Item("intVntID"))
                    End If
                Next

                ' Now remove all of the rows
                For Each intVntID As Integer In removeintVntIDs
                    For Each dr As DataRow In ds.Tables("TreeNode").Rows
                        If dr.Item("intVntID") = intVntID Then
                            ds.Tables("TreeNode").Rows.Remove(dr)
                            Exit For
                        End If
                    Next
                Next

                relation = New DataRelation("ParentChild", ds.Tables("TreeNode").Columns("intVntID"), _
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
