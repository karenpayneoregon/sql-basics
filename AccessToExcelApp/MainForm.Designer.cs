namespace AccessToExcelApp;

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
        FindDatabasesButton = new Button();
        LocalExportTestButton = new Button();
        SuspendLayout();
        // 
        // FindDatabasesButton
        // 
        FindDatabasesButton.Location = new Point(35, 46);
        FindDatabasesButton.Name = "FindDatabasesButton";
        FindDatabasesButton.Size = new Size(278, 29);
        FindDatabasesButton.TabIndex = 0;
        FindDatabasesButton.Text = "Find databases";
        FindDatabasesButton.UseVisualStyleBackColor = true;
        FindDatabasesButton.Click += FindDatabasesButton_Click;
        // 
        // LocalExportTestButton
        // 
        LocalExportTestButton.Location = new Point(35, 92);
        LocalExportTestButton.Name = "LocalExportTestButton";
        LocalExportTestButton.Size = new Size(278, 29);
        LocalExportTestButton.TabIndex = 1;
        LocalExportTestButton.Text = "Local export test";
        LocalExportTestButton.UseVisualStyleBackColor = true;
        LocalExportTestButton.Click += LocalExportTestButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(LocalExportTestButton);
        Controls.Add(FindDatabasesButton);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private Button FindDatabasesButton;
    private Button LocalExportTestButton;
}
