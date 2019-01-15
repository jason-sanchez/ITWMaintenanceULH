Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Web

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Globalization
Imports System.Collections
Imports ITWMaintenance.Library.Lookup



Namespace Exports
    <Serializable()> _
    Public Class Exportsp
        Inherits BusinessBase(Of Exportsp)

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


        Private Shared stringbproperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("stringb"))
        Public Property stringb() As String
            Get
                Return GetProperty(Of String)(stringbproperty)
            End Get
            Set(value As String)
                SetProperty(Of String)(stringbproperty, value)
            End Set
        End Property


#End Region

#Region "Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region "Factory Methods"

        Public Shared Function RunExport(ByVal dr1 As Integer, ByVal p1 As String, ByVal p2 As String, ByVal p3 As String, ByVal p4 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String, ByVal v4 As String, ByVal c1 As String, ByVal c2 As String, ByVal d1 As String, ByVal d2 As String, ByVal d3 As String, ByVal d4 As String, ByVal intake As Integer) As Exportsp

            If d1 = "datetime" And d2 = "datetime" Then
                'Dim paramx = param1
                Dim format() = {"MM/dd/yyyy", "MM/d/yyyy", "M/dd/yyyy", "M/d/yyyy", "MM/dd/yy", "MM/d/yy", "M/dd/yy", "M/d/yy", "MM-dd-yyyy", "MM-d-yyyy", "M-dd-yyyy", "M-d-yyyy", "MM-dd-yy", "MM-d-yy", "M-dd-yy", "M-d-yy"}
                Dim date1 As Date = Date.ParseExact(p1, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)
                Dim date2 As Date = Date.ParseExact(p2, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None)

                If DateDiff(DateInterval.Day, date1, date2) < 0 Then
                    Throw New System.Security.SecurityException("The 'From' date cannot be later than the 'To' date!")
                End If

                'If DateDiff(DateInterval.Day, date1, date2) > 90 Then
                '    Throw New System.Security.SecurityException("The date range cannot exceed 90 days!")
                'End If

                If DateDiff(DateInterval.Day, date1, date2) > dr1 Then
                    Throw New System.Security.SecurityException("The date range cannot exceed " & dr1 & " days!")
                End If

            End If

            Dim num As Integer
            If d1 = "int" Then
                If Not Integer.TryParse(p1, num) Then
                    Throw New System.Security.SecurityException("The value must be an integer!")
                End If
            End If

            If d2 = "int" Then
                If Not Integer.TryParse(p2, num) Then
                    Throw New System.Security.SecurityException("The value must be an integer!")
                End If
            End If

            If d3 = "int" Then
                If Not Integer.TryParse(p3, num) Then
                    Throw New System.Security.SecurityException("The value must be an integer!")
                End If
            End If

            If d4 = "int" Then
                If Not Integer.TryParse(p4, num) Then
                    Throw New System.Security.SecurityException("The value must be an integer!")
                End If
            End If

            Return DataPortal.Fetch(Of Exportsp)(New Criteria(p1, p2, p3, p4, v1, v2, v3, v4, c1, c2, intake))
        End Function

#End Region

