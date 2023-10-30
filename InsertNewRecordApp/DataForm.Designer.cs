using InsertNewRecordApp.Components;

namespace InsertNewRecordApp;

partial class DataForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
        dataGridView1 = new DataGridView();
        SaveButton = new Button();
        panel1 = new Panel();
        RefreshButton = new Button();
        AddButton = new Button();
        RemoveButton = new Button();
        MockUpdateCurrentButton = new Button();
        CurrentButton = new Button();
        coreBindingNavigator1 = new CoreBindingNavigator();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        coreBindingNavigator1.BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.BackgroundColor = Color.White;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(0, 33);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 29;
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new Size(568, 618);
        dataGridView1.TabIndex = 0;
        // 
        // SaveButton
        // 
        SaveButton.Location = new Point(12, 27);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new Size(104, 29);
        SaveButton.TabIndex = 1;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += SaveButton_Click;
        // 
        // panel1
        // 
        panel1.Controls.Add(RefreshButton);
        panel1.Controls.Add(AddButton);
        panel1.Controls.Add(RemoveButton);
        panel1.Controls.Add(MockUpdateCurrentButton);
        panel1.Controls.Add(SaveButton);
        panel1.Controls.Add(CurrentButton);
        panel1.Location = new Point(574, 31);
        panel1.Name = "panel1";
        panel1.Size = new Size(128, 618);
        panel1.TabIndex = 2;
        // 
        // RefreshButton
        // 
        RefreshButton.Location = new Point(12, 254);
        RefreshButton.Name = "RefreshButton";
        RefreshButton.Size = new Size(104, 29);
        RefreshButton.TabIndex = 7;
        RefreshButton.Text = "Refresh";
        RefreshButton.UseVisualStyleBackColor = true;
        RefreshButton.Click += RefreshButton_Click;
        // 
        // AddButton
        // 
        AddButton.Location = new Point(12, 207);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(104, 29);
        AddButton.TabIndex = 6;
        AddButton.Text = "Add";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // RemoveButton
        // 
        RemoveButton.Location = new Point(12, 162);
        RemoveButton.Name = "RemoveButton";
        RemoveButton.Size = new Size(104, 29);
        RemoveButton.TabIndex = 5;
        RemoveButton.Text = "Remove";
        RemoveButton.UseVisualStyleBackColor = true;
        RemoveButton.Click += RemoveButton_Click;
        // 
        // MockUpdateCurrentButton
        // 
        MockUpdateCurrentButton.Location = new Point(12, 117);
        MockUpdateCurrentButton.Name = "MockUpdateCurrentButton";
        MockUpdateCurrentButton.Size = new Size(104, 29);
        MockUpdateCurrentButton.TabIndex = 4;
        MockUpdateCurrentButton.Text = "Update";
        MockUpdateCurrentButton.UseVisualStyleBackColor = true;
        MockUpdateCurrentButton.Click += MockUpdateCurrentButton_Click;
        // 
        // CurrentButton
        // 
        CurrentButton.Location = new Point(12, 72);
        CurrentButton.Name = "CurrentButton";
        CurrentButton.Size = new Size(104, 29);
        CurrentButton.TabIndex = 3;
        CurrentButton.Text = "Current";
        CurrentButton.UseVisualStyleBackColor = true;
        CurrentButton.Click += CurrentButton_Click;
        // 
        // coreBindingNavigator1
        // 
        coreBindingNavigator1.BackColor = Color.SeaShell;
        coreBindingNavigator1.ImageScalingSize = new Size(20, 20);
        coreBindingNavigator1.Location = new Point(0, 0);
        coreBindingNavigator1.Name = "coreBindingNavigator1";
        coreBindingNavigator1.Size = new Size(706, 27);
        coreBindingNavigator1.TabIndex = 3;
        coreBindingNavigator1.Text = "coreBindingNavigator1";
        // 
        // DataForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(706, 657);
        Controls.Add(dataGridView1);
        Controls.Add(coreBindingNavigator1);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "DataForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Data operation samples";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        coreBindingNavigator1.EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dataGridView1;
    private Button SaveButton;
    private Panel panel1;
    private Button CurrentButton;
    private CoreBindingNavigator coreBindingNavigator1;
    private Button MockUpdateCurrentButton;
    private Button RemoveButton;
    private Button AddButton;
    private Button RefreshButton;
}