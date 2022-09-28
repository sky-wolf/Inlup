using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace HangMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRuning = true;
            Game game = new Game();

            game.Init();

            do
            {
                Console.WriteLine("  ___ ___                                              \r\n /   |   \\_____    ____    ____   _____ _____    ____  \r\n/    ~    \\__  \\  /    \\  / ___\\ /     \\\\__  \\  /    \\ \r\n\\    Y    // __ \\|   |  \\/ /_/  >  Y Y  \\/ __ \\|   |  \\\r\n \\___|_  /(____  /___|  /\\___  /|__|_|  (____  /___|  /\r\n       \\/      \\/     \\//_____/       \\/     \\/     \\/ ");

                Console.WriteLine("\n\n");
                Console.WriteLine("\t\tGAME MENU");
                Console.WriteLine("\t\t\tn: New Game");
                Console.WriteLine("\t\t\tq: Quit");
                var choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.N:
                        {
                            Console.Clear();
                            game.Run();

                        }
                        break;
                    case ConsoleKey.Q:
                        {
                            Console.Clear();
                            Console.WriteLine("Hej då");
                            isRuning = false;

                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Only use n or q");
                        }
                        break;

                }
                Console.Clear();
            } while (isRuning);

            Console.ReadKey();

        }
    }
}
