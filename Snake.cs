using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    public class Snake
    {
        static int snakeLength { get; set; }

        static List<SnakeTailPosition> snakeLengthTail = new List<SnakeTailPosition>();

        public char snakeSymbol { get; set; } //вид змеи
        public char snakeTaileSymbol { get; set; } //вид хвоста

        static ConsoleColor snakeColor = ConsoleColor.DarkRed;

        static bool snakeIsAlive;

        
        static int snakePositionVertical;
        static int snakePositionHorizontal;

        int borderWidth;
        int borderHeight;

        public bool SnakeIsAlive { get { return snakeIsAlive; } }

        

        public Snake(int borderWidth, int borderHeight, char snakeSymbol, char snakeTaileSymbol) //передаем координаты игрового поля
        {
            snakeLength = 0; //родилась у змеи только голова
            this.snakeSymbol = snakeSymbol;
            this.snakeTaileSymbol = snakeTaileSymbol;
            this.borderWidth = borderWidth;
            this.borderHeight = borderHeight;
            snakeBorn();
        }

        void snakeBorn()
        {
            snakePositionHorizontal = borderWidth/2; //horisontal создаем змею по центру
            snakePositionVertical = borderHeight/2; //vertical

            Console.SetCursorPosition(snakePositionHorizontal, snakePositionVertical);
            Console.ForegroundColor = snakeColor;
            Console.Write(snakeSymbol);
            snakeIsAlive = true;
        }

        

        #region snakeCrawling ver 1.1
        public void snakeСrawling(ConsoleKey crawlingDirection) //в этот метод попадаем только если нажата стрелка, проверки не нужны
        {
            SnakeGrow(snakePositionHorizontal, snakePositionVertical); //место где была змея добавляем в хвост(очередь)
            switch (crawlingDirection)
            {
                case ConsoleKey.LeftArrow:
                    snakePositionHorizontal--;
                    break;
                case ConsoleKey.RightArrow:
                    snakePositionHorizontal++;
                    break;
                case ConsoleKey.DownArrow:
                    snakePositionVertical++;
                    break;
                case ConsoleKey.UpArrow:
                    snakePositionVertical--;
                    break;
            }
            SnakeHeadNewPosition(); //переместить голову змеи
        }
        #endregion


        

        void SnakeHeadNewPosition() //голова змеи
        {
            Console.SetCursorPosition(snakePositionHorizontal, snakePositionVertical);
            Console.Write(snakeSymbol);

            //проверить если змея укусила себя за хвост или вышла за рамки поля
            if (snakeLengthTail.Exists( s => s.h == snakePositionHorizontal && s.v == snakePositionVertical))
            {
                Console.Write("The snake has bitten its own tail");
                snakeIsAlive = false;
            }
            if (snakePositionHorizontal == 0 || snakePositionHorizontal >= borderWidth)
            {
                Console.WriteLine("You left the playing field");
                snakeIsAlive = false;
            }
            if (snakePositionVertical == 0 || snakePositionVertical >= borderHeight)
            {
                Console.WriteLine("You left the playing field");
                snakeIsAlive = false;
            }

        }

      

        public int[] SnakeGEtCurrentPosition()
        {
            int[] snakePosition = new int[2];
            snakePosition[0] = snakePositionHorizontal;
            snakePosition[1] = snakePositionVertical;
            return snakePosition;
        }

        public void SnakeEatFood(Food food)
        {
            food.Die(); //мышь умирает, но на в другом месте рождаются новые
            snakeLength++; //увеличить длину хвоста змеи
        }

        public void SnakeTail() //int snakeLength //нарисовать хвост змеи
        {
            SnakeTailPosition elementTailPosition;

            snakeLengthTail.Reverse();

            int p = 0;
            while (p < snakeLength)
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                elementTailPosition = snakeLengthTail[p];
                Console.SetCursorPosition(elementTailPosition.h, elementTailPosition.v); //ставим в том месте курсор
                Console.Write(snakeTaileSymbol); //печатаем хвост
                Console.ForegroundColor = consoleColor;
                p++;
            }
            if (snakeLengthTail.Count > p)
            {
                while (p <= snakeLengthTail.Count)
                {
                    elementTailPosition = snakeLengthTail[p];
                    Console.SetCursorPosition(elementTailPosition.h, elementTailPosition.v); //ставим в том месте курсор
                    Console.Write(" "); //иначе затираем следы где ползла змеюка
                    snakeLengthTail.RemoveAt(p);
                    p++;
                }
            }
            snakeLengthTail.Reverse();
        }



        public void SnakeGrow(int h, int v)
        {
            
            if (snakeLength > 0)
            {
                snakeLengthTail.Add(new SnakeTailPosition(h, v));
                SnakeTail();
            }
            else
            {
                Console.SetCursorPosition(h, v); //ставим в том месте курсор
                Console.Write(" "); //иначе затираем следы где ползла змеюка
            }
            


            #region for debug
            Console.SetCursorPosition(0, borderHeight+2); //ставим в том месте курсор
            Console.Write($"Points {snakeLengthTail.Count}");
            #endregion

        }


    }


    class SnakeTailPosition
    {
        public int h { get; set; }
        public int v { get; set; }

        public SnakeTailPosition(int h, int v)
        {
            this.h = h;
            this.v = v;
        }

    }
}
