using System;

namespace Sparta_Csharp
{
    public enum DungeonType
    {
        Easy = 1,
        Normal = 2,
        Hard = 3
    }
    public class Dungeon
    {
        public DungeonType Type { get; private set; }
        public int RecommendedDefense { get; private set; }

        public Dungeon(DungeonType type, int recommendedDefense)
        {
            Type = type;
            RecommendedDefense = recommendedDefense;
        }

        public bool EnterDungeon(int playerDefense)
        {
            Random rand = new Random();

            if (RecommendedDefense <= playerDefense || rand.Next(0, 10) >= 4) return true;
            return false;
        }
    }
}