using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Twitter_Projekt
{
    internal class LoginManagment
    {
        public static int loginChoice;
        public static string loggedInUsername;


        public static bool Login()
        {
            Console.Write("Ange ditt användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ditt lösenord: ");
            string password = ReadPassword();

            foreach (UserManagment user in UserManagment.users)
            {
                if (user.Username == username && user.Password == password)
                {
                    loggedInUsername = username;
                    Console.WriteLine("Inloggning lyckades!");
                    return true;
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Fel användarnamn eller lösenord.");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
            return false;
        }

        public static void Logout()
        {
            Console.Write("Är du säker på att du vill logga ut? (Ja/Nej): ");
            if (Console.ReadLine().Trim().ToLower() == "ja")
            {
                Console.Clear();
                loggedInUsername = null;
                Console.WriteLine("Du har loggat ut.");
                Thread.Sleep(2000);
                MenuManagment.HandleLoginMenu();
            }
        }
        public static string ReadPassword()
        {
            string password = "";
            while (true)
            {

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("\n");
                    break;
                }

                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }

                else if (key.KeyChar != '\0')
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }

            return password;
        }
    }
}
