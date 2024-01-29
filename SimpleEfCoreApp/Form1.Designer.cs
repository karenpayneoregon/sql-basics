namespace SimpleEfCoreApp;

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
        IdColumn = new DataGridViewTextBoxColumn();
        FirstNameColumn = new DataGridViewTextBoxColumn();
        LastNameColumn = new DataGridViewTextBoxColumn();
        BirthDateColumn = new Classes.CalendarColumn();
        panel1 = new Panel();
        AddButton = new Button();
        CurrentPersonButton = new Button();
        ResetDataButton = new Button();
        bindingNavigator1 = new Classes.CoreBindingNavigator();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        bindingNavigator1.BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { IdColumn, FirstNameColumn, LastNameColumn, BirthDateColumn });
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 27);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(621, 338);
        dataGridView1.TabIndex = 1;
        // 
        // IdColumn
        // 
        IdColumn.DataPropertyName = "Id";
        IdColumn.HeaderText = "Id";
        IdColumn.MinimumWidth = 6;
        IdColumn.Name = "IdColumn";
        IdColumn.ReadOnly = true;
        IdColumn.Width = 125;
        // 
        // FirstNameColumn
        // 
        FirstNameColumn.DataPropertyName = "FirstName";
        FirstNameColumn.HeaderText = "First";
        FirstNameColumn.MinimumWidth = 6;
        FirstNameColumn.Name = "FirstNameColumn";
        FirstNameColumn.Width = 125;
        // 
        // LastNameColumn
        // 
        LastNameColumn.DataPropertyName = "LastName";
        LastNameColumn.HeaderText = "Last";
        LastNameColumn.MinimumWidth = 6;
        LastNameColumn.Name = "LastNameColumn";
        LastNameColumn.Width = 125;
        // 
        // BirthDateColumn
        // 
        BirthDateColumn.DataPropertyName = "BirthDate";
        BirthDateColumn.HeaderText = "Birth date";
        BirthDateColumn.MinimumWidth = 6;
        BirthDateColumn.Name = "BirthDateColumn";
        BirthDateColumn.Resizable = DataGridViewTriState.True;
        BirthDateColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        BirthDateColumn.Width = 125;
        // 
        // panel1
        // 
        panel1.Controls.Add(AddButton);
        panel1.Controls.Add(CurrentPersonButton);
        panel1.Controls.Add(ResetDataButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 365);
        panel1.Name = "panel1";
        panel1.Size = new Size(621, 85);
        panel1.TabIndex = 2;
        // 
        // AddButton
        // 
        AddButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        AddButton.Location = new Point(504, 17);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(105, 44);
        AddButton.TabIndex = 6;
        AddButton.Text = "Add new";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // CurrentPersonButton
        // 
        CurrentPersonButton.Location = new Point(12, 17);
        CurrentPersonButton.Name = "CurrentPersonButton";
        CurrentPersonButton.Size = new Size(77, 44);
        CurrentPersonButton.TabIndex = 5;
        CurrentPersonButton.Text = "Current";
        CurrentPersonButton.UseVisualStyleBackColor = true;
        CurrentPersonButton.Click += CurrentPersonButton_Click;
        // 
        // ResetDataButton
        // 
        ResetDataButton.Location = new Point(95, 17);
        ResetDataButton.Name = "ResetDataButton";
        ResetDataButton.Size = new Size(77, 44);
        ResetDataButton.TabIndex = 4;
        ResetDataButton.Text = "Reset";
        ResetDataButton.UseVisualStyleBackColor = true;
        ResetDataButton.Click += ResetDataButton_Click;
        // 
        // bindingNavigator1
        // 
        bindingNavigator1.ImageScalingSize = new Size(20, 20);
        bindingNavigator1.Location = new Point(0, 0);
        bindingNavigator1.Name = "bindingNavigator1";
        bindingNavigator1.Size = new Size(621, 27);
        bindingNavigator1.TabIndex = 3;
        bindingNavigator1.Text = "coreBindingNavigator1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(621, 450);
        Controls.Add(dataGridView1);
        Controls.Add(bindingNavigator1);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "EF Core simple";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        bindingNavigator1.EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private DataGridView dataGridView1;
    private Panel panel1;
    private Classes.CoreBindingNavigator bindingNavigator1;
    private Button ResetDataButton;
    private Button CurrentPersonButton;
    private Button AddButton;
    private DataGridViewTextBoxColumn IdColumn;
    private DataGridViewTextBoxColumn FirstNameColumn;
    private DataGridViewTextBoxColumn LastNameColumn;
    private Classes.CalendarColumn BirthDateColumn;
}
