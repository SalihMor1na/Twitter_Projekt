using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Twitter_Projekt
{
    internal class MenuManagment
    {
        private static void PrintMenuHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=================================================");
            Console.WriteLine($"         {title.ToUpper()}");
            Console.WriteLine("=================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        private static void PrintMenuOption(int number, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"| {number.ToString().PadLeft(2)}: {text.PadRight(25)}|");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-------------------------------------------------");
        }

        private static void PrintSeparator()
        {
            Console.WriteLine("=================================================");
        }

        public static void HandleLoginMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                PrintMenuHeader("Login Menu");

                PrintMenuOption(1, "Skapa Konto");
                PrintMenuOption(2, "Logga in");
                PrintMenuOption(3, "Inställningar");
                PrintMenuOption(4, "Avsluta Programmet", ConsoleColor.Red);

                try
                {
                    LoginManagment.loginChoice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("⚠️ Det måste vara en siffra. Försök igen!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                switch (LoginManagment.loginChoice)
                {
                    case 1:
                        UserManagment.CreateAccount();
                        break;
                    case 2:
                        if (LoginManagment.Login())
                        {
                            Console.WriteLine($"Välkommen, {LoginManagment.loggedInUsername}!");
                            Thread.Sleep(2000);
                            isRunning = false;
                        }
                        break;
                    case 3:
                        UserManagment.Theme();
                        break;
                    case 4:
                        Console.WriteLine("Programmet avslutas nu.");
                        Thread.Sleep(2000);
                        isRunning = false;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("⚠️ Vänligen ange ett giltigt val!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                   
                }
            }
        }

        public static void HandleMenu()
        {
            bool runProgram = true;
            
            while (runProgram)
            {
                PrintMenuHeader("Main Menu");
                
                PrintMenuOption(1, "Skapa ett inlägg");
                PrintMenuOption(2, "Visa alla tweets");
                PrintMenuOption(3, "Ta bort tweet");
                PrintMenuOption(4, "Sök efter följare");
                PrintMenuOption(5, "Retweeta");
                PrintMenuOption(6, "Skicka DM");
                PrintMenuOption(7, "Visa mina följare");
                PrintMenuOption(8, "Logga ut");
                PrintMenuOption(9, "Redigera inlägg");
                PrintMenuOption(10, "Lika/dislika inlägg");
                PrintMenuOption(11, "Kontoinställningar");

                PrintSeparator();
                PrintMenuOption(12, "Avsluta programmet", ConsoleColor.Red);

                Console.WriteLine();

                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("⚠️ Det måste vara ett nummer! Försök igen.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                

                switch (choice)
                {
                    case 1:
                        PostManagment.CreatePost();
                        AdManagment.ShowAd();
                        break;
                    case 2:
                        PostManagment.ShowAllPost();
                        break;
                    case 3:
                        PostManagment.DeleteTweet();
                        break;
                    case 4:
                        UserManagment.SearchForUSer();
                        break;
                    case 5:
                        if (PostManagment.listofposts.Count >= 1)
                        {
                            PostManagment.Reposta();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Det finns inga inlägg att retweeta!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case 6:
                        MessageManagment.SendDirectMessage();
                        break;
                    case 7:
                        UserManagment.ShowUserInfo();
                        break;
                    case 8:
                        LoginManagment.Logout();
                        return;
                    case 9:
                        PostManagment.EditPost();
                        break;
                    case 10:
                        PostManagment.Like_Dislike();
                        break;
                    case 11:
                        UserManagment.HandleSettings();
                        break;
                    case 12:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Programmet avslutas nu.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("⚠️ Ogiltigt val! Försök igen.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }
}
