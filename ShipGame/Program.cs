using System;
using System.Linq;
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
            DisplayCenteredText("*              BATTLESHIP GAME              *", ConsoleColor.Yellow);
            DisplayCenteredText("*********************************************", ConsoleColor.Cyan);
            Console.ResetColor();
            Console.WriteLine();

            string fullname;
            while (true)
            {
                Console.WriteLine("Enter your full name (first name and last name, letters only): ");
                fullname = Console.ReadLine();

                if (IsValidFullName(fullname))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter your full name with two words, using only letters.");
                }
            }

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
        }

        static void DisplayCenteredText(string text, ConsoleColor color)
        {
            int consoleWidth = Console.WindowWidth;
            int textLength = text.Length;
            int spaces = (consoleWidth - textLength) / 2;

            Console.ForegroundColor = color;
            Console.WriteLine(new string(' ', spaces) + text);
            Console.ResetColor();
        }

        static bool IsValidFullName(string fullName)
        {
            var parts = fullName.Split(' ');
            if (parts.Length != 2) return false;

            return parts.All(part => part.All(char.IsLetter));
        }
    }
}
