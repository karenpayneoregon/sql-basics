using FluentValidation;
using SimpleEfCoreApp.Models;

namespace SimpleEfCoreApp.Validators
{
    /// <summary>
    /// Basic example for a validator
    /// </summary>
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a first name");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a last name");

            /*
             * Is DateTime to let the custom DataGridView calendar control to work
             * else would be a DateOnly.
             */
            RuleFor(x => x.BirthDate)
                .LessThan(x => new DateTime(2006, 1, 1))
                .WithMessage("Please enter a a birth date less than the year 2006");
        }
    }
}