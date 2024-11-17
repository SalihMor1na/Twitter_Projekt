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
        public static List<string> likeDislike = new List<string>();
        public static int likeCount = 0;
        public static int disLikeCount = 0;

        public static void CreatePost()
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            if (!string.IsNullOrEmpty(post))
            {
                listofposts.Add(post);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Inlägg har skapats");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inlägget kan inte vara tomt");
                Console.ForegroundColor = ConsoleColor.White;
            }
           
        }

        public static void ShowAllPost()
        {
            Console.WriteLine();

            if (listofposts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Du har inga inlägg att visa!");
                Console.ForegroundColor = ConsoleColor.White;
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
            if (listofposts.Count == 0)
            {
                Console.WriteLine("\nDu har inga inlägg att visa!");
                return;
            }
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
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"Du tog bort inlägg nummer {removePost + 1}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void EditPost()
        {
            Console.WriteLine("Ange numret på inlägget du vill redigera:");
            PostManagment.ShowAllPost();

            if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber > 0 && postNumber <= listofposts.Count)
            {
                string newPostText;

                do
                {
                    Console.Write("Skriv din nya text för inlägget: ");
                    newPostText = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPostText))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Inlägget får inte vara tomt. Försök igen.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (string.IsNullOrWhiteSpace(newPostText));

                listofposts[postNumber - 1] = newPostText;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Inlägget har uppdaterats");
                Console.ForegroundColor = ConsoleColor.White;
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

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Vill du se alla dina repost svara med Ja/Nej");
                string showRepostChoice = Console.ReadLine().ToLower();


                if (showRepostChoice == "ja")
                {
                    foreach (var item in repostList)
                    {
                        Console.WriteLine(item);
                    }
                    isRunning = false;
                }else if (showRepostChoice == "nej")
                {
                    isRunning = false;
                }
                else if (repostList == null)
                {
                    Console.WriteLine("Finns inga repost");
                }
                else
                {
                    Console.WriteLine("Du måste skriva antingen ja eller nej");
                }
            }
          

        }

        public static void Like_Dislike()
        {

            Console.WriteLine("Vilket inlägg vill du lika eller dislika");
            ShowAllPost();
            int choice = 0;
            choice = int.Parse(Console.ReadLine()) - 1;
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Vill du lika eller dislika?");
                string choiceLikeOrDislike = Console.ReadLine().ToLower();
                if (choiceLikeOrDislike == "lika")
                {
                    likeCount++;
                    Console.WriteLine($"Du har nu likat Inlägg [{listofposts[choice]}]");
                    likeDislike.Add(listofposts[choice] + " " + $"Likes [{likeCount}]");
                    isRunning = false;
                }
                else if (choiceLikeOrDislike == "dislika")
                {
                    disLikeCount++;
                    Console.WriteLine($"Du har nu dislikat inlägg [{listofposts[choice]}] ");
                    likeDislike.Add(listofposts[choice] + " " + $"Dislikes [{disLikeCount}]");
                    isRunning = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Du måste skirva antigen lika eller dislika!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Vill du se alla inlägg och hur många likes/dislikes de har");
                Console.WriteLine("Ja/Nej");
                string choiceShowAllPosts = Console.ReadLine().ToLower();
                int i = 1;
                if (choiceShowAllPosts == "ja")
                {
                    foreach (string post in likeDislike)
                    {
                        Console.WriteLine($"{i}.{post}");
                        i++;
                    }
                    isRunning = false;
                }
                else if (choiceShowAllPosts == "nej")
                {
                    isRunning = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Du måste skriva ja eller nej");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
          
        }

    }
}
