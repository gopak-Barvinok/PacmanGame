using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace PacmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char[,] map = ReadMap("map.txt");
            
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                //Бесконечное считывание введённых клавиш
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            int packmanX = 1;
            int packmanY = 1;
            int score = 0;

            while(true)
            {
                Console.Clear();

                //Обработка введённых клавиш
                HandleInput(pressedKey, ref packmanX, ref packmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Red;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(packmanX, packmanY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.SetCursorPosition(34, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(1000);
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            
            char[,] map =  new char[GetMaxLengthOfLine(file), file.Length];

            for(int x = 0; x < map.GetLength(0); x++)
            {
                for(int y = 0;  y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int packmanX, ref int packmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPackmanPositionX = packmanX + direction[0];
            int nextPackmanPositionY = packmanY + direction[1];

            char nextCell = map[nextPackmanPositionX, nextPackmanPositionY];

            if (nextCell == ' ' || nextCell == '$')
            {
                packmanX = nextPackmanPositionX;
                packmanY = nextPackmanPositionY;

                if(nextCell == '$')
                {
                    score++;
                    map[nextPackmanPositionX, nextPackmanPositionY] = ' ';
                }
            }
            
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.W)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.S)
            {
                direction[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.D)
            {
                direction[0] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.A)
            {
                direction[0] = -1;
            }
            return direction;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length; 

            foreach(var line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;

                }
            }    
            return maxLength;
        }
    }
}
