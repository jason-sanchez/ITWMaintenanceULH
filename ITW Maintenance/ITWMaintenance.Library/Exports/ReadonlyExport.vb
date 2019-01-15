Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ReadonlyExport
        Inherits ReadOnlyBase(Of ReadonlyExport)

#Region " Business Methods"

        Private Shared IDProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProp)
            End Get
        End Property

        Private Shared CollectionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("collectioName"))
        Public ReadOnly Property CollectionName() As String
            Get
                Return GetProperty(Of String)(CollectionProperty)
            End Get
        End Property


        Private Shared DBProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cDataBase"))
        Public ReadOnly Property cDataBase() As String
            Get
                Return GetProperty(Of String)(DBProperty)
            End Get
        End Property


        Private Shared CatalogProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cCatalog"))
        Public ReadOnly Property cCatalog() As String
            Get
                Return GetProperty(Of String)(CatalogProperty)
            End Get
        End Property

        Private Shared ObjectProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cObject"))
        Public ReadOnly Property cObject() As String
            Get
                Return GetProperty(Of String)(ObjectProperty)
            End Get
        End Property


        Private Shared param1Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param1"))
        Public ReadOnly Property param1() As String
            Get
                Return GetProperty(Of String)(param1Property)
            End Get
        End Property

        Private Shared param2Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param2"))
        Public ReadOnly Property param2() As String
            Get
                Return GetProperty(Of String)(param2Property)
            End Get
        End Property

        Private Shared param3Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param3"))
        Public ReadOnly Property param3() As String
            Get
                Return GetProperty(Of String)(param3Property)
            End Get
        End Property

        Private Shared infosecuritylevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("infoSecurityLevel"))
        Public ReadOnly Property infosecuritylevel() As Integer
            Get
                Return GetProperty(Of Integer)(infosecuritylevelProperty)
            End Get
        End Property

        Private Shared param1datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param1DataType"))
        Public ReadOnly Property param1datatype() As String
            Get
                Return GetProperty(Of String)(param1datatypeProperty)
            End Get
        End Property

        Private Shared param2datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param2DataType"))
        Public ReadOnly Property param2datatype() As String
            Get
                Return GetProperty(Of String)(param2datatypeProperty)
            End Get
        End Property

        Private Shared param3datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param3DataType"))
        Public ReadOnly Property param3datatype() As String
            Get
                Return GetProperty(Of String)(param3datatypeProperty)
            End Get
        End Property

        Private Shared var1Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var1"))
        Public ReadOnly Property var1() As String
            Get
                Return GetProperty(Of String)(var1Property)
            End Get
        End Property

        Private Shared var2Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var2"))
        Public ReadOnly Property var2() As String
            Get
                Return GetProperty(Of String)(var2Property)
            End Get
        End Property

        Private Shared var3Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var3"))
        Public ReadOnly Property var3() As String
            Get
                Return GetProperty(Of String)(var3Property)
            End Get
        End Property

#End Region

#Region " Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Private Sub New()

        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProp, .GetInt32("ID"))
                LoadProperty(Of String)(CollectionProperty, .GetString("collectionName"))
            End With
        End Sub

        Public Shared Function GetExport(ByRef ExportID As Integer) As ReadonlyExport
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view Exports")
            End If
            Return DataPortal.Fetch(Of ReadonlyExport)(New Criteria(ExportID))
        End Function

#End Region

#Region " Data Access "
        <Serializable()> _
        Private Class Criteria
            Private _ExportID As Integer
            Public ReadOnly Property ExportID() As Integer
                Get
                    Return Me._ExportID
                End Get
            End Property

            Public Sub New(ByVal ExportID As Integer)
                Me._ExportID = ExportID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [collectionName], [param1] "
                    sql &= "FROM [105infoCollection] "
                    sql &= "WHERE [ID] = " & criteria.ExportID.ToString() & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProp, .GetInt32("ID"))
                            LoadProperty(Of String)(CollectionProperty, .GetString("collectionName"))
                            LoadProperty(Of String)(param1Property, .GetString("param1"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace



