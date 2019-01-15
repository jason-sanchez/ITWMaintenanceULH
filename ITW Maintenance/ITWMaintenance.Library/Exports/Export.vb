Imports System.Data.SqlClient
Imports ITWMaintenance.Library.Lookup

Namespace Exports

    <Serializable()>
    Public Class Export
        Inherits BusinessBase(Of Export)

#Region "Business Methods"

        Private Shared IDProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProp)
            End Get
        End Property

        Private Shared CollectionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("collectioName"))
        Public Property CollectionName() As String
            Get
                Return GetProperty(Of String)(CollectionProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(CollectionProperty, value)
            End Set
        End Property


        Private Shared DBProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cDataBase"))
        Public Property cDataBase() As String
            Get
                Return GetProperty(Of String)(DBProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(DBProperty, value)
            End Set
        End Property


        Private Shared CatalogProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cCatalog"))
        Public Property cCatalog() As String
            Get
                Return GetProperty(Of String)(CatalogProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(CatalogProperty, value)
            End Set
        End Property

        Private Shared ObjectProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("cObject"))
        Public Property cObject() As String
            Get
                Return GetProperty(Of String)(ObjectProperty)
            End Get
            Set(value As String)
                SetProperty(Of String)(ObjectProperty, value)
            End Set
        End Property


        Private Shared param1Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param1"))
        Public Property param1() As String
            Get
                Return GetProperty(Of String)(param1Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param1Property, value)
            End Set
        End Property

        Private Shared param2Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param2"))
        Public Property param2() As String
            Get
                Return GetProperty(Of String)(param2Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param2Property, value)
            End Set
        End Property

        Private Shared param3Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("param3"))
        Public Property param3() As String
            Get
                Return GetProperty(Of String)(param3Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param3Property, value)
            End Set
        End Property

        Private Shared infosecuritylevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("infoSecurityLevel"))
        Public Property infosecuritylevel() As Integer
            Get
                Return GetProperty(Of Integer)(infosecuritylevelProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(infosecuritylevelProperty, value)
            End Set
        End Property

        Private Shared param1datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param1DataType"))
        Public Property param1datatype() As String
            Get
                Return GetProperty(Of String)(param1datatypeProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param1datatypeProperty, value)
            End Set
        End Property

        Private Shared param2datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param2DataType"))
        Public Property param2datatype() As String
            Get
                Return GetProperty(Of String)(param2datatypeProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param2datatypeProperty, value)
            End Set
        End Property

        Private Shared param3datatypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Param3DataType"))
        Public Property param3datatype() As String
            Get
                Return GetProperty(Of String)(param3datatypeProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(param3datatypeProperty, value)
            End Set
        End Property

        Private Shared var1Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var1"))
        Public Property var1() As String
            Get
                Return GetProperty(Of String)(var1Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(var1Property, value)
            End Set
        End Property

        Private Shared var2Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var2"))
        Public Property var2() As String
            Get
                Return GetProperty(Of String)(var2Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(var2Property, value)
            End Set
        End Property

        Private Shared var3Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("var3"))
        Public Property var3() As String
            Get
                Return GetProperty(Of String)(var3Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(var3Property, value)
            End Set
        End Property



#End Region

#Region "Validation Rules"

        Protected Overrides Sub AddBusinessRules()


            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, CollectionProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(CollectionProperty, 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, DBProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(DBProperty, 100))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, CatalogProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(CatalogProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, ObjectProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(ObjectProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param1Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param1Property, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param2Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param2Property, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param3Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param3Property, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param1datatypeProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param1datatypeProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param2datatypeProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param2datatypeProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, param3datatypeProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(param3datatypeProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, var1Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(var1Property, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, var2Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(var2Property, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, var3Property)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                        New Validation.CommonRules.MaxLengthRuleArgs(var3Property, 50))

        End Sub
#End Region

#Region "Authorization Rules"

        Protected Overrides Sub AddAuthorizationRules()
            ' Add AuthorizationRules here
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            ' Use the inactive switch
            Return False 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function



#End Region

#Region "Factory Methods"

        Public Shared Function NewExport() As Export
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add an Export")
            End If
            Return DataPortal.Create(Of Export)()
        End Function

        Public Shared Function GetExport(ByVal ExportID As Integer)
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Export")
            End If
            Return DataPortal.Fetch(New Criteria(ExportID))
        End Function

        Public Shared Sub DeleteExport(ByVal ExportID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to delete an Export")
            End If

            DataPortal.Delete(New Criteria(ExportID))
        End Sub

        Public Overloads Function Save() As Export
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to delete an Export")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorzied to add an Export")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update an Export")
            End If

            Return MyBase.Save()
        End Function

        Public Sub New()

        End Sub

#End Region

#Region "Data Access"

        <Serializable()>
        Private Class Criteria
            Private _ExportID As Integer
            Public ReadOnly Property ExportID() As Integer
                Get
                    Return _ExportID
                End Get
            End Property

            Public Sub New(ByVal ExportID As Integer)
                _ExportID = ExportID
            End Sub

        End Class

        '<RunLocal()> _
        'Protected Overloads Sub DataPortal_Create()
        '    LoadProperty(Of Boolean)(Inact)
        'End Sub

        Private Overloads Sub DataPortal_Fetch()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [collectionName], [cDataBase], [cCatalog], [cObject], "
                    sql &= "[param1], [param2], [param3], [infoSecurityLevel], "
                    sql &= "[Param1DataType], [Param2DataType], [Param3DataType], [var1], [var2], [var3] "
                    sql &= "FROM [105infoCollection] "
                    'sql &= "WHERE [ID] =  " & ExportID & "  "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProp, .GetInt32("ID"))
                            LoadProperty(Of String)(CollectionProperty, .GetString("collectionName"))
                            LoadProperty(Of String)(DBProperty, .GetString("cDataBase"))
                            LoadProperty(Of String)(CatalogProperty, .GetString("cCatalog"))
                            LoadProperty(Of String)(ObjectProperty, .GetString("cObject"))
                            LoadProperty(Of String)(param1Property, .GetString("param1"))
                            LoadProperty(Of String)(param2Property, .GetString("param2"))
                            LoadProperty(Of String)(param3Property, .GetString("param3"))
                            LoadProperty(Of Integer)(infosecuritylevelProperty, .GetInt32("infoSecurityLevel"))
                            LoadProperty(Of String)(param1datatypeProperty, .GetString("Param1DataType"))
                            LoadProperty(Of String)(param2datatypeProperty, .GetString("Param2DataType"))
                            LoadProperty(Of String)(param3datatypeProperty, .GetString("Param3DataType"))
                            LoadProperty(Of String)(var1Property, .GetString("var1"))
                            LoadProperty(Of String)(var2Property, .GetString("var2"))
                            LoadProperty(Of String)(var3Property, .GetString("var3"))

                        End With
                    End Using
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "INSERT INTO [105infoCollection] ([collectionName], [cDataBase], "
                    sql &= "[cCatalog], [cObject], [param1], [param2], [param3], [infoSecurityLevel], "
                    sql &= "[Param1DataType], [Param2DataType], [Param3DataType], [var1], [var2], [var3], "
                    sql &= "VALUES (@collectionName, @cDataBase, @cCatalog, @cObject, @param1, "
                    sql &= "@param2, @param3, @intakeFacility, @infoSecurityLevel, @Param1DataType, "
                    sql &= "@Param2DataType, @Param3DataType, @var1, @var2, @var3) "
                    sql &= "SELECT @NewID = SCOPE_IDENTITY"

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@collectionName", ReadProperty(Of String)(CollectionProperty))
                    cmd.Parameters.AddWithValue("@cDataBase", ReadProperty(Of String)(DBProperty))
                    cmd.Parameters.AddWithValue("@cCatalog", ReadProperty(Of String)(CatalogProperty))
                    cmd.Parameters.AddWithValue("@cObject", ReadProperty(Of String)(ObjectProperty))
                    cmd.Parameters.AddWithValue("@param1", ReadProperty(Of String)(param1Property))
                    cmd.Parameters.AddWithValue("@param2", ReadProperty(Of String)(param2Property))
                    cmd.Parameters.AddWithValue("@param3", ReadProperty(Of String)(param3datatypeProperty))
                    cmd.Parameters.AddWithValue("@infoSecurityLevel", ReadProperty(Of Integer)(infosecuritylevelProperty))
                    cmd.Parameters.AddWithValue("@Param1DataType", ReadProperty(Of String)(param1datatypeProperty))
                    cmd.Parameters.AddWithValue("@Param2DataType", ReadProperty(Of String)(param2datatypeProperty))
                    cmd.Parameters.AddWithValue("@Param3DataType", ReadProperty(Of String)(param3datatypeProperty))
                    cmd.Parameters.AddWithValue("@var1", ReadProperty(Of String)(var1Property))
                    cmd.Parameters.AddWithValue("@var2", ReadProperty(Of String)(var2Property))
                    cmd.Parameters.AddWithValue("@var3", ReadProperty(Of String)(var3Property))

                    Dim param As New SqlParameter("@NewID", SqlDbType.Int)
                    param.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(param)

                    LoadProperty(Of Integer)(IDProp, CInt(cmd.ExecuteScalar()))
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If IsSelfDirty Then
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "UPDATE [105infoCollection] SET "
                        sql &= "[collectionName] = @collectionName, "
                        sql &= "[cDatabase] = @cDatabase, "
                        sql &= "[cCatalog] = @cCatalog, "
                        sql &= "[cObject] = @cObject, "
                        sql &= "[param1] = @param1, "
                        sql &= "[param2] = @param2, "
                        sql &= "[param3] = @param3, "
                        sql &= "[infoSecurityLevel] = @infoSecurityLevel, "
                        sql &= "[Param1DataType] = @Param1DataType, "
                        sql &= "[Param2DataType] = @Param2DataType, "
                        sql &= "[Param3DataType] = @Param3DataType, "
                        sql &= "[var1] = @var1, "
                        sql &= "[var2] = @var2, "
                        sql &= "[var3] = @var3 "
                        sql &= "WHERE [ID] = @ID "


                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProp))
                        cmd.Parameters.AddWithValue("@collectionName", ReadProperty(Of String)(CollectionProperty))
                        cmd.Parameters.AddWithValue("@cDataBase", ReadProperty(Of String)(DBProperty))
                        cmd.Parameters.AddWithValue("@cCatalog", ReadProperty(Of String)(CatalogProperty))
                        cmd.Parameters.AddWithValue("@cObject", ReadProperty(Of String)(ObjectProperty))
                        cmd.Parameters.AddWithValue("@param1", ReadProperty(Of String)(param1Property))
                        cmd.Parameters.AddWithValue("@param2", ReadProperty(Of String)(param2Property))
                        cmd.Parameters.AddWithValue("@param3", ReadProperty(Of String)(param3datatypeProperty))
                        cmd.Parameters.AddWithValue("@infoSecurityLevel", ReadProperty(Of Integer)(infosecuritylevelProperty))
                        cmd.Parameters.AddWithValue("@Param1DataType", ReadProperty(Of String)(param1datatypeProperty))
                        cmd.Parameters.AddWithValue("@Param2DataType", ReadProperty(Of String)(param2datatypeProperty))
                        cmd.Parameters.AddWithValue("@Param3DataType", ReadProperty(Of String)(param3datatypeProperty))
                        cmd.Parameters.AddWithValue("@var1", ReadProperty(Of String)(var1Property))
                        cmd.Parameters.AddWithValue("@var2", ReadProperty(Of String)(var2Property))
                        cmd.Parameters.AddWithValue("@var3", ReadProperty(Of String)(var3Property))
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProp)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "DELETE FROM [105infoCollection] "
                    sql &= "WHERE [ID] = @ID"

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", criteria.ExportID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace