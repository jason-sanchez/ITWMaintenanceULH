Imports System.Data.SqlClient
Imports System.Reflection
Imports ITWMaintenance.Library.Evaluations.Utilities

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class NursingNoteForm
            Inherits BusinessBase(Of NursingNoteForm)


#Region " Business Methods "

            ' 03/02/2010 Matt - We are now allowing the user to insert and remove form fields.
            '   Since fields are linked with Global Lookup items, we need a way of preserving
            '   the link between the field and the lookup item after the fields have been shifted around.
            '   To do this, I have created

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            'Private ReadOnly Property ID() As Integer
            '    Get
            '        Return GetProperty(Of Integer)(IDProperty)
            '    End Get
            'End Property

            Private Shared FormIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property FormID() As Integer
                Get
                    Return GetProperty(Of Integer)(FormIDProperty)
                End Get
            End Property

            Private Shared HasBeenUsedProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("HasBeenUsed"))
            Public ReadOnly Property HasBeenUsed() As Boolean
                Get
                    Return GetProperty(Of Boolean)(HasBeenUsedProperty)
                End Get
            End Property

            Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID", "Parent ID", 0))
            Public ReadOnly Property ParentID() As Integer
                Get
                    Return GetProperty(Of Integer)(ParentIDProperty)
                End Get
            End Property

            Private Shared ParentNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ParentName", "Parent Name"))
            Public ReadOnly Property ParentName() As String
                Get
                    Return GetProperty(Of String)(ParentNameProperty)
                End Get
            End Property

            Private Shared LevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Level", "Level", 1))
            Public ReadOnly Property Level() As Integer
                Get
                    Return GetProperty(Of Integer)(LevelProperty)
                End Get
            End Property

            Private Shared FormNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FormName", "Form Name"))
            Public Property FormName() As String
                Get
                    Return GetProperty(Of String)(FormNameProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(FormNameProperty, value)
                End Set
            End Property

            Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline"))
            Public ReadOnly Property Discipline() As Integer
                Get
                    Return GetProperty(Of Integer)(DisciplineProperty)
                End Get
            End Property

            Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
            Public Property DisplayOrder() As Integer
                Get
                    Return GetProperty(Of Integer)(DisplayOrderProperty)
                End Get
                Set(ByVal value As Integer)
                    SetProperty(Of Integer)(DisplayOrderProperty, value)
                End Set
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(InactiveProperty, value)
                End Set
            End Property

            Private Shared RequiredProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Required"))
            Public Property Required() As Boolean
                Get
                    Return GetProperty(Of Boolean)(RequiredProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(RequiredProperty, value)
                End Set
            End Property

            Private Shared EducationProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Education"))
            Public Property Education() As Boolean
                Get
                    Return GetProperty(Of Boolean)(EducationProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(EducationProperty, value)
                End Set
            End Property

            Private Shared PCarOnlyProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("PCarOnly", "PCar Only"))
            Public Property PCarOnly() As Boolean
                Get
                    Return GetProperty(Of Boolean)(PCarOnlyProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(PCarOnlyProperty, value)
                End Set
            End Property

            Private Shared NursingNoteOnlyProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("NursingNoteOnly", "Nursing Note Only"))
            Public Property NursingNoteOnly() As Boolean
                Get
                    Return GetProperty(Of Boolean)(NursingNoteOnlyProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(NursingNoteOnlyProperty, value)
                End Set
            End Property

            Private Shared RedirectToProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("RedirectTo", "Redirect To"))
            Public Property RedirectTo() As String
                Get
                    Return GetProperty(Of String)(RedirectToProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(RedirectToProperty, value)
                End Set
            End Property

            Private Shared CalculatedProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Calculated"))
            Public Property Calculated() As Boolean
                Get
                    Return GetProperty(Of Boolean)(CalculatedProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(CalculatedProperty, value)
                End Set
            End Property

            Private Shared CalculationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Calculation"))
            Public Property Calculation() As String
                Get
                    Return GetProperty(Of String)(CalculationProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(CalculationProperty, value)
                End Set
            End Property

            Private Shared NoResultProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("NoResult", "No Result"))
            Public Property NoResult() As Boolean
                Get
                    Return GetProperty(Of Boolean)(NoResultProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(NoResultProperty, value)
                End Set
            End Property

            Private Shared LockedProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Locked"))
            Public Property Locked() As Boolean
                Get
                    Return GetProperty(Of Boolean)(LockedProperty)
                End Get
                Set(ByVal value As Boolean)
                    SetProperty(Of Boolean)(LockedProperty, value)
                End Set
            End Property

            Private Shared LockNotesProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("LockNotes", "Lock Notes"))
            Public Property LockNotes() As String
                Get
                    Return ReadProperty(Of String)(LockNotesProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(LockNotesProperty, value)
                End Set
            End Property

#Region " Form Field Labels "

            Private Shared Field01LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Label", "Field 01 Label"))
            Public Property Field01Label() As String
                Get
                    Return ReadProperty(Of String)(Field01LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field01LabelProperty, value)
                End Set
            End Property

            Private Shared Field02LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Label", "Field 02 Label"))
            Public Property Field02Label() As String
                Get
                    Return ReadProperty(Of String)(Field02LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field02LabelProperty, value)
                End Set
            End Property

            Private Shared Field03LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Label", "Field 03 Label"))
            Public Property Field03Label() As String
                Get
                    Return ReadProperty(Of String)(Field03LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field03LabelProperty, value)
                End Set
            End Property

            Private Shared Field04LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Label", "Field 04 Label"))
            Public Property Field04Label() As String
                Get
                    Return ReadProperty(Of String)(Field04LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field04LabelProperty, value)
                End Set
            End Property

            Private Shared Field05LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Label", "Field 05 Label"))
            Public Property Field05Label() As String
                Get
                    Return ReadProperty(Of String)(Field05LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field05LabelProperty, value)
                End Set
            End Property

            Private Shared Field06LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Label", "Field 06 Label"))
            Public Property Field06Label() As String
                Get
                    Return ReadProperty(Of String)(Field06LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field06LabelProperty, value)
                End Set
            End Property

            Private Shared Field07LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Label", "Field 07 Label"))
            Public Property Field07Label() As String
                Get
                    Return ReadProperty(Of String)(Field07LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field07LabelProperty, value)
                End Set
            End Property

            Private Shared Field08LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Label", "Field 08 Label"))
            Public Property Field08Label() As String
                Get
                    Return ReadProperty(Of String)(Field08LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field08LabelProperty, value)
                End Set
            End Property

            Private Shared Field09LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Label", "Field 09 Label"))
            Public Property Field09Label() As String
                Get
                    Return ReadProperty(Of String)(Field09LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field09LabelProperty, value)
                End Set
            End Property

            Private Shared Field10LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Label", "Field 10 Label"))
            Public Property Field10Label() As String
                Get
                    Return ReadProperty(Of String)(Field10LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field10LabelProperty, value)
                End Set
            End Property

            Private Shared Field11LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Label", "Field 11 Label"))
            Public Property Field11Label() As String
                Get
                    Return ReadProperty(Of String)(Field11LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field11LabelProperty, value)
                End Set
            End Property

            Private Shared Field12LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Label", "Field 12 Label"))
            Public Property Field12Label() As String
                Get
                    Return ReadProperty(Of String)(Field12LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field12LabelProperty, value)
                End Set
            End Property

            Private Shared Field13LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Label", "Field 13 Label"))
            Public Property Field13Label() As String
                Get
                    Return ReadProperty(Of String)(Field13LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field13LabelProperty, value)
                End Set
            End Property

            Private Shared Field14LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Label", "Field 14 Label"))
            Public Property Field14Label() As String
                Get
                    Return ReadProperty(Of String)(Field14LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field14LabelProperty, value)
                End Set
            End Property

            Private Shared Field15LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Label", "Field 15 Label"))
            Public Property Field15Label() As String
                Get
                    Return ReadProperty(Of String)(Field15LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field15LabelProperty, value)
                End Set
            End Property

            Private Shared Field16LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Label", "Field 16 Label"))
            Public Property Field16Label() As String
                Get
                    Return ReadProperty(Of String)(Field16LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field16LabelProperty, value)
                End Set
            End Property

            Private Shared Field17LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Label", "Field 17 Label"))
            Public Property Field17Label() As String
                Get
                    Return ReadProperty(Of String)(Field17LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field17LabelProperty, value)
                End Set
            End Property

            Private Shared Field18LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Label", "Field 18 Label"))
            Public Property Field18Label() As String
                Get
                    Return ReadProperty(Of String)(Field18LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field18LabelProperty, value)
                End Set
            End Property

            Private Shared Field19LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Label", "Field 19 Label"))
            Public Property Field19Label() As String
                Get
                    Return ReadProperty(Of String)(Field19LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field19LabelProperty, value)
                End Set
            End Property

            Private Shared Field20LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Label", "Field 20 Label"))
            Public Property Field20Label() As String
                Get
                    Return ReadProperty(Of String)(Field20LabelProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field20LabelProperty, value)
                End Set
            End Property

#End Region

#Region " Form Field Types "

            Private Shared Field01TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Type", "Field 01 Type"))
            Public Property Field01Type() As String
                Get
                    Return ReadProperty(Of String)(Field01TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field01TypeProperty, value)
                End Set
            End Property

            Private Shared Field02TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Type", "Field 02 Type"))
            Public Property Field02Type() As String
                Get
                    Return ReadProperty(Of String)(Field02TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field02TypeProperty, value)
                End Set
            End Property

            Private Shared Field03TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Type", "Field 03 Type"))
            Public Property Field03Type() As String
                Get
                    Return ReadProperty(Of String)(Field03TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field03TypeProperty, value)
                End Set
            End Property

            Private Shared Field04TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Type", "Field 04 Type"))
            Public Property Field04Type() As String
                Get
                    Return ReadProperty(Of String)(Field04TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field04TypeProperty, value)
                End Set
            End Property

            Private Shared Field05TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Type", "Field 05 Type"))
            Public Property Field05Type() As String
                Get
                    Return ReadProperty(Of String)(Field05TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field05TypeProperty, value)
                End Set
            End Property

            Private Shared Field06TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Type", "Field 06 Type"))
            Public Property Field06Type() As String
                Get
                    Return ReadProperty(Of String)(Field06TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field06TypeProperty, value)
                End Set
            End Property

            Private Shared Field07TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Type", "Field 07 Type"))
            Public Property Field07Type() As String
                Get
                    Return ReadProperty(Of String)(Field07TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field07TypeProperty, value)
                End Set
            End Property

            Private Shared Field08TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Type", "Field 08 Type"))
            Public Property Field08Type() As String
                Get
                    Return ReadProperty(Of String)(Field08TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field08TypeProperty, value)
                End Set
            End Property

            Private Shared Field09TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Type", "Field 09 Type"))
            Public Property Field09Type() As String
                Get
                    Return ReadProperty(Of String)(Field09TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field09TypeProperty, value)
                End Set
            End Property

            Private Shared Field10TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Type", "Field 10 Type"))
            Public Property Field10Type() As String
                Get
                    Return ReadProperty(Of String)(Field10TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field10TypeProperty, value)
                End Set
            End Property

            Private Shared Field11TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Type", "Field 11 Type"))
            Public Property Field11Type() As String
                Get
                    Return ReadProperty(Of String)(Field11TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field11TypeProperty, value)
                End Set
            End Property

            Private Shared Field12TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Type", "Field 12 Type"))
            Public Property Field12Type() As String
                Get
                    Return ReadProperty(Of String)(Field12TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field12TypeProperty, value)
                End Set
            End Property

            Private Shared Field13TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Type", "Field 13 Type"))
            Public Property Field13Type() As String
                Get
                    Return ReadProperty(Of String)(Field13TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field13TypeProperty, value)
                End Set
            End Property

            Private Shared Field14TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Type", "Field 14 Type"))
            Public Property Field14Type() As String
                Get
                    Return ReadProperty(Of String)(Field14TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field14TypeProperty, value)
                End Set
            End Property

            Private Shared Field15TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Type", "Field 15 Type"))
            Public Property Field15Type() As String
                Get
                    Return ReadProperty(Of String)(Field15TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field15TypeProperty, value)
                End Set
            End Property

            Private Shared Field16TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Type", "Field 16 Type"))
            Public Property Field16Type() As String
                Get
                    Return ReadProperty(Of String)(Field16TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field16TypeProperty, value)
                End Set
            End Property

            Private Shared Field17TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Type", "Field 17 Type"))
            Public Property Field17Type() As String
                Get
                    Return ReadProperty(Of String)(Field17TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field17TypeProperty, value)
                End Set
            End Property

            Private Shared Field18TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Type", "Field 18 Type"))
            Public Property Field18Type() As String
                Get
                    Return ReadProperty(Of String)(Field18TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field18TypeProperty, value)
                End Set
            End Property

            Private Shared Field19TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Type", "Field 19 Type"))
            Public Property Field19Type() As String
                Get
                    Return ReadProperty(Of String)(Field19TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field19TypeProperty, value)
                End Set
            End Property

            Private Shared Field20TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Type", "Field 20 Type"))
            Public Property Field20Type() As String
                Get
                    Return ReadProperty(Of String)(Field20TypeProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field20TypeProperty, value)
                End Set
            End Property

#End Region

#Region " Form Field Help Text "

            Private Shared Field01HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01HelpText", "Field 01 Help Text"))
            Public Property Field01HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field01HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field01HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field02HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02HelpText", "Field 02 Help Text"))
            Public Property Field02HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field02HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field02HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field03HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03HelpText", "Field 03 Help Text"))
            Public Property Field03HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field03HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field03HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field04HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04HelpText", "Field 04 Help Text"))
            Public Property Field04HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field04HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field04HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field05HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05HelpText", "Field 05 Help Text"))
            Public Property Field05HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field05HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field05HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field06HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06HelpText", "Field 06 Help Text"))
            Public Property Field06HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field06HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field06HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field07HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07HelpText", "Field 07 Help Text"))
            Public Property Field07HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field07HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field07HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field08HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08HelpText", "Field 08 Help Text"))
            Public Property Field08HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field08HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field08HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field09HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09HelpText", "Field 09 Help Text"))
            Public Property Field09HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field09HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field09HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field10HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10HelpText", "Field 10 Help Text"))
            Public Property Field10HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field10HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field10HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field11HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11HelpText", "Field 11 Help Text"))
            Public Property Field11HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field11HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field11HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field12HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12HelpText", "Field 12 Help Text"))
            Public Property Field12HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field12HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field12HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field13HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13HelpText", "Field 13 Help Text"))
            Public Property Field13HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field13HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field13HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field14HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14HelpText", "Field 14 Help Text"))
            Public Property Field14HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field14HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field14HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field15HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15HelpText", "Field 15 Help Text"))
            Public Property Field15HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field15HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field15HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field16HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16HelpText", "Field 16 Help Text"))
            Public Property Field16HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field16HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field16HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field17HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17HelpText", "Field 17 Help Text"))
            Public Property Field17HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field17HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field17HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field18HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18HelpText", "Field 18 Help Text"))
            Public Property Field18HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field18HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field18HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field19HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19HelpText", "Field 19 Help Text"))
            Public Property Field19HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field19HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field19HelpTextProperty, value)
                End Set
            End Property

            Private Shared Field20HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20HelpText", "Field 20 Help Text"))
            Public Property Field20HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field20HelpTextProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field20HelpTextProperty, value)
                End Set
            End Property

#End Region

#Region " Form Field Options "

            Private Shared Field01OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Options", "Field 01 Options"))
            Public Property Field01Options() As String
                Get
                    Return ReadProperty(Of String)(Field01OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field01OptionsProperty, value)
                End Set
            End Property

            Private Shared Field02OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Options", "Field 02 Options"))
            Public Property Field02Options() As String
                Get
                    Return ReadProperty(Of String)(Field02OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field02OptionsProperty, value)
                End Set
            End Property

            Private Shared Field03OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Options", "Field 03 Options"))
            Public Property Field03Options() As String
                Get
                    Return ReadProperty(Of String)(Field03OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field03OptionsProperty, value)
                End Set
            End Property

            Private Shared Field04OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Options", "Field 04 Options"))
            Public Property Field04Options() As String
                Get
                    Return ReadProperty(Of String)(Field04OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field04OptionsProperty, value)
                End Set
            End Property

            Private Shared Field05OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Options", "Field 05 Options"))
            Public Property Field05Options() As String
                Get
                    Return ReadProperty(Of String)(Field05OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field05OptionsProperty, value)
                End Set
            End Property

            Private Shared Field06OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Options", "Field 06 Options"))
            Public Property Field06Options() As String
                Get
                    Return ReadProperty(Of String)(Field06OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field06OptionsProperty, value)
                End Set
            End Property

            Private Shared Field07OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Options", "Field 07 Options"))
            Public Property Field07Options() As String
                Get
                    Return ReadProperty(Of String)(Field07OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field07OptionsProperty, value)
                End Set
            End Property

            Private Shared Field08OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Options", "Field 08 Options"))
            Public Property Field08Options() As String
                Get
                    Return ReadProperty(Of String)(Field08OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field08OptionsProperty, value)
                End Set
            End Property

            Private Shared Field09OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Options", "Field 09 Options"))
            Public Property Field09Options() As String
                Get
                    Return ReadProperty(Of String)(Field09OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field09OptionsProperty, value)
                End Set
            End Property

            Private Shared Field10OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Options", "Field 10 Options"))
            Public Property Field10Options() As String
                Get
                    Return ReadProperty(Of String)(Field10OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field10OptionsProperty, value)
                End Set
            End Property

            Private Shared Field11OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Options", "Field 11 Options"))
            Public Property Field11Options() As String
                Get
                    Return ReadProperty(Of String)(Field11OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field11OptionsProperty, value)
                End Set
            End Property

            Private Shared Field12OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Options", "Field 12 Options"))
            Public Property Field12Options() As String
                Get
                    Return ReadProperty(Of String)(Field12OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field12OptionsProperty, value)
                End Set
            End Property

            Private Shared Field13OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Options", "Field 13 Options"))
            Public Property Field13Options() As String
                Get
                    Return ReadProperty(Of String)(Field13OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field13OptionsProperty, value)
                End Set
            End Property

            Private Shared Field14OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Options", "Field 14 Options"))
            Public Property Field14Options() As String
                Get
                    Return ReadProperty(Of String)(Field14OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field14OptionsProperty, value)
                End Set
            End Property

            Private Shared Field15OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Options", "Field 15 Options"))
            Public Property Field15Options() As String
                Get
                    Return ReadProperty(Of String)(Field15OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field15OptionsProperty, value)
                End Set
            End Property

            Private Shared Field16OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Options", "Field 16 Options"))
            Public Property Field16Options() As String
                Get
                    Return ReadProperty(Of String)(Field16OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field16OptionsProperty, value)
                End Set
            End Property

            Private Shared Field17OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Options", "Field 17 Options"))
            Public Property Field17Options() As String
                Get
                    Return ReadProperty(Of String)(Field17OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field17OptionsProperty, value)
                End Set
            End Property

            Private Shared Field18OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Options", "Field 18 Options"))
            Public Property Field18Options() As String
                Get
                    Return ReadProperty(Of String)(Field18OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field18OptionsProperty, value)
                End Set
            End Property

            Private Shared Field19OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Options", "Field 19 Options"))
            Public Property Field19Options() As String
                Get
                    Return ReadProperty(Of String)(Field19OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field19OptionsProperty, value)
                End Set
            End Property

            Private Shared Field20OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Options", "Field 20 Options"))
            Public Property Field20Options() As String
                Get
                    Return ReadProperty(Of String)(Field20OptionsProperty)
                End Get
                Set(ByVal value As String)
                    SetProperty(Of String)(Field20OptionsProperty, value)
                End Set
            End Property

#End Region

            Private Shared LookupListProperty As PropertyInfo(Of NursingNoteFormLookupList) = RegisterProperty(New PropertyInfo(Of NursingNoteFormLookupList)("LookupList"))
            Public Property LookupList() As NursingNoteFormLookupList
                Get
                    Return ReadProperty(Of NursingNoteFormLookupList)(LookupListProperty)
                End Get
                Set(ByVal value As NursingNoteFormLookupList)
                    SetProperty(Of NursingNoteFormLookupList)(LookupListProperty, value)
                End Set
            End Property

            Protected Overrides Function GetIdValue() As Object
                Return ReadProperty(Of Integer)(FormIDProperty)
            End Function

#End Region

#Region " Validation Rules "

            Protected Overrides Sub AddBusinessRules()
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, FormNameProperty)
                ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                    New Validation.CommonRules.MaxLengthRuleArgs(FormNameProperty, 200))

                ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                    New Validation.CommonRules.IntegerMinValueRuleArgs(DisplayOrderProperty, 0))

                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field01Type", "Field 01 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field02Type", "Field 02 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field03Type", "Field 03 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field04Type", "Field 04 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field05Type", "Field 05 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field06Type", "Field 06 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field07Type", "Field 07 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field08Type", "Field 08 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field09Type", "Field 09 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field10Type", "Field 10 Type"))

                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field11Type", "Field 11 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field12Type", "Field 12 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field13Type", "Field 13 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field14Type", "Field 14 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field15Type", "Field 15 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field16Type", "Field 16 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field17Type", "Field 17 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field18Type", "Field 18 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field19Type", "Field 19 Type"))
                'ValidationRules.AddRule(Of NursingNoteForm)(AddressOf ValidFieldType(Of NursingNoteForm), _
                '    New Csla.Validation.RuleArgs("Field20Type", "Field 20 Type"))
            End Sub

            'Private Shared Function ValidFieldType(Of T As NursingNoteForm)(ByVal target As T, _
            '    ByVal e As Validation.RuleArgs) As Boolean
            '    ' Validate that each field is linked to a valid type

            '    ' Use Reflection to get the value of the property that's referenced
            '    Dim pi As PropertyInfo = target.GetType().GetProperty(e.PropertyName)
            '    Dim thisType As String = pi.GetValue(target, Nothing)

            '    ' Is the type a valid type?
            '    Dim types As NursingNoteFormFieldTypes = NursingNoteFormFieldTypes.GetNursingNoteFormFieldTypes()

            '    For Each type As NursingNoteFormFieldTypes.NameValuePair In types
            '        If type.Key = thisType Then
            '            Return True
            '        End If
            '    Next

            '    e.Description = e.PropertyFriendlyName & " is not valid."
            '    Return False
            'End Function

