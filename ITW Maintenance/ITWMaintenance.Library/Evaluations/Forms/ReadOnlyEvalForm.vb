Imports System.Data.SqlClient

Namespace Evaluations

    Namespace Forms

        <Serializable()> _
        Public Class ReadOnlyEvalForm
            Inherits ReadOnlyBase(Of ReadOnlyEvalForm)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            Private ReadOnly Property ID() As Integer
                Get
                    Return GetProperty(Of Integer)(IDProperty)
                End Get
            End Property

            Private Shared EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property EvalID() As Integer
                Get
                    Return GetProperty(Of Integer)(EvalIDProperty)
                End Get
            End Property

            Private Shared FormNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FormName", "Form Name"))
            Public ReadOnly Property FormName() As String
                Get
                    Return GetProperty(Of String)(FormNameProperty)
                End Get
            End Property

            Private Shared EPathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EPath"))
            Public ReadOnly Property EPath() As String
                Get
                    Return GetProperty(Of String)(EPathProperty)
                End Get
            End Property

            Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
            Public ReadOnly Property DisplayOrder() As Integer
                Get
                    Return GetProperty(Of Integer)(DisplayOrderProperty)
                End Get
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public ReadOnly Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
                End Get
            End Property

            Private Shared RequiredProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Required"))
            Public ReadOnly Property Required() As Boolean
                Get
                    Return GetProperty(Of Boolean)(RequiredProperty)
                End Get
            End Property

            Private Shared ProblemProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Problem"))
            Public ReadOnly Property Problem() As Boolean
                Get
                    Return GetProperty(Of Boolean)(ProblemProperty)
                End Get
            End Property

            Private Shared PatientGoalProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("PatientGoal", "Patient Goal"))
            Public ReadOnly Property PatientGoal() As Boolean
                Get
                    Return GetProperty(Of Boolean)(PatientGoalProperty)
                End Get
            End Property

            Private Shared OutcomeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Outcome"))
            Public ReadOnly Property Outcome() As Boolean
                Get
                    Return GetProperty(Of Boolean)(OutcomeProperty)
                End Get
            End Property

            Private Shared TestProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Test"))
            Public ReadOnly Property Test() As Boolean
                Get
                    Return GetProperty(Of Boolean)(TestProperty)
                End Get
            End Property

            Private Shared FunctionalObjectiveFindingProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("FunctionalObjectiveFinding", "Functional Objective Finding"))
            Public ReadOnly Property FunctionalObjectiveFinding() As Boolean
                Get
                    Return GetProperty(Of Boolean)(FunctionalObjectiveFindingProperty)
                End Get
            End Property

            Private Shared RequiredForPediatricsProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("RequiredForPediatrics", "Required For Pediatrics"))
            Public ReadOnly Property RequiredForPediatrics() As Boolean
                Get
                    Return GetProperty(Of Boolean)(RequiredForPediatricsProperty)
                End Get
            End Property

            Private Shared SubjectiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Subjective"))
            Public ReadOnly Property Subjective() As Boolean
                Get
                    Return GetProperty(Of Boolean)(SubjectiveProperty)
                End Get
            End Property

            Private Shared CalculatedProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Calculated"))
            Public ReadOnly Property Calculated() As Boolean
                Get
                    Return GetProperty(Of Boolean)(CalculatedProperty)
                End Get
            End Property

            Private Shared CalculationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Calculation"))
            Public ReadOnly Property Calculation() As String
                Get
                    Return GetProperty(Of String)(CalculationProperty)
                End Get
            End Property

            Private Shared ReportDescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ReportDescription", "Report Description"))
            Public ReadOnly Property ReportDescription() As String
                Get
                    Return ReadProperty(Of String)(ReportDescriptionProperty)
                End Get
            End Property

