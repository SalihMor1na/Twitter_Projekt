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
        private static void ShowAboutUs()
        {
            Console.Clear();

            Console.WriteLine(@"
  _______       _ _   _             _____ 
 |__   __|     (_) | | |           / ____|
    | |_      ___| |_| |_ ___ _ __| |     
    | \ \ /\ / / | __| __/ _ \ '__| |     
    | |\ V  V /| | |_| ||  __/ |  | |____ 
    |_| \_/\_/ |_|\__|\__\___|_|   \_____");
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("                     OM TWITTERC                     ");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine("Detta är ett Twitter liknande program utvecklad ");
            Console.WriteLine("för att hantera tweets, följare och meddelanden. ");
            Console.WriteLine();
            Console.WriteLine("Skapad av: Salih, Ahmad och Petar");
            Console.WriteLine("Version: 1.0");
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn");
            Console.ReadKey();
                

        }



        // All kod för hantering av inloggningsmenyn
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
                PrintMenuOption(5, "Om TwitterC");
                

                try
                {
                    LoginManagment.loginChoice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara en siffra. Försök igen!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
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
                        Console.WriteLine("Vänligen ange ett giltigt val!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 5:
                        ShowAboutUs();
                        break;
                  
                }
            }
        }

        //Kod för huvudmenyn
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
                PrintMenuOption(8, "Redigera inlägg");
                PrintMenuOption(9, "Lika/dislika inlägg");
                PrintMenuOption(10, "Kontoinställningar");
                PrintMenuOption(11, "Logga ut");
                PrintMenuOption(12, "Visa hjälp", ConsoleColor.Green);

                PrintSeparator();
                PrintMenuOption(13, "Avsluta programmet", ConsoleColor.Red);
                Console.WriteLine("=================================================");

                Console.ForegroundColor = ConsoleColor.Blue;
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"Nuvarande tid: {currentTime}");
                Console.ResetColor();

                Console.SetCursorPosition(Console.WindowWidth - 16, Console.CursorTop);
                Console.WriteLine("www.twitterc.com");

                
                Console.WriteLine();

                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara ett nummer! Försök igen.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    continue;
                }

                

                switch (choice)
                {
                    case 1:
                        PostService.CreatePost();                       
                        break;
                    case 2:
                        PostDisplayService.ShowAllPost();
                        break;
                    case 3:
                        PostService.DeleteTweet();
                        break;
                    case 4:
                        UserManagment.SearchForUSer();
                        break;
                    case 5:
                        if (PostService.listOfPosts.Count >= 1)
                        {
                            InteractionService.Reposta();
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
                        PostService.EditPost();
                        break;
                    case 9:
                        InteractionService.Like_Dislike();
                        break;
                    case 10:
                        UserManagment.HandleSettings();
                        break;
                    case 11:
                        LoginManagment.Logout();
                        return;
                    case 12:
                        UserManagment.ShowHelp();
                        break;
                    case 13:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Programmet avslutas nu.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Ogiltigt val! Försök igen.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }


}
