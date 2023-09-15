using System.ComponentModel;
using DateLibrary;
using DateLibrary.Models;
using WorkingWithDatesSqlServer.Classes;
using WorkingWithDatesSqlServer.Extensions;

namespace WorkingWithDatesSqlServer;

/// <summary>
/// - We use a form as it requires minimal effort verses a web app
/// - All database operations are in a class project
/// </summary>
public partial class Form1 : Form
{
    private BindingList<Calendar> _calendars;
    private BindingSource _source = new ();
    public Form1()
    {
        InitializeComponent();
    }

    private void GetCalendar4Button_Click(object sender, EventArgs e)
    {

        var (data, exception) = SqlOperations.GetCalendar4(
            2023, 5, 1, DayOfWeek.Monday);

        if (exception is null)
        {
            _calendars = new BindingList<Calendar>(data);
            _source = new BindingSource();
            _source.DataSource = _calendars;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _source;
            
            AppOperations.Instance.CalendarColumns
                .ForEach(name => dataGridView1.Columns[name]!.Visible = false);
            
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.ValueType.Name != "DateTime") continue;
                column.DefaultCellStyle.Format = "MM/dd/yyyy";
            }

            dataGridView1.ExpandColumns();

        }
        else
        {
            MessageBox.Show(@"Failed to complete operation, see log file.");
        }
    }

    private void StartEndCurrentWeekButton_Click(object sender, EventArgs e)
    {
        var (startDate, endDate) = SqlOperations.GetCurrentWeekStartEnd();
        CurrentWeekTextBox.Text = $@"from {startDate} to {endDate}";
    }

    private void StartEndCurrentWeekDapperButton_Click(object sender, EventArgs e)
    {
        var (startDate, endDate) = SqlOperations.GetCurrentWeekStartEndDapper();
        CurrentWeekDapperTextBox.Text = $@"from {startDate} to {endDate}";
    }

    private void HolidaysDuringTheWeekForYearButton_Click(object sender, EventArgs e)
    {

        var list = SqlOperations.HolidaysDuringTheWeekForYear(
            2023, true, true);

        if (list.Any())
        {
            MessageBox.Show(list.FirstOrDefault()?.CalendarDateDescription);
        }
        
    }

    private void HolidaysBetweenMonthsByYearButton_Click(object sender, EventArgs e)
    {
        var data = SqlOperations.HolidaysInYearBetweenMonths(
            2023, 1, 6);

        _calendars = new BindingList<Calendar>(data);
        _source = new BindingSource();
        _source.DataSource = _calendars;


        dataGridView1.DataSource = _source;
        
        AppOperations.Instance.HolidayColumns
            .ForEach(name => dataGridView1.Columns[name]!.Visible = false);

        dataGridView1.ExpandColumns();
    }

    /// <summary>
    /// Example to calculate age of a person
    /// </summary>
    private void CalculateAgeButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show(SqlOperations.GetPeoplesAges());
    }
}
