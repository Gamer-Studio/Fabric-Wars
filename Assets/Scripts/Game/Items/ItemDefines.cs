namespace FabricWars.Game.Items
{
    public partial class Item
    {
        public static Item None { get; private set; }
        public static Item Log { get; private set; }
        public static Item Coin { get; private set; }
        
        public static void InitItems()
        {
            None = General.items["None"];
            Log = General.items["Log"];
            Coin = General.items["Coin"];
        }
    }
}