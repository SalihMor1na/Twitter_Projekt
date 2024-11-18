using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Twitter_Projekt
{
    // Kod för reklam
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

                adShown = false;
            }
        }
        public static void ShowAd()
        {
            Console.Clear();
            Console.WriteLine("Du kollar nu på en reklam, reklamen slutar om 5 sekunder...");

            Thread.Sleep(5000);

            Console.Clear();
            Console.WriteLine("Du kan nu fortsätta använda din kostnadsfria Twitter");
            MenuManagment.HandleMenu();
        }
        Task adTask = Ad();

        public static void StartAdTask()
        {
            Task.Run(() => Ad());
        }

    }
}
