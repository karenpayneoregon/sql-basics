namespace DataGridViewSample
{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            GetCurrentFromTableButton = new Button();
            EditCurrentButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridView1.Location = new Point(16, 9);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(958, 192);
            dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.DataPropertyName = "Id";
            Column1.HeaderText = "Id";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "Title";
            Column2.HeaderText = "Title";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.DataPropertyName = "Price";
            Column3.HeaderText = "Price";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 125;
            // 
            // GetCurrentFromTableButton
            // 
            GetCurrentFromTableButton.Image = (Image)resources.GetObject("GetCurrentFromTableButton.Image");
            GetCurrentFromTableButton.ImageAlign = ContentAlignment.MiddleLeft;
            GetCurrentFromTableButton.Location = new Point(16, 209);
            GetCurrentFromTableButton.Margin = new Padding(3, 4, 3, 4);
            GetCurrentFromTableButton.Name = "GetCurrentFromTableButton";
            GetCurrentFromTableButton.Size = new Size(173, 31);
            GetCurrentFromTableButton.TabIndex = 2;
            GetCurrentFromTableButton.Text = "Current";
            GetCurrentFromTableButton.UseVisualStyleBackColor = true;
            GetCurrentFromTableButton.Click += GetCurrentFromTableButton_Click;
            // 
            // EditCurrentButton
            // 
            EditCurrentButton.ImageAlign = ContentAlignment.MiddleLeft;
            EditCurrentButton.Location = new Point(801, 209);
            EditCurrentButton.Margin = new Padding(3, 4, 3, 4);
            EditCurrentButton.Name = "EditCurrentButton";
            EditCurrentButton.Size = new Size(173, 31);
            EditCurrentButton.TabIndex = 3;
            EditCurrentButton.Text = "Edit";
            EditCurrentButton.UseVisualStyleBackColor = true;
            EditCurrentButton.Click += EditCurrentButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 253);
            Controls.Add(EditCurrentButton);
            Controls.Add(GetCurrentFromTableButton);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Karen Payne code sample";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView1;
        private Button GetCurrentFromTableButton;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button EditCurrentButton;
    }
}