using FluentValidation;
using SqlLiteSample2.Classes;

namespace SqlLiteSample2;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        // Set up FluentValidation for .WithName
        ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            ValidatorOptions.Global.PropertyNameResolver(type, memberInfo, expression)
                .SplitPascalCase();


        //await EntityOperations.ReadAllCustomers();
        //await EntityOperations.AddNewContact();
        //await EntityOperations.AddNewContactBad();
        await EntityOperations.ReadAllCategories();
        //await EntityOperations.ReadAllEmployeesUpDateOne();
        //await EntityOperations.RemoveOneOrder();
        //await EntityOperations.GetTableNamesInDatabase();
        //await EntityOperations.UpdateCustomerCity();
        //EntityOperations.AddNewCustomerValidateOneProperty();
        EntityOperations.WithName();



        SpectreConsoleHelpers.ExitPrompt();
    }


}