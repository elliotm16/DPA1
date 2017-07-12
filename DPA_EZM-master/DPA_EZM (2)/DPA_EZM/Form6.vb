Imports System.Data.OleDb

Public Class Form6

    'Database interface objects

    Private CN As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Stock.mdb;")

    'Moves data between program and database

    Private DA As OleDbDataAdapter

    'Data structure to store records from the DB

    Private DT As New DataTable

    Private Sub Form6_Load(sender As System.Object, e As System.EventArgs)

        'Set up properties of DataGridView

        InitDGV()

        'Get records from DB

        ReadDB()

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

    'Get records from DB

    Private Sub ReadDB()

        'Give SELECT query string to DataAdapter

        DA = New OleDbDataAdapter("SELECT * FROM tblItem", CN)

        'Fill DataTable

        DA.Fill(DT)

        'Bind DataGridView to DataTable

        DGV.DataSource = DT

    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click

        'Create new row for DataTable

        Dim DR As DataRow = DT.NewRow

        'Fill fields from TextBoxes

        DR("StockID") = txtStockID.Text

        DR("Description") = txtDescription.Text

        'Convert to a Single

        DR("Price") = CSng(txtPrice.Text)

        'Convert to Integers, ensuring a default value of 0

        DR("QuantityInStock") = CInt("0" & txtQuantityInStock.Text)

        DR("ReorderLevel") = CInt("0" & txtReorderLevel.Text)

        DR("ReorderQuantity") = CInt("0" & txtReorderQuantity.Text)

        'Convert to a Date

        DR("DateLastOrder") = CDate(txtDateLastOrder.Text)

        'Convert to a Boolean

        DR("WhetherReceived") = CBool(txtWhetherReceived.Text)

        'Add row to DataTable

        DT.Rows.Add(DR)

        'Select just added record

        DGV.CurrentCell = DGV.Item(0, DGV.RowCount - 1)

    End Sub

    Private Sub btnAmend_Click(sender As System.Object, e As System.EventArgs) Handles btnAmend.Click

        DGV.Item("StockID", DGV.CurrentRow.Index).Value = txtStockID.Text

        DGV.Item("Description", DGV.CurrentRow.Index).Value = txtDescription.Text

        DGV.Item("Price", DGV.CurrentRow.Index).Value = CStr(txtPrice.Text)

        DGV.Item("QuantityInStock", DGV.CurrentRow.Index).Value = CInt("0" & txtQuantityInStock.Text)

        DGV.Item("ReorderLevel", DGV.CurrentRow.Index).Value = CInt("0" & txtReorderLevel.Text)

        DGV.Item("ReorderQuantity", DGV.CurrentRow.Index).Value = CInt("0" & txtReorderQuantity.Text)

        DGV.Item("DateLastOrder", DGV.CurrentRow.Index).Value = CDate(txtDateLastOrder.Text)

        DGV.Item("WhetherReceived", DGV.CurrentRow.Index).Value = CBool(txtWhetherReceived.Text)

        'Write changes into DataTable

        DGV.BindingContext(DT).EndCurrentEdit()

    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click

        'Skip if there are no records

        If DGV.RowCount = 0 Then Exit Sub

        'Memorise selected row

        Dim intSelRow = DGV.CurrentRow.Index

        'Remove the selected record

        DGV.Rows.RemoveAt(intSelRow)

        'If it was the last record

        If DGV.RowCount = 0 Then

            ClearBoxes()

        Else

            'If it was the end one

            If intSelRow = DGV.RowCount Then

                'Move the selected row up

                intSelRow -= 1

            End If

            DGV.CurrentCell = DGV.Item(0, intSelRow)

            DGV.Item(0, intSelRow).Selected = True

        End If

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

       Dim CB As OleDbCommandBuilder = New OleDbCommandBuilder(DA)

        DA.Update(DT)

    End Sub

End Class