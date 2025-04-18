using System;
using Sparta_Csharp.Managers;

namespace Sparta_Csharp
{
    
    public class Program
    {
        private static Player player;
        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

            player = new Player();
            var playerManager = new PlayerManager(player);
            var inventoryManager = new InventoryManager(player);
            var storeManager = new StoreManager(player);
            var dungeonManager = new DungeonManager(player);

            while (true)
            {
                Console.Clear();
                DisplayMainScene();
                int command = InputCommand.ReadInt(1, 5);

                Console.Clear();
                switch (command)
                {
                    case 1:
                        playerManager.RunState();
                        break;
                    case 2:
                        inventoryManager.Run();
                        break;
                    case 3:
                        storeManager.Run();
                        break;
                    case 4:
                        dungeonManager.Run();
                        break;
                    case 5:
                        playerManager.RunRest();
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
    }
}
