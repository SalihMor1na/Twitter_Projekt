using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Twitter_Projekt
{
    class Program
    {
        public static List<string> listofposts = new List<string>();
        public static List<string> listOfUser = new List<string>();
        public static string username;
        public static string password;
        public static void Main(string[] args)
        {

            Console.WriteLine("1: Skapa ett konto");
            Console.WriteLine("2: Logga in");
            int loginChooise = int.Parse(Console.ReadLine());

            if (loginChooise == 1)
            {
                createaccoount();
            }
            else if (loginChooise == 2) 
            {
                Login();
            }
            else
            {
                Console.WriteLine("Vänligen ange ett giltigt val!");
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
            Console.Write("vänligen ange ditt användernamn: ");
            username = Console.ReadLine();
            listOfUser.Add(username);

            Console.Write("vänligen ange ditt lösenord: ");
            password = Console.ReadLine();          

        }

        public static void Login()
        {
           
            
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