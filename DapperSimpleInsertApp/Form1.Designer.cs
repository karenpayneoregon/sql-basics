namespace DapperSimpleInsertApp;

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
        FirstNameTextBox = new TextBox();
        LastNameTextBox = new TextBox();
        ActiveCheckBox = new CheckBox();
        AddButton = new Button();
        label1 = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // FirstNameTextBox
        // 
        FirstNameTextBox.AccessibleDescription = "Enter first name";
        FirstNameTextBox.Location = new Point(51, 46);
        FirstNameTextBox.Name = "FirstNameTextBox";
        FirstNameTextBox.Size = new Size(125, 27);
        FirstNameTextBox.TabIndex = 0;
        FirstNameTextBox.Text = "Paul";
        // 
        // LastNameTextBox
        // 
        LastNameTextBox.AccessibleDescription = "Enter last name";
        LastNameTextBox.Location = new Point(51, 107);
        LastNameTextBox.Name = "LastNameTextBox";
        LastNameTextBox.Size = new Size(125, 27);
        LastNameTextBox.TabIndex = 1;
        LastNameTextBox.Text = "Gallagher";
        // 
        // ActiveCheckBox
        // 
        ActiveCheckBox.AccessibleDescription = "Is customer active";
        ActiveCheckBox.AutoSize = true;
        ActiveCheckBox.Checked = true;
        ActiveCheckBox.CheckState = CheckState.Checked;
        ActiveCheckBox.Location = new Point(213, 46);
        ActiveCheckBox.Name = "ActiveCheckBox";
        ActiveCheckBox.Size = new Size(84, 24);
        ActiveCheckBox.TabIndex = 2;
        ActiveCheckBox.Text = "Is active";
        ActiveCheckBox.UseVisualStyleBackColor = true;
        // 
        // AddButton
        // 
        AddButton.Image = Properties.Resources.AddItem_16x;
        AddButton.ImageAlign = ContentAlignment.MiddleLeft;
        AddButton.Location = new Point(51, 159);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(125, 29);
        AddButton.TabIndex = 3;
        AddButton.Text = "Add new";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(51, 23);
        label1.Name = "label1";
        label1.Size = new Size(36, 20);
        label1.TabIndex = 4;
        label1.Text = "First";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(51, 84);
        label2.Name = "label2";
        label2.Size = new Size(35, 20);
        label2.TabIndex = 5;
        label2.Text = "Last";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(344, 221);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(AddButton);
        Controls.Add(ActiveCheckBox);
        Controls.Add(LastNameTextBox);
        Controls.Add(FirstNameTextBox);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Add new customer";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox FirstNameTextBox;
    private TextBox LastNameTextBox;
    private CheckBox ActiveCheckBox;
    private Button AddButton;
    private Label label1;
    private Label label2;
}
