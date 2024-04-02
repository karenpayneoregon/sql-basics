using FluentValidation;
using SqlLiteSample2.Classes;
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


        var contacts = DapperOperations.Contacts();

        RuleFor(c => c.ContactId)
            .ExclusiveBetween(1, contacts.Max(x => x.ContactId));

        var countries = DapperOperations.Countries();

        RuleFor(c => c.CountryIdentifier)
            .ExclusiveBetween(1, countries.Max(x => x.CountryIdentifier));
    }

}