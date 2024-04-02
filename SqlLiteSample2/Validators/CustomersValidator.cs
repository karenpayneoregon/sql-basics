using FluentValidation;
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