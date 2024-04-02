using FluentValidation.Internal;
using SqlLiteSample2.Models;

namespace SqlLiteSample2.Validators;

/// <summary>
/// Validates two properties while ignoring the rest
/// * CompanyName
/// * ContactId
/// * CountryIdentifier
/// </summary>
public class CustomersCityStreetValidator
{
    public static bool Validate(Customers customers)
    {
        
        var properties = new[] { nameof(Customers.Street), nameof(Customers.City) };

        FL.ValidationContext<Customers> context = new(customers,
            new PropertyChain(),
            new MemberNameValidatorSelector(properties));

        FL.IValidator validate = new CustomersValidator();

        
        return validate.Validate(context).IsValid;
    }
}