using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Twitter_Projekt
{
    internal class MenuManagment
    {
        public static void HandleLoginMenu()
        {
            bool isRunnning = true;
            while (isRunnning)
            {
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 1: Skapa Konto        |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 2: Logga in           |");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 3: Avsluta Programmet |");
                Console.WriteLine(" -----------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                try
                {
                    LoginManagment.loginChooise = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Det måste vara en siffra");
                }

                if (LoginManagment.loginChooise == 1)
                {
                    UserManagment.CreateAccount();
                }
                else if (LoginManagment.loginChooise == 2)
                {
                    if (LoginManagment.Login())
                    {
                        Console.WriteLine($"Välkommen, {LoginManagment.loggedInUsername}!");
                        Thread.Sleep(2000);
                        isRunnning = false;

                    }
                }
                else if (LoginManagment.loginChooise == 3)
                {

                    Console.WriteLine("Programmet avslutas nu.");
                    Thread.Sleep(2000);
                    isRunnning = false;
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine("Vänligen ange ett giltigt val!");
                }
            }
        }

        public static void HandleMenu()
        {
            bool error = false;
            bool runProgram = true;
            AdManagment.Ad();


            while (runProgram)
            {

                Console.Clear();

                if (error == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara ett nummer!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Välj ett av följande alternativ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 1: Skapa ett inlägg   |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 2: Visa alla tweets   |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 3: Ta bort tweet      |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 4: Sök efter följare  |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 5: Retweeta           |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 6: Skicka DM          |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 7: Visa mina följare  |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 8: Logga ut           |");
                Console.WriteLine(" -----------------------");
                Console.WriteLine(" -----------------------");
                Console.WriteLine("|9: Redigera inlägg     |");
                Console.WriteLine(" -----------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 10: Avsluta programmet |");
                Console.WriteLine(" -----------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine();


                int chooise = 0;
                try { chooise = int.Parse(Console.ReadLine()); }
                catch
                {
                    error = true;
                }

                switch (chooise)
                {
                    case 1:
                        PostManagment.CreatePost();
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
                        else if (PostManagment.listofposts.Count < 1)
                        {
                            Console.WriteLine("finns inga inlägg att reposta!");
                        }
                        break;
                    case 7:
                        UserManagment.ShowUserInfo();
                        break;
                    case 6:
                        MessageManagment.SendDirectMessage();
                        break;
                    case 8:
                        LoginManagment.Logout();
                        break;
                    case 9:
                        PostManagment.EditPost();
                        break;
                    case 10:
                        runProgram = false;
                        Console.WriteLine("Programmet avslutas nu.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                }

                Console.ReadKey();
            }
        }
    }
}
