namespace WorkingWithDatesSqlServer;

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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HolidaysBetweenMonthsByYearButton = new System.Windows.Forms.Button();
            this.HolidaysDuringTheWeekForYearButton = new System.Windows.Forms.Button();
            this.CurrentWeekDapperTextBox = new System.Windows.Forms.TextBox();
            this.StartEndCurrentWeekDapperButton = new System.Windows.Forms.Button();
            this.CurrentWeekTextBox = new System.Windows.Forms.TextBox();
            this.StartEndCurrentWeekButton = new System.Windows.Forms.Button();
            this.GetCalendar4Button = new System.Windows.Forms.Button();
            this.CalculateAgeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(1635, 243);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CalculateAgeButton);
            this.panel1.Controls.Add(this.HolidaysBetweenMonthsByYearButton);
            this.panel1.Controls.Add(this.HolidaysDuringTheWeekForYearButton);
            this.panel1.Controls.Add(this.CurrentWeekDapperTextBox);
            this.panel1.Controls.Add(this.StartEndCurrentWeekDapperButton);
            this.panel1.Controls.Add(this.CurrentWeekTextBox);
            this.panel1.Controls.Add(this.StartEndCurrentWeekButton);
            this.panel1.Controls.Add(this.GetCalendar4Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1635, 193);
            this.panel1.TabIndex = 1;
            // 
            // HolidaysBetweenMonthsByYearButton
            // 
            this.HolidaysBetweenMonthsByYearButton.Location = new System.Drawing.Point(294, 14);
            this.HolidaysBetweenMonthsByYearButton.Name = "HolidaysBetweenMonthsByYearButton";
            this.HolidaysBetweenMonthsByYearButton.Size = new System.Drawing.Size(255, 29);
            this.HolidaysBetweenMonthsByYearButton.TabIndex = 6;
            this.HolidaysBetweenMonthsByYearButton.Text = "Holidays: 2023 between months";
            this.HolidaysBetweenMonthsByYearButton.UseVisualStyleBackColor = true;
            this.HolidaysBetweenMonthsByYearButton.Click += new System.EventHandler(this.HolidaysBetweenMonthsByYearButton_Click);
            // 
            // HolidaysDuringTheWeekForYearButton
            // 
            this.HolidaysDuringTheWeekForYearButton.Location = new System.Drawing.Point(21, 152);
            this.HolidaysDuringTheWeekForYearButton.Name = "HolidaysDuringTheWeekForYearButton";
            this.HolidaysDuringTheWeekForYearButton.Size = new System.Drawing.Size(255, 29);
            this.HolidaysDuringTheWeekForYearButton.TabIndex = 5;
            this.HolidaysDuringTheWeekForYearButton.Text = "Holidays During The Week For Year";
            this.HolidaysDuringTheWeekForYearButton.UseVisualStyleBackColor = true;
            this.HolidaysDuringTheWeekForYearButton.Click += new System.EventHandler(this.HolidaysDuringTheWeekForYearButton_Click);
            // 
            // CurrentWeekDapperTextBox
            // 
            this.CurrentWeekDapperTextBox.Location = new System.Drawing.Point(294, 108);
            this.CurrentWeekDapperTextBox.Name = "CurrentWeekDapperTextBox";
            this.CurrentWeekDapperTextBox.Size = new System.Drawing.Size(255, 27);
            this.CurrentWeekDapperTextBox.TabIndex = 4;
            // 
            // StartEndCurrentWeekDapperButton
            // 
            this.StartEndCurrentWeekDapperButton.Location = new System.Drawing.Point(21, 107);
            this.StartEndCurrentWeekDapperButton.Name = "StartEndCurrentWeekDapperButton";
            this.StartEndCurrentWeekDapperButton.Size = new System.Drawing.Size(255, 29);
            this.StartEndCurrentWeekDapperButton.TabIndex = 3;
            this.StartEndCurrentWeekDapperButton.Text = "Start/End of current week Dapper";
            this.StartEndCurrentWeekDapperButton.UseVisualStyleBackColor = true;
            this.StartEndCurrentWeekDapperButton.Click += new System.EventHandler(this.StartEndCurrentWeekDapperButton_Click);
            // 
            // CurrentWeekTextBox
            // 
            this.CurrentWeekTextBox.Location = new System.Drawing.Point(294, 64);
            this.CurrentWeekTextBox.Name = "CurrentWeekTextBox";
            this.CurrentWeekTextBox.Size = new System.Drawing.Size(255, 27);
            this.CurrentWeekTextBox.TabIndex = 2;
            // 
            // StartEndCurrentWeekButton
            // 
            this.StartEndCurrentWeekButton.Location = new System.Drawing.Point(21, 62);
            this.StartEndCurrentWeekButton.Name = "StartEndCurrentWeekButton";
            this.StartEndCurrentWeekButton.Size = new System.Drawing.Size(255, 29);
            this.StartEndCurrentWeekButton.TabIndex = 1;
            this.StartEndCurrentWeekButton.Text = "Start/End of current week ado";
            this.StartEndCurrentWeekButton.UseVisualStyleBackColor = true;
            this.StartEndCurrentWeekButton.Click += new System.EventHandler(this.StartEndCurrentWeekButton_Click);
            // 
            // GetCalendar4Button
            // 
            this.GetCalendar4Button.Location = new System.Drawing.Point(21, 14);
            this.GetCalendar4Button.Name = "GetCalendar4Button";
            this.GetCalendar4Button.Size = new System.Drawing.Size(255, 29);
            this.GetCalendar4Button.TabIndex = 0;
            this.GetCalendar4Button.Text = "GetCalendar4";
            this.GetCalendar4Button.UseVisualStyleBackColor = true;
            this.GetCalendar4Button.Click += new System.EventHandler(this.GetCalendar4Button_Click);
            // 
            // CalculateAgeButton
            // 
            this.CalculateAgeButton.Location = new System.Drawing.Point(294, 152);
            this.CalculateAgeButton.Name = "CalculateAgeButton";
            this.CalculateAgeButton.Size = new System.Drawing.Size(255, 29);
            this.CalculateAgeButton.TabIndex = 7;
            this.CalculateAgeButton.Text = "Calculate age";
            this.CalculateAgeButton.UseVisualStyleBackColor = true;
            this.CalculateAgeButton.Click += new System.EventHandler(this.CalculateAgeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Working with TSQL";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private DataGridView dataGridView1;
    private Panel panel1;
    private Button GetCalendar4Button;
    private TextBox CurrentWeekTextBox;
    private Button StartEndCurrentWeekButton;
    private Button StartEndCurrentWeekDapperButton;
    private TextBox CurrentWeekDapperTextBox;
    private Button HolidaysDuringTheWeekForYearButton;
    private Button HolidaysBetweenMonthsByYearButton;
    private Button CalculateAgeButton;
}
