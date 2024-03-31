namespace DapperGetDatabaseAndTableNamesApp1;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new Panel();
        DatabaseNamesComboBox = new ComboBox();
        ForeignKeysButton = new Button();
        IterateDatabasesButton = new Button();
        ResultsTextBox = new TextBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(DatabaseNamesComboBox);
        panel1.Controls.Add(ForeignKeysButton);
        panel1.Controls.Add(IterateDatabasesButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 382);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 68);
        panel1.TabIndex = 0;
        // 
        // DatabaseNamesComboBox
        // 
        DatabaseNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        DatabaseNamesComboBox.FormattingEnabled = true;
        DatabaseNamesComboBox.Location = new Point(482, 15);
        DatabaseNamesComboBox.Name = "DatabaseNamesComboBox";
        DatabaseNamesComboBox.Size = new Size(262, 28);
        DatabaseNamesComboBox.TabIndex = 4;
        // 
        // ForeignKeysButton
        // 
        ForeignKeysButton.Location = new Point(297, 15);
        ForeignKeysButton.Name = "ForeignKeysButton";
        ForeignKeysButton.Size = new Size(179, 29);
        ForeignKeysButton.TabIndex = 3;
        ForeignKeysButton.Text = "Foreign keys";
        ForeignKeysButton.UseVisualStyleBackColor = true;
        ForeignKeysButton.Click += ForeignKeysButton_Click;
        // 
        // IterateDatabasesButton
        // 
        IterateDatabasesButton.Location = new Point(12, 15);
        IterateDatabasesButton.Name = "IterateDatabasesButton";
        IterateDatabasesButton.Size = new Size(179, 29);
        IterateDatabasesButton.TabIndex = 2;
        IterateDatabasesButton.Text = "Iterate databases";
        IterateDatabasesButton.UseVisualStyleBackColor = true;
        IterateDatabasesButton.Click += IterateDatabasesButton_Click;
        // 
        // ResultsTextBox
        // 
        ResultsTextBox.Dock = DockStyle.Fill;
        ResultsTextBox.Font = new Font("Courier New", 9F);
        ResultsTextBox.Location = new Point(0, 0);
        ResultsTextBox.Multiline = true;
        ResultsTextBox.Name = "ResultsTextBox";
        ResultsTextBox.Size = new Size(800, 382);
        ResultsTextBox.TabIndex = 1;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(ResultsTextBox);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code sample";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel panel1;
    private Button IterateDatabasesButton;
    private TextBox ResultsTextBox;
    private Button ForeignKeysButton;
    private ComboBox DatabaseNamesComboBox;
}
