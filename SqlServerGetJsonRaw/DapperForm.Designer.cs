namespace SqlServerGetJsonRaw;

partial class DapperForm
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
        PersonDataGridView = new DataGridView();
        IdColumn = new DataGridViewTextBoxColumn();
        FirstNameColum = new DataGridViewTextBoxColumn();
        LastNameColumn = new DataGridViewTextBoxColumn();
        BirthDateColumn = new DataGridViewTextBoxColumn();
        panel1 = new Panel();
        AddressesDataGridView = new DataGridView();
        LastNamesComboBox = new ComboBox();
        ((System.ComponentModel.ISupportInitialize)PersonDataGridView).BeginInit();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)AddressesDataGridView).BeginInit();
        SuspendLayout();
        // 
        // PersonDataGridView
        // 
        PersonDataGridView.AllowUserToAddRows = false;
        PersonDataGridView.AllowUserToDeleteRows = false;
        PersonDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        PersonDataGridView.Columns.AddRange(new DataGridViewColumn[] { IdColumn, FirstNameColum, LastNameColumn, BirthDateColumn });
        PersonDataGridView.Dock = DockStyle.Fill;
        PersonDataGridView.Location = new Point(0, 0);
        PersonDataGridView.Name = "PersonDataGridView";
        PersonDataGridView.ReadOnly = true;
        PersonDataGridView.RowHeadersWidth = 51;
        PersonDataGridView.Size = new Size(625, 128);
        PersonDataGridView.TabIndex = 3;
        // 
        // IdColumn
        // 
        IdColumn.DataPropertyName = "ID";
        IdColumn.HeaderText = "Id";
        IdColumn.MinimumWidth = 6;
        IdColumn.Name = "IdColumn";
        IdColumn.ReadOnly = true;
        IdColumn.Width = 125;
        // 
        // FirstNameColum
        // 
        FirstNameColum.DataPropertyName = "FirstName";
        FirstNameColum.HeaderText = "First";
        FirstNameColum.MinimumWidth = 6;
        FirstNameColum.Name = "FirstNameColum";
        FirstNameColum.ReadOnly = true;
        FirstNameColum.Width = 125;
        // 
        // LastNameColumn
        // 
        LastNameColumn.DataPropertyName = "LastName";
        LastNameColumn.HeaderText = "Last";
        LastNameColumn.MinimumWidth = 6;
        LastNameColumn.Name = "LastNameColumn";
        LastNameColumn.ReadOnly = true;
        LastNameColumn.Width = 125;
        // 
        // BirthDateColumn
        // 
        BirthDateColumn.DataPropertyName = "DateOfBirth";
        BirthDateColumn.HeaderText = "Birth";
        BirthDateColumn.MinimumWidth = 6;
        BirthDateColumn.Name = "BirthDateColumn";
        BirthDateColumn.ReadOnly = true;
        BirthDateColumn.Width = 125;
        // 
        // panel1
        // 
        panel1.Controls.Add(AddressesDataGridView);
        panel1.Controls.Add(LastNamesComboBox);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 128);
        panel1.Name = "panel1";
        panel1.Size = new Size(625, 183);
        panel1.TabIndex = 4;
        // 
        // AddressesDataGridView
        // 
        AddressesDataGridView.AllowUserToAddRows = false;
        AddressesDataGridView.AllowUserToDeleteRows = false;
        AddressesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        AddressesDataGridView.Location = new Point(12, 52);
        AddressesDataGridView.Name = "AddressesDataGridView";
        AddressesDataGridView.ReadOnly = true;
        AddressesDataGridView.RowHeadersWidth = 51;
        AddressesDataGridView.Size = new Size(586, 121);
        AddressesDataGridView.TabIndex = 4;
        // 
        // LastNamesComboBox
        // 
        LastNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        LastNamesComboBox.FormattingEnabled = true;
        LastNamesComboBox.Location = new Point(12, 18);
        LastNamesComboBox.Name = "LastNamesComboBox";
        LastNamesComboBox.Size = new Size(151, 28);
        LastNamesComboBox.TabIndex = 3;
        // 
        // DapperForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(625, 311);
        Controls.Add(PersonDataGridView);
        Controls.Add(panel1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "DapperForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Dapper sample";
        ((System.ComponentModel.ISupportInitialize)PersonDataGridView).EndInit();
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)AddressesDataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView PersonDataGridView;
    private DataGridViewTextBoxColumn IdColumn;
    private DataGridViewTextBoxColumn FirstNameColum;
    private DataGridViewTextBoxColumn LastNameColumn;
    private DataGridViewTextBoxColumn BirthDateColumn;
    private Panel panel1;
    private DataGridView AddressesDataGridView;
    private ComboBox LastNamesComboBox;
}