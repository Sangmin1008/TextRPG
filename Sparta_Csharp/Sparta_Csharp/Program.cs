using System;

namespace Sparta_Csharp
{
    public class Program
    {
        private static Player player;
        
        public static int InputCommand(int start, int end)
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
                int command = InputCommand(1, 3);

                switch (command)
                {
                    case 1:
                        Console.Clear();
                        player.DisplayState();
                        InputCommand(0, 0);
                        break;

                    case 2:
                        Console.Clear();
                        ManageInventory();
                        break;

                    case 3:
                        Console.Clear();
                        ManageStore();
                        break;
                }
            }
        }

        static void DisplayMainScene()
        {
            Console.WriteLine("[메인 메뉴]");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 상점");
        }

        static void ManageInventory()
        {
            player.Inventory.DisplayInventory(false);
            while (true)
            {
                Console.WriteLine("\n0. 나가기\n1. 장착 관리");
                int input = InputCommand(0, 1);
                if (input == 0) break;

                player.Inventory.DisplayInventory(true);
                while (true)
                {
                    Console.WriteLine("\n0. 나가기");
                    int select = InputCommand(0, player.Inventory.GetInventorySize());
                    if (select == 0)
                    {
                        ManageInventory();                                    
                        return;
                    }

                    player.Inventory.SelectItem(select - 1);
                    player.UpdateStats();
                    Console.Clear();
                    player.Inventory.DisplayInventory(true);
                }
            }
        }

        static void ManageStore()
        {
            Console.WriteLine($"\n[보유 골드] {player.GetGold()} G");
            player.Store.DisplayStore(false);
            Console.WriteLine("\n0. 나가기\n1. 아이템 구매");
            while (true)
            {
                int input = InputCommand(0, 1);
                if (input == 0) break;

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

                    if (player.Store.IsItemPurchased(select - 1))
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
        }
    }
}
