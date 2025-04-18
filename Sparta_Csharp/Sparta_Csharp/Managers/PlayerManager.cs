using System;

namespace Sparta_Csharp.Managers
{
    public class PlayerManager
    {
        public Player Player { get; private set; }

        public PlayerManager(Player player)
        {
            Player = player;
        }

        public void RunState()
        {
            Console.Clear();
            Player.DisplayState();
            InputCommand.ReadInt(0, 0);
        }

        public void RunRest()
        {
            Console.Clear();
            Console.WriteLine("[휴식]");
            Console.Write("500 G를 내면 체력을 회복할 수 있습니다. ");
            Console.WriteLine($"보유골드 : {Player.Gold} G");
            Console.WriteLine("\n0. 나가기\n1. 휴식하기");

            while (true)
            {
                int input = InputCommand.ReadInt(0, 1);
                if (input == 0) break;

                if (Player.Gold < 500)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
                else
                {
                    Player.SetHealth(100);
                    Player.AddGold(-500);
                    Console.WriteLine("휴식을 완료했습니다.");
                }
            }
        }
    }
}