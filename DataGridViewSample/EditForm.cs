﻿using DataGridViewSample.Classes;

namespace DataGridViewSample;
public partial class EditForm : Form
{
    private readonly int _position;

    public EditForm()
    {
        InitializeComponent();
        Shown += EditFormShown;
    }

    public EditForm(int position)
    {
        InitializeComponent();
        Shown += EditFormShown;
        _position = position;
    }

    private void EditFormShown(object sender, EventArgs e)
    {
        TitleTextBox.DataBindings.Add(
            "Text",
            DataSource.Instance.BindingList[_position],
            "Title");

        PriceTextBox.DataBindings.Add(
            "Text",
            DataSource.Instance.BindingList[_position],
            "Price");
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        DataSource.Instance.BindingSource.ResetCurrentItem();
        DialogResult = DialogResult.OK;
    }
}
