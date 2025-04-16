namespace Sparta_Csharp
{
    public enum ItemType
    {
        Attack,
        Defense,
    }

    public class Item
    {
        public string name;
        public ItemType type;
        public int point;
        public bool isSelected = false;
        public bool isPurchased = false;
        public string state;
        public int price;

        public Item(string _name, ItemType _type, int _point, string _state, int _price)
        {
            name = _name;
            type = _type;
            point = _point;
            state = _state;
            price = _price;
        }
    }
}