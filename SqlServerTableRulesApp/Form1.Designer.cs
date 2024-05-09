namespace SqlServerTableRulesApp;

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
        DatabaseNamesComboBox = new ComboBox();
        panel1 = new Panel();
        CurrentButton = new Button();
        GetRulesButton = new Button();
        dataGridView1 = new DataGridView();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // DatabaseNamesComboBox
        // 
        DatabaseNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        DatabaseNamesComboBox.FormattingEnabled = true;
        DatabaseNamesComboBox.Location = new Point(23, 26);
        DatabaseNamesComboBox.Name = "DatabaseNamesComboBox";
        DatabaseNamesComboBox.Size = new Size(299, 28);
        DatabaseNamesComboBox.TabIndex = 0;
        // 
        // panel1
        // 
        panel1.Controls.Add(CurrentButton);
        panel1.Controls.Add(GetRulesButton);
        panel1.Controls.Add(DatabaseNamesComboBox);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 364);
        panel1.Name = "panel1";
        panel1.Size = new Size(1173, 86);
        panel1.TabIndex = 1;
        // 
        // CurrentButton
        // 
        CurrentButton.Enabled = false;
        CurrentButton.Location = new Point(567, 25);
        CurrentButton.Name = "CurrentButton";
        CurrentButton.Size = new Size(148, 29);
        CurrentButton.TabIndex = 2;
        CurrentButton.Text = "Current";
        CurrentButton.UseVisualStyleBackColor = true;
        CurrentButton.Click += CurrentButton_Click;
        // 
        // GetRulesButton
        // 
        GetRulesButton.Enabled = false;
        GetRulesButton.Location = new Point(362, 25);
        GetRulesButton.Name = "GetRulesButton";
        GetRulesButton.Size = new Size(148, 29);
        GetRulesButton.TabIndex = 1;
        GetRulesButton.Text = "Rules";
        GetRulesButton.UseVisualStyleBackColor = true;
        GetRulesButton.Click += GetRulesButton_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(1173, 364);
        dataGridView1.TabIndex = 2;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1173, 450);
        Controls.Add(dataGridView1);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Table rules";
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private ComboBox DatabaseNamesComboBox;
    private Panel panel1;
    private Button GetRulesButton;
    private DataGridView dataGridView1;
    private Button CurrentButton;
}
