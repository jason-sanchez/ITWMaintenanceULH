Imports System.Configuration.ConfigurationManager

Module Database

    Public ReadOnly Property ITWConnection() As String
        Get
            Return ConnectionStrings("ITW").ConnectionString
        End Get
    End Property

    Public ReadOnly Property ITWCernerConnection() As String
        Get
            Return ConnectionStrings("ITWCerner").ConnectionString
        End Get
    End Property

    Public ReadOnly Property mCareJHConnection() As String
        Get
            Return ConnectionStrings("mCareJH").ConnectionString
        End Get
    End Property

End Module
