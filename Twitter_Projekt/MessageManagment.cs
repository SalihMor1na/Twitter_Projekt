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
            UserManagment user = Program.users.FirstOrDefault(u => u.Username == recipient);
            if (user != null)
            {
                Console.Write("Skriv ditt meddelande: ");
                string message = Console.ReadLine();
                Console.WriteLine($"Meddelande skickat till {recipient}: {message}");
            }
            else
            {
                Console.WriteLine("Användaren finns inte.");
            }
        }

    }
}
