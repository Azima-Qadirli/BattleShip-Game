﻿using System;
using System.Collections.Generic;

namespace ShipGame.Models
{
    public class Grid
    {
        private int size;
        private char[,] grid;
        private List<Ship> ships;
        private Random random;
        private List<Mine> mines;
        public int Size => size; 

        public Grid(int size)
        {
            this.size = size;
            grid = new char[size, size];
            ships = new List<Ship>();
            random = new Random();
            mines = new List<Mine>();

            InitializeGrid();
            PlaceShipsRandomly();
            PlaceMinesRandomly(); // it shows mines are placed randomly
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '*'; // Water
                }
            }
        }

        private void PlaceShipsRandomly()
        {
            int numberOfShips = size / 2; // Arbitrary number of ships

            for (int i = 0; i < numberOfShips; i++)
            {
                int shipLength = random.Next(1, 5); // it shows and generate random length of ship between 1 and 4
                Ship ship = new Ship(shipLength);
                PlaceShip(ship);
                ships.Add(ship);
            }
        }

        private void PlaceShip(Ship ship)
        {
            bool placed = false;

            while (!placed)
            {
                int startX = random.Next(0, size);
                int startY = random.Next(0, size);
                bool horizontal = random.Next(0, 2) == 0;

                if (CanPlaceShip(startX, startY, ship.Length, horizontal))
                {
                    for (int j = 0; j < ship.Length; j++)
                    {
                        if (horizontal)
                        {
                            grid[startX, startY + j] = 'S';
                            ship.AddPosition(startX, startY + j);
                        }
                        else
                        {
                            grid[startX + j, startY] = 'S';
                            ship.AddPosition(startX + j, startY);
                        }
                    }
                    placed = true;
                }
            }
        }

        private bool CanPlaceShip(int startX, int startY, int length, bool horizontal)
        {
            if (horizontal)
            {
                if (startY + length > size) return false;

                for (int i = 0; i < length; i++)
                {
                    if (grid[startX, startY + i] == 'S') return false;
                }
            }
            else
            {
                if (startX + length > size) return false;

                for (int i = 0; i < length; i++)
                {
                    if (grid[startX + i, startY] == 'S') return false;
                }
            }
            return true;
        }

        public bool Shoot(int x, int y)
        {
            if (grid[x, y] == 'S')
            {
                grid[x, y] = 'H'; // Hit
                return true;
            }
            else if (grid[x, y] == 'M')
            {
                grid[x, y] = 'X'; // it shows that mine hit
                return false;
            }
            else
            {
                grid[x, y] = 'M'; // it defines that ship misses
                return false;
            }
        }

        public void DisplayHiddenGrid()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char displayChar = grid[i, j];
                    if (displayChar == 'S') // Ship is hidden in grid
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(displayChar + " ");
                    }
                }
                Console.WriteLine();
            }
        }


        public bool AllShipsHit()
        {
            foreach (var ship in ships)
            {
                foreach (var position in ship.Positions)
                {
                    if (grid[position.Item1, position.Item2] != 'H')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void PlaceMinesRandomly()
        {
            int numberOfMines = size / 4;
            for (int i = 0; i < numberOfMines; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(0, size);
                    y = random.Next(0, size);
                }
                while (grid[x, y] != '*');  // Ensure mines are placed on water, not overlapping with ships

                grid[x, y] = 'M';
                mines.Add(new Mine(x, y));
            }
        }

        public bool IsMine(int x, int y)
        {
            return grid[x, y] == 'M';
        }
    }
}
