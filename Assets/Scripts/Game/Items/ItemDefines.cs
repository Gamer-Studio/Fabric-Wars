namespace FabricWars.Game.Items
{
    public partial class ItemManager
    {
        public static Item None { get; private set; }
        public static Item Log { get; private set; }
        
        public static void InitItems()
        {
            None = General.items["None"];
            Log = General.items["Log"];
        }
    }
}