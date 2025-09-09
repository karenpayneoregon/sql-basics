namespace DateTimeNullDapperApp;

partial class MainForm
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
        ReadDataButton = new Button();
        SuspendLayout();
        // 
        // ReadDataButton
        // 
        ReadDataButton.Location = new Point(41, 26);
        ReadDataButton.Name = "ReadDataButton";
        ReadDataButton.Size = new Size(94, 29);
        ReadDataButton.TabIndex = 0;
        ReadDataButton.Text = "button1";
        ReadDataButton.UseVisualStyleBackColor = true;
        ReadDataButton.Click += ReadDataButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(ReadDataButton);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private Button ReadDataButton;
}
