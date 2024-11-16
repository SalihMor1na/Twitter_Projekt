﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Twitter_Projekt
{
    public class UserManagment
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Followers { get; set; } = new List<string>();

        public static List<UserManagment> users =LoadUsers();
        public static void CreateAccount()
        {
            Console.Write("Ange ett användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange ett lösenord: ");
            string password = LoginManagment.ReadPassword();
            while (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver, Försök igen!");
                Console.Write("Ange ett lösenord: ");
                password = Console.ReadLine();
            }

            Console.WriteLine("Ange din e-postadress: ");
            string email = Console.ReadLine();
            while (!IsValidEmail(email))
            {
                Console.WriteLine("Ogiltig e-postadress. Försök igen: ");
                email = Console.ReadLine();
            }



            Console.WriteLine("Ange ditt förnamn: ");
            string firstname = Console.ReadLine();
            while (string.IsNullOrEmpty(firstname))
            {
                Console.WriteLine("Förnamn får inte vara tomt. Ange ditt förnamn igen");
                Console.Write("Ange ditt förnamn: ");
                firstname = Console.ReadLine();
            }

            Console.WriteLine("Ange ditt efternamn: ");
            string lastname = Console.ReadLine();
            while (string.IsNullOrEmpty(lastname))
            {
                Console.WriteLine("Efternamn får inte vara tomt. Ange ditt förnamn igen");
                Console.Write("Ange ditt efternamn: ");
                lastname = Console.ReadLine();
            }

            Console.WriteLine("Välj kön (1 - Man, 2 - Kvinna, 3 - Annat) : ");
            string genderInput = Console.ReadLine();
            string gender = "";
            while (genderInput != "1" && genderInput != "2" && genderInput != "3")
            {
                Console.WriteLine("Ogiltigt val. Välj 1 för Man, 2 för Kvinna eller 3 för annat");
                genderInput = Console.ReadLine();
            }
            switch (genderInput)
            {
                case "1": gender = "Man"; break;
                case "2": gender = "Kvinna"; break;
                case "3": gender = "Annat"; break;
            }



            foreach (UserManagment user in users)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Användarnamnet är upptaget. Försök igen.");
                    return;
                }
            }

            UserManagment newUser = new UserManagment { Username = username, Password = password };
            users.Add(newUser);

            SaveUsers();
            Console.WriteLine("Konto skapat!");
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void ShowUserInfo()
        {
            UserManagment user = users.FirstOrDefault(u => u.Username == LoginManagment.loggedInUsername);
            if (user != null)
            {
                int followersCount = user.Followers.Count;
                int followingCount = users.Count(u => u.Followers.Contains(LoginManagment.loggedInUsername));
                Console.WriteLine($"Du har {followersCount} följare och följer {followingCount} person.");
            }
        }
        public static void SearchForUSer()
        {
            Console.Write("Ange användarnamnet på personen du vill följa: ");
            string userToFollow = Console.ReadLine();

            UserManagment user = users.FirstOrDefault(u => u.Username.Equals(userToFollow, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                user.Followers.Add(LoginManagment.loggedInUsername);
                Console.WriteLine($"Du följer nu {userToFollow}.");
            }
            else
            {
                Console.WriteLine("Användaren finns inte.");
            }
        }

        public static void SaveUsers()
        {
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
        }

        public static List<UserManagment> LoadUsers()
        {
            if (File.Exists("users.json"))
            {
                string jsonString = File.ReadAllText("users.json");
                return JsonSerializer.Deserialize<List<UserManagment>>(jsonString) ?? new List<UserManagment>();
            }
            return new List<UserManagment>();
        }
        public static void HandleSettings()
        {
            UserManagment currentUser = users.FirstOrDefault(u => u.Username == LoginManagment.loggedInUsername);
            if (currentUser == null)
            {
                Console.WriteLine("Något gick fel. Användaren kunde inte hittas");
                return;
            }

            bool inSettings = true;

            while (inSettings)
            {
                Console.Clear();
                Console.WriteLine("INSTÄLLNINGAR");
                Console.WriteLine("1. Ändra ditt användarnamn");
                Console.WriteLine("2. Ändra ditt lösenord");
                Console.WriteLine("3. Gå tillbaka till huvudmenyn");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        Console.Write("Ange ditt nya användarnamn: ");
                        string newUsername = Console.ReadLine();

                        if (users.Any(u => u.Username.Equals(newUsername, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine("Det angivna användarnamnet är upptaget. Försök med ett annat användarnamn");
                        }
                        else
                        {
                            currentUser.Username = newUsername;
                            LoginManagment.loggedInUsername = newUsername;
                            Console.WriteLine("Användarnamnet har ändrats!");
                        }
                        break;

                    case "2":

                        Console.Write("Ange ditt nya lösenord: ");
                        string newPassword = Console.ReadLine();

                        while (newPassword.Length < 6 || !newPassword.Any(char.IsDigit) || !newPassword.Any(char.IsLetter))
                        {
                            Console.WriteLine("Lösenordet måste vara minst 6 tecken långt och innehålla både siffror och bokstäver. Försök igen: ");
                            newPassword = LoginManagment.ReadPassword();
                        }
                        currentUser.Password = newPassword;
                        Console.WriteLine("Lösenordet har ändrats.");
                        break;
                }
            }
        }
    }
}
