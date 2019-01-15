Imports System.Data.SqlClient

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class ReadOnlyNursingNoteSearchResult
            Inherits ReadOnlyBase(Of ReadOnlyNursingNoteSearchResult)

#Region " Business Methods "

            'Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            'Public ReadOnly Property ID() As Integer
            '    Get
            '        Return GetProperty(Of Integer)(IDProperty)
            '    End Get
            'End Property

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property ID() As Integer
                Get
                    Return GetProperty(Of Integer)(IDProperty)
                End Get
            End Property

            Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
            Public ReadOnly Property Name() As String
                Get
                    Return GetProperty(Of String)(NameProperty)
                End Get
            End Property

            Private Shared DisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Discipline"))
            Public ReadOnly Property Discipline() As String
                Get
                    Return GetProperty(Of String)(DisciplineProperty)
                End Get
            End Property

            Private Shared IsFormProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("IsForm", "Is Form"))
            Public ReadOnly Property IsForm() As Boolean
                Get
                    Return GetProperty(Of Boolean)(IsFormProperty)
                End Get
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public ReadOnly Property Inactive() As Boolean
                Get
                    Return ReadProperty(Of Boolean)(InactiveProperty)
                End Get
            End Property

            Protected Overrides Function GetIdValue() As Object
                Return ReadProperty(Of Integer)(IDProperty)
            End Function

#End Region

#Region " Authorization Rules "

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

#End Region

#Region " Factory Methods "

            Private Sub New()
                ' Require use of factory methods
            End Sub

            Friend Sub New(ByRef dr As SafeDataReader)
                With dr
                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                    LoadProperty(Of String)(NameProperty, .GetString("Name"))
                    LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                    If .GetBoolean("Final") Then
                        LoadProperty(Of Boolean)(IsFormProperty, True)
                    Else
                        LoadProperty(Of Boolean)(IsFormProperty, False)
                    End If
                    LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                End With
            End Sub

#End Region

        End Class

    End Namespace

End Namespace