using System;

namespace SpaceWar
{
    public class Ship
    {
        public int Posx;
        public int Posy;
        public int Life;
        public int Delay;
        public char[,] Structure;

        public Ship(char[,] structure, int life)
        {
            Structure = structure;
            Life = life;
            Delay = 0;
        }

        public Ship()
        {
            Delay = 0;
            Life = 0;
            Posx = 0;
            Posy = 0;
            Structure = Buf.RedStructure;
        }

        public static void Cleaner(Ship ship)
        {
            Console.BackgroundColor = ConsoleColor.Black; //Cleaner
            for (byte i = 0; i < ship.Structure.GetUpperBound(0) + 1; i++)
            {
                for (byte j = 0; j < ship.Structure.GetUpperBound(1) + 1; j++)
                {
                    Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);

                    if (ship.Structure[i, j] != 'x')//here it is
                    {
                        Console.Write(' ');
                    }
                    Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'v';
                }
            }
        }

        public static void Drawer(Ship ship)
        {
            for (byte i = 0; i < ship.Structure.GetUpperBound(0) + 1; i++)
            {
                for (byte j = 0; j < ship.Structure.GetUpperBound(1) + 1; j++)
                {
                    Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                    switch (ship.Structure[i, j])
                    {
                        case 'b'://blue
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'b';
                            Console.Write(' ');
                            break;

                        case 'r'://red
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'r';
                            Console.Write(' ');
                            break;

                        case 'g'://green
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'g';
                            Console.Write(' ');
                            break;

                        case 'y'://yellow
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'y';
                            Console.Write(' ');
                            break;

                        case 'v'://void
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = 'v';
                            Console.Write(' ');
                            break;

                        default:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(ship.Posx + j, ship.Posy + i);
                            Buf.Buffer[ship.Posy + i, ship.Posx + j] = ship.Structure[i, j];
                            Console.Write(' ');
                            break;
                    }
                }
            }
        }

        public static void MovePlayer(Ship ship, int x, int y)
        {

            if (ship.Posx + x > 0 && ship.Posx + x < 140)
            {
                Cleaner(ship);
                ship.Posx += x;
                ship.Posy += y;
                Drawer(ship);
            }

        }

        public static void Move(Ship ship, int x, int y)
        {
            Cleaner(ship);
            if (
                ship.Posy + y < 40 && //ултрапроверка, чтоб не налезали, ублюдки!
                ship.Posx + x < 140 && //160
                ship.Posx + x > 0 &&
                ship.Posy + y > 0 &&
                Buf.Buffer[ship.Posy + y, ship.Posx + x] == 'v' &&
                Buf.Buffer[ship.Posy + y + 8, ship.Posx + x] == 'v' &&
                Buf.Buffer[ship.Posy + y, ship.Posx + x + 8] == 'v' &&
                Buf.Buffer[ship.Posy + y + 8, ship.Posx + x + 8] == 'v' &&
                Buf.Buffer[ship.Posy + y + 7, ship.Posx + x] == 'v' &&
                Buf.Buffer[ship.Posy + y, ship.Posx + x + 7] == 'v' &&
                Buf.Buffer[ship.Posy + y + 7, ship.Posx + x + 7] == 'v' &&
                Buf.Buffer[ship.Posy + y + 9, ship.Posx + x] == 'v' &&
                Buf.Buffer[ship.Posy + y, ship.Posx + x + 9] == 'v' &&
                Buf.Buffer[ship.Posy + y + 9, ship.Posx + x + 9] == 'v'
                )
            {
                if (ship.Posy >= 16)
                {
                    y = -2;
                }
                ship.Posx += x;
                ship.Posy += y;
            }
            Drawer(ship);
        }
    }
}
