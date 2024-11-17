using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Twitter_Projekt
{
    public class UserManagment
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool NotificationsEnabled { get; set; }
        public List<string> Followers { get; set; } = new List<string>();

        public static List<UserManagment> users = LoadUsers();
        public static void CreateAccount()
        {

            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            foreach (UserManagment user in users)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Användarnamnet är upptaget. Försök igen.");
                    Console.Write($"\nTryck på valfri tangetknapp för att forsätta .... ");
                    Console.ReadKey();
                    return;
                }else if (string.IsNullOrWhiteSpace(username))
                {
                    bool isRunning = true;
                    while (isRunning)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Det får inte vara tomt");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Försök igen");
                        username = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(username))
                        {
                            isRunning = false;
                        }

                    }
                    
                }
            }

            Console.Write("Ange ett lösenord: ");
            string password = LoginManagment.ReadPassword();
            while (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver, Försök igen!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Ange ett lösenord: ");
                password = LoginManagment.ReadPassword();
            }

            Console.WriteLine("Ange din e-postadress: ");
            string email = Console.ReadLine();
            while (!IsValidEmail(email))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ogiltig e-postadress. Försök igen: ");
                Console.ForegroundColor = ConsoleColor.White;
                email = Console.ReadLine();
            }



            Console.WriteLine("Ange ditt förnamn: ");
            string firstname = Console.ReadLine();
            while (string.IsNullOrEmpty(firstname))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Förnamn får inte vara tomt. Ange ditt förnamn igen");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Ange ditt förnamn: ");
                firstname = Console.ReadLine();
            }

            Console.WriteLine("Ange ditt efternamn: ");
            string lastname = Console.ReadLine();
            while (string.IsNullOrEmpty(lastname))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Efternamn får inte vara tomt. Ange ditt förnamn igen");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Ange ditt efternamn: ");
                lastname = Console.ReadLine();
            }

            Console.WriteLine("Välj kön (1 - Man, 2 - Kvinna, 3 - Annat) : ");
            string genderInput = Console.ReadLine();
            string gender = "";
            while (genderInput != "1" && genderInput != "2" && genderInput != "3")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ogiltigt val. Välj 1 för Man, 2 för Kvinna eller 3 för annat");
                Console.ForegroundColor = ConsoleColor.White;
                genderInput = Console.ReadLine();
            }
            switch (genderInput)
            {
                case "1": gender = "Man"; break;
                case "2": gender = "Kvinna"; break;
                case "3": gender = "Annat"; break;
            }

            UserManagment newUser = new UserManagment { Username = username, Password = password };
            users.Add(newUser);

            SaveUsers();
            Console.WriteLine("Konto skapat!");
            Thread.Sleep(1000);
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void ShowUserInfo()
        {
            UserManagment user = users.FirstOrDefault(u => u.Username == LoginManagment.loggedInUsername);
            if (user != null)
            {
                int followersCount = user.Followers.Count;
                int followingCount = users.Count(u => u.Followers.Contains(LoginManagment.loggedInUsername));
                Console.WriteLine($"Du har {followersCount} följare och följer {followingCount} person.");
            }
        }
        public static void SearchForUSer()
        {
            Console.Write("Ange användarnamnet på personen du vill följa: ");
            string userToFollow = Console.ReadLine();

            UserManagment user = users.FirstOrDefault(u => u.Username.Equals(userToFollow, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                user.Followers.Add(LoginManagment.loggedInUsername);
                Console.WriteLine($"Du följer nu {userToFollow}.");
            }
            else
            {
                Console.WriteLine("Användaren finns inte.");
            }
        }

        public static void SaveUsers()
        {
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
        }

        public static List<UserManagment> LoadUsers()
        {
            if (File.Exists("users.json"))
            {
                string jsonString = File.ReadAllText("users.json");
                return JsonSerializer.Deserialize<List<UserManagment>>(jsonString) ?? new List<UserManagment>();
            }
            return new List<UserManagment>();
        }

        public static void ShowHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine("         HJÄLP - ANVÄNDARINSTRUKTIONER         ");
            Console.WriteLine("===============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine("1. Skapa ett inlägg: Skapa nya tweets och dela dina tankar.");
            Console.WriteLine("2. Visa alla tweets: Se alla tweets som du eller andra har postat.");
            Console.WriteLine("3. Ta bort tweet: Ta bort en tweet som du tidigare postat.");
            Console.WriteLine("4. Sök efter följare: Sök och följ andra användare.");
            Console.WriteLine("5. Retweeta: Dela andras tweets.");
            Console.WriteLine("6. Skicka DM: Skicka direktmeddelanden till andra användare.");
            Console.WriteLine("7. Visa mina följare: Se vilka som följer dig.");
            Console.WriteLine("8. Logga ut: Logga ut från ditt konto.");
            Console.WriteLine("9. Redigera inlägg: Ändra innehållet i dina tidigare tweets.");
            Console.WriteLine("10. Lika/dislika inlägg: Gilla eller ogilla tweets.");
            Console.WriteLine("11. Kontoinställningar: Ändra dina personliga inställningar.");
            Console.WriteLine("12. Visa hjälp: Visa denna hjälpsida.");
            Console.WriteLine("13. Avsluta programmet: Stänger programmet.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        public static void HandleSettings()
        {
            UserManagment currentUser = users.FirstOrDefault(u => u.Username == LoginManagment.loggedInUsername);
            if (currentUser == null)
            {
                Console.WriteLine("Något gick fel. Användaren kunde inte hittas");
                return;
            }

            bool inSettings = true;
            bool notificationsEnabled = currentUser.NotificationsEnabled;
            while (inSettings)
            {
                Console.Clear();
                Console.WriteLine("INSTÄLLNINGAR");
                Console.WriteLine("1. Ändra ditt användarnamn");
                Console.WriteLine("2. Ändra ditt lösenord");
                Console.WriteLine("3. Hantera notifikationer");
                Console.WriteLine("4. Gå tillbaka till huvudmenyn");
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
                        else if (string.IsNullOrWhiteSpace(newUsername))
                        {
                            bool isRunning = true;
                            while (isRunning)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Det får inte vara tomt");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Försök igen");
                                newUsername = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newUsername))
                                {
                                    Console.WriteLine("Användarnamnet har ändrats!");
                                    isRunning = false;
                                }

                            }

                        }
                        else
                        {
                            currentUser.Username = newUsername;
                            LoginManagment.loggedInUsername = newUsername;
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

                    case "3":
                        Console.WriteLine("Notifikationer: ");

                        if (notificationsEnabled)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("PÅ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("AV");
                        }
                        Console.ResetColor();

                        Console.WriteLine("Ändra notifikationsstatus");
                        Console.WriteLine("1. Aktivera notifikationer");
                        Console.WriteLine("2. Avaktivera notifikationer");
                        Console.Write("Välj ett alternativ: ");
                        string notificationsChoice = Console.ReadLine();
                        
                        if (notificationsChoice == "1")
                        {
                            notificationsEnabled = true;
                            currentUser.NotificationsEnabled = true;
                            Console.WriteLine("Notifikationer är på");
                        }
                        else if (notificationsChoice == "2")
                        {
                            notificationsEnabled = false;
                            currentUser.NotificationsEnabled = false;
                            Console.WriteLine("Notifikationer är av");
                        }
                        else
                        {
                            Console.WriteLine("Ogiltligt val, återgår till inställningsmenyn");
                        }
                        break;


                    case "4":
                        inSettings = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;

                    
        
                }
                SaveUsers();
                Thread.Sleep(1000);
            }
        }

        public static void Like_Dislike()
        {

        }

        public static void Theme()
        {
            int choiceTheme = 0;
            Console.WriteLine("Vill du sätta på mörkt läge eller vill du ha ljust");
            Console.WriteLine("1: Mörkt");
            Console.WriteLine("2: Ljust");

            try { choiceTheme = int.Parse(Console.ReadLine()); }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det måste vara ett nummer");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (choiceTheme == 1 || choiceTheme == 2)
            {
                try { choiceTheme = int.Parse(Console.ReadLine()); }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara ett nummer");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det måste vara ett av tillgängliga nummer");
                Console.ForegroundColor = ConsoleColor.White;
            }



            if (choiceTheme == 1)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine("Temat är nu mörkt!");
            }
            else if (choiceTheme == 2)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("Temat är nu ljust!");

            }

        }

    }
}
