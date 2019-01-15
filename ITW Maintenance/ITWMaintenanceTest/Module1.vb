Imports ITWMaintenance.Library.Nursing.Diagnoses

Module Module1

    Sub Main()
        Dim diagnosis As Diagnosis = diagnosis.GetDiagnosis(1)

        Console.WriteLine("Diagnosis: " & diagnosis.Description & " (" & diagnosis.ID & ") ")

        For Each linkedForm As DiagnosisForm In diagnosis.LinkedForms
            Console.WriteLine(vbTab & linkedForm.FormName & " (" & linkedForm.FormID & ")")
        Next

        Console.ReadLine()
    End Sub

End Module
