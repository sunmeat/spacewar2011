using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceWar
{
    class Star
    {
        public int Posx;
        public int Posy;
        public int Speed;
        public Random R=new Random();

        public Star()
        {
            Thread.Sleep(2);
            Posx = R.Next(0, Buf.Buffer.GetUpperBound(1));
            Posy = R.Next(0, Buf.Buffer.GetUpperBound(0));
            Speed = R.Next(1, 4);
        }

        public static void Draw(Star star)
        {
            if (Buf.Buffer[star.Posy, star.Posx] != 'v') return;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(star.Posx, star.Posy);
            Console.Write('.');
        }

        public static void Clear(Star star)
        {
            if (Buf.Buffer[star.Posy, star.Posx] != 'v') return;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(star.Posx, star.Posy);
            Console.Write(' ');
        }

        public static void GoStars(List<Star> stars)
        {
            for (var i = 0; i < stars.Count; i++)
            {
                var star = stars[i];
                if (star.Posy < Buf.Buffer.GetUpperBound(0)-4)
                {
                    Clear(star);
                    star.Posy += star.Speed;
                    Draw(star);
                }

                else
                {
                    Clear(star);
                    stars.Remove(star);
                    stars.Add(new Star());
                }
            }
        }
    }
}
