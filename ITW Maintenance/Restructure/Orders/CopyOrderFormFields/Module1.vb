Imports System.Data.SqlClient
Imports System.Configuration

Module Module1

    ''' <summary>
    ''' This program populates the new [109IntOrderField] table based
    ''' on the [109IntOrderEl] table.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Main()
        Console.WriteLine("This program will build the [109IntOrderField] table" & vbCrLf & _
                          "based on the records in [109IntOrderEl], [109IntOrderCombo], and [109IntOrderRadio]." & _
                          vbCrLf & vbCrLf & "Press Enter to continue, or close this window to abort the process." & vbCrLf)

        Console.ReadLine()

        Dim orderID As Integer = 0
        Dim sql As String
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ITW").ConnectionString)
        conn.Open()
        Dim cmd As New SqlCommand()
        cmd.Connection = conn

        Console.Write("Clearing the [109IntOrderField] table... ")

        sql = "DELETE FROM [109IntOrderField] "

        cmd.CommandText = sql
        cmd.ExecuteNonQuery()

        Console.WriteLine("done!" & vbCrLf & vbCrLf & "Preparing [109IntOrderEl] information...")

        ' Get the [109IntOrderEl] records
        sql = "SELECT * FROM [109IntOrderEl] "
        sql &= "ORDER BY IntOrderID "

        Dim da As New SqlDataAdapter(sql, conn)
        Dim ds As New DataSet
        da.Fill(ds, "Elements")


        Dim label As String = ""
        Dim type As String = ""
        Dim options As String = ""
        Dim comboCount As Integer = 0
        Dim radioCount As Integer = 0

        For Each row As DataRow In ds.Tables("Elements").Rows
            orderID = row("IntOrderID")
            Console.WriteLine("   Processing Order #" & orderID)
            comboCount = 0
            radioCount = 0

            ' Loop the fields and add them to the [109IntOrderField] table
            For field As Integer = 1 To 20
                label = ""
                type = ""
                options = ""

                If field < 10 Then
                    If Not IsDBNull(row("label0" & field)) Then
                        label = row("label0" & field)
                    End If
                    If Not IsDBNull(row("type0" & field)) Then
                        type = LCase(row("type0" & field))
                    End If
                Else
                    If Not IsDBNull(row("label" & field)) Then
                        label = row("label" & field)
                    End If
                    If Not IsDBNull(row("type" & field)) Then
                        type = LCase(row("type" & field))
                    End If
                End If

                ' Pull in any combo values
                If type = "combobox" Then
                    comboCount += 1

                    sql = "SELECT optionValue FROM [109IntOrderCombo] "
                    sql &= "WHERE elementID = " & orderID & " "
                    sql &= "AND comboNo = " & comboCount & " "
                    sql &= "ORDER BY sequence "

                    cmd.CommandText = sql
                    Using dr As SqlDataReader = cmd.ExecuteReader
                        While dr.Read()
                            options &= dr(0) & vbCrLf
                        End While
                        If Len(options) > 0 Then
                            options = Left(options, Len(options) - 2)
                        End If
                    End Using
                ElseIf type = "radio" Then
                    radioCount += 1

                    sql = "SELECT radioValue FROM [109IntOrderRadio] "
                    sql &= "WHERE elementID = " & orderID & " "
                    sql &= "AND radioNo = " & radioCount & " "
                    sql &= "ORDER BY sequence "

                    cmd.CommandText = sql
                    Using dr As SqlDataReader = cmd.ExecuteReader
                        While dr.Read()
                            options &= dr(0) & vbCrLf
                        End While
                        If Len(options) > 0 Then
                            options = Left(options, Len(options) - 2)
                        End If
                    End Using
                End If

                If Not String.IsNullOrEmpty(Trim(label)) Then
                    sql = "INSERT INTO [109IntOrderField] (IntOrderID, [FieldNumber], [Label], [Type], [Options]) VALUES ("
                    sql &= orderID & ", "
                    sql &= field & ", "
                    sql &= "'" & Replace(label, "'", "''") & "', "
                    sql &= "'" & Replace(type, "'", "''") & "', "
                    sql &= "'" & Replace(options, "'", "''") & "') "

                    cmd.CommandText = sql
                    cmd.ExecuteNonQuery()
                End If
            Next
        Next

        Console.WriteLine("Complete.")
    End Sub

End Module
