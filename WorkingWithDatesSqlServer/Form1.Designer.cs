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
        dataGridView1 = new DataGridView();
        panel1 = new Panel();
        CalculateAgeButton = new Button();
        HolidaysBetweenMonthsByYearButton = new Button();
        HolidaysDuringTheWeekForYearButton = new Button();
        CurrentWeekDapperTextBox = new TextBox();
        StartEndCurrentWeekDapperButton = new Button();
        CurrentWeekTextBox = new TextBox();
        StartEndCurrentWeekButton = new Button();
        GetCalendar4Button = new Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 29;
        dataGridView1.Size = new Size(1635, 243);
        dataGridView1.TabIndex = 0;
        // 
        // panel1
        // 
        panel1.Controls.Add(CalculateAgeButton);
        panel1.Controls.Add(HolidaysBetweenMonthsByYearButton);
        panel1.Controls.Add(HolidaysDuringTheWeekForYearButton);
        panel1.Controls.Add(CurrentWeekDapperTextBox);
        panel1.Controls.Add(StartEndCurrentWeekDapperButton);
        panel1.Controls.Add(CurrentWeekTextBox);
        panel1.Controls.Add(StartEndCurrentWeekButton);
        panel1.Controls.Add(GetCalendar4Button);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 249);
        panel1.Name = "panel1";
        panel1.Size = new Size(1635, 193);
        panel1.TabIndex = 1;
        // 
        // CalculateAgeButton
        // 
        CalculateAgeButton.Location = new Point(294, 152);
        CalculateAgeButton.Name = "CalculateAgeButton";
        CalculateAgeButton.Size = new Size(255, 29);
        CalculateAgeButton.TabIndex = 7;
        CalculateAgeButton.Text = "Calculate age";
        CalculateAgeButton.UseVisualStyleBackColor = true;
        CalculateAgeButton.Click += CalculateAgeButton_Click;
        // 
        // HolidaysBetweenMonthsByYearButton
        // 
        HolidaysBetweenMonthsByYearButton.Location = new Point(294, 14);
        HolidaysBetweenMonthsByYearButton.Name = "HolidaysBetweenMonthsByYearButton";
        HolidaysBetweenMonthsByYearButton.Size = new Size(255, 29);
        HolidaysBetweenMonthsByYearButton.TabIndex = 6;
        HolidaysBetweenMonthsByYearButton.Text = "Holidays: 2023 between months";
        HolidaysBetweenMonthsByYearButton.UseVisualStyleBackColor = true;
        HolidaysBetweenMonthsByYearButton.Click += HolidaysBetweenMonthsByYearButton_Click;
        // 
        // HolidaysDuringTheWeekForYearButton
        // 
        HolidaysDuringTheWeekForYearButton.Location = new Point(21, 152);
        HolidaysDuringTheWeekForYearButton.Name = "HolidaysDuringTheWeekForYearButton";
        HolidaysDuringTheWeekForYearButton.Size = new Size(255, 29);
        HolidaysDuringTheWeekForYearButton.TabIndex = 5;
        HolidaysDuringTheWeekForYearButton.Text = "Holidays During The Week For Year";
        HolidaysDuringTheWeekForYearButton.UseVisualStyleBackColor = true;
        HolidaysDuringTheWeekForYearButton.Click += HolidaysDuringTheWeekForYearButton_Click;
        // 
        // CurrentWeekDapperTextBox
        // 
        CurrentWeekDapperTextBox.Location = new Point(294, 108);
        CurrentWeekDapperTextBox.Name = "CurrentWeekDapperTextBox";
        CurrentWeekDapperTextBox.Size = new Size(255, 27);
        CurrentWeekDapperTextBox.TabIndex = 4;
        // 
        // StartEndCurrentWeekDapperButton
        // 
        StartEndCurrentWeekDapperButton.Location = new Point(21, 107);
        StartEndCurrentWeekDapperButton.Name = "StartEndCurrentWeekDapperButton";
        StartEndCurrentWeekDapperButton.Size = new Size(255, 29);
        StartEndCurrentWeekDapperButton.TabIndex = 3;
        StartEndCurrentWeekDapperButton.Text = "Start/End of current week Dapper";
        StartEndCurrentWeekDapperButton.UseVisualStyleBackColor = true;
        StartEndCurrentWeekDapperButton.Click += StartEndCurrentWeekDapperButton_Click;
        // 
        // CurrentWeekTextBox
        // 
        CurrentWeekTextBox.Location = new Point(294, 64);
        CurrentWeekTextBox.Name = "CurrentWeekTextBox";
        CurrentWeekTextBox.Size = new Size(255, 27);
        CurrentWeekTextBox.TabIndex = 2;
        // 
        // StartEndCurrentWeekButton
        // 
        StartEndCurrentWeekButton.Location = new Point(21, 62);
        StartEndCurrentWeekButton.Name = "StartEndCurrentWeekButton";
        StartEndCurrentWeekButton.Size = new Size(255, 29);
        StartEndCurrentWeekButton.TabIndex = 1;
        StartEndCurrentWeekButton.Text = "Start/End of current week ado";
        StartEndCurrentWeekButton.UseVisualStyleBackColor = true;
        StartEndCurrentWeekButton.Click += StartEndCurrentWeekButton_Click;
        // 
        // GetCalendar4Button
        // 
        GetCalendar4Button.Location = new Point(21, 14);
        GetCalendar4Button.Name = "GetCalendar4Button";
        GetCalendar4Button.Size = new Size(255, 29);
        GetCalendar4Button.TabIndex = 0;
        GetCalendar4Button.Text = "GetCalendar4";
        GetCalendar4Button.UseVisualStyleBackColor = true;
        GetCalendar4Button.Click += GetCalendar4Button_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1635, 442);
        Controls.Add(panel1);
        Controls.Add(dataGridView1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Working with TSQL";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
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
