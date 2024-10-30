using System;
using System.Collections.Generic;

namespace Twitter_Projekt
{
    class Program
    {
        public static List<string> listofposts = new List<string>();

        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Välj ett av följande alternativ");
                Console.WriteLine("1: Skapa ett inlägg");
                Console.WriteLine("2: Visa alla tweets");
                Console.WriteLine("3: Ta bort tweet");
                Console.WriteLine("4: Skapa konto");
                Console.WriteLine("5: Logga in");

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

               
            }
        }

        public static void ShowAllPost()
        {
            Console.WriteLine("Här kommer alla inlägg");
            int i = 1;
            foreach (string post in listofposts)
            {
                Console.WriteLine($"{i}.{ post}");
                i++;
            }
        }
        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            listofposts.Add(post);
    
        }

        //public static void CreateAccoount()
        //{
        //    Console.Write("Vänligen ange ditt användernamn: ");
        //    string userName = Console.ReadLine();

        //    Console.Write("Vänligen ange ditt lösenord: ");
        //    string passWord = Console.ReadLine();
        //}

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