#Region " Form Field Labels "

            Private Shared Field01LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Label", "Field 01 Label"))
            Public ReadOnly Property Field01Label() As String
                Get
                    Return ReadProperty(Of String)(Field01LabelProperty)
                End Get
            End Property

            Private Shared Field02LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Label", "Field 02 Label"))
            Public ReadOnly Property Field02Label() As String
                Get
                    Return ReadProperty(Of String)(Field02LabelProperty)
                End Get
            End Property

            Private Shared Field03LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Label", "Field 03 Label"))
            Public ReadOnly Property Field03Label() As String
                Get
                    Return ReadProperty(Of String)(Field03LabelProperty)
                End Get
            End Property

            Private Shared Field04LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Label", "Field 04 Label"))
            Public ReadOnly Property Field04Label() As String
                Get
                    Return ReadProperty(Of String)(Field04LabelProperty)
                End Get
            End Property

            Private Shared Field05LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Label", "Field 05 Label"))
            Public ReadOnly Property Field05Label() As String
                Get
                    Return ReadProperty(Of String)(Field05LabelProperty)
                End Get
            End Property

            Private Shared Field06LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Label", "Field 06 Label"))
            Public ReadOnly Property Field06Label() As String
                Get
                    Return ReadProperty(Of String)(Field06LabelProperty)
                End Get
            End Property

            Private Shared Field07LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Label", "Field 07 Label"))
            Public ReadOnly Property Field07Label() As String
                Get
                    Return ReadProperty(Of String)(Field07LabelProperty)
                End Get
            End Property

            Private Shared Field08LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Label", "Field 08 Label"))
            Public ReadOnly Property Field08Label() As String
                Get
                    Return ReadProperty(Of String)(Field08LabelProperty)
                End Get
            End Property

            Private Shared Field09LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Label", "Field 09 Label"))
            Public ReadOnly Property Field09Label() As String
                Get
                    Return ReadProperty(Of String)(Field09LabelProperty)
                End Get
            End Property

            Private Shared Field10LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Label", "Field 10 Label"))
            Public ReadOnly Property Field10Label() As String
                Get
                    Return ReadProperty(Of String)(Field10LabelProperty)
                End Get
            End Property

            Private Shared Field11LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Label", "Field 11 Label"))
            Public ReadOnly Property Field11Label() As String
                Get
                    Return ReadProperty(Of String)(Field11LabelProperty)
                End Get
            End Property

            Private Shared Field12LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Label", "Field 12 Label"))
            Public ReadOnly Property Field12Label() As String
                Get
                    Return ReadProperty(Of String)(Field12LabelProperty)
                End Get
            End Property

            Private Shared Field13LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Label", "Field 13 Label"))
            Public ReadOnly Property Field13Label() As String
                Get
                    Return ReadProperty(Of String)(Field13LabelProperty)
                End Get
            End Property

            Private Shared Field14LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Label", "Field 14 Label"))
            Public ReadOnly Property Field14Label() As String
                Get
                    Return ReadProperty(Of String)(Field14LabelProperty)
                End Get
            End Property

            Private Shared Field15LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Label", "Field 15 Label"))
            Public ReadOnly Property Field15Label() As String
                Get
                    Return ReadProperty(Of String)(Field15LabelProperty)
                End Get
            End Property

            Private Shared Field16LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Label", "Field 16 Label"))
            Public ReadOnly Property Field16Label() As String
                Get
                    Return ReadProperty(Of String)(Field16LabelProperty)
                End Get
            End Property

            Private Shared Field17LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Label", "Field 17 Label"))
            Public ReadOnly Property Field17Label() As String
                Get
                    Return ReadProperty(Of String)(Field17LabelProperty)
                End Get
            End Property

            Private Shared Field18LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Label", "Field 18 Label"))
            Public ReadOnly Property Field18Label() As String
                Get
                    Return ReadProperty(Of String)(Field18LabelProperty)
                End Get
            End Property

            Private Shared Field19LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Label", "Field 19 Label"))
            Public ReadOnly Property Field19Label() As String
                Get
                    Return ReadProperty(Of String)(Field19LabelProperty)
                End Get
            End Property

            Private Shared Field20LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Label", "Field 20 Label"))
            Public ReadOnly Property Field20Label() As String
                Get
                    Return ReadProperty(Of String)(Field20LabelProperty)
                End Get
            End Property

#End Region

