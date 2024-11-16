﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class PostManagment
    {
        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            Program.listofposts.Add(post);
        }

        public static void ShowAllPost()
        {
            Console.WriteLine();

            if (Program.listofposts.Count == 0)
            {
                Console.WriteLine("Du har inga inlägg att visa!");
            }
            else
            {
                Console.WriteLine("Här kommer alla inlägg");
                int i = 1;
                foreach (string post in Program.listofposts)
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
                if (removePost > Program.listofposts.Count || removePost < 0)
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

            Program.listofposts.RemoveAt(removePost);
            Console.WriteLine($"Du tog bort inlägg nummer {removePost + 1}");
        }

        public static void EditPost()
        {
            Console.WriteLine("Ange numret på inlägget du vill redigera:");
            PostManagment.ShowAllPost();
            if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber > 0 && postNumber <= Program.listofposts.Count)
            {
                Console.Write("Skriv din nya text för inlägget: ");
                Program.listofposts[postNumber - 1] = Console.ReadLine();
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

                try { Program.repostChoice = int.Parse(Console.ReadLine()) - 1; }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Det måste vara ett av följade nummer!");
                    try { Program.repostChoice = int.Parse(Console.ReadLine()) - 1; }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Det måste vara ett av följade nummer!");
                        Program.repostChoice = int.Parse(Console.ReadLine()) - 1;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (Program.repostChoice >= 0 && Program.repostChoice < Program.listofposts.Count)
                {
                    run = false;
                }
            }

            var saveRepost = Program.listofposts[Program.repostChoice];
            Program.repostList.Add(saveRepost);
            Console.WriteLine($"Du har nu repostat {Program.listofposts[Program.repostChoice]}");

            Console.WriteLine("Vill du se alla dina repost svara med Ja/Nej");
            string showRepostChoice = Console.ReadLine().ToLower();


            if (showRepostChoice == "ja")
            {
                foreach (var item in Program.repostList)
                {
                    Console.WriteLine(item);
                }
            }
            else if (Program.repostList == null)
            {
                Console.WriteLine("Finns inga repost");
            }

        }

    }
}
