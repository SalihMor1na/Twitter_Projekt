using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Twitter_Projekt
{
    class Program
    {
        public static List<string> listofposts = new List<string>();
        public static List<User> users = LoadUsers();
        public static string username;
        public static string password;
        public static int loginChooise;

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
                    Console.WriteLine("Det måste vare en siffra");
                }

                if (loginChooise == 1)
                {
                    Createaccoount();
                }
                else if (loginChooise == 2)
                {
                   if (Login())
                    {
                        Console.WriteLine($"Välkommen!");
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
                }

                Console.ReadKey();
            }



          
        }

        public static void ShowAllPost()
        {
            Console.WriteLine();
            Console.WriteLine("Här kommer alla inlägg");
            int i = 1;
            foreach (string post in listofposts)
            {
                Console.WriteLine($"{i}.{post}");
                i++;

            }
        }
        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            listofposts.Add(post);


        }

        public static void Createaccoount()
        {

            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ett lösenord: ");
            string password = Console.ReadLine();

            // Kontrollera om användarnamnet redan finns
            foreach (User user in users)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Användarnamnet är upptaget. Försök igen.");
                    return;
                }
            }

            // Skapa ny användare och lägg till i listan
            User newUser = new User { Username = username, Password = password };
            users.Add(newUser);

            // Spara användare till JSON-fil
            SaveUsers();
            Console.WriteLine("Konto skapat!");



        }

        public static bool Login()
        {
            Console.Write("Ange ditt användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ditt lösenord: ");
            string password = Console.ReadLine();

            // Kontrollera om användaren finns och lösenordet är korrekt
            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    Console.WriteLine("Inloggning lyckades! Välkommen, " + username);
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


        public static void DeleteTweet()
        {
            Console.WriteLine("Skriv vilket inlägg du vill ta bort");
            ShowAllPost();
            int removePost = int.Parse(Console.ReadLine());
            listofposts.RemoveAt(removePost - 1);
            Console.WriteLine($"Du tog bort {removePost}");
        }
    }
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
}