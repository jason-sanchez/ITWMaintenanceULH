Imports System.Data.SqlClient

Namespace Exports

    <Serializable()> _
    Public Class ExportList
        Inherits ReadOnlyListBase(Of ExportList, readonlyExport)


#Region " Factory Methods "

        Public Shared Function GetExportList(ByVal infoLevel As Integer, ByVal dept As String, ByVal intake As Integer) As ExportList
            Return DataPortal.Fetch(Of ExportList)(New criteria(infoLevel, dept, intake))
        End Function

        'Private Sub New()

        'End Sub

        'Public Overloads Function Contains(ByVal ExportID As Integer) As Boolean
        '    For Each Item As ReadonlyExport In Me
        '        If Item.ID = ExportID Then
        '            Return True
        '        End If
        '    Next
        'End Function
#End Region

#Region " Data Access"
        <Serializable()> _
        Private Class criteria

            Private _ExportID As Integer
            Public ReadOnly Property ExportID() As Integer
                Get
                    Return Me._ExportID
                End Get
            End Property
            'Public Sub New(ByVal ExportID As Integer)
            '    Me._ExportID = ExportID
            'End Sub
            Private _infolevel As Integer
            Public ReadOnly Property info() As Integer
                Get
                    Return Me._infolevel
                End Get
            End Property

            Private _dept As String
            Public ReadOnly Property dept() As String
                Get
                    Return Me._dept
                End Get
            End Property
            Private _intake As String
            Public ReadOnly Property intake() As String
                Get
                    Return Me._intake
                End Get
            End Property

            Public Sub New(ByVal info As Integer, ByVal dept As String, ByVal intake As Integer)
                Me._infolevel = info
                Me._dept = dept
                Me._intake = intake
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As criteria)

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT i.[ID], i.[collectionName], [param1], [param2], [param3] "
                    sql &= "FROM [105infocollection] i "
                    sql &= "INNER JOIN [105ExportBridge] ex on i.id = ex.ExportID "
                    sql &= "WHERE [infoSecurityLevel] <= " & criteria.info & " "
                    sql &= "and ex.[intakefacility] = '" & criteria.intake.ToString() & "' "
                    sql &= "Order by i.[collectionName] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using datareader As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While datareader.Read()
                            Me.Add(New ReadonlyExport(datareader))
                        End While
                        IsReadOnly = True
                    End Using

                End Using
            End Using

            'RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace
