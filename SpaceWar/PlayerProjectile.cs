using System;
using System.Collections.Generic;

namespace SpaceWar
{
    class PlayerProjectile
    {
        private int width;
        private int height;
        public int Posx;
        public int Posy;

        public PlayerProjectile(int width, int height, int posx, int posy)//, int direction)
        {
            this.width = width;
            this.height = height;
            Posx = posx;
            Posy = posy;
        }

        public static void Clear (PlayerProjectile projectile)
        {
            Console.BackgroundColor = ConsoleColor.Black; //Cleaner
            {
                for (byte i = 0; i < projectile.height; i++)
                {
                    for (byte j = 0; j < projectile.width; j++)
                    {
                        switch (Buf.Buffer[projectile.Posy + i, projectile.Posx + j])
                        {
                            case 'z':
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

        public static void Draw (PlayerProjectile projectile, ref bool impact)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            for (byte i = 0; i < projectile.height; i++)
            {
                for (byte j = 0; j < projectile.width; j++)
                {
                    if (Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'v')// || Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'e')
                    {
                        Console.SetCursorPosition(projectile.Posx + j, projectile.Posy + i);
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] = 'z';
                        Console.Write(' ');
                    }

                    else if ((
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'e' ||//why??
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'r' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'g' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'b' ||
                        Buf.Buffer[projectile.Posy + i, projectile.Posx + j] == 'y') &&
                        projectile.Posy < 25)
                    {
                        impact = true;
                    }
                }
            }
        }

        public static bool Fire (List<PlayerProjectile> projectiles)//her ego znaet
        {
            bool impact = false;
            if (projectiles.Count > 0)
                for (int i = 0; i < projectiles.Count; i++)
                {
                    var projectile = projectiles[i];
                    if (projectile.Posy > 3)
                    {
                        Clear(projectile);
                        projectile.Posy -= 1;
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
    }
}
