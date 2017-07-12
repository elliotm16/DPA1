Imports System.IO
Imports System.Text

Public Class Form3

    'Name of disk file

    Private Const strFileName As String = "File1.txt"

    'Number of records

    Private intNumRecs As Integer = 0

    'Index number of current record

    Private intCurrRec As Integer = 0

    'Text of current record

    Private strRecord As String = ""

    'Total length of record: extra 2 bytes are for CR + LF characters

    Private intRecLen As Integer = intStockIDLen + intDescriptionLen + intPriceLen + intQuantityInStockLen + intReorderLevelLen + intReorderQuantityLen + intDateLastOrderLen + intWhetherReceivedLen + 2

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ReadFile()

        'If there are any records

        If intNumRecs > 0 Then

            'Start at the first

            intCurrRec = 1

            'Get and display it

            ReadRecord()

        Else

            ShowPosition()

        End If

    End Sub

    Private Sub ReadFile()

        'Create file if it does not exist

        If Not File.Exists(strFileName) Then

            File.Create(strFileName)

        End If

        'Calculate number of records from length of file

        intNumRecs = CInt(FileLen(strFileName) / intRecLen)

    End Sub

    Private Sub ShowPosition()

        lblWhere.Text = intCurrRec & "/" & intNumRecs

    End Sub

    Private Sub ReadRecord()

        'Open an already-existing file for reading

        Dim FS As New FileStream(strFileName, FileMode.Open, FileAccess.Read)

        'Set the file pointer

        FS.Seek((intCurrRec - 1) * intRecLen, SeekOrigin.Begin)

        'Read bytes at this position into a byte array

        Dim byteArray(intRecLen) As Byte

        FS.Read(byteArray, 0, intRecLen)

        FS.Close()

        'Convert byte array to a string:

        Dim enc As New UTF8Encoding

        strRecord = enc.GetString(byteArray)

        'Display current record

        OutputRecord()

    End Sub

    Private Sub OutputRecord()

        'Extract fields and output to TextBoxes

        txtStockID.Text = strRecord.Substring(0, intStockIDLen)

        txtDescription.Text = strRecord.Substring(intStockIDLen, intDescriptionLen)

        txtPrice.Text = strRecord.Substring(intStockIDLen + intDescriptionLen, intPriceLen)

        txtQuantityInStock.Text = strRecord.Substring(intStockIDLen + intDescriptionLen + intPriceLen, intQuantityInStockLen)

        txtReorderLevel.Text = strRecord.Substring(intStockIDLen + intDescriptionLen + intPriceLen + intQuantityInStockLen, intReorderLevelLen)

        txtReorderQuantity.Text = strRecord.Substring(intStockIDLen + intDescriptionLen + intPriceLen + intQuantityInStockLen + intReorderLevelLen, intReorderQuantityLen)

        txtDateLastOrder.Text = strRecord.Substring(intStockIDLen + intDescriptionLen + intPriceLen + intQuantityInStockLen + intReorderLevelLen + intReorderQuantityLen, intDateLastOrderLen)

        txtWhetherReceived.Text = strRecord.Substring(intStockIDLen + intDescriptionLen + intPriceLen + intQuantityInStockLen + intReorderLevelLen + intReorderQuantityLen + intDateLastOrderLen)

    End Sub

    'Get fields from TextBoxes and format into record

    Private Function InputRecord() As String

        'Format string fields to specified length with spaces on right
        'and numerical fields (or ID) to specified length with zeros on left:

        Dim strStockID As String = txtStockID.Text.PadLeft(intStockIDLen, ChrW(48))

        Dim strDescription As String = txtDescription.Text.PadRight(intDescriptionLen, ChrW(32))

        Dim strPrice As String = txtPrice.Text.PadLeft(intPriceLen, ChrW(48))

        Dim strQuantityInStock As String = txtQuantityInStock.Text.PadLeft(intQuantityInStockLen, ChrW(48))

        Dim strReorderLevel As String = txtReorderLevel.Text.PadLeft(intReorderLevelLen, ChrW(48))

        Dim strReorderQuantity As String = txtReorderQuantity.Text.PadLeft(intReorderQuantityLen, ChrW(48))

        Dim strDateLastOrder As String = txtDateLastOrder.Text.PadRight(intDateLastOrderLen, ChrW(32))

        Dim strWhetherReceived As String = txtWhetherReceived.Text.PadRight(intWhetherReceivedLen, ChrW(32))

        'Concatenate fields and return record
        Return strStockID & strDescription & strPrice & strQuantityInStock & strReorderLevel & strReorderQuantity & strDateLastOrder & strWhetherReceived & vbCrLf

    End Function

    'Remove all displayed fields

    Private Sub ClearBoxes()

        txtStockID.Clear()

        txtDescription.Clear()

        txtPrice.Clear()

        txtQuantityInStock.Clear()

        txtReorderLevel.Clear()

        txtReorderQuantity.Clear()

        txtDateLastOrder.Clear()

        txtWhetherReceived.Clear()

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        'If there is a next row

        If intCurrRec < intNumRecs Then

            'select it

            intCurrRec += 1

            'Display current record

            ReadRecord()

        End If

    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click

        'If there is a next row
        If intCurrRec < intNumRecs Then

            'select it
            intCurrRec -= 1

            'Display current record
            ReadRecord()

        End If

    End Sub

    Private Sub btnInsert_Click(sender As System.Object, e As System.EventArgs) Handles btnInsert.Click

        'Get record from TextBoxes

        strRecord = InputRecord()

        'Open file stream

        Dim FS As New FileStream(strFileName, FileMode.Open, FileAccess.Write)

        'Move pointer to end of file

        FS.Seek(intNumRecs * intRecLen, SeekOrigin.Begin)

        'Convert record to byte array and write this to file

        Dim enc As New UTF8Encoding

        FS.Write(enc.GetBytes(strRecord), 0, intRecLen)

        FS.Close()

        'Increment number of records

        intNumRecs += 1

        'Set current record to new last one

        intCurrRec = intNumRecs

        'Display position in file

        ShowPosition()

    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click

        'Cannot amend a record if none exist

        If intCurrRec > 0 Then

            'Get record from TextBoxes

            strRecord = InputRecord()

            'Open file stream

            Dim FS As New FileStream(strFileName, FileMode.Open, FileAccess.Write)

            'Move pointer to byte position of current record

            FS.Seek((intCurrRec - 1) * intRecLen, SeekOrigin.Begin)

            'Write byte array at current position

            Dim enc As New UTF8Encoding

            FS.Write(enc.GetBytes(strRecord), 0, intRecLen)

            FS.Close()

        End If

    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click

        'Skip if there are no records

        If intNumRecs = 0 Then Exit Sub

        'Remove record by copying file

        CopyWithDelete()

        'Reduce the count of records

        intNumRecs -= 1

        'If it was the last record

        If intNumRecs = 0 Then

            'there is no current one

            intCurrRec = 0

            ClearBoxes()

            'Show position of this record in the file

            ShowPosition()

        Else

            'If it was the end one

            If intCurrRec > intNumRecs Then

                'Move the selected row up

                intCurrRec -= 1

            End If

            'Read record at the updated position

            ReadRecord()

        End If

    End Sub

    'Remove record

    Private Sub CopyWithDelete()

        'Open disk file

        Dim FSOld As New FileStream(strFileName, FileMode.Open, FileAccess.Read)

        'Open new version of the file

        Dim FSNew As New FileStream("Temp", FileMode.Create, FileAccess.Write)

        'Byte array to temporarily store record

        Dim byteArray(intRecLen) As Byte

        'Number of records written

        Dim intWrite As Integer = 0

        'For each record to read

        For intRead As Integer = 0 To intNumRecs - 1

            'Leave out the current record

            If intRead <> intCurrRec - 1 Then

                'Set old file pointer to the next record

                FSOld.Seek(intRead * intRecLen, SeekOrigin.Begin)

                'Read bytes at this position into a byte array

                FSOld.Read(byteArray, 0, intRecLen)

                'Set new file pointer to next record

                FSNew.Seek(intWrite * intRecLen, SeekOrigin.Begin)

                'Write record

                FSNew.Write(byteArray, 0, intRecLen)

                'Count number of records written

                intWrite += 1

            End If

        Next

        FSOld.Close()

        FSNew.Close()

        'Delete the old file

        My.Computer.FileSystem.DeleteFile(strFileName)

        'Give the new file the name of the old file

        My.Computer.FileSystem.RenameFile("Temp", strFileName)

    End Sub

End Class