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
            Game game = new Game();

            game.Init();
            game.Run();

        }
    }
}
