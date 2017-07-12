'Library needed for i/o streams:

Imports System.IO

Public Class Form2

    'Name of disk file
    Private Const strFileName As String = "File2.txt"

    'Number of records
    Private intNumRecs As Integer = 0

    'Index number of current record
    Private intCurrRec As Integer = 0

    'List to store records
    Private RecordList As New List(Of String)

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ReadFile()

        'If there are any records
        If intNumRecs > 0 Then

            'start at the first
            intCurrRec = 1

            'Display current record
            OutputRecord()

        Else

            ShowPosition()

        End If

    End Sub

    Private Sub ReadFile()

        'Open file or create a new one if it does not exist
        Dim FS As New FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Read)

        'Associate StreamReader with file
        Dim SR As New StreamReader(FS)

        'Read all lines from file
        Do Until SR.EndOfStream

            'and add to List
            RecordList.Add(SR.ReadLine)

            'Count one more record
            intNumRecs += 1

        Loop

        'Close stream and file

        SR.Close()

        FS.Close()

    End Sub

    Private Sub OutputRecord()

        'Divide current record into fields
        Dim strFields() As String = RecordList(intCurrRec - 1).Split(Chr(124))

        '***WARNING*** Assumes the order of the fields

        txtStockID.Text = strFields(0)

        txtDescription.Text = strFields(1)

        txtPrice.Text = strFields(2)

        txtQuantityInStock.Text = strFields(3)

        txtReorderLevel.Text = strFields(4)

        txtReorderQuantity.Text = strFields(5)

        txtDateLastOrder.Text = strFields(6)

        txtWhetherReceived.Text = strFields(7)

        'Show position in file
        ShowPosition()

    End Sub

    'Show position of record in file

    Private Sub ShowPosition()

        lblWhere.Text = intCurrRec & "/" & intNumRecs

    End Sub

    'Format contents of TextBoxes into record and return it

    Private Function InputRecord() As String

        'Get fields from Textboxes
        'There is no need to format them to fixed length

        Dim strStockID As String = txtStockID.Text

        Dim strDescription As String = txtDescription.Text

        Dim strPrice As String = txtPrice.Text

        Dim strQuantityInStock As String = txtQuantityInStock.Text

        Dim strReorderLevel As String = txtReorderLevel.Text

        Dim strReorderQuantity As String = txtReorderQuantity.Text

        Dim strDateLastOrder As String = txtDateLastOrder.Text

        Dim strWhetherReceived As String = txtWhetherReceived.Text

        'Return formatted together with end-of-field markers
        Return strStockID & "|" & strDescription & "|" & strPrice & "|" & strQuantityInStock & "|" & strReorderLevel & "|" & strReorderQuantity & "|" & strDateLastOrder & "|" & strWhetherReceived

    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Add new record to list

        RecordList.Add(InputRecord())

        'Increment number of records

        intNumRecs += 1

        'select it

        intCurrRec = intNumRecs

        'echo back for checking

        OutputRecord()

    End Sub

    Private Sub btnAmend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Replace record in list
        RecordList(intCurrRec - 1) = InputRecord()

        'echo back for checking
        OutputRecord()

    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Skip if there are no records

        If intNumRecs = 0 Then Exit Sub

        'Remove current record

        RecordList.RemoveAt(intCurrRec - 1)

        'Reduce the count of records

        intNumRecs -= 1

        'If it was the last record

        If intNumRecs = 0 Then

            'there is no current one

            intCurrRec = 0

            'clear the TextBoxes

            ClearBoxes()

            'Show position of this record in the file

            ShowPosition()

        Else

            'If it was the end one

            If intCurrRec > intNumRecs Then

                'Move the selected row up

                intCurrRec -= 1

            End If

            'Update selection

            OutputRecord()

        End If

    End Sub

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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Open disk file stream for writing

        'Overwrites file with new contents

        Dim SW As New StreamWriter(strFileName)

        For Each Item In RecordList

            'Write to file

            SW.WriteLine(Item)

        Next

        'Close stream

        SW.Close()

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.click

        'If there is a next row

        If intCurrRec < intNumRecs Then

            'select it

            intCurrRec += 1

            'Display current record

            OutputRecord()

        End If

    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click

        'If there is a next row
        If intCurrRec < intNumRecs Then

            'select it
            intCurrRec -= 1

            'Display current record
            OutputRecord()

        End If

    End Sub

    Private Sub Form2_Load_1(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class