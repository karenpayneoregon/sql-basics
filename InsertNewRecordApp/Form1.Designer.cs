namespace InsertNewRecordApp;

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
        dataGridView1 = new DataGridView();
        panel1 = new Panel();
        GetCurrenPersonButton = new Button();
        CountLabel = new Label();
        label1 = new Label();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 29;
        dataGridView1.Size = new Size(591, 382);
        dataGridView1.TabIndex = 0;
        // 
        // panel1
        // 
        panel1.Controls.Add(GetCurrenPersonButton);
        panel1.Controls.Add(CountLabel);
        panel1.Controls.Add(label1);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 382);
        panel1.Name = "panel1";
        panel1.Size = new Size(591, 68);
        panel1.TabIndex = 1;
        // 
        // GetCurrenPersonButton
        // 
        GetCurrenPersonButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        GetCurrenPersonButton.Location = new Point(389, 27);
        GetCurrenPersonButton.Name = "GetCurrenPersonButton";
        GetCurrenPersonButton.Size = new Size(184, 29);
        GetCurrenPersonButton.TabIndex = 2;
        GetCurrenPersonButton.Text = "Get current person";
        GetCurrenPersonButton.UseVisualStyleBackColor = true;
        GetCurrenPersonButton.Click += GetCurrentPersonButton_Click;
        // 
        // CountLabel
        // 
        CountLabel.AutoSize = true;
        CountLabel.Location = new Point(178, 22);
        CountLabel.Name = "CountLabel";
        CountLabel.Size = new Size(17, 20);
        CountLabel.TabIndex = 3;
        CountLabel.Text = "0";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(23, 22);
        label1.Name = "label1";
        label1.Size = new Size(149, 20);
        label1.TabIndex = 2;
        label1.Text = "Total records in table";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(591, 450);
        Controls.Add(dataGridView1);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Add records and get primary key back";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView dataGridView1;
    private Panel panel1;
    private Label CountLabel;
    private Label label1;
    private Button GetCurrenPersonButton;
}
