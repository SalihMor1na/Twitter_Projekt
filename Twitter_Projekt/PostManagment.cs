using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class PostManagment
    {
        public static List<string> listofposts = new List<string>();
        public static int repostChoice;
        public static List<string> repostList = new List<string>();


        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            listofposts.Add(post);
        }

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
        public static void DeleteTweet()
        {
            Console.WriteLine("Skriv vilket inlägg du vill ta bort");
            ShowAllPost();
            int removePost = 0;
            try
            {
                removePost = int.Parse(Console.ReadLine()) - 1;
                if (removePost > listofposts.Count || removePost < 0)
                {
                    Console.WriteLine("Det inlägget finns ej! försök igen");
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Det måste vara ett nummer!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            listofposts.RemoveAt(removePost);
            Console.WriteLine($"Du tog bort inlägg nummer {removePost + 1}");
        }

        public static void EditPost()
        {
            Console.WriteLine("Ange numret på inlägget du vill redigera:");
            PostManagment.ShowAllPost();
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

        public static void Reposta()
        {
            Console.WriteLine("Vilket inlägg vill du reposta");
            PostManagment.ShowAllPost();
            bool run = true;
            while (run)
            {

                try {repostChoice = int.Parse(Console.ReadLine()) - 1; }
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
            if (!repostList.Contains(saveRepost))
            {
                repostList.Add(saveRepost);
                Console.WriteLine($"Du har nu repostat {listofposts[repostChoice]}");
            }
            else
            {
                Console.WriteLine("Det här inlägget har redan repostats.");
            }
          

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

    }
}
