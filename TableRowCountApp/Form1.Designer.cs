namespace TableRowCountApp;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DatabaseNamesListBox = new ListBox();
        TablesDataGridView = new DataGridView();
        TableNameColumn = new DataGridViewTextBoxColumn();
        RowCountColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)TablesDataGridView).BeginInit();
        SuspendLayout();
        // 
        // DatabaseNamesListBox
        // 
        DatabaseNamesListBox.FormattingEnabled = true;
        DatabaseNamesListBox.Location = new Point(21, 43);
        DatabaseNamesListBox.Name = "DatabaseNamesListBox";
        DatabaseNamesListBox.Size = new Size(308, 384);
        DatabaseNamesListBox.TabIndex = 0;
        // 
        // TablesDataGridView
        // 
        TablesDataGridView.AllowUserToAddRows = false;
        TablesDataGridView.AllowUserToDeleteRows = false;
        TablesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        TablesDataGridView.Columns.AddRange(new DataGridViewColumn[] { TableNameColumn, RowCountColumn });
        TablesDataGridView.Location = new Point(349, 43);
        TablesDataGridView.Name = "TablesDataGridView";
        TablesDataGridView.ReadOnly = true;
        TablesDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        TablesDataGridView.RowHeadersVisible = false;
        TablesDataGridView.RowHeadersWidth = 51;
        TablesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        TablesDataGridView.Size = new Size(439, 384);
        TablesDataGridView.TabIndex = 1;
        // 
        // TableNameColumn
        // 
        TableNameColumn.DataPropertyName = "SchemaTableName";
        TableNameColumn.HeaderText = "Table";
        TableNameColumn.MinimumWidth = 6;
        TableNameColumn.Name = "TableNameColumn";
        TableNameColumn.ReadOnly = true;
        TableNameColumn.Width = 125;
        // 
        // RowCountColumn
        // 
        RowCountColumn.DataPropertyName = "RowCount";
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
        RowCountColumn.DefaultCellStyle = dataGridViewCellStyle1;
        RowCountColumn.HeaderText = "Count";
        RowCountColumn.MinimumWidth = 6;
        RowCountColumn.Name = "RowCountColumn";
        RowCountColumn.ReadOnly = true;
        RowCountColumn.Width = 125;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(TablesDataGridView);
        Controls.Add(DatabaseNamesListBox);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code sample";
        ((System.ComponentModel.ISupportInitialize)TablesDataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private ListBox DatabaseNamesListBox;
    private DataGridView TablesDataGridView;
    private DataGridViewTextBoxColumn TableNameColumn;
    private DataGridViewTextBoxColumn RowCountColumn;
}
