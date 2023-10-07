namespace RowFilterApp;

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
        textBoxSpecial1 = new Classes.TextBoxSpecial();
        panel1 = new Panel();
        dataGridView1 = new DataGridView();
        coreBindingNavigator1 = new Classes.CoreBindingNavigator();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        coreBindingNavigator1.BeginInit();
        SuspendLayout();
        // 
        // textBoxSpecial1
        // 
        textBoxSpecial1.BindingSource = null;
        textBoxSpecial1.CaseSensitiveLike = false;
        textBoxSpecial1.ColumnName = "CompanyName";
        textBoxSpecial1.HasData = false;
        textBoxSpecial1.Location = new Point(12, 21);
        textBoxSpecial1.Name = "textBoxSpecial1";
        textBoxSpecial1.Size = new Size(125, 27);
        textBoxSpecial1.TabIndex = 0;
        textBoxSpecial1.Text = "ON";
        // 
        // panel1
        // 
        panel1.Controls.Add(textBoxSpecial1);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 380);
        panel1.Name = "panel1";
        panel1.Size = new Size(877, 70);
        panel1.TabIndex = 1;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 27);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 29;
        dataGridView1.Size = new Size(877, 353);
        dataGridView1.TabIndex = 2;
        // 
        // coreBindingNavigator1
        // 
        coreBindingNavigator1.ImageScalingSize = new Size(20, 20);
        coreBindingNavigator1.Location = new Point(0, 0);
        coreBindingNavigator1.Name = "coreBindingNavigator1";
        coreBindingNavigator1.Size = new Size(877, 27);
        coreBindingNavigator1.TabIndex = 3;
        coreBindingNavigator1.Text = "coreBindingNavigator1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(877, 450);
        Controls.Add(dataGridView1);
        Controls.Add(coreBindingNavigator1);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        coreBindingNavigator1.EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Classes.TextBoxSpecial textBoxSpecial1;
    private Panel panel1;
    private DataGridView dataGridView1;
    private Classes.CoreBindingNavigator coreBindingNavigator1;
}
