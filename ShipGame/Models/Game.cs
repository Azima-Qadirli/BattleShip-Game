using System;

namespace ShipGame.Models
{
    public class Game
    {
        private Grid grid;
        private string playerFullName;
       

        public Game(int gridSize, string fullname)
        {
            grid = new Grid(gridSize);
            playerFullName = fullname;
        }

        public void Start()
        {
            
            while (true)
            {
                grid.DisplayHiddenGrid();
                Console.WriteLine("Enter coordinates to shoot (as x,y) or type 'exit' to finish: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the game. Goodbye!");
                    break;
                }

                string[] parts = input.Split(',');

                if (parts.Length != 2)
                {
                    Console.WriteLine("Incorrect format. Please use the format x,y.");
                    continue;
                }

                try
                {
                    int x = int.Parse(parts[0].Trim());
                    int y = int.Parse(parts[1].Trim());

                    bool hit = grid.Shoot(x, y);
                    if (hit)
                    {
                        Console.WriteLine("Hit!");
                    }
                    else
                    {
                        Console.WriteLine("Miss!");
                    }

                    if (grid.AllShipsHit())
                    {
                        Console.WriteLine($"Congratulations {playerFullName}! You have sunk all the ships!");
                        break; // Exit the loop when all ships are sunk
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect coordinates. Please enter correct integers for x and y.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            Console.ReadKey();
        }

    }
}
