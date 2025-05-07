namespace WinFormsApp1;

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
        pictureBox1 = new PictureBox();
        ExamineButton = new Button();
        Examine1Button = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Location = new Point(138, 87);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(125, 62);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // ExamineButton
        // 
        ExamineButton.Location = new Point(28, 245);
        ExamineButton.Name = "ExamineButton";
        ExamineButton.Size = new Size(94, 29);
        ExamineButton.TabIndex = 2;
        ExamineButton.Text = "Examine";
        ExamineButton.UseVisualStyleBackColor = true;
        ExamineButton.Click += ExamineButton_Click;
        // 
        // Examine1Button
        // 
        Examine1Button.Location = new Point(296, 245);
        Examine1Button.Name = "Examine1Button";
        Examine1Button.Size = new Size(94, 29);
        Examine1Button.TabIndex = 3;
        Examine1Button.Text = "Examine 1";
        Examine1Button.UseVisualStyleBackColor = true;
        Examine1Button.Click += Examine1Button_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(411, 326);
        Controls.Add(Examine1Button);
        Controls.Add(ExamineButton);
        Controls.Add(pictureBox1);
        Name = "Form1";
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private Button ExamineButton;
    private Button Examine1Button;
}
