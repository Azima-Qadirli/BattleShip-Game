using System;
using ShipGame.Models;

namespace ShipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            DisplayCenteredText("*********************************************", ConsoleColor.Cyan);
            DisplayCenteredText("*      BATTLESHIP GAME        *", ConsoleColor.Yellow);
            DisplayCenteredText("*********************************************", ConsoleColor.Cyan);
            Console.ResetColor();
            Console.WriteLine();
            
            
            Console.WriteLine("Enter your fullname: ");
            string fullname = Console.ReadLine();
            

            int gridSize;
            while (true)
            {
                Console.WriteLine("Enter grid size (positive integer): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out gridSize) && gridSize > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter a positive integer.");
                }
            }

            Game game = new Game(gridSize, fullname);
            game.Start();


            static void DisplayCenteredText(string text,ConsoleColor color )
            {
                int consoleWidth = Console.WindowWidth;
                int textLength = text.Length;
                int spaces = (consoleWidth - textLength) / 2;

                Console.ForegroundColor = color;
                Console.WriteLine(new string(' ', spaces) + text);
                Console.ResetColor();
            }
        }
    }
}