#Region " Form Field Types "

            Private Shared Field01TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Type", "Field 01 Type"))
            Public ReadOnly Property Field01Type() As String
                Get
                    Return ReadProperty(Of String)(Field01TypeProperty)
                End Get
            End Property

            Private Shared Field02TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Type", "Field 02 Type"))
            Public ReadOnly Property Field02Type() As String
                Get
                    Return ReadProperty(Of String)(Field02TypeProperty)
                End Get
            End Property

            Private Shared Field03TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Type", "Field 03 Type"))
            Public ReadOnly Property Field03Type() As String
                Get
                    Return ReadProperty(Of String)(Field03TypeProperty)
                End Get
            End Property

            Private Shared Field04TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Type", "Field 04 Type"))
            Public ReadOnly Property Field04Type() As String
                Get
                    Return ReadProperty(Of String)(Field04TypeProperty)
                End Get
            End Property

            Private Shared Field05TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Type", "Field 05 Type"))
            Public ReadOnly Property Field05Type() As String
                Get
                    Return ReadProperty(Of String)(Field05TypeProperty)
                End Get
            End Property

            Private Shared Field06TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Type", "Field 06 Type"))
            Public ReadOnly Property Field06Type() As String
                Get
                    Return ReadProperty(Of String)(Field06TypeProperty)
                End Get
            End Property

            Private Shared Field07TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Type", "Field 07 Type"))
            Public ReadOnly Property Field07Type() As String
                Get
                    Return ReadProperty(Of String)(Field07TypeProperty)
                End Get
            End Property

            Private Shared Field08TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Type", "Field 08 Type"))
            Public ReadOnly Property Field08Type() As String
                Get
                    Return ReadProperty(Of String)(Field08TypeProperty)
                End Get
            End Property

            Private Shared Field09TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Type", "Field 09 Type"))
            Public ReadOnly Property Field09Type() As String
                Get
                    Return ReadProperty(Of String)(Field09TypeProperty)
                End Get
            End Property

            Private Shared Field10TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Type", "Field 10 Type"))
            Public ReadOnly Property Field10Type() As String
                Get
                    Return ReadProperty(Of String)(Field10TypeProperty)
                End Get
            End Property

            Private Shared Field11TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Type", "Field 11 Type"))
            Public ReadOnly Property Field11Type() As String
                Get
                    Return ReadProperty(Of String)(Field11TypeProperty)
                End Get
            End Property

            Private Shared Field12TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Type", "Field 12 Type"))
            Public ReadOnly Property Field12Type() As String
                Get
                    Return ReadProperty(Of String)(Field12TypeProperty)
                End Get
            End Property

            Private Shared Field13TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Type", "Field 13 Type"))
            Public ReadOnly Property Field13Type() As String
                Get
                    Return ReadProperty(Of String)(Field13TypeProperty)
                End Get
            End Property

            Private Shared Field14TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Type", "Field 14 Type"))
            Public ReadOnly Property Field14Type() As String
                Get
                    Return ReadProperty(Of String)(Field14TypeProperty)
                End Get
            End Property

            Private Shared Field15TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Type", "Field 15 Type"))
            Public ReadOnly Property Field15Type() As String
                Get
                    Return ReadProperty(Of String)(Field15TypeProperty)
                End Get
            End Property

            Private Shared Field16TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Type", "Field 16 Type"))
            Public ReadOnly Property Field16Type() As String
                Get
                    Return ReadProperty(Of String)(Field16TypeProperty)
                End Get
            End Property

            Private Shared Field17TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Type", "Field 17 Type"))
            Public ReadOnly Property Field17Type() As String
                Get
                    Return ReadProperty(Of String)(Field17TypeProperty)
                End Get
            End Property

            Private Shared Field18TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Type", "Field 18 Type"))
            Public ReadOnly Property Field18Type() As String
                Get
                    Return ReadProperty(Of String)(Field18TypeProperty)
                End Get
            End Property

            Private Shared Field19TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Type", "Field 19 Type"))
            Public ReadOnly Property Field19Type() As String
                Get
                    Return ReadProperty(Of String)(Field19TypeProperty)
                End Get
            End Property

            Private Shared Field20TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Type", "Field 20 Type"))
            Public ReadOnly Property Field20Type() As String
                Get
                    Return ReadProperty(Of String)(Field20TypeProperty)
                End Get
            End Property

