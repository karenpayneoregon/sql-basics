using FluentValidation;
using SqlLiteSample2.Classes;
using SqlLiteSample2.Interfaces;
using SqlLiteSample2.Models;

namespace SqlLiteSample2.Validators;
public class CustomersValidator : AbstractValidator<Customers>
{
    public CustomersValidator()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(c => c.Street)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(c => c.City)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(c => c.ContactId)
            .GreaterThan(0);

        RuleFor(c => c.CountryIdentifier)
            .GreaterThan(0);
    }
}

        public class ContactValidator : AbstractValidator<Contacts>
        {
            public ContactValidator()
            {
                Include(new FirstLastNameValidator());

                var contactTypes = DapperOperations.ContactTypes();

                RuleFor(c => c.ContactTypeIdentifier)
                    .InclusiveBetween(1, contactTypes.Max(x => x.ContactTypeIdentifier));
            }
        }



public class EmployeesValidator : AbstractValidator<Employees>
{
    public EmployeesValidator()
    {
        Include(new FirstLastNameValidator());
    }
}
public class FirstLastNameValidator : AbstractValidator<IPerson>
{
    public FirstLastNameValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(3);
    }
}