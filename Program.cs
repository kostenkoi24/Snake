using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {

        public static ConsoleKey crawlingDirection = default;
        public static ConsoleKey inputKey;

        //public static char foodForSnake;

        public static bool gameOver = false;

        static int borderWidth = 100; //ширина игрового поля
        static int borderHeight = 20; //высота игрового поля
        static char hBorderLineSymbol = '*';
        static char vBorderLineSymbol = '|';

        static char snakeSymbol = '&';
        static char snakeTaileSymbol = '@';



        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Thread musicThread = new Thread(MusicPlayer.PlayMusic);
            musicThread.Start(); //запускаем музычку

            Border.DrawBorder(hBorderLineSymbol, borderWidth, vBorderLineSymbol, borderHeight); //рисуем рамки игры

            Thread gameThread = new Thread(StartGame);
            gameThread.Start(); //запускаем игру в отдельном потоке;

            while (!Console.KeyAvailable)
            {
                if (gameOver)
                {
                    break;
                }
                inputKey = Console.ReadKey().Key;//пнуть змею плохая идея //управление движением змеи; 
                crawlingDirection = GameControl(inputKey); //змея не может сделать разворот на 180 градусов
            }
            Console.ReadKey();
        }


        static void StartGame()
        {
            Snake snake = new Snake(borderWidth, borderHeight, snakeSymbol, snakeTaileSymbol); //создаем змею
            
            Food food = new Food(borderWidth, borderHeight); //еда для змеи

            while (snake.SnakeIsAlive) //змея ползает пока жива
            {
                if (crawlingDirection == ConsoleKey.UpArrow || crawlingDirection == ConsoleKey.DownArrow || crawlingDirection == ConsoleKey.LeftArrow || crawlingDirection == ConsoleKey.RightArrow)
                {
                    snake.snakeСrawling(crawlingDirection);
                    Thread.Sleep(200); //змея не должна бегать у неё нет ног
                    if (snake.SnakeGEtCurrentPosition().SequenceEqual(food.FoodGetCurrentPosition()))
                    {
                        snake.SnakeEatFood(food); //ест мышей, а они рождаются в новом месте
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("GAME OVER press any key");
            gameOver = true;
            
        }

        static ConsoleKey GameControl(ConsoleKey inputKey)
        {
            if (crawlingDirection == default)
            {
                return inputKey; //first step
            }

            if (crawlingDirection == ConsoleKey.UpArrow && inputKey == ConsoleKey.DownArrow)
            {
                return crawlingDirection;
            }
            if (crawlingDirection == ConsoleKey.DownArrow && inputKey == ConsoleKey.UpArrow)
            {
                return crawlingDirection;
            }
            if (crawlingDirection == ConsoleKey.LeftArrow && inputKey == ConsoleKey.RightArrow)
            {
                return crawlingDirection;
            }
            if (crawlingDirection == ConsoleKey.RightArrow && inputKey == ConsoleKey.LeftArrow)
            {
                return crawlingDirection;
            }

            return inputKey;
        }

    }
}
