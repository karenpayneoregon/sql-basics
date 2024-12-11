namespace SqlServerGetJsonRaw;

partial class MainForm
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
        ReadButton = new Button();
        dataGridView1 = new DataGridView();
        panel1 = new Panel();
        LastNamesComboBox = new ComboBox();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // ReadButton
        // 
        ReadButton.Location = new Point(25, 27);
        ReadButton.Name = "ReadButton";
        ReadButton.Size = new Size(94, 29);
        ReadButton.TabIndex = 0;
        ReadButton.Text = "Read";
        ReadButton.UseVisualStyleBackColor = true;
        ReadButton.Click += ReadButton_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(932, 192);
        dataGridView1.TabIndex = 1;
        // 
        // panel1
        // 
        panel1.Controls.Add(LastNamesComboBox);
        panel1.Controls.Add(ReadButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 124);
        panel1.Name = "panel1";
        panel1.Size = new Size(932, 68);
        panel1.TabIndex = 2;
        // 
        // LastNamesComboBox
        // 
        LastNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        LastNamesComboBox.FormattingEnabled = true;
        LastNamesComboBox.Location = new Point(139, 27);
        LastNamesComboBox.Name = "LastNamesComboBox";
        LastNamesComboBox.Size = new Size(151, 28);
        LastNamesComboBox.TabIndex = 3;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(932, 192);
        Controls.Add(panel1);
        Controls.Add(dataGridView1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code sample: read json properties";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Button ReadButton;
    private DataGridView dataGridView1;
    private Panel panel1;
    private ComboBox LastNamesComboBox;
}
