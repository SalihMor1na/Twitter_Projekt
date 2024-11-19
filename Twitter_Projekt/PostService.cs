using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class PostService
    {
        public List<string> Posts { get; private set; } = new List<string>();
        public static List<string> listOfPosts = new List<string>();
        public static void CreatePost(UserInterface ui)
        {
            Console.WriteLine("Vad vill du dela med dig utav?");
            string post = Console.ReadLine();
            if (!string.IsNullOrEmpty(post))
            {
                listOfPosts.Add(post);
                ui.PrintMessage("Inlägg har skapats.", ConsoleColor.Green);
            }
            else
            {
                ui.PrintMessage("Inlägget kan inte vara tomt.", ConsoleColor.Yellow);
            }

        }



        public static void DeleteTweet(UserInterface ui)
        {
            ui.PrintMessage("Vilket inlägg vill du ta bort");
            if (listOfPosts.Count == 0)
            {
                ui.PrintMessage("\nDu har inga inlägg att visa!");
                return;
            }
            PostDisplayService.ShowAllPost();
            int removePost = 0;
            try
            {
                removePost = ui.GetUserChoice("Skriv numret på inlägget du vill ta bort:") - 1;
                if (removePost > listOfPosts.Count || removePost < 0)
                {
                    ui.PrintMessage("Det inlägget finns ej! försök igen");
                }
            }
            catch
            {
                ui.PrintMessage("Det måste vara ett nummer!", ConsoleColor.Yellow);   
            }

            listOfPosts.RemoveAt(removePost); 
            ui.PrintMessage($"Du tog bort inlägg nummer {removePost + 1}", ConsoleColor.Red);
        }

        public static void EditPost(UserInterface ui)
        {
            ui.PrintMessage("Ange numret på inlägget du vill redigera:");
            PostDisplayService.ShowAllPost();

            if (int.TryParse(Console.ReadLine(), out int postNumber) && postNumber > 0 && postNumber <= listOfPosts.Count)
            {
                string newPostText;

                do
                {
                    Console.Write("Skriv din nya text för inlägget: ");
                    newPostText = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newPostText))
                    {
                        ui.PrintMessage("Inlägget får inte vara tomt. Försök igen.", ConsoleColor.Yellow);
                    }
                } while (string.IsNullOrWhiteSpace(newPostText));

                listOfPosts[postNumber - 1] = newPostText;
                ui.PrintMessage("Inlägget har uppdaterats", ConsoleColor.Green);
            }
            else
            {
                ui.PrintMessage("Ogiltigt nummer, försök igen.", ConsoleColor.Yellow);
            }
        }

    }
}