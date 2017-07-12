<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnFormOne = New System.Windows.Forms.Button()
        Me.btnFormTwo = New System.Windows.Forms.Button()
        Me.btnFormThree = New System.Windows.Forms.Button()
        Me.btnFormFour = New System.Windows.Forms.Button()
        Me.btnFormFive = New System.Windows.Forms.Button()
        Me.btnFormSix = New System.Windows.Forms.Button()
        Me.btnFormSeven = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnFormOne
        '
        Me.btnFormOne.Location = New System.Drawing.Point(12, 12)
        Me.btnFormOne.Name = "btnFormOne"
        Me.btnFormOne.Size = New System.Drawing.Size(100, 30)
        Me.btnFormOne.TabIndex = 0
        Me.btnFormOne.Text = "Form1"
        Me.btnFormOne.UseVisualStyleBackColor = True
        '
        'btnFormTwo
        '
        Me.btnFormTwo.Location = New System.Drawing.Point(12, 48)
        Me.btnFormTwo.Name = "btnFormTwo"
        Me.btnFormTwo.Size = New System.Drawing.Size(100, 30)
        Me.btnFormTwo.TabIndex = 1
        Me.btnFormTwo.Text = "Form2"
        Me.btnFormTwo.UseVisualStyleBackColor = True
        '
        'btnFormThree
        '
        Me.btnFormThree.Location = New System.Drawing.Point(12, 84)
        Me.btnFormThree.Name = "btnFormThree"
        Me.btnFormThree.Size = New System.Drawing.Size(100, 30)
        Me.btnFormThree.TabIndex = 2
        Me.btnFormThree.Text = "Form3"
        Me.btnFormThree.UseVisualStyleBackColor = True
        '
        'btnFormFour
        '
        Me.btnFormFour.Location = New System.Drawing.Point(12, 120)
        Me.btnFormFour.Name = "btnFormFour"
        Me.btnFormFour.Size = New System.Drawing.Size(100, 30)
        Me.btnFormFour.TabIndex = 3
        Me.btnFormFour.Text = "Form4"
        Me.btnFormFour.UseVisualStyleBackColor = True
        '
        'btnFormFive
        '
        Me.btnFormFive.Location = New System.Drawing.Point(172, 12)
        Me.btnFormFive.Name = "btnFormFive"
        Me.btnFormFive.Size = New System.Drawing.Size(100, 30)
        Me.btnFormFive.TabIndex = 4
        Me.btnFormFive.Text = "Form5"
        Me.btnFormFive.UseVisualStyleBackColor = True
        '
        'btnFormSix
        '
        Me.btnFormSix.Location = New System.Drawing.Point(172, 48)
        Me.btnFormSix.Name = "btnFormSix"
        Me.btnFormSix.Size = New System.Drawing.Size(100, 30)
        Me.btnFormSix.TabIndex = 5
        Me.btnFormSix.Text = "Form6"
        Me.btnFormSix.UseVisualStyleBackColor = True
        '
        'btnFormSeven
        '
        Me.btnFormSeven.Location = New System.Drawing.Point(172, 84)
        Me.btnFormSeven.Name = "btnFormSeven"
        Me.btnFormSeven.Size = New System.Drawing.Size(100, 30)
        Me.btnFormSeven.TabIndex = 6
        Me.btnFormSeven.Text = "Form7"
        Me.btnFormSeven.UseVisualStyleBackColor = True
        '
        'Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.btnFormSeven)
        Me.Controls.Add(Me.btnFormSix)
        Me.Controls.Add(Me.btnFormFive)
        Me.Controls.Add(Me.btnFormFour)
        Me.Controls.Add(Me.btnFormThree)
        Me.Controls.Add(Me.btnFormTwo)
        Me.Controls.Add(Me.btnFormOne)
        Me.Name = "Menu"
        Me.Text = "Form3"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnFormOne As System.Windows.Forms.Button
    Friend WithEvents btnFormTwo As System.Windows.Forms.Button
    Friend WithEvents btnFormThree As System.Windows.Forms.Button
    Friend WithEvents btnFormFour As System.Windows.Forms.Button
    Friend WithEvents btnFormFive As System.Windows.Forms.Button
    Friend WithEvents btnFormSix As System.Windows.Forms.Button
    Friend WithEvents btnFormSeven As System.Windows.Forms.Button
End Class
