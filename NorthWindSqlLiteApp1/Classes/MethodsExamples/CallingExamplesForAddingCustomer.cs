using NorthWindSqlLiteApp1.Classes.Core;
using Spectre.Console;

namespace NorthWindSqlLiteApp1.Classes.MethodsExamples;

public class CallingExamplesForAddingCustomer
{
    public static void AddingSingleCustomer()
    {
 
        // nothing returned
        MethodExamples.AddCustomer1();

        bool successfully = MethodExamples.AddCustomer2();
        if (successfully)
        {
            SpectreConsoleHelpers.SuccessPill(Justify.Left, "Customer added successfully using AddCustomer2.");
        }
        else
        {
            // Failed to add customer
        }

        
        {

            //var (success, identifier) = MethodExamples.AddCustomer3();
            //if (success)
            //{
            //    // Use the identifier as needed
            //} else    
            //{
            //    // Failed to add customer
            //}

        }

        {
            var (success, identifier, exception) = MethodExamples.AddCustomer4();
            if (success)
            {
                // Customer added with identifier
            }
            else
            {
                // Failed to add customer 
                if (exception is not null)
                {
                    // 
                }
            }

        }

    }
}