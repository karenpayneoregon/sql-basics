using FluentValidation;
using SqlLiteSample2.Interfaces;

namespace SqlLiteSample2.Validators;

public class FirstLastNameValidator : FL.AbstractValidator<IPerson>
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