#Region "Data Access"
        <Serializable()> _
        Private Class Criteria

            Private _param1txt As String
            Public ReadOnly Property param1txtProperty() As String
                Get
                    Return Me._param1txt
                End Get
            End Property

            Private _param2txt As String
            Public ReadOnly Property param2txtProperty() As String
                Get
                    Return Me._param2txt
                End Get
            End Property

            Private _param3txt As String
            Public ReadOnly Property param3txtProperty() As String
                Get
                    Return Me._param3txt
                End Get
            End Property

            Private _param4txt As String
            Public ReadOnly Property param4txtProperty() As String
                Get
                    Return Me._param4txt
                End Get
            End Property

            Private _var1 As String
            Public ReadOnly Property var1Property() As String
                Get
                    Return Me._var1
                End Get
            End Property

            Private _var2 As String
            Public ReadOnly Property var2Property() As String
                Get
                    Return Me._var2
                End Get
            End Property

            Private _var3 As String
            Public ReadOnly Property var3Property() As String
                Get
                    Return Me._var3
                End Get
            End Property

            Private _var4 As String
            Public ReadOnly Property var4Property() As String
                Get
                    Return Me._var4
                End Get
            End Property

            Private _c1 As String
            Public ReadOnly Property c1Property() As String
                Get
                    Return Me._c1
                End Get
            End Property

            Private _c2 As String
            Public ReadOnly Property c2Property() As String
                Get
                    Return Me._c2
                End Get
            End Property

            Private _c3 As String
            Public ReadOnly Property c3Property() As String
                Get
                    Return Me._c3
                End Get
            End Property

            Private _d1 As String
            Public ReadOnly Property d1Property() As String
                Get
                    Return Me._d1
                End Get
            End Property

            Private _d2 As String
            Public ReadOnly Property d2Property() As String
                Get
                    Return Me._d2
                End Get
            End Property

            Private _d3 As String
            Public ReadOnly Property d3Property() As String
                Get
                    Return Me._d3
                End Get
            End Property

            Private _d4 As String
            Public ReadOnly Property d4Property() As String
                Get
                    Return Me._d4
                End Get
            End Property

            Private _intake As Integer
            Public ReadOnly Property intake() As String
                Get
                    Return Me._intake
                End Get
            End Property
            Public Sub New(ByVal param1 As String, ByVal param2 As String, ByVal param3 As String, ByVal param4 As String, ByVal var1 As String, ByVal var2 As String, ByVal var3 As String, ByVal var4 As String, ByVal c1 As String, ByVal c2 As String, ByVal intake As Integer)
                Me._param1txt = param1
                Me._param2txt = param2
                Me._param3txt = param3
                Me._param4txt = param4
                Me._var1 = var1
                Me._var2 = var2
                Me._var3 = var3
                Me._var4 = var4
                Me._c1 = c1
                Me._c2 = c2
                Me._intake = intake

            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Dim sconn = ""

            If criteria.c1Property = "mCareJH" Then
                sconn = Database.mCareJHConnection
            Else
                sconn = Database.ITWConnection
            End If

            Using conn As New SqlConnection(sconn)

                Dim sqlcomm As New SqlCommand("dbo." & criteria.c2Property, conn) With {.CommandType = CommandType.StoredProcedure}

                With sqlcomm.Parameters
                    If criteria.var1Property <> "" Then
                        .AddWithValue(criteria.var1Property, criteria.param1txtProperty)
                    End If

                    If criteria.var2Property <> "" Then
                        .AddWithValue(criteria.var2Property, criteria.param2txtProperty)
                    End If

                    If criteria.var3Property <> "" Then
                        .AddWithValue(criteria.var3Property, criteria.param3txtProperty)
                    End If

                    If criteria.var4Property <> "" Then
                        .AddWithValue(criteria.var4Property, criteria.param4txtProperty)
                    End If

                    If criteria.intake <> "" Then
                        .AddWithValue("@intakeFacility",criteria.intake)
                    End If

                End With

                Dim dt As New System.Data.DataTable()
                Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter
                DataAdapter.SelectCommand = sqlcomm
                DataAdapter.Fill(dt)


                Dim stringa As New StringBuilder()
                For c As Integer = 0 To dt.Columns.Count - 1
                    stringa.Append(dt.Columns(c).ColumnName + ",")
                Next
                stringa.Append(Environment.NewLine)

                For r As Integer = 0 To dt.Rows.Count - 1
                    For k As Integer = 0 To dt.Columns.Count - 1
                        stringa.Append(dt.Rows(r)(k).ToString().Replace(",", "") + ",")
                    Next
                    stringa.Append(Environment.NewLine)
                Next

                stringb = stringa.ToString()

            End Using

            LoadProperty(Of String)(stringbproperty, stringb)

        End Sub

#End Region


    End Class

End Namespace
