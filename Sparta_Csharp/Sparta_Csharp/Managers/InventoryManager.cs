using System;

namespace Sparta_Csharp.Managers
{
    public class InventoryManager
    {
        public Player Player { get; private set; }

        public InventoryManager(Player player)
        {
            Player = player;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Player.Inventory.DisplayInventory(false, false);
                Console.WriteLine("\n0. 나가기\n1. 장착 관리");

                if (InputCommand.ReadInt(0, 1) == 0) break;
                ManageEquip();
            }
            
        }

        private void ManageEquip()
        {
            while (true)
            {
                Console.Clear();
                Player.Inventory.DisplayInventory(true, false);
                Console.WriteLine("\n0. 나가기");

                int select = InputCommand.ReadInt(0, Player.Inventory.GetInventorySize());
                if (select == 0) break;
                
                Player.Inventory.SelectItem(select - 1);
                Player.UpdateStats();
            }
            
        }
    }
}