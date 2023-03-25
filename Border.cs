using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Border
    {
        public static void DrawBorder(char hLineSymbol, int hSize, char vLineSymbol, int vSize)
        {
            Console.WriteLine(new string(hLineSymbol, hSize));

            for (int line = 1; line < vSize; line++)
            {
                Console.SetCursorPosition(0, line);
                Console.Write(vLineSymbol);
                Console.SetCursorPosition(hSize, line);
                Console.Write(vLineSymbol);
            }
            Console.WriteLine("\n{0}",new string(hLineSymbol, hSize));
        }
    }
}
