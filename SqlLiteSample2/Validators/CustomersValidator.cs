using FluentValidation;
using FluentValidation.Internal;
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

public class CustomersCityStreetValidator
{
    public static bool Validate(Customers customers)
    {
        var properties = new[] { nameof(Customers.Street), nameof(Customers.City) };
        ValidationContext<Customers> context = new(customers,
            new PropertyChain(),
            new MemberNameValidatorSelector(properties));

        IValidator validate = new CustomersValidator();
        var result = validate.Validate(context);
        return result.IsValid;
    }
}