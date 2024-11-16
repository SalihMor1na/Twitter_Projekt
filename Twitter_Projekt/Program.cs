using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Twitter_Projekt
{
    class Program
    {
        public static List<string> listofposts = new List<string>();
        public static List<UserManagment> users = UserManagment.LoadUsers();
        public static int loginChooise;
        public static int repostChoice;
        public static List<string> repostList = new List<string>();

        public static string loggedInUsername;
        private static bool adShown = false;

        public static void Main(string[] args)
        {
            UserManagment.LoadUsers();
            HandleLoginMenu();
            HandleMenu();
        }
       
       
        // Reklam dyker upp 5 sekunder
        public static async Task Ad()
        {
            Random random = new Random();

            if (!adShown) 
            {
                adShown = true;

                int randomInterval = random.Next(5000, 10000); 
                await Task.Delay(randomInterval);

                ShowAd();
            }
        }

        public static void ShowAd()
        {
            Console.Clear();
            Console.WriteLine("Du kollar nu på en reklam, reklamen slutar om 5 sekunder...");

            Thread.Sleep(5000); 
            
            Console.Clear();
            Console.WriteLine("Du kan nu fortsätta använda din kostnadsfria Twitter");
            HandleMenu();
        }
        // Alternativ 11 - Användarinställningar
        public static void HandleSettings()
        {
            UserManagment currentUser = users.FirstOrDefault(u => u.Username == loggedInUsername);
            if (currentUser == null)
            {
                Console.WriteLine("Något gick fel. Användaren kunde inte hittas");
                return;
            }

            bool inSettings = true;

            while (inSettings)
            {
                Console.Clear();
                Console.WriteLine("INSTÄLLNINGAR");
                Console.WriteLine("1. Ändra ditt användarnamn");
                Console.WriteLine("2. Ändra ditt lösenord");
                Console.WriteLine("3. Gå tillbaka till huvudmenyn");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        Console.Write("Ange ditt nya användarnamn: ");
                        string newUsername = Console.ReadLine();

                        if (users.Any(u => u.Username.Equals(newUsername, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine("Det angivna användarnamnet är upptaget. Försök med ett annat användarnamn");
                        }
                        else
                        {
                            currentUser.Username = newUsername;
                            loggedInUsername = newUsername;
                            Console.WriteLine("Användarnamnet har ändrats!");
                        }
                        break;

                        case "2":

                        Console.Write("Ange ditt nya lösenord: ");
                        string newPassword = Console.ReadLine();

                        while (newPassword.Length < 6 || !newPassword.Any(char.IsDigit) || !newPassword.Any(char.IsLetter))
                        {
                            Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver. Försök igen: ");
                            newPassword = LoginManagment.ReadPassword();
                        }
                        currentUser.Password = newPassword;
                        Console.WriteLine("Lösenordet har ändrats.");
                        break;
                }
            }
        }
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
                    loginChooise = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Det måste vara en siffra");
                }

                if (loginChooise == 1)
                {
                    UserManagment.CreateAccount();
                }
                else if (loginChooise == 2)
                {
                    if (LoginManagment.Login())
                    {
                        Console.WriteLine($"Välkommen, {loggedInUsername}!");
                        Thread.Sleep(2000);
                        isRunnning = false;
                        
                    }
                }
                else if (loginChooise == 3)
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
        Task adTask = Ad();
        
        public static void HandleMenu()
        {
            bool error = false;
            bool runProgram = true;
            Ad();


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
                        if (listofposts.Count >= 1)
                        {
                            PostManagment.Reposta();
                        }
                        else if (listofposts.Count < 1)
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

