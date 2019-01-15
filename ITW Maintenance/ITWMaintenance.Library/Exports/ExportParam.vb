Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ExportParam
        Inherits ReadOnlyBase(Of ExportParam)

#Region "Business Methods"

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
        Private Shared param4Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param4"))
        Public ReadOnly Property param4() As String
            Get
                Return GetProperty(Of String)(param4Property)
            End Get
        End Property


        Private Shared intakefacilityProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intakeFacility"))
        Public ReadOnly Property intakefacility() As Integer
            Get
                Return GetProperty(Of Integer)(intakefacilityProperty)
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

        Private Shared param4datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param4DataType"))
        Public ReadOnly Property param4datatype() As String
            Get
                Return GetProperty(Of String)(param4datatypeProperty)
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

        Private Shared var4Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var4"))
        Public ReadOnly Property var4() As String
            Get
                Return GetProperty(Of String)(var4Property)
            End Get
        End Property

        Private Shared daterangelimitproperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("daterangelimit"))
        Public ReadOnly Property daterangelimit() As Integer
            Get
                Return GetProperty(Of Integer)(daterangelimitproperty)
            End Get
        End Property

#End Region

#Region "Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region "Factory Methods"

        Public Shared Function GetExportParam(ByVal ExportID As Integer) As ExportParam
            Return DataPortal.Fetch(Of ExportParam)(New Criteria(ExportID))
        End Function


#End Region

#Region "Data Access"
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

                    sql = "SELECT [cObject], [cCatalog], "
                    sql &= "[param1], [param2], [param3], [param4], "
                    sql &= "[Param1DataType], [Param2DataType], [Param3DataType], [Param4DataType], "
                    sql &= "[var1], [var2], [var3], [var4], [daterangelimit] "
                    sql &= "FROM [105infocollection] "
                    sql &= "WHERE [ID] = '" & criteria.ExportID.ToString() & "' "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using datareader As New SafeDataReader(cmd.ExecuteReader)
                        datareader.Read()
                        With datareader

                            LoadProperty(Of String)(ObjectProperty, .GetString("cObject"))
                            LoadProperty(Of String)(CatalogProperty, .GetString("cCatalog"))

                            If .IsDBNull("param1datatype") Then
                                LoadProperty(Of String)(param1datatypeProperty, "")
                            Else
                                LoadProperty(Of String)(param1datatypeProperty, .GetString("Param1DataType"))
                            End If

                            If .IsDBNull("Param2DataType") Then
                                LoadProperty(Of String)(param2datatypeProperty, "")
                            Else
                                LoadProperty(Of String)(param2datatypeProperty, .GetString("Param2DataType"))
                            End If

                            If .IsDBNull("Param3DataType") Then
                                LoadProperty(Of String)(param3datatypeProperty, "")
                            Else
                                LoadProperty(Of String)(param3datatypeProperty, .GetString("Param3DataType"))
                            End If

                            If .IsDBNull("Param4DataType") Then
                                LoadProperty(Of String)(param4datatypeProperty, "")
                            Else
                                LoadProperty(Of String)(param4datatypeProperty, .GetString("Param4DataType"))
                            End If

                            If .GetString("param1") = "NA" Then
                                LoadProperty(Of String)(param1Property, "")
                            Else
                                LoadProperty(Of String)(param1Property, .GetString("param1"))
                            End If

                            If .GetString("param2") = "NA" Then
                                LoadProperty(Of String)(param2Property, "")
                            Else
                                LoadProperty(Of String)(param2Property, .GetString("param2"))
                            End If

                            If .GetString("param3") = "NA" Then
                                LoadProperty(Of String)(param3Property, "")
                            Else
                                LoadProperty(Of String)(param3Property, .GetString("param3"))
                            End If

                            If .GetString("param4") = "NA" Then
                                LoadProperty(Of String)(param4Property, "")
                            Else
                                LoadProperty(Of String)(param4Property, .GetString("param4"))
                            End If

                            If .IsDBNull("var1") Then
                                LoadProperty(Of String)(var1Property, "")
                            Else
                                LoadProperty(Of String)(var1Property, .GetString("var1"))
                            End If

                            If .IsDBNull("var2") Then
                                LoadProperty(Of String)(var2Property, "")
                            Else
                                LoadProperty(Of String)(var2Property, .GetString("var2"))
                            End If

                            If .IsDBNull("var3") Then
                                LoadProperty(Of String)(var3Property, "")
                            Else
                                LoadProperty(Of String)(var3Property, .GetString("var3"))
                            End If

                            If .IsDBNull("var4") Then
                                LoadProperty(Of String)(var4Property, "")
                            Else
                                LoadProperty(Of String)(var4Property, .GetString("var4"))
                            End If

                            If .IsDBNull("daterangelimit") Then
                                LoadProperty(Of Integer)(daterangelimitproperty, "")
                            Else
                                LoadProperty(Of Integer)(daterangelimitproperty, .GetString("daterangelimit"))
                            End If

                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region


    End Class
End Namespace

