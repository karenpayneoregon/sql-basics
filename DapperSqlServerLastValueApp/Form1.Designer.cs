namespace DapperSqlServerLastValueApp;

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
        LoadButton = new Button();
        dataGridView1 = new DataGridView();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(LoadButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 381);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 69);
        panel1.TabIndex = 0;
        // 
        // LoadButton
        // 
        LoadButton.Location = new Point(12, 18);
        LoadButton.Name = "LoadButton";
        LoadButton.Size = new Size(94, 29);
        LoadButton.TabIndex = 0;
        LoadButton.Text = "Load";
        LoadButton.UseVisualStyleBackColor = true;
        LoadButton.Click += LoadButton_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(800, 381);
        dataGridView1.TabIndex = 1;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dataGridView1);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Last_Value";
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private Button LoadButton;
    private DataGridView dataGridView1;
}
