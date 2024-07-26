using System;

namespace ShipGame.Models
{
    public class Game
    {
        private Grid grid;
        private string playerFullName;
        private int maxAttempts = 5; // it shows max opportunity of user

        public Game(int gridSize, string fullname)
        {
            grid = new Grid(gridSize);
            playerFullName = fullname;
        }

        public void Start()
        {
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Clear();
                DisplayGameTitle();
                grid.DisplayHiddenGrid();

                Console.WriteLine($"{playerFullName}, enter coordinates to shoot (as x,y): ");
                string input = Console.ReadLine();
                string[] parts = input.Split(',');

                if (parts.Length != 2)
                {
                    Console.WriteLine("Incorrect format. Please use the format x,y.");
                    Console.ReadLine(); 
                    continue;
                }

                try
                {
                    int x = int.Parse(parts[0].Trim());
                    int y = int.Parse(parts[1].Trim());

                    if (x < 0 || x >= grid.Size || y < 0 || y >= grid.Size)
                    {
                        Console.WriteLine("Coordinates are out of bounds. Please enter values within the grid range.");
                        Console.ReadLine(); 
                        continue;
                    }

                    if (grid.IsMine(x, y))
                    {
                        Console.WriteLine("You hit a mine! You lose! Press Enter to exit.");
                        Console.ReadLine(); 
                        break;
                    }

                    bool hit = grid.Shoot(x, y);
                    if (hit)
                    {
                        Console.WriteLine("Hit!");
                    }
                    else
                    {
                        Console.WriteLine("Miss!");
                    }

                    attempts++;

                    if (grid.AllShipsHit())
                    {
                        Console.WriteLine($"Congratulations, {playerFullName}! All ships have been sunk. You win!");
                        Console.ReadLine(); 
                        break;
                    }

                    if (attempts >= maxAttempts)
                    {
                        Console.WriteLine($"Game over, {playerFullName}! You've used all your attempts. You lose!");
                        Console.ReadLine(); 
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect coordinates. Please enter correct integers for x and y.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error happened: {ex.Message}");
                    Console.ReadLine();
                }
            }

            Console.WriteLine($"Game over, {playerFullName}! You've used {attempts} out of {maxAttempts} attempts.");
        }

        private void DisplayGameTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine(CenterText("BATTLESHIP GAME", Console.WindowWidth));
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.ResetColor();
        }

        private string CenterText(string text, int width)
        {
            int leftPadding = (width - text.Length) / 2;
            return new string(' ', leftPadding) + text;
        }
    }
}
