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
            ItemType itemType = items[index].Type;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.Type == itemType &&
                    items[index].Name != inventoryItem.Name &&
                    inventoryItem.IsSelected)
                {
                    inventoryItem.IsSelected = false;
                }
            }
            items[index].IsSelected = !items[index].IsSelected;
        }

        public Item SellItem(int index)
        {
            Item item = items[index];
            item.IsSelected = false;
            items.RemoveAt(index);
            return item;
        }
        
        public int GetInventorySize()
        {
            return items.Count;
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public int GetItemPrice(int index)
        {
            return items[index].Price;
        }

        public void DisplayInventory(bool selectMod, bool goldVisible)
        {
            Console.WriteLine("[아이템 목록]");
            int index = 1;
            foreach (Item item in items)
            {
                string indexText = selectMod ? $"{index++}. " : "";
                string selected = item.IsSelected ? "[E] " : "";
                string typeText = item.Type == ItemType.Attack ? "공격력" : "방어력";
                string goldText = goldVisible ? $"{item.Price} G" : "";
                Console.WriteLine($"- {indexText}{selected}{item.Name}\t| {typeText} +{item.Point}\t| {item.State}\t| {goldText}");
            }
        }
    }
}