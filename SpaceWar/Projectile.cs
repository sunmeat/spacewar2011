using System;
using System.Collections.Generic;

namespace SpaceWar
{
    public class Projectile
    {
        private int width;
        private int height;
        public int Posx;
        public int Posy;
        //private int direction;

        public Projectile(int width, int height, int posx, int posy)//, int direction)
        {
            this.width = width;
            this.height = height;
            Posx = posx;
            Posy = posy;
            //this.direction = direction;
        }

        public static void Clear(Projectile projectile)
        {
            Console.BackgroundColor = ConsoleColor.Black; //Cleaner
            {
                for (byte i = 0; i < projectile.height; i++)
                {
                    for (byte j = 0; j < projectile.width; j++)
                    {
                        switch (Buf.Buffer[projectile.Posy + i, projectile.Posx + j])
                        {
                            case 'p':
                            case 'v':
                                Console.SetCursorPosition(projectile.Posx + j, projectile.Posy + i);
                                Buf.Buffer[projectile.Posy + i, projectile.Posx + j] = 'v';
                                Console.Write(' ');
                                break;
                        }
                    }
                }
            }
        }

        public static void Draw(Projectile projectile, ref bool impact)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            for (byte i = 0; i < projectile.height; i++)
            {
                for (byte j = 0; j < projectile.width; j++)
                {
                    if (Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'v' || Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'x')
                    {
                        Console.SetCursorPosition(projectile.Posx + j, projectile.Posy + i);
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] = 'p';
                        Console.Write(' ');
                    }

                    if ((Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'r' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'g' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'b' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'y') &&
                        projectile.Posy > 25)
                    {
                        impact = true;
                    }
                }
            }
        }

        public static bool Fire(List<Projectile> projectiles)
        {
            bool impact=false;
            if (projectiles.Count > 0)
                for (int i = 0; i < projectiles.Count; i++)
                {
                    var projectile = projectiles[i];
                    if (projectile.Posy + projectile.height < Buf.Buffer.GetUpperBound(0))
                    {
                        Clear(projectile);
                        projectile.Posy += 1;
                        Draw(projectile, ref impact);
                    }

                    else
                    {
                        Clear(projectile);
                        projectiles.Remove(projectile);
                    }
                }
            return impact;
        }

        //
    }
}