#End Region

#Region " Form Field Help Text "

            Private Shared Field01HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01HelpText", "Field 01 Help Text"))
            Public ReadOnly Property Field01HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field01HelpTextProperty)
                End Get
            End Property

            Private Shared Field02HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02HelpText", "Field 02 Help Text"))
            Public ReadOnly Property Field02HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field02HelpTextProperty)
                End Get
            End Property

            Private Shared Field03HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03HelpText", "Field 03 Help Text"))
            Public ReadOnly Property Field03HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field03HelpTextProperty)
                End Get
            End Property

            Private Shared Field04HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04HelpText", "Field 04 Help Text"))
            Public ReadOnly Property Field04HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field04HelpTextProperty)
                End Get
            End Property

            Private Shared Field05HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05HelpText", "Field 05 Help Text"))
            Public ReadOnly Property Field05HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field05HelpTextProperty)
                End Get
            End Property

            Private Shared Field06HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06HelpText", "Field 06 Help Text"))
            Public ReadOnly Property Field06HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field06HelpTextProperty)
                End Get
            End Property

            Private Shared Field07HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07HelpText", "Field 07 Help Text"))
            Public ReadOnly Property Field07HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field07HelpTextProperty)
                End Get
            End Property

            Private Shared Field08HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08HelpText", "Field 08 Help Text"))
            Public ReadOnly Property Field08HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field08HelpTextProperty)
                End Get
            End Property

            Private Shared Field09HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09HelpText", "Field 09 Help Text"))
            Public ReadOnly Property Field09HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field09HelpTextProperty)
                End Get
            End Property

            Private Shared Field10HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10HelpText", "Field 10 Help Text"))
            Public ReadOnly Property Field10HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field10HelpTextProperty)
                End Get
            End Property

            Private Shared Field11HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11HelpText", "Field 11 Help Text"))
            Public ReadOnly Property Field11HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field11HelpTextProperty)
                End Get
            End Property

            Private Shared Field12HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12HelpText", "Field 12 Help Text"))
            Public ReadOnly Property Field12HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field12HelpTextProperty)
                End Get
            End Property

            Private Shared Field13HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13HelpText", "Field 13 Help Text"))
            Public ReadOnly Property Field13HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field13HelpTextProperty)
                End Get
            End Property

            Private Shared Field14HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14HelpText", "Field 14 Help Text"))
            Public ReadOnly Property Field14HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field14HelpTextProperty)
                End Get
            End Property

            Private Shared Field15HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15HelpText", "Field 15 Help Text"))
            Public ReadOnly Property Field15HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field15HelpTextProperty)
                End Get
            End Property

            Private Shared Field16HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16HelpText", "Field 16 Help Text"))
            Public ReadOnly Property Field16HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field16HelpTextProperty)
                End Get
            End Property

            Private Shared Field17HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17HelpText", "Field 17 Help Text"))
            Public ReadOnly Property Field17HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field17HelpTextProperty)
                End Get
            End Property

            Private Shared Field18HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18HelpText", "Field 18 Help Text"))
            Public ReadOnly Property Field18HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field18HelpTextProperty)
                End Get
            End Property

            Private Shared Field19HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19HelpText", "Field 19 Help Text"))
            Public ReadOnly Property Field19HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field19HelpTextProperty)
                End Get
            End Property

            Private Shared Field20HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20HelpText", "Field 20 Help Text"))
            Public ReadOnly Property Field20HelpText() As String
                Get
                    Return ReadProperty(Of String)(Field20HelpTextProperty)
                End Get
            End Property

#End Region

