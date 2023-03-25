using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Food
    {
        
        char foodSymbol { get; set; }
        int foodHPosition { get; set; }
        int foodVPosition { get; set; }


        int borderWidth; //ширина игрового поля
        int borderHeight; //высота игрового поля


        Random randomPosition = new Random();

        public Food(int w, int h)
        {
            foodSymbol = '#';
            this.borderWidth = w;
            this.borderHeight = h;
            FoodPlace();
        }

        void FoodCreateRandomPosition()
        {
            foodHPosition = randomPosition.Next(5, borderWidth-5);
            foodVPosition = randomPosition.Next(3, borderHeight-3);
        }

        public void FoodPlace()
        {
            FoodCreateRandomPosition();
            Console.SetCursorPosition(foodHPosition, foodVPosition);
            var currentConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(foodSymbol);
            Console.ForegroundColor = currentConsoleColor;
        }

        public int[] FoodGetCurrentPosition()
        {
            int[] position = new int[2];
            position[0] = foodHPosition;
            position[1] = foodVPosition;
            return position;
        }

        public void Die() //мышка умирает
        {
            WMPLib.WindowsMediaPlayer Player;
            Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = ".\\Mp3\\mouse-6821.mp3";
            Player.controls.play();
            foodHPosition--;
            FoodPlace(); //и рождается новая
        }

    }
}
