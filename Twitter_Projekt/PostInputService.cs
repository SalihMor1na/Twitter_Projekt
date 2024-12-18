using System;
using System.Collections.Generic;
using System.Text;


namespace Twitter_Projekt
{
    internal class PostInputService
    {
        public string GetPostFromUser(UserInterface ui)
        {
            ui.PrintMessage("Vad vill du dela med dig utav?");
            return Console.ReadLine();
        }

        public int GetPostIndexFromUser(UserInterface ui)
        {
            ui.PrintMessage("Ange numret på inlägget:");
            if (int.TryParse(Console.ReadLine(), out int postIndex))
            {
                return postIndex - 1; 
            }

            throw new ArgumentException("Du måste ange ett giltigt nummer.");
        }
    }
}