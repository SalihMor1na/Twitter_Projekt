using System;
using System.Collections.Generic;
using System.Text;


namespace Twitter_Projekt
{
    internal class PostDisplayService
    {
        public void ShowAllPosts(UserInterface ui)
        {
            if (PostService.ListOfPosts.Count == 0)
            {
                ui.PrintMessage("Du har inga inlägg att visa!", ConsoleColor.Yellow);
                return;
            }

            ui.PrintMessage("Här är alla inlägg:");

            int i = 1;
            foreach (string post in PostService.ListOfPosts)
            {
                int postLength = post.Length + 6;
                ui.PrintMessage(new string('-', postLength), ConsoleColor.Cyan);
                ui.PrintMessage($"| {i}. {post} |", ConsoleColor.White);
                ui.PrintMessage(new string('-', postLength), ConsoleColor.Cyan);
                i++;
            }
        }
    }
}
