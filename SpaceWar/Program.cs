using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

public class Win32Interop
{
    [DllImport("crtdll.dll")]
    public static extern int _kbhit();
}
/*
 * v - void
 * g - green
 * b - blue
 * r - red
 * y - yellow
 * e - enemy environment
 * x - player environment
 * p - projectile (enemy)
 * z - zap (player)
 */
namespace SpaceWar
{
    class Program
    {
        public static void Destroyed()
        {
            foreach (var prj in RainOfProjectiles)
            {
                Projectile.Clear(prj);
            }
            RainOfProjectiles.Clear();
            foreach (var playerProjectile in Zaps)
            {
                PlayerProjectile.Clear(playerProjectile);
            }
            Zaps.Clear();
            Player.Life--;
            Ship.MovePlayer(Player, (90 - Player.Posx), 0);
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("TOTAL LIVES: " + Player.Life);
        }

        public static void KillEnemy()
        {
            //proverka na stolknovenie
            /*
             * for (int q = 0; q < Zaps.Count; q++)
            {
                var playerProjectile = Zaps[q];
             */
            for (int w = 0; w < Swarm.Count; w++)
            {
                for (int q = 0; q < Zaps.Count; q++)
                {
                    var playerProjectile = Zaps[q];
                    try
                    {
                        var enemy = Swarm[w];

                        for (int i = 0; i < 8; i++)
                        {
                            if (playerProjectile.Posx - i == enemy.Posx && playerProjectile.Posy - 7 == enemy.Posy)
                            {
                                PlayerProjectile.Clear(playerProjectile);
                                Zaps.Remove(playerProjectile);
                                enemy.Life--;
                                if (enemy.Life < 1)
                                {
                                    Ship.Cleaner(enemy);
                                    Swarm.Remove(enemy);
                                }

                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    for (int a = 0; a < 8; a++)
                                    {
                                        for (int b = 0; b < 8; b++)
                                        {
                                            Console.SetCursorPosition(enemy.Posx + b, enemy.Posy + a);
                                            Console.Write(' ');
                                        }
                                    }
                                    Thread.Sleep(5);
                                }
                            }
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        public static void CreateSwarm(int count)
        {
            if (count > 5)
                count = 5;

            for (byte i = 0; i < count; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        Swarm.Add(new Ship(Buf.GreenStructure, R.Next(7, 15)));
                        break;

                    case 1:
                        Swarm.Add(new Ship(Buf.YellowStructure, R.Next(7, 15)));
                        break;

                    case 2:
                        Swarm.Add(new Ship(Buf.RedStructure, R.Next(7, 15)));
                        break;

                    case 3:
                        Swarm.Add(new Ship(Buf.BlueStructure, R.Next(7, 15)));
                        break;
                }

                Swarm[i].Posx = 20 + i * 20;
                Swarm[i].Posy = 5;
                Ship.Move(Swarm[i], 0, 0);
            }
        }

        public static void reset()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            for (byte i = 0; i < Console.BufferHeight - 1; i++)
            {
                for (byte j = 0; j < Console.BufferWidth - 1; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(' ');
                }
            }
        }

        public static List<Projectile> RainOfProjectiles = new List<Projectile>();
        public static Ship Player = new Ship(Buf.PlayerShip, 5);
        public static List<PlayerProjectile> Zaps = new List<PlayerProjectile>();
        public static List<Ship> Swarm = new List<Ship>();
        public static Random R = new Random();

        static void Main()
        {
            var stars = new List<Star>();
            Console.Title = "Space War";
            Console.WindowLeft = Console.WindowTop = 0;
            Console.WindowHeight = Console.BufferHeight = 50;//60
            Console.WindowWidth = Console.BufferWidth = 160;//180
            Console.CursorVisible = false;
            int enemies = 2;
            int hardness = 4;
            Console.WriteLine("Choose Your Destiny!\n1) New Game\n2) Exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            //заполняем рабочую зону
            if (choice == 1)
            {
                Player.Life = 5;
                Console.Clear();
                reset();
                for (byte i = 0; i < Console.BufferHeight; i++)
                {
                    for (byte j = 0; j < Console.BufferWidth; j++)
                    {
                        Buf.Buffer[i, j] = 'v';
                    }
                }

                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("TOTAL LIVES: " + Player.Life);

                for (byte i = 0; i < 15; i++)
                {
                    stars.Add(new Star());
                }

                CreateSwarm(enemies);
                Ship.MovePlayer(Player, 90, 38);
                while (true)//let's go
                {
                    if (Swarm.Count == 0)
                    {
                        foreach (var pp in Zaps)
                        {
                            PlayerProjectile.Clear(pp);
                        }
                        foreach (var prj in RainOfProjectiles)
                        {
                            Projectile.Clear(prj);
                        }
                        Zaps.Clear();
                        RainOfProjectiles.Clear();
                        Player.Life++;
                        Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("TOTAL LIVES: " + Player.Life);
                        if (hardness > 1)
                            hardness--;
                        CreateSwarm(++enemies);
                    }

                    Star.GoStars(stars);
                    if (Player.Life == 0)
                    {
                        break;
                    }

                    foreach (var enemy in Swarm)
                    {
                        Ship.Move(enemy, R.Next(-2, 3), R.Next(-2, 3));
                        if (R.Next(0, enemy.Life * hardness) == 0 && enemy.Delay == 0)
                        {
                            RainOfProjectiles.Add(new Projectile(R.Next(1, 4), R.Next(1, 4), enemy.Posx + 4, enemy.Posy + 8));
                        }
                        enemy.Delay++;
                        if (enemy.Delay == 4)
                        {
                            enemy.Delay = 0;
                        }
                    }

                    for (byte i = 0; i < 5 - hardness; i++ )
                        if (Projectile.Fire(RainOfProjectiles))//front impact of projectile and player
                        {
                            Destroyed();
                        }

                    int delay = 0;
                    for (byte i = 0; i < 7; i++)
                    {
                        bool destroyed = false;
                        switch (Buf.Reader())
                        {
                            case 3:
                                for (byte ii = 0; ii < 7; ii++)
                                {
                                    if (Buf.Buffer[Player.Posy + ii, Player.Posx + 9] == 'p')
                                    {
                                        destroyed = true;
                                    }
                                }
                                Ship.MovePlayer(Player, 2, 0);
                                break;

                            case 4:
                                for (byte ii = 0; ii < 7; ii++)
                                {
                                    if (Buf.Buffer[Player.Posy + ii, Player.Posx - 2] == 'p')
                                    {
                                        destroyed = true;
                                    }
                                }
                                Ship.MovePlayer(Player, -2, 0);
                                break;

                            case 5:
                                if (delay == 0 && Zaps.Count<2)
                                {
                                    Zaps.Add(new PlayerProjectile(1, 3, Player.Posx + 1, Player.Posy - 1));
                                    Zaps.Add(new PlayerProjectile(1, 3, Player.Posx + 5, Player.Posy + 1));// - 1));
                                    delay++;
                                }
                                break;

                            case 6://test
                                if (delay == 0 && Zaps.Count<2)
                                {
                                    Zaps.Add(new PlayerProjectile(1, 4, Player.Posx + 3, Player.Posy + 1));
                                    delay++;
                                }
                                break;
                        }

                        if (PlayerProjectile.Fire(Zaps))//or if?!
                        {
                            KillEnemy();
                        }

                        if (destroyed)
                        {
                            Destroyed();
                        }

                        if (delay > 0)
                            delay++;
                        if (delay == 2000)
                            delay = 0;
                        Thread.Sleep(5);
                    }
                }
                reset();
                const string msg = "Вы храбро сражались, вам было тяжело...настолько тяжело, что вас победили, это печально, но факт...удачи в следующий раз D:";
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                foreach (var c in msg)
                {
                    Console.Write(c);
                    Thread.Sleep(100);
                }
                Console.ReadLine();
                Console.Clear();
                enemies = 2;
                Swarm.Clear();
                RainOfProjectiles.Clear();
                Zaps.Clear();
                Ship.MovePlayer(Player, 90, 38);
                Main();
            }

            if (choice == 2)
            {
                Environment.Exit(0);
            }
        }
    }
}

