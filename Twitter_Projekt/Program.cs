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

        public static string loggedInUsername;

        public static void Main(string[] args)
        {
            LoadUsers();
            bool isRunnning = true;
            while (isRunnning)
            {
                Console.WriteLine("1: Skapa ett konto");
                Console.WriteLine("2: Logga in");

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
                else
                {
                    Console.WriteLine("Vänligen ange ett giltigt val!");
                }
            }


            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Välj ett av följande alternativ");
                Console.WriteLine("1: Skapa ett inlägg");
                Console.WriteLine("2: Visa alla tweets");
                Console.WriteLine("3: Ta bort tweet");
                Console.WriteLine("4: Sök efter följare");
                Console.WriteLine("5: Retweeta");
                Console.WriteLine("6: Skicka DM");
                Console.WriteLine("7: Visa mina följare");
                Console.WriteLine();

                int chooise = int.Parse(Console.ReadLine());

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


                }

                Console.ReadKey();
            }
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

                    Console.WriteLine($"{i}.{post}");
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

            Console.WriteLine($"Du har nu repostat {listofposts[repostChoice]}");

        }
        // Alternativ 6 - Visa följare och personer du följer.
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
        // Skapa ett konto.
        public static void CreateAccoount()
        {
            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ett lösenord: ");
            string password = Console.ReadLine();

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


    }
}

