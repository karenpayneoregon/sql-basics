using System.Text;
using Dapper;
using DapperSimpleInsertApp.Models;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;

namespace DapperSimpleInsertApp;

public partial class Form1 : Form
{
    private static string _connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InsertExamples;Integrated Security=True;Encrypt=False";

    public Form1()
    {
        InitializeComponent();
        Shown += Form1_Shown;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        ActiveControl = AddButton;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        // create a new customer and populate from form controls
        Customer customer = new()
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Active = ActiveCheckBox.Checked
        };

        // setup validation
        CustomerValidator validator = new CustomerValidator();
        // validate
        ValidationResult result = validator.Validate(customer);

        // assert given customer is valid
        if (result.IsValid)
        {
            var statement = "INSERT INTO dbo.Customer (FirstName,LastName,Active) " +
                            "VALUES (@FirstName, @LastName, @Active); SELECT CAST(scope_identity() AS int);";

            using var cn = new SqlConnection(_connectionString);
            customer.Id = cn.QueryFirst<int>(statement, customer);
            MessageBox.Show($"Customer id is {customer.Id}");
        }
        else
        {
            StringBuilder builder = new();
            builder.AppendLine($"The following are required to add a new {nameof(Customer)}");
            foreach (var error in result.Errors)
            {
                builder.AppendLine(error.ErrorMessage);
            }

            MessageBox.Show(builder.ToString());
        }

    }
}