#Region " Form Field Options "

            Private Shared Field01OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Options", "Field 01 Options"))
            Public ReadOnly Property Field01Options() As String
                Get
                    Return ReadProperty(Of String)(Field01OptionsProperty)
                End Get
            End Property

            Private Shared Field02OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Options", "Field 02 Options"))
            Public ReadOnly Property Field02Options() As String
                Get
                    Return ReadProperty(Of String)(Field02OptionsProperty)
                End Get
            End Property

            Private Shared Field03OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Options", "Field 03 Options"))
            Public ReadOnly Property Field03Options() As String
                Get
                    Return ReadProperty(Of String)(Field03OptionsProperty)
                End Get
            End Property

            Private Shared Field04OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Options", "Field 04 Options"))
            Public ReadOnly Property Field04Options() As String
                Get
                    Return ReadProperty(Of String)(Field04OptionsProperty)
                End Get
            End Property

            Private Shared Field05OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Options", "Field 05 Options"))
            Public ReadOnly Property Field05Options() As String
                Get
                    Return ReadProperty(Of String)(Field05OptionsProperty)
                End Get
            End Property

            Private Shared Field06OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Options", "Field 06 Options"))
            Public ReadOnly Property Field06Options() As String
                Get
                    Return ReadProperty(Of String)(Field06OptionsProperty)
                End Get
            End Property

            Private Shared Field07OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Options", "Field 07 Options"))
            Public ReadOnly Property Field07Options() As String
                Get
                    Return ReadProperty(Of String)(Field07OptionsProperty)
                End Get
            End Property

            Private Shared Field08OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Options", "Field 08 Options"))
            Public ReadOnly Property Field08Options() As String
                Get
                    Return ReadProperty(Of String)(Field08OptionsProperty)
                End Get
            End Property

            Private Shared Field09OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Options", "Field 09 Options"))
            Public ReadOnly Property Field09Options() As String
                Get
                    Return ReadProperty(Of String)(Field09OptionsProperty)
                End Get
            End Property

            Private Shared Field10OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Options", "Field 10 Options"))
            Public ReadOnly Property Field10Options() As String
                Get
                    Return ReadProperty(Of String)(Field10OptionsProperty)
                End Get
            End Property

            Private Shared Field11OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Options", "Field 11 Options"))
            Public ReadOnly Property Field11Options() As String
                Get
                    Return ReadProperty(Of String)(Field11OptionsProperty)
                End Get
            End Property

            Private Shared Field12OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Options", "Field 12 Options"))
            Public ReadOnly Property Field12Options() As String
                Get
                    Return ReadProperty(Of String)(Field12OptionsProperty)
                End Get
            End Property

            Private Shared Field13OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Options", "Field 13 Options"))
            Public ReadOnly Property Field13Options() As String
                Get
                    Return ReadProperty(Of String)(Field13OptionsProperty)
                End Get
            End Property

            Private Shared Field14OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Options", "Field 14 Options"))
            Public ReadOnly Property Field14Options() As String
                Get
                    Return ReadProperty(Of String)(Field14OptionsProperty)
                End Get
            End Property

            Private Shared Field15OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Options", "Field 15 Options"))
            Public ReadOnly Property Field15Options() As String
                Get
                    Return ReadProperty(Of String)(Field15OptionsProperty)
                End Get
            End Property

            Private Shared Field16OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Options", "Field 16 Options"))
            Public ReadOnly Property Field16Options() As String
                Get
                    Return ReadProperty(Of String)(Field16OptionsProperty)
                End Get
            End Property

            Private Shared Field17OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Options", "Field 17 Options"))
            Public ReadOnly Property Field17Options() As String
                Get
                    Return ReadProperty(Of String)(Field17OptionsProperty)
                End Get
            End Property

            Private Shared Field18OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Options", "Field 18 Options"))
            Public ReadOnly Property Field18Options() As String
                Get
                    Return ReadProperty(Of String)(Field18OptionsProperty)
                End Get
            End Property

            Private Shared Field19OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Options", "Field 19 Options"))
            Public ReadOnly Property Field19Options() As String
                Get
                    Return ReadProperty(Of String)(Field19OptionsProperty)
                End Get
            End Property

            Private Shared Field20OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Options", "Field 20 Options"))
            Public ReadOnly Property Field20Options() As String
                Get
                    Return ReadProperty(Of String)(Field20OptionsProperty)
                End Get
            End Property

#End Region

