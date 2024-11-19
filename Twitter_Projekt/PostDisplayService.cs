using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class PostDisplayService
    {
        public static void ShowAllPost(UserInterface ui)
        {
            ui.PrintMessage("");

            if (PostService.listOfPosts.Count == 0)
            {
                ui.PrintMessage("Du har inga inlägg att visa!", ConsoleColor.Yellow);
            }
            else
            {
                ui.PrintMessage("Här kommer alla inlägg");
                int i = 1;
                foreach (string post in PostService.listOfPosts)
                {
                    int postLength = post.Length + 6;
                    ui.PrintMessage(new string('-', postLength), ConsoleColor.Cyan);

                    ui.PrintMessage($"| {i}.{post} |", ConsoleColor.White);

                    ui.PrintMessage(new string('-', postLength), ConsoleColor.Cyan);
                    i++;
                }
            }
        }
    }
}
