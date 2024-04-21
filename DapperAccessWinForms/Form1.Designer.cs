namespace DapperAccessWinForms;

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
        CategoriesComboBox = new ComboBox();
        GetCategoryButton = new Button();
        SuspendLayout();
        // 
        // CategoriesComboBox
        // 
        CategoriesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        CategoriesComboBox.FormattingEnabled = true;
        CategoriesComboBox.Location = new Point(12, 12);
        CategoriesComboBox.Name = "CategoriesComboBox";
        CategoriesComboBox.Size = new Size(226, 28);
        CategoriesComboBox.TabIndex = 0;
        // 
        // GetCategoryButton
        // 
        GetCategoryButton.Location = new Point(253, 12);
        GetCategoryButton.Name = "GetCategoryButton";
        GetCategoryButton.Size = new Size(94, 29);
        GetCategoryButton.TabIndex = 1;
        GetCategoryButton.Text = "Category";
        GetCategoryButton.UseVisualStyleBackColor = true;
        GetCategoryButton.Click += GetCategoryButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(380, 235);
        Controls.Add(GetCategoryButton);
        Controls.Add(CategoriesComboBox);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sample";
        ResumeLayout(false);
    }

    #endregion

    private ComboBox CategoriesComboBox;
    private Button GetCategoryButton;
}