#Region " Form Field Validations "

            Private Shared Field01ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field01Validation", "Field 01 Validation"))
            Public ReadOnly Property Field01Validation() As String
                Get
                    Return ReadProperty(Of String)(Field01ValidationProperty)
                End Get
            End Property

            Private Shared Field02ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field02Validation", "Field 02 Validation"))
            Public ReadOnly Property Field02Validation() As String
                Get
                    Return ReadProperty(Of String)(Field02ValidationProperty)
                End Get
            End Property

            Private Shared Field03ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field03Validation", "Field 03 Validation"))
            Public ReadOnly Property Field03Validation() As String
                Get
                    Return ReadProperty(Of String)(Field03ValidationProperty)
                End Get
            End Property

            Private Shared Field04ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field04Validation", "Field 04 Validation"))
            Public ReadOnly Property Field04Validation() As String
                Get
                    Return ReadProperty(Of String)(Field04ValidationProperty)
                End Get
            End Property

            Private Shared Field05ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field05Validation", "Field 05 Validation"))
            Public ReadOnly Property Field05Validation() As String
                Get
                    Return ReadProperty(Of String)(Field05ValidationProperty)
                End Get
            End Property

            Private Shared Field06ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field06Validation", "Field 06 Validation"))
            Public ReadOnly Property Field06Validation() As String
                Get
                    Return ReadProperty(Of String)(Field06ValidationProperty)
                End Get
            End Property

            Private Shared Field07ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field07Validation", "Field 07 Validation"))
            Public ReadOnly Property Field07Validation() As String
                Get
                    Return ReadProperty(Of String)(Field07ValidationProperty)
                End Get
            End Property

            Private Shared Field08ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field08Validation", "Field 08 Validation"))
            Public ReadOnly Property Field08Validation() As String
                Get
                    Return ReadProperty(Of String)(Field08ValidationProperty)
                End Get
            End Property

            Private Shared Field09ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field09Validation", "Field 09 Validation"))
            Public ReadOnly Property Field09Validation() As String
                Get
                    Return ReadProperty(Of String)(Field09ValidationProperty)
                End Get
            End Property

            Private Shared Field10ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field10Validation", "Field 10 Validation"))
            Public ReadOnly Property Field10Validation() As String
                Get
                    Return ReadProperty(Of String)(Field10ValidationProperty)
                End Get
            End Property

            Private Shared Field11ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field11Validation", "Field 11 Validation"))
            Public ReadOnly Property Field11Validation() As String
                Get
                    Return ReadProperty(Of String)(Field11ValidationProperty)
                End Get
            End Property

            Private Shared Field12ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field12Validation", "Field 12 Validation"))
            Public ReadOnly Property Field12Validation() As String
                Get
                    Return ReadProperty(Of String)(Field12ValidationProperty)
                End Get
            End Property

            Private Shared Field13ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field13Validation", "Field 13 Validation"))
            Public ReadOnly Property Field13Validation() As String
                Get
                    Return ReadProperty(Of String)(Field13ValidationProperty)
                End Get
            End Property

            Private Shared Field14ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field14Validation", "Field 14 Validation"))
            Public ReadOnly Property Field14Validation() As String
                Get
                    Return ReadProperty(Of String)(Field14ValidationProperty)
                End Get
            End Property

            Private Shared Field15ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field15Validation", "Field 15 Validation"))
            Public ReadOnly Property Field15Validation() As String
                Get
                    Return ReadProperty(Of String)(Field15ValidationProperty)
                End Get
            End Property

            Private Shared Field16ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field16Validation", "Field 16 Validation"))
            Public ReadOnly Property Field16Validation() As String
                Get
                    Return ReadProperty(Of String)(Field16ValidationProperty)
                End Get
            End Property

            Private Shared Field17ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field17Validation", "Field 17 Validation"))
            Public ReadOnly Property Field17Validation() As String
                Get
                    Return ReadProperty(Of String)(Field17ValidationProperty)
                End Get
            End Property

            Private Shared Field18ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field18Validation", "Field 18 Validation"))
            Public ReadOnly Property Field18Validation() As String
                Get
                    Return ReadProperty(Of String)(Field18ValidationProperty)
                End Get
            End Property

            Private Shared Field19ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field19Validation", "Field 19 Validation"))
            Public ReadOnly Property Field19Validation() As String
                Get
                    Return ReadProperty(Of String)(Field19ValidationProperty)
                End Get
            End Property

            Private Shared Field20ValidationProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Field20Validation", "Field 20 Validation"))
            Public ReadOnly Property Field20Validation() As String
                Get
                    Return ReadProperty(Of String)(Field20ValidationProperty)
                End Get
            End Property

