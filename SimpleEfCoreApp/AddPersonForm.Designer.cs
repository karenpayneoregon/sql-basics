namespace SimpleEfCoreApp;

partial class AddPersonForm
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
        components = new System.ComponentModel.Container();
        label1 = new Label();
        FirstNameTextBox = new TextBox();
        label2 = new Label();
        LastNameTextBox = new TextBox();
        label3 = new Label();
        BrthDateTimePicker = new DateTimePicker();
        AddNewButton = new Button();
        button1 = new Button();
        errorProvider1 = new ErrorProvider(components);
        ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(35, 28);
        label1.Name = "label1";
        label1.Size = new Size(36, 20);
        label1.TabIndex = 0;
        label1.Text = "First";
        // 
        // FirstNameTextBox
        // 
        FirstNameTextBox.Location = new Point(38, 56);
        FirstNameTextBox.Margin = new Padding(3, 4, 3, 4);
        FirstNameTextBox.Name = "FirstNameTextBox";
        FirstNameTextBox.Size = new Size(268, 27);
        FirstNameTextBox.TabIndex = 1;
        FirstNameTextBox.Tag = "FirstName";
        FirstNameTextBox.Text = "Karen";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(35, 106);
        label2.Name = "label2";
        label2.Size = new Size(35, 20);
        label2.TabIndex = 2;
        label2.Text = "Last";
        // 
        // LastNameTextBox
        // 
        LastNameTextBox.Location = new Point(38, 131);
        LastNameTextBox.Margin = new Padding(3, 4, 3, 4);
        LastNameTextBox.Name = "LastNameTextBox";
        LastNameTextBox.Size = new Size(268, 27);
        LastNameTextBox.TabIndex = 3;
        LastNameTextBox.Tag = "LastName";
        LastNameTextBox.Text = "Payne";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(35, 182);
        label3.Name = "label3";
        label3.Size = new Size(76, 20);
        label3.TabIndex = 4;
        label3.Text = "Birth Date";
        // 
        // BrthDateTimePicker
        // 
        BrthDateTimePicker.Format = DateTimePickerFormat.Short;
        BrthDateTimePicker.Location = new Point(38, 211);
        BrthDateTimePicker.Margin = new Padding(3, 4, 3, 4);
        BrthDateTimePicker.Name = "BrthDateTimePicker";
        BrthDateTimePicker.Size = new Size(268, 27);
        BrthDateTimePicker.TabIndex = 5;
        BrthDateTimePicker.Tag = "BirthDate";
        BrthDateTimePicker.Value = new DateTime(2005, 1, 26, 6, 34, 0, 0);
        // 
        // AddNewButton
        // 
        AddNewButton.ImageAlign = ContentAlignment.MiddleLeft;
        AddNewButton.Location = new Point(38, 252);
        AddNewButton.Margin = new Padding(3, 4, 3, 4);
        AddNewButton.Name = "AddNewButton";
        AddNewButton.Size = new Size(101, 42);
        AddNewButton.TabIndex = 6;
        AddNewButton.Text = "Add new";
        AddNewButton.UseVisualStyleBackColor = true;
        AddNewButton.Click += AddNewButton_Click;
        // 
        // button1
        // 
        button1.DialogResult = DialogResult.Cancel;
        button1.ImageAlign = ContentAlignment.MiddleLeft;
        button1.Location = new Point(205, 252);
        button1.Margin = new Padding(3, 4, 3, 4);
        button1.Name = "button1";
        button1.Size = new Size(101, 42);
        button1.TabIndex = 7;
        button1.Text = "Cancel";
        button1.UseVisualStyleBackColor = true;
        // 
        // errorProvider1
        // 
        errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errorProvider1.ContainerControl = this;
        // 
        // AddPersonForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(340, 322);
        Controls.Add(button1);
        Controls.Add(AddNewButton);
        Controls.Add(BrthDateTimePicker);
        Controls.Add(label3);
        Controls.Add(LastNameTextBox);
        Controls.Add(label2);
        Controls.Add(FirstNameTextBox);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Margin = new Padding(3, 4, 3, 4);
        Name = "AddPersonForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Add new person";
        ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox FirstNameTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox LastNameTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DateTimePicker BrthDateTimePicker;
    private System.Windows.Forms.Button AddNewButton;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ErrorProvider errorProvider1;
}