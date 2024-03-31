namespace DapperGetDatabaseAndTableNamesApp1;

partial class Form2
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new Panel();
        DatabaseNamesComboBox = new ComboBox();
        TableNamesComboBox = new ComboBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(TableNamesComboBox);
        panel1.Controls.Add(DatabaseNamesComboBox);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 384);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 66);
        panel1.TabIndex = 0;
        // 
        // DatabaseNamesComboBox
        // 
        DatabaseNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        DatabaseNamesComboBox.FormattingEnabled = true;
        DatabaseNamesComboBox.Location = new Point(39, 17);
        DatabaseNamesComboBox.Name = "DatabaseNamesComboBox";
        DatabaseNamesComboBox.Size = new Size(305, 28);
        DatabaseNamesComboBox.TabIndex = 0;
        // 
        // TableNamesComboBox
        // 
        TableNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        TableNamesComboBox.FormattingEnabled = true;
        TableNamesComboBox.Location = new Point(386, 17);
        TableNamesComboBox.Name = "TableNamesComboBox";
        TableNamesComboBox.Size = new Size(305, 28);
        TableNamesComboBox.TabIndex = 1;
        // 
        // Form2
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form2";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code sample";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private ComboBox TableNamesComboBox;
    private ComboBox DatabaseNamesComboBox;
}