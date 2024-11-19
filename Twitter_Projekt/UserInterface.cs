using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class UserInterface
    {
        public void PrintMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public int GetUserChoice(string prompt)
        {
            Console.Write(prompt);

            return 0;
        }
    }
}
