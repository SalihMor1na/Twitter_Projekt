using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter_Projekt
{
    internal class InteractionService
    {
        public static int repostChoice;
        public static List<string> repostList = new List<string>();
        public static List<string> likeDislike = new List<string>();
        public static int likeCount = 0;
        public static int disLikeCount = 0;
        public static void Reposta(UserInterface ui)
        {
            ui.PrintMessage("Vilket inlägg vill du reposta");
            PostDisplayService.ShowAllPost();
            bool run = true;
            while (run)
            {

                try { repostChoice = ui.GetUserChoice("") -1; }
                catch
                {
                    ui.PrintMessage("Det måste vara ett av följade nummer!", ConsoleColor.Yellow);
                    try { repostChoice = ui.GetUserChoice("") - 1; }
                    catch
                    {
                        ui.PrintMessage("Det måste vara ett av följade nummer!", ConsoleColor.Yellow);
                        repostChoice = ui.GetUserChoice("") - 1;
                    }
                }

                if (repostChoice >= 0 && repostChoice < PostService.listOfPosts.Count)
                {
                    run = false;
                }
            }


            var saveRepost = PostService.listOfPosts[repostChoice];
            if (!repostList.Contains(saveRepost))
            {
                repostList.Add(saveRepost);
                ui.PrintMessage($"Du har nu repostat {PostService.listOfPosts[repostChoice]}");
            }
            else
            {
                ui.PrintMessage("Det här inlägget har redan repostats.");
            }

            bool isRunning = true;
            while (isRunning)
            {
                ui.PrintMessage("Vill du se alla dina repost svara med Ja/Nej");
                string showRepostChoice = ui.GetUserChoice("").ToString().ToLower();

                if (showRepostChoice == "ja")
                {
                    foreach (var item in repostList)
                    {
                        ui.PrintMessage(item);
                    }
                    isRunning = false;
                }
                else if (showRepostChoice == "nej")
                {
                    isRunning = false;
                }
                else if (repostList == null)
                {
                    ui.PrintMessage("Finns inga repost");
                }
                else
                {
                    ui.PrintMessage("Du måste skriva antingen ja eller nej");
                }
            }

        }


        public static void Like_Dislike(UserInterface ui)
        {

            ui.PrintMessage("Vilket inlägg vill du lika eller dislika");
            if (PostService.listOfPosts.Count == 0)
            {
                ui.PrintMessage("\nDu har inga inlägg att visa!");
                return;
            }
            PostDisplayService.ShowAllPost();
            int choice = 0;
            choice = ui.GetUserChoice("") - 1;
            bool isRunning = true;
            while (isRunning)
            {
                ui.PrintMessage("Vill du lika eller dislika?");
                string choiceLikeOrDislike = ui.GetUserChoice("").ToString().ToLower();

                if (choiceLikeOrDislike == "lika")
                {
                    likeCount++;
                    ui.PrintMessage($"Du har nu likat Inlägg [{PostService.listOfPosts[choice]}]");
                    likeDislike.Add(PostService.listOfPosts[choice] + " " + $"Likes [{likeCount}]");
                    isRunning = false;
                }
                else if (choiceLikeOrDislike == "dislika")
                {
                    disLikeCount++;
                    ui.PrintMessage($"Du har nu dislikat inlägg [{PostService.listOfPosts[choice]}] ");
                    likeDislike.Add(PostService.listOfPosts[choice] + " " + $"Dislikes [{disLikeCount}]");
                    isRunning = false;
                }
                else
                {
                    ui.PrintMessage("Du måste skirva antigen lika eller dislika!", ConsoleColor.Yellow);
                }
            }

            isRunning = true;
            while (isRunning)
            {
                ui.PrintMessage("Vill du se alla inlägg och hur många likes/dislikes de har");
                ui.PrintMessage("Ja/Nej");
                string choiceShowAllPosts = ui.GetUserChoice("").ToString().ToLower();
                int i = 1;
                if (choiceShowAllPosts == "ja")
                {
                    foreach (string post in likeDislike)
                    {
                        ui.PrintMessage($"{i}.{post}");
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
                    ui.PrintMessage("Du måste skriva ja eller nej", ConsoleColor.Yellow);
                }
            }

        }
    }
}
