using System;
using System.Collections.Generic;

namespace Sparta_Csharp
{
    public class Store
    {
        private List<Item> items;

        public Store()
        {
            items = new List<Item>();
            InitialItems();
        }

        public Item PurchaseItem(int index)
        {
            items[index].isPurchased = true;
            return items[index];
        }

        public int GetItemPrice(int index)
        {
            return items[index].price;
        }

        public bool IsItemPurchased(int index)
        {
            return items[index].isPurchased;
        }

        public int GetStoreSize()
        {
            return items.Count;
        }

        public void DisplayStore(bool selectMod)
        {
            Console.Clear();
            Console.WriteLine("\n[상점 아이템 목록]");
            int index = 1;
            foreach (Item item in items)
            {
                string prefix = selectMod ? $"{index++}." : "";
                string typeText = item.type == ItemType.Attack ? "공격력" : "방어력";
                string priceText = item.isPurchased ? "구매완료" : $"{item.price} G";
                Console.WriteLine($"-{prefix}{item.name}\t| {typeText} +{item.point}\t| {item.state}\t| {priceText}");
            }
        }

        private void InitialItems()
        {
            items.Add(new Item("수련자 갑옷", ItemType.Defense, 5, "수련에 도움을 주는 갑옷입니다.\t", 1000));
            items.Add(new Item("무쇠 갑옷", ItemType.Defense, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.\t", 2200));
            items.Add(new Item("스파르타 갑옷", ItemType.Defense, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            items.Add(new Item("낡은 검", ItemType.Attack, 2, "쉽게 볼 수 있는 낡은 검 입니다.\t", 600));
            items.Add(new Item("청동 도끼", ItemType.Attack, 5, "어디선가 사용됐던거 같은 도끼입니다.\t", 1500));
            items.Add(new Item("스파르타의 창", ItemType.Attack, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3200));
        }
    }
}