using System;

namespace Sparta_Csharp.Managers
{
    public class StoreManager
    {
        public Player Player { get; private set; }

        public StoreManager(Player player)
        {
            Player = player;
        }
        
        private void DisplayPurchaseOptions()
        {
            Console.Clear();
            Console.WriteLine($"[보유 골드] {Player.Gold} G");
            Player.Store.DisplayStore(true);
            Console.WriteLine("\n0. 나가기");
        }
        
        private void DisplaySellOptions()
        {
            Console.Clear();
            Console.WriteLine($"[보유 골드] {Player.Gold} G");
            Player.Inventory.DisplayInventory(true, true);
            Console.WriteLine("\n0. 나가기");
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[보유 골드] {Player.Gold} G");
                Player.Store.DisplayStore(false);
                Console.WriteLine("\n0. 나가기\n1. 아이템 구매\n2. 아이템 판매");
                
                int input = InputCommand.ReadInt(0, 2);
                switch (input)
                {
                    case 0:
                        return;
                    case 1:
                        Purchase();
                        break;
                    case 2:
                        Sell();    
                        break;
                }
            }
        }
        
        private void Purchase()
        {
            DisplayPurchaseOptions();
            while (true)
            {
                int select = InputCommand.ReadInt(0, Player.Store.GetStoreSize());
                if (select == 0) return;

                if (Player.Store.IsSoldOut(select - 1))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    continue;
                }

                if (!Player.ValidPrice(select - 1))
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    continue;
                }
                
                Player.PurchaseItem(select - 1);
                DisplayPurchaseOptions();
                Console.WriteLine("구매를 완료했습니다!");
            }
        }

        private void Sell()
        {
            DisplaySellOptions();
            while (true)
            {
                int select = InputCommand.ReadInt(0, Player.Inventory.GetInventorySize());
                if (select == 0) return;

                Player.AddGold(Player.Inventory.GetItemPrice(select - 1) * 85 / 100);
                Item item = Player.Inventory.SellItem(select - 1);
                Player.Store.PurchaseItem(item);
                Player.UpdateStats();
                DisplaySellOptions();
                Console.WriteLine("판매가 완료되었습니다.");
            }
        }
    }
}