using DapperGetDatabaseAndTableNamesApp.Models;

namespace DapperGetDatabaseAndTableNamesApp.Classes
{
    class MenuOperations
    {
    
        public static SelectionPrompt<MenuItem> MainSelectionPrompt()
        {
            SelectionPrompt<MenuItem> menu = new()
            {
                HighlightStyle = new Style(Color.Black, Color.White, Decoration.None)
            };

            menu.Title("[cyan]Select[/]");
            menu.AddChoices(new List<MenuItem>()
            {
                new () 
                {
                    Id = 1, 
                    Text = "Get all databases",
                    Action = Operations.IterateDatabases 
                },
                new () 
                {
                    Id = 2, 
                    Text = "Get foreign keys", 
                    Action = Operations.GetForeignKeysForNorthWind2024 
                },
                new ()
                {
                    Id = -1,Text = "Exit"
                },
            });

            return menu;

        }

    }
 
}
