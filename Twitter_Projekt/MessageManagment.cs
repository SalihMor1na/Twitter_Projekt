using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter_Projekt
{
    internal class MessageManagment
    {
        public static void SendDirectMessage()
        {
            Console.Write("Ange mottagarens användarnamn: ");
            string recipient = Console.ReadLine();
            if (recipient == LoginManagment.loggedInUsername)
            {
                Console.WriteLine("Du kan inte skicka meddelanden till dig själv!");
                return;
            }
            
            UserManagment user = UserManagment.users.FirstOrDefault(u => u.Username == recipient);
            if (user != null)
            {
                Console.Write("Skriv ditt meddelande: ");
                string message = Console.ReadLine();
                Console.WriteLine($"Meddelande skickat till {recipient}: {message}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Användaren finns inte.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}
