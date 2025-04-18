using System;

namespace Sparta_Csharp
{
    public class Player
    {
        private int level = 1;
        private string name = "Chad";
        private string job = "전사";
        private int attack = 10;
        private int defense = 5;
        private int health = 100;
        private int gold = 15000;

        private int extraAttack = 0;
        private int extraDefense = 0;

        private Item equippedWeapon;
        private Item equippedArmor;

        public Inventory Inventory { get; private set; }
        public Store Store { get; private set; }

        public Player()
        {
            Inventory = new Inventory();
            Store = new Store();
        }

        public int GetGold()
        { 
            return gold;
        }

        public void AddGold(int extraGold)
        {
            gold += extraGold;
        }

        public void SetHealth(int newHealth)
        {
            health = newHealth;
        }

        public bool ValidPrice(int index)
        {
            return gold >= Store.GetItemPrice(index);
        }

        public void PurchaseItem(int index)
        {
            Item item = Store.SellItem(index);
            Inventory.AddItem(item);
            gold -= item.price;
        }

        public void UpdateStats()
        {
            extraAttack = 0;
            extraDefense = 0;

            foreach (var item in Inventory.GetItems())
            {
                if (!item.isSelected) continue;
                if (item.type == ItemType.Attack) extraAttack += item.point;
                else extraDefense += item.point;
            }
        }

        public void DisplayState()
        {
            Console.Clear();
            Console.WriteLine("\n[캐릭터 상태]");
            Console.WriteLine($"Lv\t: {level:D2}");
            Console.WriteLine($"이름\t: {name} ({job})");
            Console.WriteLine($"공격력\t: {attack}" + (extraAttack > 0 ? $" (+{extraAttack})" : ""));
            Console.WriteLine($"방어력\t: {defense}" + (extraDefense > 0 ? $" (+{extraDefense})" : ""));
            Console.WriteLine($"체력\t: {health}");
            Console.WriteLine($"Gold\t: {gold} G\n");
            Console.WriteLine("0. 나가기\n");
        }
    }
}