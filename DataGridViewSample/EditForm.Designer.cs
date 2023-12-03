namespace DataGridViewSample;

partial class EditForm
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
        label1 = new Label();
        TitleTextBox = new TextBox();
        SaveButton = new Button();
        label2 = new Label();
        PriceTextBox = new Controls.NumericTextBox();
        button1 = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(30, 24);
        label1.Name = "label1";
        label1.Size = new Size(38, 20);
        label1.TabIndex = 0;
        label1.Text = "Title";
        // 
        // TitleTextBox
        // 
        TitleTextBox.Location = new Point(30, 47);
        TitleTextBox.Name = "TitleTextBox";
        TitleTextBox.Size = new Size(343, 27);
        TitleTextBox.TabIndex = 1;
        // 
        // SaveButton
        // 
        SaveButton.Location = new Point(30, 176);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new Size(94, 29);
        SaveButton.TabIndex = 2;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += SaveButton_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(30, 93);
        label2.Name = "label2";
        label2.Size = new Size(41, 20);
        label2.TabIndex = 3;
        label2.Text = "Price";
        // 
        // PriceTextBox
        // 
        PriceTextBox.Location = new Point(30, 116);
        PriceTextBox.Name = "PriceTextBox";
        PriceTextBox.Size = new Size(125, 27);
        PriceTextBox.TabIndex = 4;
        PriceTextBox.TextAlign = HorizontalAlignment.Right;
        // 
        // button1
        // 
        button1.DialogResult = DialogResult.Cancel;
        button1.Location = new Point(140, 176);
        button1.Name = "button1";
        button1.Size = new Size(94, 29);
        button1.TabIndex = 5;
        button1.Text = "Cancel";
        button1.UseVisualStyleBackColor = true;
        // 
        // EditForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(543, 227);
        Controls.Add(button1);
        Controls.Add(PriceTextBox);
        Controls.Add(label2);
        Controls.Add(SaveButton);
        Controls.Add(TitleTextBox);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "EditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Edit item";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox TitleTextBox;
    private Button SaveButton;
    private Label label2;
    private Controls.NumericTextBox PriceTextBox;
    private Button button1;
}