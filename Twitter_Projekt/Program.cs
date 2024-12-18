using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Twitter_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManagment.LoadUsers();
            MenuManagment.HandleLoginMenu();
            MenuManagment.HandleMenu();

            HandlePostMenu();
        }

        private static void HandlePostMenu()
        {
            var postService = new PostService();
            var postDisplayService = new PostDisplayService();
            var postInputService = new PostInputService();
            var userInterface = new UserInterface();

            while (true)
            {
                Console.WriteLine("\nVad vill du göra med inlägg?");
                Console.WriteLine("1. Lägg till inlägg");
                Console.WriteLine("2. Visa alla inlägg");
                Console.WriteLine("3. Redigera inlägg");
                Console.WriteLine("4. Ta bort inlägg");
                Console.WriteLine("5. Tillbaka till huvudmeny");

                int choice = 0;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            string newPost = postInputService.GetPostFromUser(userInterface);
                            postService.AddPost(newPost);
                            break;
                        case 2:
                            postDisplayService.ShowAllPosts(userInterface);
                            break;
                        case 3:
                            int postIndex = postInputService.GetPostIndexFromUser(userInterface);
                            string updatedPost = postInputService.GetPostFromUser(userInterface);
                            postService.EditPost(postIndex, updatedPost);
                            break;
                        case 4:
                            int postToDeleteIndex = postInputService.GetPostIndexFromUser(userInterface);
                            postService.DeletePost(postToDeleteIndex);
                            break;
                        case 5:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Fel: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}