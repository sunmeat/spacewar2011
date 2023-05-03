using System;

namespace SpaceWar
{
    class Buf
    {
        public static char[,] Buffer = new char[50, 160];
        private static ConsoleKeyInfo _cki;
        public static char[,] RedStructure = new[,]
        {
        { 'r', 'e', 'e', 'e', 'e', 'e', 'e', 'r' }, 
        { 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r' },
        { 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r' },
        { 'e', 'e', 'e', 'r', 'r', 'e', 'e', 'e' },
        { 'e', 'e', 'e', 'r', 'r', 'e', 'e', 'e' },
        { 'e', 'r', 'r', 'e', 'e', 'r', 'r', 'e' },
        { 'e', 'r', 'r', 'e', 'e', 'r', 'r', 'e' },
        { 'e', 'r', 'e', 'e', 'e', 'e', 'r', 'e' }
        };

        public static char[,] BlueStructure = new[,]
        {
        { 'b', 'e', 'b', 'b', 'b', 'b', 'e', 'b' }, 
        { 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b' },
        { 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b' },
        { 'e', 'e', 'e', 'b', 'b', 'e', 'e', 'e' },
        { 'e', 'e', 'b', 'b', 'b', 'b', 'e', 'e' },
        { 'b', 'b', 'b', 'e', 'e', 'b', 'b', 'b' },
        { 'e', 'b', 'e', 'e', 'e', 'e', 'b', 'e' },
        { 'b', 'b', 'b', 'e', 'e', 'b', 'b', 'b' }
        };

        public static char[,] YellowStructure = new[,]
        {
        { 'y', 'e', 'e', 'e', 'e', 'e', 'e', 'y' }, 
        { 'y', 'y', 'y', 'y', 'y', 'y', 'y', 'y' },
        { 'y', 'y', 'y', 'y', 'y', 'y', 'y', 'y' },
        { 'e', 'e', 'e', 'y', 'y', 'e', 'e', 'e' },
        { 'e', 'y', 'y', 'y', 'y', 'y', 'y', 'e' },
        { 'e', 'y', 'e', 'e', 'e', 'e', 'y', 'e' },
        { 'y', 'y', 'y', 'e', 'e', 'y', 'y', 'y' },
        { 'e', 'y', 'e', 'e', 'e', 'e', 'y', 'e' }
        };

        public static char[,] GreenStructure = new[,]
        {
        {'g', 'g', 'e', 'g', 'g', 'e', 'g', 'g'},
        {'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
        {'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
        {'e', 'e', 'e', 'g', 'g', 'e', 'e', 'e'},
        {'e', 'e', 'e', 'g', 'g', 'e', 'e', 'e'},
        {'e', 'g', 'g', 'e', 'e', 'g', 'g', 'e'},
        {'g', 'g', 'g', 'e', 'e', 'g', 'g', 'g'},
        {'g', 'e', 'e', 'e', 'e', 'e', 'e', 'g'}
        };

        public static char[,] PlayerShip = new[,]
        {
        {'x', 'x', 'x', 'y', 'x', 'x', 'x'},
        {'x', 'g', 'x', 'y', 'x', 'g', 'x'},
        {'x', 'g', 'y', 'y', 'y', 'g', 'x'},
        {'x', 'g', 'b', 'b', 'b', 'g', 'x'},
        {'x', 'g', 'b', 'b', 'b', 'g', 'x'},
        {'r', 'r', 'b', 'b', 'b', 'r', 'r'},
        {'r', 'x', 'x', 'x', 'x', 'x', 'r'}
        };

        public static int Reader()
        {
            if (Console.KeyAvailable)
            {
                _cki = Console.ReadKey(true);
                if (_cki.Key == ConsoleKey.D)
                {
                    return 3;
                }

                if (_cki.Key == ConsoleKey.A)
                {
                    return 4;
                }

                if (_cki.Key == ConsoleKey.Spacebar)
                {
                    return 5;//exiter!
                }

                if (_cki.Key==ConsoleKey.W)
                {
                    return 6;
                }
            }
            return 0;
        }
    }
}
