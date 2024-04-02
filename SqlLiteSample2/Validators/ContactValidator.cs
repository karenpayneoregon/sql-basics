using FluentValidation;
using SqlLiteSample2.Classes;
using SqlLiteSample2.Models;

namespace SqlLiteSample2.Validators;

public class ContactValidator : FL.AbstractValidator<Contacts>
{
    public ContactValidator()
    {
        Include(new FirstLastNameValidator());

        var contactTypes = DapperOperations.ContactTypes();

        RuleFor(c => c.ContactTypeIdentifier)
            .ExclusiveBetween(1, contactTypes.Max(x => x.ContactTypeIdentifier));
    }
}