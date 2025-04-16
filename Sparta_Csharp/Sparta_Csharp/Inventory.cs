using System;
using System.Collections.Generic;

namespace Sparta_Csharp
{
    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void SelectItem(int index)
        {
            items[index].isSelected = !items[index].isSelected;
        }

        public int GetInventorySize()
        {
            return items.Count;
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public void DisplayInventory(bool selectMod)
        {
            Console.Clear();
            Console.WriteLine("\n[아이템 목록]");
            int index = 1;
            foreach (Item item in items)
            {
                string indexText = selectMod ? $"{index++}. " : "";
                string selected = item.isSelected ? "[E] " : "";
                string typeText = item.type == ItemType.Attack ? "공격력" : "방어력";
                Console.WriteLine($"- {indexText}{selected}{item.name}\t| {typeText} +{item.point}\t| {item.state}");
            }
        }
    }
}