#End Region

            Protected Overrides Function GetIdValue() As Object
                Return ReadProperty(Of Integer)(EvalIDProperty)
            End Function

#End Region

#Region " Authorization Rules "

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function GetEvalFormInfo(ByVal EvalID As Integer) As ReadOnlyEvalForm
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Eval Form")
                End If
                Return DataPortal.Fetch(Of ReadOnlyEvalForm)(New Criteria(EvalID))
            End Function

            Private Sub New()
                ' Require use of Factory methods
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _EvalID As Integer

                Public ReadOnly Property EvalID() As Integer
                    Get
                        Return Me._EvalID
                    End Get
                End Property

                Public Sub New(ByVal EvalID As Integer)
                    Me._EvalID = EvalID
                End Sub
            End Class


            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT e.[ID], e.EvalID, e.EName, e.EPath, e.ReportDesc, e.DOrder, "
                        sql &= "e.Required, e.inactive, eel.fxnlObjFnd, eel.subjective, eel.Test, "
                        sql &= "eel.PatientGoal, eel.eClass, eel.calculated, eel.calculation, "
                        sql &= "eel.PedReq, eel.Problem, eel.outcome, eel.locked, eel.LockNotes, "
                        sql &= "eel.Label01, eel.type01, eel.label01Desc, eel.Field01Options, eel.[Validate01], "
                        sql &= "eel.Label02, eel.type02, eel.label02Desc, eel.Field02Options, eel.[Validate02], "
                        sql &= "eel.Label03, eel.type03, eel.label03Desc, eel.Field03Options, eel.[Validate03], "
                        sql &= "eel.Label04, eel.type04, eel.label04Desc, eel.Field04Options, eel.[Validate04], "
                        sql &= "eel.Label05, eel.type05, eel.label05Desc, eel.Field05Options, eel.[Validate05], "
                        sql &= "eel.Label06, eel.type06, eel.label06Desc, eel.Field06Options, eel.[Validate06], "
                        sql &= "eel.Label07, eel.type07, eel.label07Desc, eel.Field07Options, eel.[Validate07], "
                        sql &= "eel.Label08, eel.type08, eel.label08Desc, eel.Field08Options, eel.[Validate08], "
                        sql &= "eel.Label09, eel.type09, eel.label09Desc, eel.Field09Options, eel.[Validate09], "
                        sql &= "eel.Label10, eel.type10, eel.label10Desc, eel.Field10Options, eel.[Validate10], "
                        sql &= "eel.Label11, eel.type11, eel.label11Desc, eel.Field11Options, eel.[Validate11], "
                        sql &= "eel.Label12, eel.type12, eel.label12Desc, eel.Field12Options, eel.[Validate12], "
                        sql &= "eel.Label13, eel.type13, eel.label13Desc, eel.Field13Options, eel.[Validate13], "
                        sql &= "eel.Label14, eel.type14, eel.label14Desc, eel.Field14Options, eel.[Validate14], "
                        sql &= "eel.Label15, eel.type15, eel.label15Desc, eel.Field15Options, eel.[Validate15], "
                        sql &= "eel.Label16, eel.type16, eel.label16Desc, eel.Field16Options, eel.[Validate16], "
                        sql &= "eel.Label17, eel.type17, eel.label17Desc, eel.Field17Options, eel.[Validate17], "
                        sql &= "eel.Label18, eel.type18, eel.label18Desc, eel.Field18Options, eel.[Validate18], "
                        sql &= "eel.Label19, eel.type19, eel.label19Desc, eel.Field19Options, eel.[Validate19], "
                        sql &= "eel.Label20, eel.type20, eel.label20Desc, eel.Field20Options, eel.[Validate20] "
                        sql &= "FROM [100Eval] e "
                        sql &= "INNER JOIN [100EvalEl] eel on e.EvalID = eel.evalID "
                        sql &= "WHERE e.EvalID = " & Criteria.EvalID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                LoadProperty(Of String)(FormNameProperty, .GetString("EName"))
                                LoadProperty(Of String)(EPathProperty, .GetString("EPath"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("dOrder"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("inactive"))
                                LoadProperty(Of Boolean)(RequiredProperty, .GetBoolean("Required"))
                                LoadProperty(Of Boolean)(ProblemProperty, .GetBoolean("Problem"))
                                LoadProperty(Of Boolean)(PatientGoalProperty, .GetBoolean("PatientGoal"))
                                LoadProperty(Of Boolean)(OutcomeProperty, .GetBoolean("outcome"))
                                LoadProperty(Of Boolean)(TestProperty, .GetBoolean("Test"))
                                LoadProperty(Of Boolean)(FunctionalObjectiveFindingProperty, .GetBoolean("fxnlObjFnd"))
                                LoadProperty(Of Boolean)(RequiredForPediatricsProperty, .GetBoolean("PedReq"))
                                LoadProperty(Of Boolean)(SubjectiveProperty, .GetBoolean("subjective"))
                                LoadProperty(Of Boolean)(CalculatedProperty, .GetBoolean("calculated"))
                                LoadProperty(Of String)(CalculationProperty, .GetString("calculation"))
                                LoadProperty(Of String)(ReportDescriptionProperty, .GetString("ReportDesc"))

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
                                LoadProperty(Of String)(Field01TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type01")))
                                LoadProperty(Of String)(Field02TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type02")))
                                LoadProperty(Of String)(Field03TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type03")))
                                LoadProperty(Of String)(Field04TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type04")))
                                LoadProperty(Of String)(Field05TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type05")))
                                LoadProperty(Of String)(Field06TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type06")))
                                LoadProperty(Of String)(Field07TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type07")))
                                LoadProperty(Of String)(Field08TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type08")))
                                LoadProperty(Of String)(Field09TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type09")))
                                LoadProperty(Of String)(Field10TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type10")))
                                LoadProperty(Of String)(Field11TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type11")))
                                LoadProperty(Of String)(Field12TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type12")))
                                LoadProperty(Of String)(Field13TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type13")))
                                LoadProperty(Of String)(Field14TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type14")))
                                LoadProperty(Of String)(Field15TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type15")))
                                LoadProperty(Of String)(Field16TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type16")))
                                LoadProperty(Of String)(Field17TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type17")))
                                LoadProperty(Of String)(Field18TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type18")))
                                LoadProperty(Of String)(Field19TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type19")))
                                LoadProperty(Of String)(Field20TypeProperty, EvalFormFieldTypes.GetValueByKey(.GetString("Type20")))

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

                                ' Form Field Validations
                                LoadProperty(Of String)(Field01ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate01")))
                                LoadProperty(Of String)(Field02ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate02")))
                                LoadProperty(Of String)(Field03ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate03")))
                                LoadProperty(Of String)(Field04ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate04")))
                                LoadProperty(Of String)(Field05ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate05")))
                                LoadProperty(Of String)(Field06ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate06")))
                                LoadProperty(Of String)(Field07ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate07")))
                                LoadProperty(Of String)(Field08ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate08")))
                                LoadProperty(Of String)(Field09ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate09")))
                                LoadProperty(Of String)(Field10ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate10")))
                                LoadProperty(Of String)(Field11ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate11")))
                                LoadProperty(Of String)(Field12ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate12")))
                                LoadProperty(Of String)(Field13ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate13")))
                                LoadProperty(Of String)(Field14ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate14")))
                                LoadProperty(Of String)(Field15ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate15")))
                                LoadProperty(Of String)(Field16ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate16")))
                                LoadProperty(Of String)(Field17ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate17")))
                                LoadProperty(Of String)(Field18ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate18")))
                                LoadProperty(Of String)(Field19ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate19")))
                                LoadProperty(Of String)(Field20ValidationProperty, Lookup.FormValidationList.GetValueByKey(.GetInt32("Validate20")))
                            End With
                        End Using
                    End Using
                End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace