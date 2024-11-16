using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Twitter_Projekt
{
    internal class AdManagment
    {
        public static bool adShown = false;

        public static async Task Ad()
        {
            Random random = new Random();

            if (!adShown)
            {
                adShown = true;

                int randomInterval = random.Next(5000, 10000);
                await Task.Delay(randomInterval);

                ShowAd();
            }
        }
        public static void ShowAd()
        {
            Console.Clear();
            Console.WriteLine("Du kollar nu på en reklam, reklamen slutar om 5 sekunder...");

            Thread.Sleep(5000);

            Console.Clear();
            Console.WriteLine("Du kan nu fortsätta använda din kostnadsfria Twitter");
            Program.HandleMenu();
        }
        Task adTask = Ad();
    }
}
