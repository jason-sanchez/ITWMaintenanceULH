Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Module Module1

    Sub Main()
        Dim conn As New SqlConnection(ConnectionStrings("ITW").ConnectionString)

        Console.WriteLine("Connecting to: " & ConnectionStrings("ITW").ConnectionString & ".  Press ENTER to continue...")
        Console.ReadLine()

        Try
            conn.Open()
            Console.WriteLine("Connection opened...")

            Try
                ProcessRadios(conn)
                ProcessCombos(conn)
            Catch ex As Exception
                Console.WriteLine("ERROR: " & ex.ToString())
            Finally
                conn.Close()
            End Try

            Console.WriteLine("Process Finished.")
        Catch ex As Exception
            Console.WriteLine("ERROR: Unable to open the connection.")
        End Try

        Console.ReadLine()
    End Sub

    Private Sub ProcessRadios(ByRef conn As SqlConnection)
        Dim sql As String
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Dim dr As DataRow

        Console.WriteLine("Processing Radio Values...")

        ' Get all eval forms that have radio buttons
        sql = "SELECT r.radioValue, r.radioNo, el.[ID] as ElID, "
        sql &= "el.type01, el.type02, el.type03, el.type04, el.type05, "
        sql &= "el.type06, el.type07, el.type08, el.type09, el.type10, "
        sql &= "el.type11, el.type12, el.type13, el.type14, el.type15, "
        sql &= "el.type16, el.type17, el.type18, el.type19, el.type20, "
        sql &= "el.Field01Options, el.Field02Options, el.Field03Options, el.Field04Options, el.Field05Options, "
        sql &= "el.Field06Options, el.Field07Options, el.Field08Options, el.Field09Options, el.Field10Options, "
        sql &= "el.Field11Options, el.Field12Options, el.Field13Options, el.Field14Options, el.Field15Options, "
        sql &= "el.Field16Options, el.Field17Options, el.Field18Options, el.Field19Options, el.Field20Options "
        sql &= "FROM [100CrNtRadio] r "
        sql &= "INNER JOIN [100CrNtEl] el ON r.elementID = el.[ID] "
        sql &= "ORDER BY r.elementID, r.radioNo, r.[sequence] "
        da = New SqlDataAdapter(sql, conn)
        da.Fill(ds)

        ' Loop the table
        ' To make the program run faster, we will not query for
        ' the current options set before appending. Instead, we
        ' will use what has been downloaded above, which becomes
        ' incorrect after we append the first option to it. To get
        ' around this, keep track of the currentElID and build
        ' the options using a local variable.  If the currentElID
        ' doesn't match the current records's ElID and the options
        ' variable contains data, fire an update.
        Dim currentElID As Integer = 0
        Dim options(19) As String

        Console.WriteLine("Processing " & ds.Tables(0).Rows.Count & " records...")
        For Each dr In ds.Tables(0).Rows
            If currentElID <> dr.Item("ElID") Then
                If currentElID > 0 Then
                    UpdateOptions(currentElID, conn, options)
                End If

                ' Clear the options
                For i As Integer = 0 To 19
                    options(i) = Nothing
                Next

                ' Grab the new ElID
                currentElID = dr("ElID")
            End If

            ' Find the Options field to plug the current option into
            Dim radioCount As Integer = 0

            ' Loop the fields to find the one this combo value matches up with
            For i As Integer = 1 To 20
                Dim fieldNum As String = i.ToString()

                If i < 10 Then
                    fieldNum = "0" & fieldNum
                End If

                ' What type of field is this?
                If Not IsDBNull(dr("type" & fieldNum)) AndAlso LCase(dr("type" & fieldNum)) = "radio" Then
                    radioCount += 1

                    ' Is this the right one?
                    If Not IsDBNull(dr("radioNo")) Then
                        If dr("radioNo") = radioCount Then
                            ' We need to append the option if there are already some there
                            If Not String.IsNullOrEmpty(options(i - 1)) Then
                                options(i - 1) &= vbCrLf & dr("radioValue")
                            Else
                                options(i - 1) = dr("radioValue")
                            End If
                            Exit For
                        End If
                    End If
                End If
            Next
        Next

        ' Cleanup
        If currentElID > 0 Then
            UpdateOptions(currentElID, conn, options)
        End If
    End Sub

    Private Sub ProcessCombos(ByRef conn As SqlConnection)
        Dim sql As String
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Dim dr As DataRow

        Console.WriteLine("Processing Combo Values...")

        ' Get all eval forms that have a combobox or radio buttons
        sql = "SELECT c.optionValue, c.comboNo, el.[ID] as ElID, "
        sql &= "el.type01, el.type02, el.type03, el.type04, el.type05, "
        sql &= "el.type06, el.type07, el.type08, el.type09, el.type10, "
        sql &= "el.type11, el.type12, el.type13, el.type14, el.type15, "
        sql &= "el.type16, el.type17, el.type18, el.type19, el.type20, "
        sql &= "el.Field01Options, el.Field02Options, el.Field03Options, el.Field04Options, el.Field05Options, "
        sql &= "el.Field06Options, el.Field07Options, el.Field08Options, el.Field09Options, el.Field10Options, "
        sql &= "el.Field11Options, el.Field12Options, el.Field13Options, el.Field14Options, el.Field15Options, "
        sql &= "el.Field16Options, el.Field17Options, el.Field18Options, el.Field19Options, el.Field20Options "
        sql &= "FROM [100CrNtCombo] c "
        sql &= "INNER JOIN [100CrNtEl] el ON c.elementID = el.[ID] "
        sql &= "ORDER BY c.elementID, c.comboNo, c.[sequence] "
        da = New SqlDataAdapter(sql, conn)
        da.Fill(ds)

        ' Loop the table
        ' To make the program run faster, we will not query for
        ' the current options set before appending. Instead, we
        ' will use what has been downloaded above, which becomes
        ' incorrect after we append the first option to it. To get
        ' around this, keep track of the currentElID and build
        ' the options using a local variable.  If the currentElID
        ' doesn't match the current records's ElID and the options
        ' variable contains data, fire an update.
        Dim currentElID As Integer = 0
        Dim options(19) As String

        Console.WriteLine("Processing " & ds.Tables(0).Rows.Count & " records...")
        For Each dr In ds.Tables(0).Rows
            If currentElID <> dr.Item("ElID") Then
                If currentElID > 0 Then
                    UpdateOptions(currentElID, conn, options)
                End If

                ' Clear the options
                For i As Integer = 0 To 19
                    options(i) = Nothing
                Next

                ' Grab the new ElID
                currentElID = dr("ElID")
            End If

            ' Find the Options field to plug the current option into
            Dim comboCount As Integer = 0

            ' Loop the fields to find the one this combo value matches up with
            For i As Integer = 1 To 20
                Dim fieldNum As String = i.ToString()

                If i < 10 Then
                    fieldNum = "0" & fieldNum
                End If

                ' What type of field is this?
                If Not IsDBNull(dr("type" & fieldNum)) AndAlso LCase(dr("type" & fieldNum)) = "combobox" Then
                    comboCount += 1

                    ' Is this the right one?
                    If Not IsDBNull(dr("comboNo")) Then
                        If dr("comboNo") = comboCount Then
                            ' We need to append the option if there are already some there
                            If Not String.IsNullOrEmpty(options(i - 1)) Then
                                options(i - 1) &= vbCrLf & dr("optionValue")
                            Else
                                options(i - 1) = dr("optionValue")
                            End If
                            Exit For
                        End If
                    End If
                End If
            Next
        Next

        ' Cleanup
        If currentElID > 0 Then
            UpdateOptions(currentElID, conn, options)
        End If
    End Sub

    Private Sub UpdateOptions(ByVal currentElID As Integer, ByRef conn As SqlConnection, ByRef options As String())
        Dim sql As String
        ' Update the options for the previous form
        sql = "UPDATE [100CrNtEl] SET "

        For i As Integer = 0 To 19
            Dim fieldNum As String = i + 1

            If (i + 1) < 10 Then
                fieldNum = "0" & fieldNum
            End If

            If Not String.IsNullOrEmpty(options(i)) Then
                sql &= "Field" & fieldNum & "Options = '" & Replace(options(i), "'", "''") & "', "
                'Else
                '    sql &= "Field" & fieldNum & "Options = NULL, "
            End If
        Next

        If InStr(sql, ",") > 0 Then
            ' Trim off the last ", "
            sql = Left(sql, Len(sql) - 2) & " "
            sql &= "WHERE [ID] = " & currentElID & " "

            Console.WriteLine("  Updating Form ([100CrNtEl].ID): " & currentElID)
            Dim cmd As New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End If
    End Sub

End Module
