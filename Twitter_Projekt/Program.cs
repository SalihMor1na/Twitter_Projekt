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
        public static void Main(string[] args)
        {
            UserManagment.LoadUsers();
            MenuManagment.HandleLoginMenu();
            //AdManagment.StartAdTask();
            MenuManagment.HandleMenu();
        }
    }

}

