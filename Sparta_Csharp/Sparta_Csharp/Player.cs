using System;

namespace Sparta_Csharp
{
    public class Player
    {
        public int Level { get; private set; } = 1;
        public string Name { get; private set; } = "Chad";
        public string Job { get; private set; } = "전사";
        public float Attack { get; private set; } = 10;
        public int Defense { get; private set; } = 5;
        public int Health { get; private set; } = 100;
        public int Gold { get; private set; } = 15000;

        private int extraAttack = 0;
        private int extraDefense = 0;
        

        public Inventory Inventory { get; private set; }
        public Store Store { get; private set; }

        public Player()
        {
            Inventory = new Inventory();
            Store = new Store();
        }

        public void AddGold(int extraGold)
        {
            Gold += extraGold;
        }

        public void SetHealth(int newHealth)
        {
            Health = newHealth;
        }

        public bool ValidPrice(int index)
        {
            return Gold >= Store.GetItemPrice(index);
        }

        public void PurchaseItem(int index)
        {
            Item item = Store.SellItem(index);
            Inventory.AddItem(item);
            Gold -= item.Price;
        }

        public void LevelUp()
        {
            Level += 1;
            Attack += 0.5f;
            Defense += 1;
        }

        public void UpdateStats()
        {
            extraAttack = 0;
            extraDefense = 0;

            foreach (var item in Inventory.GetItems())
            {
                if (!item.IsSelected) continue;
                if (item.Type == ItemType.Attack) extraAttack += item.Point;
                else extraDefense += item.Point;
            }
        }

        public void DisplayState()
        {
            Console.WriteLine("[캐릭터 상태]");
            Console.WriteLine($"Lv\t: {Level:D2}");
            Console.WriteLine($"이름\t: {Name} ({Job})");
            Console.WriteLine($"공격력\t: {Attack}" + (extraAttack > 0 ? $" (+{extraAttack})" : ""));
            Console.WriteLine($"방어력\t: {Defense}" + (extraDefense > 0 ? $" (+{extraDefense})" : ""));
            Console.WriteLine($"체력\t: {Health}");
            Console.WriteLine($"Gold\t: {Gold} G\n");
            Console.WriteLine("0. 나가기\n");
        }
    }
}