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
        public static List<User> users = LoadUsers();
        public static int loginChooise;
        public static int repostChoice;
        public static List<string> repostList = new List<string>();

        public static string loggedInUsername;
        private static bool adShown = false;

        public static void Main(string[] args)
        {
            LoadUsers();
            HandleLoginMenu();
            HandleMenu();


        }
        // Alternativ 1 - Skapa Post
        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            listofposts.Add(post);
        }

        // Alternativ 2 - Visa alla posts.
        public static void ShowAllPost()
        {
            Console.WriteLine();

            if (listofposts.Count == 0)
            {
                Console.WriteLine("Du har inga inlägg att visa!");
            }
            else
            {
                Console.WriteLine("Här kommer alla inlägg");
                int i = 1;
                foreach (string post in listofposts)
                {
                    int postLength = post.Length + 6;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(new string('-', postLength));

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"| {i}.{post} |");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(new string('-', postLength));
                    Console.ForegroundColor = ConsoleColor.White;
                    i++;
                }
            }
        }
        // Alternativ 3 - Ta bort tweet.
        public static void DeleteTweet()
        {
            Console.WriteLine("Skriv vilket inlägg du vill ta bort");
            ShowAllPost();
            int removePost = 0;
            try
            {
                removePost = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det måste vara ett nummer!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            bool run = true;
            while (run)
            {
                if (removePost > listofposts.Count || removePost < listofposts.Count)
                {
                    Console.WriteLine("Det inlägget finns ej! försök igen");
                    try
                    {
                        removePost = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Det måste vara ett nummer!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    run = false;
                }
            }

            listofposts.RemoveAt(removePost - 1);
            Console.WriteLine($"Du tog bort inlägg nummer {removePost}");
        }

        // Alternativ 4 - Sök efter användare.
        public static void SearchForUSer()
        {
            Console.Write("Ange användarnamnet på personen du vill följa: ");
            string userToFollow = Console.ReadLine();

            User user = users.FirstOrDefault(u => u.Username.Equals(userToFollow, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                user.Followers.Add(loggedInUsername);
                Console.WriteLine($"Du följer nu {userToFollow}.");
            }
            else
            {
                Console.WriteLine("Användaren finns inte.");
            }
        }

        //Alternativ 5 - Reposta en tweet.
        public static void Reposta()
        {
            Console.WriteLine("Vilket inlägg vill du reposta");
            ShowAllPost();
            bool run = true;
            while (run)
            {

                try { repostChoice = int.Parse(Console.ReadLine()) - 1; }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara ett av följade nummer!");
                    try { repostChoice = int.Parse(Console.ReadLine()) - 1; }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Det måste vara ett av följade nummer!");
                        repostChoice = int.Parse(Console.ReadLine()) - 1;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (repostChoice >= 0 && repostChoice < listofposts.Count)
                {
                    run = false;
                }
            }

            var saveRepost = listofposts[repostChoice];
            repostList.Add(saveRepost);
            Console.WriteLine($"Du har nu repostat {listofposts[repostChoice]}");

            Console.WriteLine("Vill du se alla dina repost svara med Ja/Nej");
            string showRepostChoice = Console.ReadLine().ToLower();


            if (showRepostChoice == "ja")
            {
                foreach (var item in repostList)
                {
                    Console.WriteLine(item);
                }
            }
            else if (repostList == null)
            {
                Console.WriteLine("Finns inga repost");
            }

        }

        //Alternativ 6 - Skickar meddelande till en användare.
        public static void SendDirectMessage()
        {
            Console.Write("Ange mottagarens användarnamn: ");
            string recipient = Console.ReadLine();
            User user = users.FirstOrDefault(u => u.Username == recipient);
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

        // Alternativ 7 - Visa följare och personer du följer.
        public static void ShowUserInfo()
        {
            User user = users.FirstOrDefault(u => u.Username == loggedInUsername);
            if (user != null)
            {
                int followersCount = user.Followers.Count;
                int followingCount = users.Count(u => u.Followers.Contains(loggedInUsername));
                Console.WriteLine($"Du har {followersCount} följare och följer {followingCount} person.");
            }
        }

        //Alternativ 8
        public static void Logout()
        {
            Console.Write("Är du säker på att du vill logga ut? (Ja/Nej): ");
            if (Console.ReadLine().Trim().ToLower() == "ja")
            {
                Console.Clear();
                loggedInUsername = null;
                Console.WriteLine("Du har loggat ut.");
                Thread.Sleep(2000);
                HandleLoginMenu();
            }
        }
        //Alternativ 9 
        public static void EditPost()
        {
            Console.WriteLine("Ange numret på inlägget du vill redigera:");
            ShowAllPost();
            if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber > 0 && postNumber <= listofposts.Count)
            {
                Console.Write("Skriv din nya text för inlägget: ");
                listofposts[postNumber - 1] = Console.ReadLine();
                Console.WriteLine("Inlägget har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Ogiltigt nummer, försök igen.");
            }
        }
        // Skapa ett konto.
        public static void CreateAccount()
        {
            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ett lösenord: ");
            string password = ReadPassword();
            while (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver, Försök igen!");
                Console.Write("Ange ett lösenord: ");
                password = Console.ReadLine();
            }

            Console.WriteLine("Ange din e-postadress: ");
            string email = Console.ReadLine();
            while (!IsValidEmail(email))
            {
                Console.WriteLine("Ogiltig e-postadress. Försök igen: ");
                email = Console.ReadLine();
            }
            

            
            Console.WriteLine("Ange ditt förnamn: ");
            string firstname = Console.ReadLine();
            while (string.IsNullOrEmpty(firstname))
            {
                Console.WriteLine("Förnamn får inte vara tomt. Ange ditt förnamn igen");
                Console.Write("Ange ditt förnamn: ");
                firstname = Console.ReadLine();
            }

            Console.WriteLine("Ange ditt efternamn: ");
            string lastname = Console.ReadLine();
            while (string.IsNullOrEmpty(lastname))
            {
                Console.WriteLine("Efternamn får inte vara tomt. Ange ditt förnamn igen");
                Console.Write("Ange ditt efternamn: ");
                lastname = Console.ReadLine();
            }

            Console.WriteLine("Välj kön (1 - Man, 2 - Kvinna, 3 - Annat) : ");
            string genderInput = Console.ReadLine();
            string gender = "";
            while (genderInput != "1" && genderInput != "2" && genderInput != "3")
            {
                Console.WriteLine("Ogiltigt val. Välj 1 för Man, 2 för Kvinna eller 3 för annat");
                genderInput = Console.ReadLine();
            }
            switch (genderInput)
            {
                case "1": gender = "Man"; break;
                case "2": gender = "Kvinna"; break;
                case "3": gender = "Annat"; break;
            }



            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Användarnamnet är upptaget. Försök igen.");
                    return;
                }
            }

            User newUser = new User { Username = username, Password = password };
            users.Add(newUser);

            SaveUsers();
            Console.WriteLine("Konto skapat!");
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



        // Login
        public static bool Login()
        {
            Console.Write("Ange ditt användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ditt lösenord: ");
            string password = ReadPassword();

            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    loggedInUsername = username;
                    Console.WriteLine("Inloggning lyckades!");
                    return true;
                }
            }

            Console.WriteLine("Fel användarnamn eller lösenord.");
            return false;
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
        public static void SaveUsers()
        {
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
        }

        static List<User> LoadUsers()
        {
            if (File.Exists("users.json"))
            {
                string jsonString = File.ReadAllText("users.json");
                return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
            }
            return new List<User>();
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
            User currentUser = users.FirstOrDefault(u => u.Username == loggedInUsername);
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
                            newPassword = ReadPassword();
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
                    CreateAccount();
                }
                else if (loginChooise == 2)
                {
                    if (Login())
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
                        CreatePost();
                        break;
                    case 2:
                        ShowAllPost();
                        break;
                    case 3:
                        DeleteTweet();
                        break;
                    case 4:
                        SearchForUSer();
                        break;
                    case 5:
                        if (listofposts.Count >= 1)
                        {
                            Reposta();
                        }
                        else if (listofposts.Count < 1)
                        {
                            Console.WriteLine("finns inga inlägg att reposta!");
                        }
                        break;
                    case 7:
                        ShowUserInfo();
                        break;
                    case 6:
                        SendDirectMessage();
                        break;
                    case 8:
                        Logout();
                        break;
                    case 9:
                        EditPost();
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

