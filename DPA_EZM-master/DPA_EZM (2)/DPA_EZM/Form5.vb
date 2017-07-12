Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Form5

    'Name of disk file

    Private Const strFileName As String = "File5.bin"

    'Number of records

    Private intNumRecs As Integer = 0

    'Index number of current record

    Private intCurrRec As Integer = 0

    'List to store Item objects

    Private ItemList As New List(Of Item)

    'Used to intercept automatic events

    Private blnStopAuto As Boolean = False

    Private Sub Form5_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Set up properties of DataGridView

        InitDGV()

        'Create or open existing file

        ReadFile()

        'Display position in file in case not already done

        ShowPosition()

    End Sub

    Private Sub ShowPosition()

        lblWhere.Text = intCurrRec & "/" & intNumRecs

    End Sub

    'Set up properties of DataGridView

    Private Sub InitDGV()

        With DGV

            .Font = New Font("Microsoft Sans Serif", 11, FontStyle.Regular)
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True

        End With

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

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        'If there is a next record

        If intCurrRec < intNumRecs Then

            'select it

            intCurrRec += 1

            'Handles output record:

            DGV.CurrentCell = DGV.Item(0, intCurrRec - 1)

        End If

    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click

        'If there is a next record

        If intCurrRec < intNumRecs Then

            'select it

            intCurrRec -= 1

            'Handles output record:

            DGV.CurrentCell = DGV.Item(0, intCurrRec - 1)

        End If

    End Sub

    Private Sub ReadFile()

        Dim FS As New FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Read)

        'If there are any records

        If FileLen(strFileName) > 0 Then

            'Read data from file into ItemList

            Dim BF As New BinaryFormatter

            ItemList = DirectCast(BF.Deserialize(FS), List(Of Item))

            'Discover how many records there are

            intNumRecs = ItemList.Count

            'Start at first

            intCurrRec = 1

            'Show current data in grid

            UpdateDGV()

            OutputRecord()

        End If

        FS.Close()

    End Sub

    Private Sub UpdateDGV()

        'Prevent RowEnter firing automatically

        blnStopAuto = True

        With DGV

            'Reset

            .DataSource = Nothing

            'Prevent error when last row removed

            If ItemList.Count > 0 Then

                .DataSource = ItemList

                'Reselect current record

                .CurrentCell = .Item(0, intCurrRec - 1)

            End If

        End With

        blnStopAuto = False

    End Sub

    Private Sub OutputRecord()

        With ItemList(intCurrRec - 1)

            txtStockID.Text = .StockID

            txtDescription.Text = .Description

            txtPrice.Text = .Price.ToString

            txtQuantityInStock.Text = .QuantityinStock.ToString

            txtReorderLevel.Text = .ReorderLevel.ToString

            txtReorderQuantity.Text = .ReorderQuantity.ToString

            txtDateLastOrder.Text = .DateLastOrder.ToString

            txtWhetherReceived.Text = .WhetherReceived.ToString

        End With

        ShowPosition()

    End Sub

    Private Function InputRecord() As Item

        Dim AnItem As New Item

        With AnItem

            'Just take the string:

            .StockID = txtStockID.Text

            .Description = txtDescription.Text

            'Convert to a Single

            .Price = CSng(txtPrice.Text)

            'Convert to Integers, ensuring default value of zero

            .QuantityinStock = CInt("0" & txtQuantityInStock.Text)

            .ReorderLevel = CInt("0" & txtReorderLevel.Text)

            .ReorderQuantity = CInt("0" & txtReorderQuantity.Text)

            'Convert to a Date

            .DateLastOrder = CDate(txtDateLastOrder.Text)

            'Convert to a Boolean

            .WhetherReceived = CBool(txtWhetherReceived.Text)

        End With

        Return AnItem

    End Function

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click

        'Add new record to List

        ItemList.Add(InputRecord)

        'Count it

        intNumRecs += 1

        'Make new one the current one

        intCurrRec = intNumRecs

        'Update display

        UpdateDGV()

        OutputRecord()

    End Sub

    Private Sub btnAmend_Click(sender As System.Object, e As System.EventArgs) Handles btnAmend.Click

        'Overwrite current record with new fields

        ItemList(intCurrRec - 1) = InputRecord()

        'Update display

        UpdateDGV()

        OutputRecord()

    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click

        'Don't try this if there aren’t any records

        If intNumRecs = 0 Then Exit Sub

        ItemList.RemoveAt(intCurrRec - 1)

        intNumRecs -= 1

        'If it was the last one

        If intNumRecs = 0 Then

            intCurrRec = 0

            ClearBoxes()

            ShowPosition()

        Else

            'If it was the end one

            If intCurrRec > intNumRecs Then

                intCurrRec -= 1

            End If

            OutputRecord()

        End If

        UpdateDGV()

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

        Dim FS As New FileStream(strFileName, FileMode.Create)

        Dim BF As New BinaryFormatter

        BF.Serialize(FS, ItemList)

        FS.Close()

    End Sub

End Class