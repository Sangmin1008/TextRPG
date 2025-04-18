using System;
using System.Collections.Generic;

namespace Sparta_Csharp.Managers
{
    public class DungeonManager
    {
        public Player Player { get; private set; }
        public Dictionary<DungeonType, Dungeon> Dungeons;
        private Random rand = new Random();

        public DungeonManager(Player player)
        {
            Player = player;
            Dungeons = new Dictionary<DungeonType, Dungeon>
            {
                { DungeonType.Easy, new Dungeon(DungeonType.Easy, 5) },
                { DungeonType.Normal, new Dungeon(DungeonType.Normal, 11) },
                { DungeonType.Hard, new Dungeon(DungeonType.Hard, 25) }
            };
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("[던전 목록]");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("\n0. 나가기");
            foreach (var dungeon in Dungeons)
            {
                Console.WriteLine($"{(int)dungeon.Key}. {dungeon.Key} 던전\t| 방어력 {dungeon.Value.RecommendedDefense} 이상 권장");
            }
        }

        public void Run()
        {
            while (true)
            {
                DisplayOptions();
                int input = InputCommand.ReadInt(0, 3);
                if (input == 0) break;

                DungeonType type = (DungeonType)input;
                Dungeon dungeon = Dungeons[type];
                bool success = dungeon.EnterDungeon(Player.Defense);
                Update(type, dungeon.RecommendedDefense, success);
            }
        }

        private void Update(DungeonType type, int recommendedDefense, bool success)
        {
            int gold;
            if (type == DungeonType.Easy) gold = 1000;
            else if (type == DungeonType.Normal) gold = 1700;
            else gold = 2500;

            int defenseDiff = Player.Defense - recommendedDefense;
            
            Console.Clear();
            if (success)
            {
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{type} 던전을 클리어 하였습니다.\n");
                Console.WriteLine("[탐험 결과]");

                int damage = rand.Next(20 - defenseDiff, 36 - defenseDiff);
                int extraGold = gold * rand.Next((int)Player.Attack, (int)Player.Attack * 2 + 1) / 100;
                
                Console.WriteLine($"체력 {Player.Health} -> {Math.Max(Player.Health - damage, 0)}");
                Console.WriteLine($"Gold {Player.Gold} -> {Player.Gold + gold + extraGold}");
                Player.SetHealth(Math.Max(Player.Health - damage, 0));
                Player.AddGold(gold + extraGold);
                Player.LevelUp();
            }
            else
            {
                Console.WriteLine($"{type} 던전을 실패했습니다.");
                Console.WriteLine("[탐험 결과]");
                
                Console.WriteLine($"체력 {Player.Health} -> {Player.Health / 2}");
                Player.SetHealth(Player.Health / 2);
            }
            
            Console.WriteLine("\n0. 나가기");
            InputCommand.ReadInt(0, 0);
        }
    }
}