#End Region

#Region " Authorization Rules "

            Protected Overrides Sub AddAuthorizationRules()
                ' Add AuthorizationRules here
            End Sub

            Public Shared Function CanAddObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

            Public Shared Function CanDeleteObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

            Public Shared Function CanEditObject() As Boolean
                Return True 'Csla.ApplicationContext.User.IsInRole("Administrator")
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function NewNursingNoteForm(ByVal ParentEvalID As Integer) As NursingNoteForm
                If Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Nursing Note Form")
                End If
                Return DataPortal.Create(Of NursingNoteForm)(New CreateCriteria(ParentEvalID))
            End Function

            Public Shared Function GetNursingNoteForm(ByVal FormID As Integer) As NursingNoteForm
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view a Nursing Note Form")
                End If
                Return DataPortal.Fetch(Of NursingNoteForm)(New Criteria(FormID))
            End Function

            Public Shared Sub DeleteNursingNoteForm(ByVal FormID As Integer)
                If Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Nursing Note Form")
                End If
                DataPortal.Delete(New Criteria(FormID))
            End Sub

            Public Overloads Function Save() As NursingNoteForm
                If IsDeleted AndAlso Not CanDeleteObject() Then
                    Throw New System.Security.SecurityException("User not authorized to remove a Nursing Note Form")
                ElseIf IsNew AndAlso Not CanAddObject() Then
                    Throw New System.Security.SecurityException("User not authorized to add a Nursing Note Form")
                ElseIf Not CanEditObject() Then
                    Throw New System.Security.SecurityException("User not authorized to update a Nursing Note Form")
                End If

                Return MyBase.Save()
            End Function

            Private Sub New()
                ' Require use of Factory methods
            End Sub

            Public Shared Sub CopyForm(ByRef ExistingForm As NursingNoteForm, ByRef NewForm As NursingNoteForm)
                ' Copy all values from the Existing Form to the New Form.
                ' Only copy the fields that can be modified (don't modify the
                ' values that will be built during the insert).
                NewForm.FormName = ExistingForm.FormName

                ' TODO - Use the next display order... don't copy that
                NewForm.DisplayOrder = ExistingForm.DisplayOrder

                ' 03/08/2010 Matt - Always make a form that is pasted inactive
                ' so that it can be changed before it is made available.
                NewForm.Inactive = True 'ExistingForm.Inactive

                NewForm.Required = ExistingForm.Required
                NewForm.Education = ExistingForm.Education
                NewForm.PCarOnly = ExistingForm.PCarOnly
                NewForm.NursingNoteOnly = ExistingForm.NursingNoteOnly
                'NewForm.IncludeGraphics = ExistingForm.IncludeGraphics
                NewForm.RedirectTo = ExistingForm.RedirectTo
                NewForm.Calculated = ExistingForm.Calculated
                NewForm.Calculation = ExistingForm.Calculation
                NewForm.NoResult = ExistingForm.NoResult

                NewForm.Field01Label = ExistingForm.Field01Label
                NewForm.Field01Type = ExistingForm.Field01Type
                NewForm.Field01HelpText = ExistingForm.Field01HelpText
                NewForm.Field01Options = ExistingForm.Field01Options

                NewForm.Field02Label = ExistingForm.Field02Label
                NewForm.Field02Type = ExistingForm.Field02Type
                NewForm.Field02HelpText = ExistingForm.Field02HelpText
                NewForm.Field02Options = ExistingForm.Field02Options

                NewForm.Field03Label = ExistingForm.Field03Label
                NewForm.Field03Type = ExistingForm.Field03Type
                NewForm.Field03HelpText = ExistingForm.Field03HelpText
                NewForm.Field03Options = ExistingForm.Field03Options

                NewForm.Field04Label = ExistingForm.Field04Label
                NewForm.Field04Type = ExistingForm.Field04Type
                NewForm.Field04HelpText = ExistingForm.Field04HelpText
                NewForm.Field04Options = ExistingForm.Field04Options

                NewForm.Field05Label = ExistingForm.Field05Label
                NewForm.Field05Type = ExistingForm.Field05Type
                NewForm.Field05HelpText = ExistingForm.Field05HelpText
                NewForm.Field05Options = ExistingForm.Field05Options

                NewForm.Field06Label = ExistingForm.Field06Label
                NewForm.Field06Type = ExistingForm.Field06Type
                NewForm.Field06HelpText = ExistingForm.Field06HelpText
                NewForm.Field06Options = ExistingForm.Field06Options

                NewForm.Field07Label = ExistingForm.Field07Label
                NewForm.Field07Type = ExistingForm.Field07Type
                NewForm.Field07HelpText = ExistingForm.Field07HelpText
                NewForm.Field07Options = ExistingForm.Field07Options

                NewForm.Field08Label = ExistingForm.Field08Label
                NewForm.Field08Type = ExistingForm.Field08Type
                NewForm.Field08HelpText = ExistingForm.Field08HelpText
                NewForm.Field08Options = ExistingForm.Field08Options

                NewForm.Field09Label = ExistingForm.Field09Label
                NewForm.Field09Type = ExistingForm.Field09Type
                NewForm.Field09HelpText = ExistingForm.Field09HelpText
                NewForm.Field09Options = ExistingForm.Field09Options

                NewForm.Field10Label = ExistingForm.Field10Label
                NewForm.Field10Type = ExistingForm.Field10Type
                NewForm.Field10HelpText = ExistingForm.Field10HelpText
                NewForm.Field10Options = ExistingForm.Field10Options

                NewForm.Field11Label = ExistingForm.Field11Label
                NewForm.Field11Type = ExistingForm.Field11Type
                NewForm.Field11HelpText = ExistingForm.Field11HelpText
                NewForm.Field11Options = ExistingForm.Field11Options

                NewForm.Field12Label = ExistingForm.Field12Label
                NewForm.Field12Type = ExistingForm.Field12Type
                NewForm.Field12HelpText = ExistingForm.Field12HelpText
                NewForm.Field12Options = ExistingForm.Field12Options

                NewForm.Field13Label = ExistingForm.Field13Label
                NewForm.Field13Type = ExistingForm.Field13Type
                NewForm.Field13HelpText = ExistingForm.Field13HelpText
                NewForm.Field13Options = ExistingForm.Field13Options

                NewForm.Field14Label = ExistingForm.Field14Label
                NewForm.Field14Type = ExistingForm.Field14Type
                NewForm.Field14HelpText = ExistingForm.Field14HelpText
                NewForm.Field14Options = ExistingForm.Field14Options

                NewForm.Field15Label = ExistingForm.Field15Label
                NewForm.Field15Type = ExistingForm.Field15Type
                NewForm.Field15HelpText = ExistingForm.Field15HelpText
                NewForm.Field15Options = ExistingForm.Field15Options

                NewForm.Field16Label = ExistingForm.Field16Label
                NewForm.Field16Type = ExistingForm.Field16Type
                NewForm.Field16HelpText = ExistingForm.Field16HelpText
                NewForm.Field16Options = ExistingForm.Field16Options

                NewForm.Field17Label = ExistingForm.Field17Label
                NewForm.Field17Type = ExistingForm.Field17Type
                NewForm.Field17HelpText = ExistingForm.Field17HelpText
                NewForm.Field17Options = ExistingForm.Field17Options

                NewForm.Field18Label = ExistingForm.Field18Label
                NewForm.Field18Type = ExistingForm.Field18Type
                NewForm.Field18HelpText = ExistingForm.Field18HelpText
                NewForm.Field18Options = ExistingForm.Field18Options

                NewForm.Field19Label = ExistingForm.Field19Label
                NewForm.Field19Type = ExistingForm.Field19Type
                NewForm.Field19HelpText = ExistingForm.Field19HelpText
                NewForm.Field19Options = ExistingForm.Field19Options

                NewForm.Field20Label = ExistingForm.Field20Label
                NewForm.Field20Type = ExistingForm.Field20Type
                NewForm.Field20HelpText = ExistingForm.Field20HelpText
                NewForm.Field20Options = ExistingForm.Field20Options

                For Each lookupValue As NursingNoteFormLookup In ExistingForm.LookupList
                    Dim copyLookup As NursingNoteFormLookup = NursingNoteFormLookup.NewNursingNoteFormLookup()
                    copyLookup.Description = lookupValue.Description
                    copyLookup.FormField = lookupValue.FormField
                    NewForm.LookupList.Add(copyLookup)
                Next
            End Sub

            Public Sub InsertField(ByVal InsertAt As Integer)
                For i As Integer = 20 To InsertAt + 1 Step -1
                    Select Case i
                        Case 20
                            Field20Label = Field19Label
                            Field20Type = Field19Type
                            Field20Options = Field19Options
                            Field20HelpText = Field19HelpText
                        Case 19
                            Field19Label = Field18Label
                            Field19Type = Field18Type
                            Field19Options = Field18Options
                            Field19HelpText = Field18HelpText
                        Case 18
                            Field18Label = Field17Label
                            Field18Type = Field17Type
                            Field18Options = Field17Options
                            Field18HelpText = Field17HelpText
                        Case 17
                            Field17Label = Field16Label
                            Field17Type = Field16Type
                            Field17Options = Field16Options
                            Field17HelpText = Field16HelpText
                        Case 16
                            Field16Label = Field15Label
                            Field16Type = Field15Type
                            Field16Options = Field15Options
                            Field16HelpText = Field15HelpText
                        Case 15
                            Field15Label = Field14Label
                            Field15Type = Field14Type
                            Field15Options = Field14Options
                            Field15HelpText = Field14HelpText
                        Case 14
                            Field14Label = Field13Label
                            Field14Type = Field13Type
                            Field14Options = Field13Options
                            Field14HelpText = Field13HelpText
                        Case 13
                            Field13Label = Field12Label
                            Field13Type = Field12Type
                            Field13Options = Field12Options
                            Field13HelpText = Field12HelpText
                        Case 12
                            Field12Label = Field11Label
                            Field12Type = Field11Type
                            Field12Options = Field11Options
                            Field12HelpText = Field11HelpText
                        Case 11
                            Field11Label = Field10Label
                            Field11Type = Field10Type
                            Field11Options = Field10Options
                            Field11HelpText = Field10HelpText
                        Case 10
                            Field10Label = Field09Label
                            Field10Type = Field09Type
                            Field10Options = Field09Options
                            Field10HelpText = Field09HelpText
                        Case 9
                            Field09Label = Field08Label
                            Field09Type = Field08Type
                            Field09Options = Field08Options
                            Field09HelpText = Field08HelpText
                        Case 8
                            Field08Label = Field07Label
                            Field08Type = Field07Type
                            Field08Options = Field07Options
                            Field08HelpText = Field07HelpText
                        Case 7
                            Field07Label = Field06Label
                            Field07Type = Field06Type
                            Field07Options = Field06Options
                            Field07HelpText = Field06HelpText
                        Case 6
                            Field06Label = Field05Label
                            Field06Type = Field05Type
                            Field06Options = Field05Options
                            Field06HelpText = Field05HelpText
                        Case 5
                            Field05Label = Field04Label
                            Field05Type = Field04Type
                            Field05Options = Field04Options
                            Field05HelpText = Field04HelpText
                        Case 4
                            Field04Label = Field03Label
                            Field04Type = Field03Type
                            Field04Options = Field03Options
                            Field04HelpText = Field03HelpText
                        Case 3
                            Field03Label = Field02Label
                            Field03Type = Field02Type
                            Field03Options = Field02Options
                            Field03HelpText = Field02HelpText
                        Case 2
                            Field02Label = Field01Label
                            Field02Type = Field01Type
                            Field02Options = Field01Options
                            Field02HelpText = Field01HelpText
                    End Select
                Next

                ' Update the lookup values
                For Each lookupValue As NursingNoteFormLookup In LookupList
                    If lookupValue.FormField >= InsertAt Then
                        lookupValue.FormField += 1
                    End If
                Next

                ClearField(InsertAt)
            End Sub

            Public Sub ClearField(ByVal FieldIndex As Integer)
                Select Case FieldIndex
                    Case 1
                        Field01Label = ""
                        Field01Type = ""
                        Field01Options = ""
                        Field01HelpText = ""
                    Case 2
                        Field02Label = ""
                        Field02Type = ""
                        Field02Options = ""
                        Field02HelpText = ""
                    Case 3
                        Field03Label = ""
                        Field03Type = ""
                        Field03Options = ""
                        Field03HelpText = ""
                    Case 4
                        Field04Label = ""
                        Field04Type = ""
                        Field04Options = ""
                        Field04HelpText = ""
                    Case 5
                        Field05Label = ""
                        Field05Type = ""
                        Field05Options = ""
                        Field05HelpText = ""
                    Case 6
                        Field06Label = ""
                        Field06Type = ""
                        Field06Options = ""
                        Field06HelpText = ""
                    Case 7
                        Field07Label = ""
                        Field07Type = ""
                        Field07Options = ""
                        Field07HelpText = ""
                    Case 8
                        Field08Label = ""
                        Field08Type = ""
                        Field08Options = ""
                        Field08HelpText = ""
                    Case 9
                        Field09Label = ""
                        Field09Type = ""
                        Field09Options = ""
                        Field09HelpText = ""
                    Case 10
                        Field10Label = ""
                        Field10Type = ""
                        Field10Options = ""
                        Field10HelpText = ""
                    Case 11
                        Field11Label = ""
                        Field11Type = ""
                        Field11Options = ""
                        Field11HelpText = ""
                    Case 12
                        Field12Label = ""
                        Field12Type = ""
                        Field12Options = ""
                        Field12HelpText = ""
                    Case 13
                        Field13Label = ""
                        Field13Type = ""
                        Field13Options = ""
                        Field13HelpText = ""
                    Case 14
                        Field14Label = ""
                        Field14Type = ""
                        Field14Options = ""
                        Field14HelpText = ""
                    Case 15
                        Field15Label = ""
                        Field15Type = ""
                        Field15Options = ""
                        Field15HelpText = ""
                    Case 16
                        Field16Label = ""
                        Field16Type = ""
                        Field16Options = ""
                        Field16HelpText = ""
                    Case 17
                        Field17Label = ""
                        Field17Type = ""
                        Field17Options = ""
                        Field17HelpText = ""
                    Case 18
                        Field18Label = ""
                        Field18Type = ""
                        Field18Options = ""
                        Field18HelpText = ""
                    Case 19
                        Field19Label = ""
                        Field19Type = ""
                        Field19Options = ""
                        Field19HelpText = ""
                    Case 20
                        Field20Label = ""
                        Field20Type = ""
                        Field20Options = ""
                        Field20HelpText = ""
                End Select

                ' Remove any lookup values linked with this field
                Dim removeList As NursingNoteFormLookupList = NursingNoteFormLookupList.NewNursingNoteFormLookupList
                For Each lookupValue As NursingNoteFormLookup In LookupList
                    If lookupValue.FormField = FieldIndex Then
                        removeList.Add(lookupValue)
                    End If
                Next
                For Each lookupValue As NursingNoteFormLookup In removeList
                    LookupList.Remove(lookupValue)
                Next
            End Sub

            Public Sub RemoveField(ByVal Index As Integer)
                ' Clear the field being removed first to remove
                ' any lookup values linked to it
                ClearField(Index)

                For i As Integer = Index To 20
                    Select Case i
                        Case 1
                            Field01Label = Field02Label
                            Field01Type = Field02Type
                            Field01Options = Field02Options
                            Field01HelpText = Field02HelpText
                        Case 2
                            Field02Label = Field03Label
                            Field02Type = Field03Type
                            Field02Options = Field03Options
                            Field02HelpText = Field03HelpText
                        Case 3
                            Field03Label = Field04Label
                            Field03Type = Field04Type
                            Field03Options = Field04Options
                            Field03HelpText = Field04HelpText
                        Case 4
                            Field04Label = Field05Label
                            Field04Type = Field05Type
                            Field04Options = Field05Options
                            Field04HelpText = Field05HelpText
                        Case 5
                            Field05Label = Field06Label
                            Field05Type = Field06Type
                            Field05Options = Field06Options
                            Field05HelpText = Field06HelpText
                        Case 6
                            Field06Label = Field07Label
                            Field06Type = Field07Type
                            Field06Options = Field07Options
                            Field06HelpText = Field07HelpText
                        Case 7
                            Field07Label = Field08Label
                            Field07Type = Field08Type
                            Field07Options = Field08Options
                            Field07HelpText = Field08HelpText
                        Case 8
                            Field08Label = Field09Label
                            Field08Type = Field09Type
                            Field08Options = Field09Options
                            Field08HelpText = Field09HelpText
                        Case 9
                            Field09Label = Field10Label
                            Field09Type = Field10Type
                            Field09Options = Field10Options
                            Field09HelpText = Field10HelpText
                        Case 10
                            Field10Label = Field11Label
                            Field10Type = Field11Type
                            Field10Options = Field11Options
                            Field10HelpText = Field11HelpText
                        Case 11
                            Field11Label = Field12Label
                            Field11Type = Field12Type
                            Field11Options = Field12Options
                            Field11HelpText = Field12HelpText
                        Case 12
                            Field12Label = Field13Label
                            Field12Type = Field13Type
                            Field12Options = Field13Options
                            Field12HelpText = Field13HelpText
                        Case 13
                            Field13Label = Field14Label
                            Field13Type = Field14Type
                            Field13Options = Field14Options
                            Field13HelpText = Field14HelpText
                        Case 14
                            Field14Label = Field15Label
                            Field14Type = Field15Type
                            Field14Options = Field15Options
                            Field14HelpText = Field15HelpText
                        Case 15
                            Field15Label = Field16Label
                            Field15Type = Field16Type
                            Field15Options = Field16Options
                            Field15HelpText = Field16HelpText
                        Case 16
                            Field16Label = Field17Label
                            Field16Type = Field17Type
                            Field16Options = Field17Options
                            Field16HelpText = Field17HelpText
                        Case 17
                            Field17Label = Field18Label
                            Field17Type = Field18Type
                            Field17Options = Field18Options
                            Field17HelpText = Field18HelpText
                        Case 18
                            Field18Label = Field19Label
                            Field18Type = Field19Type
                            Field18Options = Field19Options
                            Field18HelpText = Field19HelpText
                        Case 19
                            Field19Label = Field20Label
                            Field19Type = Field20Type
                            Field19Options = Field20Options
                            Field19HelpText = Field20HelpText
                        Case 20
                            Field20Label = ""
                            Field20Type = ""
                            Field20Options = ""
                            Field20HelpText = ""
                    End Select
                Next

                ' Update the lookup values
                For Each lookupValue As NursingNoteFormLookup In LookupList
                    If lookupValue.FormField > Index Then
                        lookupValue.FormField -= 1
                    End If
                Next
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class CreateCriteria
                Private _ParentID As Integer

                Public ReadOnly Property ParentID() As Integer
                    Get
                        Return Me._ParentID
                    End Get
                End Property

                Public Sub New(ByVal ParentID As Integer)
                    Me._ParentID = ParentID
                End Sub
            End Class

            <Serializable()> _
            Private Class Criteria
                Private _FormID As Integer

                Public ReadOnly Property FormID() As Integer
                    Get
                        Return Me._FormID
                    End Get
                End Property

                Public Sub New(ByVal FormID As Integer)
                    Me._FormID = FormID
                End Sub
            End Class

            Private Overloads Sub DataPortal_Create(ByVal criteria As CreateCriteria)
                ' Set the parent ID
                SetProperty(Of Integer)(ParentIDProperty, criteria.ParentID)

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT nsNtLevel + 1 as ThisFolderLevel, Discipline, NName "
                        sql &= "FROM [100CrNt] "
                        sql &= "WHERE nsNtID = " & criteria.ParentID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(LevelProperty, dr.GetInt32("ThisFolderLevel"))
                                SetProperty(Of Integer)(DisciplineProperty, dr.GetInt32("Discipline"))
                                SetProperty(Of String)(ParentNameProperty, dr.GetString("NName"))
                            End If
                        End Using

                        ' Get the next displayOrder
                        sql = "SELECT ISNULL(MAX(DOrder), 0) + 1 as NextDOrder "
                        sql &= "FROM [100CrNt] "
                        sql &= "WHERE ParentID = " & ReadProperty(Of Integer)(ParentIDProperty) & " "
                        sql &= "AND Discipline = " & ReadProperty(Of Integer)(DisciplineProperty) & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                SetProperty(Of Integer)(DisplayOrderProperty, dr.GetInt32("NextDOrder"))
                            End If
                        End Using
                    End Using
                End Using

                LoadProperty(Of NursingNoteFormLookupList)(LookupListProperty, NursingNoteFormLookupList.NewNursingNoteFormLookupList())
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT n.[ID], n.[nsNtID], n.[parentID], p.NName AS ParentName, "
                        sql &= "n.[nsNtLevel], n.[NName], n.[discipline], n.[dOrder], n.[nInactive], "
                        sql &= "n.[Required], el.[education], n.[PCarOnly], n.[NNonly], "
                        sql &= "el.[redirectForm], el.[calculated], el.[calculation], "
                        sql &= "el.[NoResult], el.[locked], el.[LockNotes], "
                        sql &= "el.Label01, el.type01, el.label01Desc, el.Field01Options, "
                        sql &= "el.Label02, el.type02, el.label02Desc, el.Field02Options, "
                        sql &= "el.Label03, el.type03, el.label03Desc, el.Field03Options, "
                        sql &= "el.Label04, el.type04, el.label04Desc, el.Field04Options, "
                        sql &= "el.Label05, el.type05, el.label05Desc, el.Field05Options, "
                        sql &= "el.Label06, el.type06, el.label06Desc, el.Field06Options, "
                        sql &= "el.Label07, el.type07, el.label07Desc, el.Field07Options, "
                        sql &= "el.Label08, el.type08, el.label08Desc, el.Field08Options, "
                        sql &= "el.Label09, el.type09, el.label09Desc, el.Field09Options, "
                        sql &= "el.Label10, el.type10, el.label10Desc, el.Field10Options, "
                        sql &= "el.Label11, el.type11, el.label11Desc, el.Field11Options, "
                        sql &= "el.Label12, el.type12, el.label12Desc, el.Field12Options, "
                        sql &= "el.Label13, el.type13, el.label13Desc, el.Field13Options, "
                        sql &= "el.Label14, el.type14, el.label14Desc, el.Field14Options, "
                        sql &= "el.Label15, el.type15, el.label15Desc, el.Field15Options, "
                        sql &= "el.Label16, el.type16, el.label16Desc, el.Field16Options, "
                        sql &= "el.Label17, el.type17, el.label17Desc, el.Field17Options, "
                        sql &= "el.Label18, el.type18, el.label18Desc, el.Field18Options, "
                        sql &= "el.Label19, el.type19, el.label19Desc, el.Field19Options, "
                        sql &= "el.Label20, el.type20, el.label20Desc, el.Field20Options "
                        sql &= "FROM [100CrNt] n "
                        sql &= "INNER JOIN [100CrNtEl] el on n.nsNtID = el.nsNtID "
                        sql &= "LEFT JOIN [100CrNt] p ON n.ParentID = p.nsNtID "
                        sql &= "WHERE n.nsNtID = " & Criteria.FormID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(FormIDProperty, .GetInt32("nsNtID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("parentID"))
                                LoadProperty(Of String)(ParentNameProperty, .GetString("ParentName"))
                                LoadProperty(Of Integer)(LevelProperty, .GetInt32("nsNtLevel"))
                                LoadProperty(Of String)(FormNameProperty, .GetString("NName"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("discipline"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("nInactive"))
                                LoadProperty(Of Boolean)(RequiredProperty, .GetBoolean("Required"))
                                LoadProperty(Of Boolean)(EducationProperty, .GetBoolean("education"))
                                LoadProperty(Of Boolean)(PCarOnlyProperty, .GetBoolean("PCarOnly"))
                                LoadProperty(Of Boolean)(NursingNoteOnlyProperty, .GetBoolean("NNonly"))
                                LoadProperty(Of String)(RedirectToProperty, .GetString("redirectForm"))
                                LoadProperty(Of Boolean)(CalculatedProperty, .GetBoolean("calculated"))
                                LoadProperty(Of String)(CalculationProperty, .GetString("calculation"))
                                LoadProperty(Of Boolean)(NoResultProperty, .GetBoolean("NoResult"))
                                LoadProperty(Of Boolean)(LockedProperty, .GetBoolean("locked"))
                                LoadProperty(Of String)(LockNotesProperty, .GetString("LockNotes"))

                                ' Form Field Labels
                                LoadProperty(Of String)(Field01LabelProperty, .GetString("Label01"))
                                LoadProperty(Of String)(Field02LabelProperty, .GetString("Label02"))
                                LoadProperty(Of String)(Field03LabelProperty, .GetString("Label03"))
                                LoadProperty(Of String)(Field04LabelProperty, .GetString("Label04"))
                                LoadProperty(Of String)(Field05LabelProperty, .GetString("Label05"))
                                LoadProperty(Of String)(Field06LabelProperty, .GetString("Label06"))
                                LoadProperty(Of String)(Field07LabelProperty, .GetString("Label07"))
                                LoadProperty(Of String)(Field08LabelProperty, .GetString("Label08"))
                                LoadProperty(Of String)(Field09LabelProperty, .GetString("Label09"))
                                LoadProperty(Of String)(Field10LabelProperty, .GetString("Label10"))
                                LoadProperty(Of String)(Field11LabelProperty, .GetString("Label11"))
                                LoadProperty(Of String)(Field12LabelProperty, .GetString("Label12"))
                                LoadProperty(Of String)(Field13LabelProperty, .GetString("Label13"))
                                LoadProperty(Of String)(Field14LabelProperty, .GetString("Label14"))
                                LoadProperty(Of String)(Field15LabelProperty, .GetString("Label15"))
                                LoadProperty(Of String)(Field16LabelProperty, .GetString("Label16"))
                                LoadProperty(Of String)(Field17LabelProperty, .GetString("Label17"))
                                LoadProperty(Of String)(Field18LabelProperty, .GetString("Label18"))
                                LoadProperty(Of String)(Field19LabelProperty, .GetString("Label19"))
                                LoadProperty(Of String)(Field20LabelProperty, .GetString("Label20"))

                                ' Form Field Types
                                LoadProperty(Of String)(Field01TypeProperty, .GetString("Type01"))
                                LoadProperty(Of String)(Field02TypeProperty, .GetString("Type02"))
                                LoadProperty(Of String)(Field03TypeProperty, .GetString("Type03"))
                                LoadProperty(Of String)(Field04TypeProperty, .GetString("Type04"))
                                LoadProperty(Of String)(Field05TypeProperty, .GetString("Type05"))
                                LoadProperty(Of String)(Field06TypeProperty, .GetString("Type06"))
                                LoadProperty(Of String)(Field07TypeProperty, .GetString("Type07"))
                                LoadProperty(Of String)(Field08TypeProperty, .GetString("Type08"))
                                LoadProperty(Of String)(Field09TypeProperty, .GetString("Type09"))
                                LoadProperty(Of String)(Field10TypeProperty, .GetString("Type10"))
                                LoadProperty(Of String)(Field11TypeProperty, .GetString("Type11"))
                                LoadProperty(Of String)(Field12TypeProperty, .GetString("Type12"))
                                LoadProperty(Of String)(Field13TypeProperty, .GetString("Type13"))
                                LoadProperty(Of String)(Field14TypeProperty, .GetString("Type14"))
                                LoadProperty(Of String)(Field15TypeProperty, .GetString("Type15"))
                                LoadProperty(Of String)(Field16TypeProperty, .GetString("Type16"))
                                LoadProperty(Of String)(Field17TypeProperty, .GetString("Type17"))
                                LoadProperty(Of String)(Field18TypeProperty, .GetString("Type18"))
                                LoadProperty(Of String)(Field19TypeProperty, .GetString("Type19"))
                                LoadProperty(Of String)(Field20TypeProperty, .GetString("Type20"))

                                ' Form Field Help Text
                                LoadProperty(Of String)(Field01HelpTextProperty, .GetString("Label01Desc"))
                                LoadProperty(Of String)(Field02HelpTextProperty, .GetString("Label02Desc"))
                                LoadProperty(Of String)(Field03HelpTextProperty, .GetString("Label03Desc"))
                                LoadProperty(Of String)(Field04HelpTextProperty, .GetString("Label04Desc"))
                                LoadProperty(Of String)(Field05HelpTextProperty, .GetString("Label05Desc"))
                                LoadProperty(Of String)(Field06HelpTextProperty, .GetString("Label06Desc"))
                                LoadProperty(Of String)(Field07HelpTextProperty, .GetString("Label07Desc"))
                                LoadProperty(Of String)(Field08HelpTextProperty, .GetString("Label08Desc"))
                                LoadProperty(Of String)(Field09HelpTextProperty, .GetString("Label09Desc"))
                                LoadProperty(Of String)(Field10HelpTextProperty, .GetString("Label10Desc"))
                                LoadProperty(Of String)(Field11HelpTextProperty, .GetString("Label11Desc"))
                                LoadProperty(Of String)(Field12HelpTextProperty, .GetString("Label12Desc"))
                                LoadProperty(Of String)(Field13HelpTextProperty, .GetString("Label13Desc"))
                                LoadProperty(Of String)(Field14HelpTextProperty, .GetString("Label14Desc"))
                                LoadProperty(Of String)(Field15HelpTextProperty, .GetString("Label15Desc"))
                                LoadProperty(Of String)(Field16HelpTextProperty, .GetString("Label16Desc"))
                                LoadProperty(Of String)(Field17HelpTextProperty, .GetString("Label17Desc"))
                                LoadProperty(Of String)(Field18HelpTextProperty, .GetString("Label18Desc"))
                                LoadProperty(Of String)(Field19HelpTextProperty, .GetString("Label19Desc"))
                                LoadProperty(Of String)(Field20HelpTextProperty, .GetString("Label20Desc"))

                                ' Form Field Options
                                LoadProperty(Of String)(Field01OptionsProperty, .GetString("Field01Options"))
                                LoadProperty(Of String)(Field02OptionsProperty, .GetString("Field02Options"))
                                LoadProperty(Of String)(Field03OptionsProperty, .GetString("Field03Options"))
                                LoadProperty(Of String)(Field04OptionsProperty, .GetString("Field04Options"))
                                LoadProperty(Of String)(Field05OptionsProperty, .GetString("Field05Options"))
                                LoadProperty(Of String)(Field06OptionsProperty, .GetString("Field06Options"))
                                LoadProperty(Of String)(Field07OptionsProperty, .GetString("Field07Options"))
                                LoadProperty(Of String)(Field08OptionsProperty, .GetString("Field08Options"))
                                LoadProperty(Of String)(Field09OptionsProperty, .GetString("Field09Options"))
                                LoadProperty(Of String)(Field10OptionsProperty, .GetString("Field10Options"))
                                LoadProperty(Of String)(Field11OptionsProperty, .GetString("Field11Options"))
                                LoadProperty(Of String)(Field12OptionsProperty, .GetString("Field12Options"))
                                LoadProperty(Of String)(Field13OptionsProperty, .GetString("Field13Options"))
                                LoadProperty(Of String)(Field14OptionsProperty, .GetString("Field14Options"))
                                LoadProperty(Of String)(Field15OptionsProperty, .GetString("Field15Options"))
                                LoadProperty(Of String)(Field16OptionsProperty, .GetString("Field16Options"))
                                LoadProperty(Of String)(Field17OptionsProperty, .GetString("Field17Options"))
                                LoadProperty(Of String)(Field18OptionsProperty, .GetString("Field18Options"))
                                LoadProperty(Of String)(Field19OptionsProperty, .GetString("Field19Options"))
                                LoadProperty(Of String)(Field20OptionsProperty, .GetString("Field20Options"))
                            End With
                        End Using


                        ' Has this form been used?
                        sql = "SELECT count(t.[ID]) AS TimesUsed "
                        sql &= "FROM [005CrNtTrans] t "
                        sql &= "INNER JOIN [001Episode] e ON t.epnum = e.epnum "
                        sql &= "WHERE t.TransID = " & Criteria.FormID & " "
                        sql &= "AND e.TestCase = 0 "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            LoadProperty(Of Boolean)(HasBeenUsedProperty, (dr("TimesUsed") > 0))
                        End Using


                        ' Is this form associated with any Global Lookup items?
                        LoadProperty(Of NursingNoteFormLookupList)(LookupListProperty, NursingNoteFormLookupList.GetNursingNoteFormLookupList(ReadProperty(Of Integer)(FormIDProperty)))
                    End Using
                End Using
            End Sub

            Protected Overrides Sub DataPortal_Insert()
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        ' Insert the new form
                        sql = "SET NOCOUNT ON "
                        sql &= "INSERT INTO [100CrNt] (nsNtID, parentID, nsNtLevel, "
                        sql &= "NName, nFinal, Discipline, dOrder, Required, "
                        sql &= "nInactive, PCarOnly, NNOnly) VALUES ("
                        sql &= ReadProperty(Of Integer)(FormIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(ParentIDProperty) & ", "
                        sql &= ReadProperty(Of Integer)(LevelProperty) & ", "
                        sql &= "'" & Replace(ReadProperty(Of String)(FormNameProperty), "'", "''") & "', "
                        sql &= "1, "
                        sql &= ReadProperty(Of Integer)(DisciplineProperty) & ", "
                        sql &= ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                        If ReadProperty(Of Boolean)(RequiredProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        If ReadProperty(Of Boolean)(InactiveProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        If ReadProperty(Of Boolean)(PCarOnlyProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        If ReadProperty(Of Boolean)(NursingNoteOnlyProperty) Then
                            sql &= "1) "
                        Else
                            sql &= "0) "
                        End If
                        sql &= "SELECT SCOPE_IDENTITY() AS NewID "
                        sql &= "SET NOCOUNT OFF "

                        cmd.CommandText = sql

                        ' Save the new ID that was added
                        LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                        LoadProperty(Of Integer)(FormIDProperty, ReadProperty(Of Integer)(IDProperty))

                        ' Update the NsNtID, which is always the same as the ID
                        sql = "UPDATE [100CrNt] SET "
                        sql &= "NsNtID = @ID "
                        sql &= "WHERE [ID] = @ID "

                        cmd.CommandText = sql
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.ExecuteNonQuery()


                        ' Insert the form fields
                        sql = "INSERT INTO [100CrNtEl] (nsNtID, education, redirectForm, "
                        sql &= "calculated, calculation, NoResult, locked, LockNotes, "
                        sql &= "Label01, type01, label01Desc, Field01Options, "
                        sql &= "Label02, type02, label02Desc, Field02Options, "
                        sql &= "Label03, type03, label03Desc, Field03Options, "
                        sql &= "Label04, type04, label04Desc, Field04Options, "
                        sql &= "Label05, type05, label05Desc, Field05Options, "
                        sql &= "Label06, type06, label06Desc, Field06Options, "
                        sql &= "Label07, type07, label07Desc, Field07Options, "
                        sql &= "Label08, type08, label08Desc, Field08Options, "
                        sql &= "Label09, type09, label09Desc, Field09Options, "
                        sql &= "Label10, type10, label10Desc, Field10Options, "
                        sql &= "Label11, type11, label11Desc, Field11Options, "
                        sql &= "Label12, type12, label12Desc, Field12Options, "
                        sql &= "Label13, type13, label13Desc, Field13Options, "
                        sql &= "Label14, type14, label14Desc, Field14Options, "
                        sql &= "Label15, type15, label15Desc, Field15Options, "
                        sql &= "Label16, type16, label16Desc, Field16Options, "
                        sql &= "Label17, type17, label17Desc, Field17Options, "
                        sql &= "Label18, type18, label18Desc, Field18Options, "
                        sql &= "Label19, type19, label19Desc, Field19Options, "
                        sql &= "Label20, type20, label20Desc, Field20Options) VALUES ("
                        sql &= ReadProperty(Of Integer)(FormIDProperty) & ", "
                        If ReadProperty(Of Boolean)(EducationProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        sql &= "'" & Replace(ReadProperty(Of String)(RedirectToProperty), "'", "''") & "', "
                        If ReadProperty(Of Boolean)(CalculatedProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        sql &= "'" & Replace(ReadProperty(Of String)(CalculationProperty), "'", "''") & "', "
                        If ReadProperty(Of Boolean)(NoResultProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        If ReadProperty(Of Boolean)(LockedProperty) Then
                            sql &= "1, "
                        Else
                            sql &= "0, "
                        End If
                        sql &= "'" & Replace(ReadProperty(Of String)(LockNotesProperty), "'", "''") & "', "

                        ' Fields
                        sql &= "'" & Replace(ReadProperty(Of String)(Field01LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field01TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field01HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field01OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field02LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field02TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field02HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field02OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field03LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field03TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field03HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field03OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field04LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field04TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field04HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field04OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field05LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field05TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field05HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field05OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field06LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field06TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field06HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field06OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field07LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field07TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field07HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field07OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field08LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field08TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field08HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field08OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field09LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field09TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field09HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field09OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field10LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field10TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field10HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field10OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field11LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field11TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field11HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field11OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field12LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field12TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field12HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field12OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field13LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field13TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field13HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field13OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field14LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field14TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field14HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field14OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field15LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field15TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field15HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field15OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field16LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field16TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field16HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field16OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field17LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field17TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field17HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field17OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field18LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field18TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field18HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field18OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field19LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field19TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field19HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field19OptionsProperty), "'", "''") & "', "

                        sql &= "'" & Replace(ReadProperty(Of String)(Field20LabelProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field20TypeProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field20HelpTextProperty), "'", "''") & "', "
                        sql &= "'" & Replace(ReadProperty(Of String)(Field20OptionsProperty), "'", "''") & "') "

                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' Update child objects
                For Each lookupValue In Me.LookupList
                    lookupValue.FormNumber = Me.FormID
                Next
                LookupList.Save()
            End Sub

            Protected Overrides Sub DataPortal_Update()
                If MyBase.IsDirty Then
                    Using conn As New SqlConnection(Database.ITWConnection)
                        conn.Open()
                        Using cmd As SqlCommand = conn.CreateCommand
                            Dim sql As String

                            ' Update the form
                            sql = "UPDATE [100CrNt] SET "
                            sql &= "NName = '" & Replace(ReadProperty(Of String)(FormNameProperty), "'", "''") & "', "
                            sql &= "dOrder = " & ReadProperty(Of Integer)(DisplayOrderProperty) & ", "
                            If ReadProperty(Of Boolean)(InactiveProperty) Then
                                sql &= "nInactive = 1, "
                            Else
                                sql &= "nInactive = 0, "
                            End If
                            If ReadProperty(Of Boolean)(RequiredProperty) Then
                                sql &= "Required = 1, "
                            Else
                                sql &= "Required = 0, "
                            End If
                            If ReadProperty(Of Boolean)(PCarOnlyProperty) Then
                                sql &= "PCarOnly = 1, "
                            Else
                                sql &= "PCarOnly = 0, "
                            End If
                            If ReadProperty(Of Boolean)(NursingNoteOnlyProperty) Then
                                sql &= "NNonly = 1 "
                            Else
                                sql &= "NNonly = 0 "
                            End If
                            sql &= "WHERE [ID] = " & ReadProperty(Of Integer)(IDProperty) & " "

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()


                            ' Update the form fields
                            sql = "UPDATE [100CrNtEl] SET "

                            If ReadProperty(Of Boolean)(EducationProperty) Then
                                sql &= "education = 1, "
                            Else
                                sql &= "education = 0, "
                            End If
                            sql &= "redirectForm = '" & Replace(ReadProperty(Of String)(RedirectToProperty), "'", "''") & "', "
                            If ReadProperty(Of Boolean)(CalculatedProperty) Then
                                sql &= "calculated = 1, "
                            Else
                                sql &= "calculated = 0, "
                            End If

                            sql &= "calculation = '" & Replace(ReadProperty(Of String)(CalculationProperty), "'", "''") & "', "

                            If ReadProperty(Of Boolean)(NoResultProperty) Then
                                sql &= "NoResult = 1, "
                            Else
                                sql &= "NoResult = 0, "
                            End If

                            If ReadProperty(Of Boolean)(LockedProperty) Then
                                sql &= "locked = 1, "
                            Else
                                sql &= "locked = 0, "
                            End If

                            sql &= "LockNotes = '" & Replace(ReadProperty(Of String)(LockNotesProperty), "'", "''") & "', "

                            ' Fields
                            sql &= "Label01 = '" & Replace(ReadProperty(Of String)(Field01LabelProperty), "'", "''") & "', "
                            sql &= "type01 = '" & Replace(ReadProperty(Of String)(Field01TypeProperty), "'", "''") & "', "
                            sql &= "label01Desc = '" & Replace(ReadProperty(Of String)(Field01HelpTextProperty), "'", "''") & "', "
                            sql &= "Field01Options = '" & Replace(ReadProperty(Of String)(Field01OptionsProperty), "'", "''") & "', "

                            sql &= "Label02 = '" & Replace(ReadProperty(Of String)(Field02LabelProperty), "'", "''") & "', "
                            sql &= "type02 = '" & Replace(ReadProperty(Of String)(Field02TypeProperty), "'", "''") & "', "
                            sql &= "label02Desc = '" & Replace(ReadProperty(Of String)(Field02HelpTextProperty), "'", "''") & "', "
                            sql &= "Field02Options = '" & Replace(ReadProperty(Of String)(Field02OptionsProperty), "'", "''") & "', "

                            sql &= "Label03 = '" & Replace(ReadProperty(Of String)(Field03LabelProperty), "'", "''") & "', "
                            sql &= "type03 = '" & Replace(ReadProperty(Of String)(Field03TypeProperty), "'", "''") & "', "
                            sql &= "label03Desc = '" & Replace(ReadProperty(Of String)(Field03HelpTextProperty), "'", "''") & "', "
                            sql &= "Field03Options = '" & Replace(ReadProperty(Of String)(Field03OptionsProperty), "'", "''") & "', "

                            sql &= "Label04 = '" & Replace(ReadProperty(Of String)(Field04LabelProperty), "'", "''") & "', "
                            sql &= "type04 = '" & Replace(ReadProperty(Of String)(Field04TypeProperty), "'", "''") & "', "
                            sql &= "label04Desc = '" & Replace(ReadProperty(Of String)(Field04HelpTextProperty), "'", "''") & "', "
                            sql &= "Field04Options = '" & Replace(ReadProperty(Of String)(Field04OptionsProperty), "'", "''") & "', "

                            sql &= "Label05 = '" & Replace(ReadProperty(Of String)(Field05LabelProperty), "'", "''") & "', "
                            sql &= "type05 = '" & Replace(ReadProperty(Of String)(Field05TypeProperty), "'", "''") & "', "
                            sql &= "label05Desc = '" & Replace(ReadProperty(Of String)(Field05HelpTextProperty), "'", "''") & "', "
                            sql &= "Field05Options = '" & Replace(ReadProperty(Of String)(Field05OptionsProperty), "'", "''") & "', "

                            sql &= "Label06 = '" & Replace(ReadProperty(Of String)(Field06LabelProperty), "'", "''") & "', "
                            sql &= "type06 = '" & Replace(ReadProperty(Of String)(Field06TypeProperty), "'", "''") & "', "
                            sql &= "label06Desc = '" & Replace(ReadProperty(Of String)(Field06HelpTextProperty), "'", "''") & "', "
                            sql &= "Field06Options = '" & Replace(ReadProperty(Of String)(Field06OptionsProperty), "'", "''") & "', "

                            sql &= "Label07 = '" & Replace(ReadProperty(Of String)(Field07LabelProperty), "'", "''") & "', "
                            sql &= "type07 = '" & Replace(ReadProperty(Of String)(Field07TypeProperty), "'", "''") & "', "
                            sql &= "label07Desc = '" & Replace(ReadProperty(Of String)(Field07HelpTextProperty), "'", "''") & "', "
                            sql &= "Field07Options = '" & Replace(ReadProperty(Of String)(Field07OptionsProperty), "'", "''") & "', "

                            sql &= "Label08 = '" & Replace(ReadProperty(Of String)(Field08LabelProperty), "'", "''") & "', "
                            sql &= "type08 = '" & Replace(ReadProperty(Of String)(Field08TypeProperty), "'", "''") & "', "
                            sql &= "label08Desc = '" & Replace(ReadProperty(Of String)(Field08HelpTextProperty), "'", "''") & "', "
                            sql &= "Field08Options = '" & Replace(ReadProperty(Of String)(Field08OptionsProperty), "'", "''") & "', "

                            sql &= "Label09 = '" & Replace(ReadProperty(Of String)(Field09LabelProperty), "'", "''") & "', "
                            sql &= "type09 = '" & Replace(ReadProperty(Of String)(Field09TypeProperty), "'", "''") & "', "
                            sql &= "label09Desc = '" & Replace(ReadProperty(Of String)(Field09HelpTextProperty), "'", "''") & "', "
                            sql &= "Field09Options = '" & Replace(ReadProperty(Of String)(Field09OptionsProperty), "'", "''") & "', "

                            sql &= "Label10 = '" & Replace(ReadProperty(Of String)(Field10LabelProperty), "'", "''") & "', "
                            sql &= "type10 = '" & Replace(ReadProperty(Of String)(Field10TypeProperty), "'", "''") & "', "
                            sql &= "label10Desc = '" & Replace(ReadProperty(Of String)(Field10HelpTextProperty), "'", "''") & "', "
                            sql &= "Field10Options = '" & Replace(ReadProperty(Of String)(Field10OptionsProperty), "'", "''") & "', "

                            sql &= "Label11 = '" & Replace(ReadProperty(Of String)(Field11LabelProperty), "'", "''") & "', "
                            sql &= "type11 = '" & Replace(ReadProperty(Of String)(Field11TypeProperty), "'", "''") & "', "
                            sql &= "label11Desc = '" & Replace(ReadProperty(Of String)(Field11HelpTextProperty), "'", "''") & "', "
                            sql &= "Field11Options = '" & Replace(ReadProperty(Of String)(Field11OptionsProperty), "'", "''") & "', "

                            sql &= "Label12 = '" & Replace(ReadProperty(Of String)(Field12LabelProperty), "'", "''") & "', "
                            sql &= "type12 = '" & Replace(ReadProperty(Of String)(Field12TypeProperty), "'", "''") & "', "
                            sql &= "label12Desc = '" & Replace(ReadProperty(Of String)(Field12HelpTextProperty), "'", "''") & "', "
                            sql &= "Field12Options = '" & Replace(ReadProperty(Of String)(Field12OptionsProperty), "'", "''") & "', "

                            sql &= "Label13 = '" & Replace(ReadProperty(Of String)(Field13LabelProperty), "'", "''") & "', "
                            sql &= "type13 = '" & Replace(ReadProperty(Of String)(Field13TypeProperty), "'", "''") & "', "
                            sql &= "label13Desc = '" & Replace(ReadProperty(Of String)(Field13HelpTextProperty), "'", "''") & "', "
                            sql &= "Field13Options = '" & Replace(ReadProperty(Of String)(Field13OptionsProperty), "'", "''") & "', "

                            sql &= "Label14 = '" & Replace(ReadProperty(Of String)(Field14LabelProperty), "'", "''") & "', "
                            sql &= "type14 = '" & Replace(ReadProperty(Of String)(Field14TypeProperty), "'", "''") & "', "
                            sql &= "label14Desc = '" & Replace(ReadProperty(Of String)(Field14HelpTextProperty), "'", "''") & "', "
                            sql &= "Field14Options = '" & Replace(ReadProperty(Of String)(Field14OptionsProperty), "'", "''") & "', "

                            sql &= "Label15 = '" & Replace(ReadProperty(Of String)(Field15LabelProperty), "'", "''") & "', "
                            sql &= "type15 = '" & Replace(ReadProperty(Of String)(Field15TypeProperty), "'", "''") & "', "
                            sql &= "label15Desc = '" & Replace(ReadProperty(Of String)(Field15HelpTextProperty), "'", "''") & "', "
                            sql &= "Field15Options = '" & Replace(ReadProperty(Of String)(Field15OptionsProperty), "'", "''") & "', "

                            sql &= "Label16 = '" & Replace(ReadProperty(Of String)(Field16LabelProperty), "'", "''") & "', "
                            sql &= "type16 = '" & Replace(ReadProperty(Of String)(Field16TypeProperty), "'", "''") & "', "
                            sql &= "label16Desc = '" & Replace(ReadProperty(Of String)(Field16HelpTextProperty), "'", "''") & "', "
                            sql &= "Field16Options = '" & Replace(ReadProperty(Of String)(Field16OptionsProperty), "'", "''") & "', "

                            sql &= "Label17 = '" & Replace(ReadProperty(Of String)(Field17LabelProperty), "'", "''") & "', "
                            sql &= "type17 = '" & Replace(ReadProperty(Of String)(Field17TypeProperty), "'", "''") & "', "
                            sql &= "label17Desc = '" & Replace(ReadProperty(Of String)(Field17HelpTextProperty), "'", "''") & "', "
                            sql &= "Field17Options = '" & Replace(ReadProperty(Of String)(Field17OptionsProperty), "'", "''") & "', "

                            sql &= "Label18 = '" & Replace(ReadProperty(Of String)(Field18LabelProperty), "'", "''") & "', "
                            sql &= "type18 = '" & Replace(ReadProperty(Of String)(Field18TypeProperty), "'", "''") & "', "
                            sql &= "label18Desc = '" & Replace(ReadProperty(Of String)(Field18HelpTextProperty), "'", "''") & "', "
                            sql &= "Field18Options = '" & Replace(ReadProperty(Of String)(Field18OptionsProperty), "'", "''") & "', "

                            sql &= "Label19 = '" & Replace(ReadProperty(Of String)(Field19LabelProperty), "'", "''") & "', "
                            sql &= "type19 = '" & Replace(ReadProperty(Of String)(Field19TypeProperty), "'", "''") & "', "
                            sql &= "label19Desc = '" & Replace(ReadProperty(Of String)(Field19HelpTextProperty), "'", "''") & "', "
                            sql &= "Field19Options = '" & Replace(ReadProperty(Of String)(Field19OptionsProperty), "'", "''") & "', "

                            sql &= "Label20 = '" & Replace(ReadProperty(Of String)(Field20LabelProperty), "'", "''") & "', "
                            sql &= "type20 = '" & Replace(ReadProperty(Of String)(Field20TypeProperty), "'", "''") & "', "
                            sql &= "label20Desc = '" & Replace(ReadProperty(Of String)(Field20HelpTextProperty), "'", "''") & "', "
                            sql &= "Field20Options = '" & Replace(ReadProperty(Of String)(Field20OptionsProperty), "'", "''") & "' "

                            sql &= "WHERE nsNtID = " & ReadProperty(Of Integer)(FormIDProperty) & " "
                            cmd.CommandText = sql
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End If

                ' Update child objects
                LookupList.Save()
            End Sub

            Protected Overrides Sub DataPortal_DeleteSelf()
                DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(FormIDProperty)))
            End Sub

            Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "DELETE FROM [100CrNtEl] "
                        sql &= "WHERE nsNtID = " & criteria.FormID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()

                        sql = "DELETE FROM [100CrNt] "
                        sql &= "WHERE nsNtID = " & criteria.FormID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' Update child objects
                SetProperty(Of NursingNoteFormLookupList)(LookupListProperty, NursingNoteFormLookupList.NewNursingNoteFormLookupList)
            End Sub

#End Region

        End Class

    End Namespace

End Namespace