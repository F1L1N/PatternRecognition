using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatternRecognition.Layers;
using PatternRecognition.Templates;

namespace PatternRecognition
{
    class Program
    {
        private static void clearingConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        private static void creatingNetwork()
        {
            clearingConsole();
        }

        private static void testNetwork()
        {
            clearingConsole();
        }

        private static void demo()
        {
            clearingConsole();
        }

        static void Main(string[] args)
        {
            int mode = -1;
            while (mode != 0)
            {
                clearingConsole();
                Console.WriteLine("Welcome! Enter mode: ");
                Console.WriteLine("1. Create network and train it");
                Console.WriteLine("2. Test existing network");
                Console.WriteLine("3. Demo");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice: ");
                switch (Console.Read())
                {
                    case 1:
                        creatingNetwork();
                        break;
                    case 2:
                        testNetwork();
                        break;
                    case 3:
                        demo();
                        break;
                    case 0:
                        mode = 0;
                        break;
                }
            }
        }
    }
}
