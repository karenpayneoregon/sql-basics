namespace ContinentsDapperApp;

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
        ContinentsComboBox = new ComboBox();
        CountriesComboBox = new ComboBox();
        CapitalLabel = new Label();
        FactLabel = new Label();
        SuspendLayout();
        // 
        // ContinentsComboBox
        // 
        ContinentsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        ContinentsComboBox.FormattingEnabled = true;
        ContinentsComboBox.Location = new Point(38, 24);
        ContinentsComboBox.Name = "ContinentsComboBox";
        ContinentsComboBox.Size = new Size(209, 28);
        ContinentsComboBox.TabIndex = 0;
        // 
        // CountriesComboBox
        // 
        CountriesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        CountriesComboBox.FormattingEnabled = true;
        CountriesComboBox.Location = new Point(273, 24);
        CountriesComboBox.Name = "CountriesComboBox";
        CountriesComboBox.Size = new Size(274, 28);
        CountriesComboBox.TabIndex = 1;
        // 
        // CapitalLabel
        // 
        CapitalLabel.AutoSize = true;
        CapitalLabel.Location = new Point(42, 122);
        CapitalLabel.Name = "CapitalLabel";
        CapitalLabel.Size = new Size(56, 20);
        CapitalLabel.TabIndex = 2;
        CapitalLabel.Text = "Capital";
        // 
        // FactLabel
        // 
        FactLabel.Location = new Point(42, 152);
        FactLabel.Name = "FactLabel";
        FactLabel.Size = new Size(719, 136);
        FactLabel.TabIndex = 3;
        FactLabel.Text = "Fact";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(FactLabel);
        Controls.Add(CapitalLabel);
        Controls.Add(CountriesComboBox);
        Controls.Add(ContinentsComboBox);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ComboBox ContinentsComboBox;
    private ComboBox CountriesComboBox;
    private Label CapitalLabel;
    private Label FactLabel;
}
