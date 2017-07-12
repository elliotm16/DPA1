Imports System.IO

Public Class Form4

    'Name of disk file

    Private Const strFileName As String = "File4.xml"

    'Number of records

    Private intNumRecs As Integer = 0

    'Index number of current record

    Private intCurrRec As Integer = 0

    'Data structure to store records

    Private DT As New DataTable("Items")

    'Used to intercept automatic events

    Private blnStopAuto As Boolean = False

    Private Sub Form4_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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

    'Create contents of DataTable

    Private Sub InitDT()

        With DT.Columns

            .Add(New DataColumn("StockID", GetType(String)))

            .Add(New DataColumn("Description", GetType(String)))

            .Add(New DataColumn("Price", GetType(Single)))

            .Add(New DataColumn("QuantityInStock", GetType(Integer)))

            .Add(New DataColumn("ReorderLevel", GetType(Integer)))

            .Add(New DataColumn("ReorderQuantity", GetType(Integer)))

            .Add(New DataColumn("DateLastOrder", GetType(String)))

            .Add(New DataColumn("WhetherReceived", GetType(Boolean)))

        End With

    End Sub

    Private Sub ReadFile()

        'Create file if it doesn't exist:

        Dim FS As New FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Read)

        'If there are any records in file

        If FileLen(strFileName) > 0 Then

            'Load data into DataTable

            DT.ReadXmlSchema(strFileName)

            DT.ReadXml(strFileName)

            'Discover how many records in file

            intNumRecs = DT.Rows.Count

            'Start at first record

            intCurrRec = 1

            'Display current record

            OutputRecord()

        Else 'File is empty

            'Set up DataTable

            InitDT()

        End If

        FS.Close()

        'Prevent DGV autoevents firing

        blnStopAuto = True

        'Display DataTable in DataGridView

        DGV.DataSource = DT

        blnStopAuto = False

    End Sub

    'Display current record in TextBoxes

    Private Sub OutputRecord()

        txtStockID.Text = DT(intCurrRec - 1)("StockID").ToString

        txtDescription.Text = DT(intCurrRec - 1)("Description").ToString

        txtPrice.Text = DT(intCurrRec - 1)("Price").ToString

        txtQuantityInStock.Text = DT(intCurrRec - 1)("QuantityInStock").ToString

        txtReorderLevel.Text = DT(intCurrRec - 1)("ReorderLevel").ToString

        txtReorderQuantity.Text = DT(intCurrRec - 1)("ReorderQuantity").ToString

        txtDateLastOrder.Text = DT(intCurrRec - 1)("DateLastOrder").ToString

        txtWhetherReceived.Text = DT(intCurrRec - 1)("WhetherReceived").ToString

        ShowPosition()

    End Sub

    Private Sub InputRecord(ByRef DR As DataRow)

        'Get fields from Textboxes

        DR("StockID") = txtStockID.Text

        DR("Description") = txtDescription.Text

        DR("Price") = txtPrice.Text

        'Prevent errors by forcing default zero

        DR("QuantityInStock") = CInt("0" & txtQuantityInStock.Text)

        DR("ReorderLevel") = CInt("0" & txtReorderLevel.Text)

        DR("ReorderQuantity") = CInt("0" & txtReorderQuantity.Text)

        DR("DateLastOrder") = txtDateLastOrder.Text

        DR("WhetherReceived") = txtWhetherReceived.Text

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

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs)

        'New DataRow for DataTable

        Dim DR As DataRow = DT.NewRow

        'Get contents of DataRow

        InputRecord(DR)

        'Add DataRow to DataTable, without unwanted consequences

        blnStopAuto = True

        DT.Rows.Add(DR)

        blnStopAuto = False

        'Increment number of records

        intNumRecs += 1

        'select it

        intCurrRec = intNumRecs

        DGV.CurrentCell = DGV.Item(0, intCurrRec - 1)

    End Sub

    Private Sub btnAmend_Click(sender As System.Object, e As System.EventArgs)

        'Send current row to be updated

        InputRecord(DT.Rows(intCurrRec - 1))

    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs)

        'Skip if there are no records

        If intNumRecs = 0 Then Exit Sub

        'Remove the selected record, without unwanted consequences

        blnStopAuto = True

        DT.Rows.RemoveAt(intCurrRec - 1)

        blnStopAuto = False

        'Reduce the count of records

        intNumRecs -= 1

        'If it was the last record

        If intNumRecs = 0 Then

            intCurrRec = 0

            ClearBoxes()

            ShowPosition()

        Else

            'If it was the end one

            If intCurrRec > intNumRecs Then

                'Move the selected row up

                intCurrRec -= 1

            End If

            'necessary to explicitly update selection as well as currency

            DGV.CurrentCell = DGV.Item(0, intCurrRec - 1)

            DGV.Item(0, intCurrRec - 1).Selected = True

            'Update selection

            OutputRecord()

        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        DT.WriteXmlSchema(strFileName)

        DT.WriteXml(strFileName)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellContentClick, DataGridView1.CellContentClick

    End Sub

    Private Sub btnReport_Click(sender As System.Object, e As System.EventArgs) Handles btnReport.Click

        'Line to write to file

        Dim strLine As String

        'Stock value variables

        Dim sngTotalHeld As Single = 0

        Dim sngTotalOnOrder As Single = 0

        'Open disk file stream for writing:

        Dim SW As New StreamWriter("StockReport.csv")

        'Add abbreviated headings to the file:

        SW.WriteLine("StockID,Description,InStock,ReOlevel,Ordered,Quantity,Days ago")

        'For all the rows of the DataTable

        For Each Row As DataRow In DT.Rows

            'Update total value of stock held

            sngTotalHeld += CSng(Row("Price")) * CInt(Row("QuantityInStock"))

            'If stock level too low

            If CInt(Row("QuantityInStock")) <= CInt(Row("ReorderLevel")) Then

                'Build output line

                strLine = "'" & Row("StockID") & "," & Row("Description") & "," & Row("QuantityInStock") & "," & Row("ReorderLevel")

                If Row("WhetherReceived") = "True" Then

                    strLine &= ",No"

                Else

                    strLine &= ",Yes," & Row("ReorderQuantity") & "," & DateDiff(DateInterval.Day, CDate(Row("DateLastOrder")), Today).ToString()

                    'Update total of value on order

                    sngTotalOnOrder += CSng(Row("Price")) * CInt(Row("ReorderQuantity"))

                End If

                'Add new Item line to file

                SW.WriteLine(strLine)

            End If

        Next

        'Add summary information

        SW.WriteLine()

        SW.WriteLine(",Total value of stock held: £," & sngTotalHeld)

        SW.WriteLine(",Total value of stock on order: £," & sngTotalOnOrder)

        'Close stream

        SW.Close()

        'Open text file in Excel

        System.Diagnostics.Process.Start("StockReport.csv")

    End Sub
End Class