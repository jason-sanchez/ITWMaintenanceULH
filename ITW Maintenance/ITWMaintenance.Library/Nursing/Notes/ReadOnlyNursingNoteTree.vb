Imports System.Data.SqlClient
Imports System.Xml

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class ReadOnlyNursingNoteTree
            Inherits ReadOnlyBase(Of ReadOnlyNursingNoteTree)

#Region " Business Methods "

            Private Shared TreeDataXMLProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("TreeDataXML"))
            Public ReadOnly Property TreeDataXML() As String
                Get
                    Return ReadProperty(Of String)(TreeDataXMLProperty)
                End Get
            End Property

#End Region

#Region " Authorization Rules "

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function GetNursingNoteTree(ByVal Discipline As Integer, ByVal ActiveOnly As Boolean, ByVal FilterOutEmptyFolders As Boolean) As ReadOnlyNursingNoteTree
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view a Nursing Note Tree")
                End If
                Return DataPortal.Fetch(Of ReadOnlyNursingNoteTree)(New Criteria(Discipline, ActiveOnly, FilterOutEmptyFolders))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _Discipline As Integer
                Private _ActiveOnly As Boolean
                Private _FilterOutEmptyFolders As Boolean

                Public ReadOnly Property Discipline() As Integer
                    Get
                        Return Me._Discipline
                    End Get
                End Property

                Public ReadOnly Property ActiveOnly() As Boolean
                    Get
                        Return Me._ActiveOnly
                    End Get
                End Property

                Public ReadOnly Property FilterOutEmptyFolders() As Boolean
                    Get
                        Return Me._FilterOutEmptyFolders
                    End Get
                End Property

                Public Sub New(ByVal Discipline As Integer, ByVal ActiveOnly As Boolean, ByVal FilterOutEmptyFolders As Boolean)
                    Me._Discipline = Discipline
                    Me._ActiveOnly = ActiveOnly
                    Me._FilterOutEmptyFolders = FilterOutEmptyFolders
                End Sub
            End Class

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    ' Use an SqlDataAdapter to populate the dataset with information
                    Dim sql As String
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet()
                    Dim relation As DataRelation

                    sql = "SELECT [nsNtID] AS [ID], [ParentID], [NName] AS [Name], "
                    sql &= "[nsNtLevel] AS [Level], [NFinal] AS [Final], [NInactive] AS [Inactive] "
                    sql &= "FROM [100CrNt] "
                    sql &= "WHERE [discipline] = " & criteria.Discipline & " "
                    If criteria.ActiveOnly Then
                        sql &= "AND [NInactive] = 0 "
                    End If
                    If criteria.FilterOutEmptyFolders() Then
                        ' Filter out the level 1 folders that are empty (that were created to categorize the goals)
                        sql &= "AND (ParentID > 0 "
                        sql &= "    OR (NFinal = 1 "
                        sql &= "        OR nsNtID IN ("
                        sql &= "            SELECT ParentID "
                        sql &= "            FROM [100CrNt] "
                        sql &= "            WHERE discipline = 6 "
                        sql &= "            AND nInactive = 0 "
                        sql &= "        ) "
                        sql &= "    ) "
                        sql &= ") "
                    End If
                    sql &= "ORDER BY [nsNtLevel], [DOrder] "

                    conn.Open()
                    da = New SqlDataAdapter(sql, conn)
                    da.Fill(ds, "TreeNode")

                    ds.Tables("TreeNode").PrimaryKey = New DataColumn() {ds.Tables("TreeNode").Columns(0)}

                    Dim removeFormIDs As New List(Of Integer)

                    ' Remove any abandoned children...
                    For Each dr As DataRow In ds.Tables("TreeNode").Rows
                        If (dr.Item("Level") > 1) AndAlso (Not ds.Tables("TreeNode").Rows.Contains(dr.Item("ParentID")) OrElse removeFormIDs.Contains(dr.Item("ParentID"))) Then
                            removeFormIDs.Add(dr.Item("ID"))
                        End If
                    Next

                    ' Now remove all of the rows
                    For Each formID As Integer In removeFormIDs
                        For Each dr As DataRow In ds.Tables("TreeNode").Rows
                            If dr.Item("ID") = formID Then
                                ds.Tables("TreeNode").Rows.Remove(dr)
                                Exit For
                            End If
                        Next
                    Next

                    relation = New DataRelation("ParentChild", ds.Tables("TreeNode").Columns("ID"), _
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

End Namespace