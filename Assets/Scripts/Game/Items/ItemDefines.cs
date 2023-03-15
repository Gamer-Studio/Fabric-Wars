namespace FabricWars.Game.Items
{
    public partial class Item
    {
        private static bool _inited = false;
        public static Item None, Log, Coin;
        
        public static void InitItems()
        {
            if (_inited) return;

            None = General.items["None"];
            Log = General.items["Log"];
            Coin = General.items["Coin"];

            _inited = true;
        }
    }
}