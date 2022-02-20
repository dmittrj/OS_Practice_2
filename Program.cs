using System;

namespace OS_Practice_2
{
    class Program
    {
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
            Console.Write(" [7]: Exit\n\n > ");
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

                return "";
            }
            if (r == "5")
            {
                Console.Write(" Enter hash > ");
                return Console.ReadLine();
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

        static bool OS_Process()
        {
            Console.WriteLine(" Do you want to see the progress of the operation in the console?");
            Console.WriteLine(" ATTENTION! Bruteforce with console displaying will be slower\n (y/n) > ");
            return false;
        }

        static void OS_Bruteforce(string hash, int threads, bool disp)
        {

        }

        static void Main(string[] args)
        {
            string OS_Hash = OS_HashToBruteforce();
            if (OS_Hash == "") return;
            int OS_ThreadsNumber = OS_Threads();
            OS_Process();
        }
    }
}
