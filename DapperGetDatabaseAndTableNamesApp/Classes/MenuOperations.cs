using DapperGetDatabaseAndTableNamesApp.Models;

namespace DapperGetDatabaseAndTableNamesApp.Classes
{
    class MenuOperations
    {
        /// <summary>
        /// Create main menu
        /// The property Information is optional
        /// </summary>
        //public static SelectionPrompt<MenuItem> MainSelectionPrompt()
        //{
        //    SelectionPrompt<MenuItem> menu = new()
        //    {
        //        HighlightStyle = new Style(Color.Black, Color.White, Decoration.None)
        //    };

        //    menu.Title("[cyan]Select a option[/]");
        //    menu.AddChoices(new List<MenuItem>()
        //    {
        //        new () {Id = 1, Text = "Enter an animal",  Information = "Enter",  
        //            Action = Option1 },
        //        new () {Id = 2, Text = "Have all animals speaks", Information = "Speak", 
        //            Action = () =>  AnsiConsole.MarkupLine("[yellow]Call method to have animals speak[/]") },
        //        new () {Id = -1,Text = "Exit"},
        //    });

        //    return menu;

        //}

        public static SelectionPrompt<MenuItem> MainSelectionPrompt()
        {
            SelectionPrompt<MenuItem> menu = new()
            {
                HighlightStyle = new Style(Color.Black, Color.White, Decoration.None)
            };

            menu.Title("[cyan]Select[/]");
            menu.AddChoices(new List<MenuItem>()
            {
                new () {Id = 1, Text = "Get all databases",
                    Action = Operations.IterateDatabases },
                new () {Id = 2, Text = "Get foreign keys", 
                    Action = Operations.GetForeignKeysForNorthWind2024 },
                new () {Id = -1,Text = "Exit"},
            });

            return menu;

        }

        public static void Option1()
        {
            
            while (true)
            {
                Console.Clear();
                var menuItem = AnsiConsole.Prompt(MainSelectionPrompt());
                if (menuItem.Id != -1)
                {
                    menuItem.Action();
                }
                else
                {
                    return;
                }
            }

        }

        public static void EnterAnimal()
        {
            Console.WriteLine("Enter animal code goes here");
            Console.ReadLine();
        }

        public static void DoSomething(string text)
        {
            Console.WriteLine(text);
            Console.ReadLine();
        }
    }
 
}
