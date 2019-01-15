Imports System.Data.SqlClient

Namespace Menu

    <Serializable()> _
    Public Class ReadOnlyMenuItemList
        Inherits ReadOnlyListBase(Of ReadOnlyMenuItemList, ReadOnlyMenuItem)

#Region " Factory Methods "

        Public Shared Function GetMenuItemList(ByVal ParentID As Integer, ByVal SecurityLevel As Integer, ByVal IntakeFacility As Integer) As ReadOnlyMenuItemList
            Return DataPortal.Fetch(Of ReadOnlyMenuItemList)(New Criteria(ParentID, SecurityLevel, IntakeFacility))
        End Function

        Public Function GetMenuItemByDescription(ByVal MenuItemDescription As String) As ReadOnlyMenuItem
            For Each item As ReadOnlyMenuItem In Me
                If item.Description = MenuItemDescription Then
                    Return item
                End If
            Next

            Return Nothing
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ParentID As Integer
            Private _SecurityLevel As Integer
            Private _IntakeFacility As Integer

            Public ReadOnly Property ParentID() As Integer
                Get
                    Return Me._ParentID
                End Get
            End Property

            Public ReadOnly Property SecurityLevel() As Integer
                Get
                    Return Me._SecurityLevel
                End Get
            End Property

            Public ReadOnly Property IntakeFacility() As Integer
                Get
                    Return Me._IntakeFacility
                End Get
            End Property

            Public Sub New(ByVal ParentID As Integer, ByVal SecurityLevel As Integer, ByVal intakeFacility As Integer)
                Me._ParentID = ParentID
                Me._SecurityLevel = SecurityLevel
                Me._IntakeFacility = intakeFacility
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Description], [Link] "
                    sql &= "FROM [300MaintMenu] "
                    sql &= "WHERE [ParentID] = " & criteria.ParentID & " "
                    ' 2/2/2015 Matt - We're now filtering the menu based on the user's security level
                    sql &= "AND [MinimumSecurityLevel] <= " & criteria.SecurityLevel & " "
                    ' 06/06/2017 MW - #6825 - filtering menue based on intake facility
                    sql &= "AND [intakeFacility] = " & criteria.IntakeFacility & " "
                    sql &= "ORDER BY [DisplayOrder] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyMenuItem(dr))
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