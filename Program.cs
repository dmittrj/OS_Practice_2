using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OS_Practice_2
{
    class OS_Thread
    {
        Thread thread;
        public static readonly List<string> OS_PASSWORDS = new List<string>();
        int OS_ThreadNumber;
        int OS_ThreadCount;
        static bool OS_Cancel;
        static DateTime startBruteforce = DateTime.Parse("01.01.2001 00:00");
        static DateTime endBruteforce;
        
        public OS_Thread(string hash, int threadNum, int threads)
        {
            thread = new Thread(this.OS_Bruteforce);
            OS_ThreadNumber = threadNum;
            OS_ThreadCount = threads;
            OS_Cancel = false;
            thread.Start(hash);
        }
        public void OS_Bruteforce(object num)
        {
            if (startBruteforce == DateTime.Parse("01.01.2001 00:00"))
                startBruteforce = DateTime.Now;
            string OS_Current_Hash = num.ToString();
            int OS_Start = 11881376 / OS_ThreadCount * OS_ThreadNumber;
            int OS_Limit = 11881376 / OS_ThreadCount * (OS_ThreadNumber + 1);
            var crypt = new System.Security.Cryptography.SHA256Managed();
            for (int OS_Counter = OS_Start; OS_Counter <= OS_Limit; OS_Counter++)
            {
                if (OS_Cancel) return;
                string password = OS_PASSWORDS[OS_Counter];
                //Console.WriteLine("Поток [" + OS_ThreadNumber.ToString() + "]: пароль " + password);

                var hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }

                if (hash.ToString() == OS_Current_Hash)
                {
                    endBruteforce = DateTime.Now;
                    Console.WriteLine("\n\n Password found > " + password);
                    Console.WriteLine(" Time required > " + endBruteforce.Subtract(startBruteforce).ToString());
                    OS_Cancel = true;
                    startBruteforce = DateTime.Parse("01.01.2001 00:00");
                    return;
                }
            }
            Console.WriteLine(" Thread " + OS_ThreadNumber.ToString() + "/" + OS_ThreadCount.ToString() + " did't guess yout password");

        }

    }

    class Program
    {
        //static readonly List<string> OS_PASSWORDS = new List<string>();
        static string OS_ComputeHash(string hashString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        static void OS_ComputePasswords()
        {
            Console.WriteLine(" Please wait... Program will start soon");
            for (char a = 'a'; a <= 'z'; a++)
                for (char b = 'a'; b <= 'z'; b++)
                    for (char c = 'a'; c <= 'z'; c++)
                        for (char d = 'a'; d <= 'z'; d++)
                            for (char e = 'a'; e <= 'z'; e++)
                                OS_Thread.OS_PASSWORDS.Add(a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString());
        }

        static void OS_Statistic()
        {
            Console.Clear();
            Console.WriteLine("\n\n Your current session statistic");
        }

        static string OS_HashToBruteforce()
        {
            Console.Clear();
            Console.WriteLine("\n\n This program guesses the password that matches the hash value");
            Console.WriteLine(" What hash do you want to bruteforce?\n");
            Console.WriteLine(" [1]: 1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad");
            Console.WriteLine(" [2]: 3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b");
            Console.WriteLine(" [3]: 74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f");
            Console.WriteLine(" [4]: Your (from file)");
            Console.WriteLine(" [5]: Your (from keyboard)");
            Console.WriteLine("\n [6]: (dev) Password to hash");
            Console.WriteLine(" [7]: Statistics");
            Console.Write(" [8]: Exit\n\n > ");
            string r = Console.ReadLine();
            if (r == "1")
                return "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad";
            if (r == "2")
                return "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b";
            if (r == "3")
                return "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f";
            if (r == "4")
            {
                Console.Write(" Enter file path > ");
                string OS_Path = Console.ReadLine();
                FileStream file = new FileStream(OS_Path, FileMode.OpenOrCreate);
                byte[] array = new byte[file.Length];
                file.Read(array, 0, array.Length);
                string textFromFile = System.Text.ASCIIEncoding.ASCII.GetString(array);
                Console.WriteLine(" hash > " + textFromFile);
                return textFromFile;
            }
            if (r == "5")
            {
                Console.Write(" Enter hash > ");
                return Console.ReadLine();
            }
            if (r == "6")
            {
                Console.Write(" Enter string (exactly 5 letters) > ");
                Console.WriteLine(OS_ComputeHash(Console.ReadLine()));
                return OS_ComputeHash(Console.ReadLine());
            }
            return "";
        }

        static int OS_Threads()
        {
            _OS_incorrect_input_threads:
            Console.Write(" How many threads do you want to use?\n\n > ");
            int x;
            try
            {
                x = Int32.Parse(Console.ReadLine());
                if (x < 1) throw new Exception();
            } 
            catch
            {
                goto _OS_incorrect_input_threads;
            }
            return x;
        }

        static void OS_Bruteforce(string hash, int threads)
        {
            List<OS_Thread> myThread = new List<OS_Thread>();
            for (int i = 0; i < threads; i++)
                myThread.Add(new OS_Thread(hash, i, threads));
            Console.WriteLine("\n Password bruteforcing is in progress...");
        }

        static void Main(string[] args)
        {
            OS_ComputePasswords();
            _OS_start:
            string OS_Hash = OS_HashToBruteforce();
            if (OS_Hash == "") return;
            int OS_ThreadsNumber = OS_Threads();
            OS_Bruteforce(OS_Hash, OS_ThreadsNumber);
            Console.ReadKey();
            goto _OS_start;
        }
    }
}
