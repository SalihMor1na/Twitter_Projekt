using System;
using System.Collections.Generic;

namespace Twitter_Projekt
{
    class Program
    {
        public static List<string> listofposts = new List<string>();

        public static void Main(string[] args)
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
            }
        }

        public static void ShowAllPost()
        {
            Console.WriteLine("Här kommer alla inlägg");
            foreach (string post in listofposts)
            {
                Console.WriteLine(post);
            }
        }

        private static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            listofposts.Add(post);
        }
    }
}