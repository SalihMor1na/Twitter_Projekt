using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;


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
                    i++;
                }
            }
        }
        // Alternativ 3 - Ta bort tweet.
        public static void DeleteTweet()
        {
            Console.WriteLine("Skriv vilket inlägg du vill ta bort");
            ShowAllPost();
            int removePost = int.Parse(Console.ReadLine());
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
            repostChoice = int.Parse(Console.ReadLine()) - 1;

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

        //Alternativ 9
        public static void Logout()
        {
            Console.Clear();
            loggedInUsername = null;
            Console.WriteLine("Du har loggat ut.");
            Thread.Sleep(2000);
            HandleLoginMenu();
        }
        // Skapa ett konto.
        public static void CreateAccoount()
        {
            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ett lösenord: ");
            string password = Console.ReadLine();
            while (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver, Försök igen!");
                Console.Write("Ange ett lösenord: ");
                password = Console.ReadLine();
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
        // Login
        public static bool Login()
        {
            Console.Write("Ange ditt användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ditt lösenord: ");
            string password = Console.ReadLine();

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
                    CreateAccoount();
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
    
        public static void HandleMenu()
        {
            bool error = false;
            bool runProgram = true;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -----------------------");
                Console.WriteLine("| 9: Avsluta programmet |");
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
                        Reposta();
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

