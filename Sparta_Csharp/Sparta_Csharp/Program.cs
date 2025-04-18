using System;

namespace Sparta_Csharp
{
    public class Program
    {
        private static Player player;
        
        private static int InputCommand(int start, int end)
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int command) && command >= start && command <= end)
                {
                    return command;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            player = new Player();

            while (true)
            {
                Console.Clear();
                DisplayMainScene();
                int command = InputCommand(1, 5);

                Console.Clear();
                switch (command)
                {
                    case 1:
                        player.DisplayState();
                        InputCommand(0, 0);
                        break;
                    case 2:
                        ManageInventory();
                        break;
                    case 3:
                        ManageStore();
                        break;
                    case 4:
                        break;
                    case 5:
                        ManageRest();
                        break;
                }
            }
        }

        private static void DisplayMainScene()
        {
            Console.WriteLine("[메인 메뉴]");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
        }

        private static void ManageInventory()
        {
            player.Inventory.DisplayInventory(false, false);
            while (true)
            {
                Console.WriteLine("\n0. 나가기\n1. 장착 관리");
                int input = InputCommand(0, 1);
                if (input == 0) break;
                
                Console.Clear();
                player.Inventory.DisplayInventory(true, false);
                while (true)
                {
                    Console.WriteLine("\n0. 나가기");
                    int select = InputCommand(0, player.Inventory.GetInventorySize());
                    if (select == 0)
                    {
                        Console.Clear();
                        ManageInventory();                                    
                        return;
                    }
                    
                    player.Inventory.SelectItem(select - 1);
                    player.UpdateStats();
                    Console.Clear();
                    player.Inventory.DisplayInventory(true, false);
                }
            }
        }

        private static void ManageStore()
        {
            Console.Clear();
            Console.WriteLine($"\n[보유 골드] {player.GetGold()} G");
            player.Store.DisplayStore(false);
            Console.WriteLine("\n0. 나가기\n1. 아이템 구매\n2. 아이템 판매");
            int input = InputCommand(0, 2);
            switch (input)
            {
                case 0:
                    return;
                case 1:
                    PurchaseItem();
                    return;
                case 2:
                    SellItem();    
                    return;
            }
        }

        private static void PurchaseItem()
        {
            Console.Clear();
            Console.WriteLine($"\n[보유 골드] {player.GetGold()} G");
            player.Store.DisplayStore(true);
            Console.WriteLine("\n0. 나가기");
            while (true)
            {
                int select = InputCommand(0, player.Store.GetStoreSize());
                if (select == 0)
                {
                    ManageStore();
                    return;
                }

                if (player.Store.IsSoldOut(select - 1))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    continue;
                }

                if (!player.ValidPrice(select - 1))
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    continue;
                }

                player.PurchaseItem(select - 1);
                Console.WriteLine("구매를 완료했습니다!");
            }
        }

        private static void SellItem()
        {
            Console.Clear();
            Console.WriteLine($"\n[보유 골드] {player.GetGold()} G");
            player.Inventory.DisplayInventory(true, true);
            Console.WriteLine("\n0. 나가기");
            while (true)
            {
                int select = InputCommand(0, player.Inventory.GetInventorySize());
                if (select == 0)
                {
                    ManageStore();
                    return;
                }

                player.AddGold(player.Inventory.GetItemPrice(select - 1) * 85 / 100);
                Item item = player.Inventory.SellItem(select - 1);
                player.Store.PurchaseItem(item);
                player.UpdateStats();
                Console.Clear();
                Console.WriteLine($"\n[보유 골드] {player.GetGold()} G");
                player.Inventory.DisplayInventory(true, true);
                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("판매가 완료되었습니다.");
            }
        }

        private static void ManageRest()
        {
            Console.Write("500 G를 내면 체력을 회복할 수 있습니다. ");
            Console.WriteLine($"보유골드 : {player.GetGold()} G");
            Console.WriteLine("\n0. 나가기\n1. 휴식하기");

            while (true)
            {
                int input = InputCommand(0, 1);
                if (input == 0) break;

                if (player.GetGold() < 500)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
                else
                {
                    player.SetHealth(100);
                    player.AddGold(-500);
                    Console.WriteLine("휴식을 완료했습니다.");
                }
            }
        }
    }
}
