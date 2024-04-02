using SqlLiteSample2.Models;

namespace SqlLiteSample2.Validators;

public class EmployeesValidator : FL.AbstractValidator<Employees>
{
    public EmployeesValidator()
    {
        Include(new FirstLastNameValidator());
    }
}