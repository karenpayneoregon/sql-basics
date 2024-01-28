using DapperSimpleEfCoreApp.Classes;
using DapperSimpleEfCoreApp.Models;
using DapperSimpleEfCoreApp.Validators;
using FluentValidation.Results;

// ReSharper disable CollectionNeverQueried.Local
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0067 // Event is never used

namespace DapperSimpleEfCoreApp;
public partial class AddPersonForm : Form
{
    public delegate void OnValidatePerson(Person person);
    public event OnValidatePerson ValidPerson;
    private readonly Dictionary<string, Control> _controls = new();
    private Person Person { get; set; }

    public AddPersonForm()
    {
        InitializeComponent();

        IEnumerable<Control> items = this.Descendants<Control>()
            .Where(x => x.Tag != null);

        foreach (Control item in items)
        {
            _controls.Add(item.Tag!.ToString()!, item);
        }
    }

    private void AddNewButton_Click(object sender, EventArgs e)
    {
        // clear error provider text on each control
        foreach (var control in _controls)
        {
            errorProvider1.SetError(control.Value, "");
        }

        Person = new Person()
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            BirthDate = BrthDateTimePicker.Value
        };

        /*
         * Validate Person
         */
        PersonValidator validator = new PersonValidator();
        ValidationResult result = validator.Validate(Person);

        if (!result.IsValid)
        {
            /*
             * Show issues
             *
             */
            //var builder = new StringBuilder();
            //result.Errors.Select(x => x.ErrorMessage).ToList().ForEach(x => builder.AppendLine(x));
            //MessageBox.Show(builder.ToString());
            foreach (var item in result.Errors)
            {

                if (_controls.TryGetValue(item.PropertyName, out var control))
                {
                    errorProvider1.SetError(control, item.ErrorMessage);
                }
            }
        }
        else
        {
            ValidPerson?.Invoke(Person);
            Close();
        }
    }
}
