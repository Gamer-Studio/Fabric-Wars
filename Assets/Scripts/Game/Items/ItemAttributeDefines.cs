namespace FabricWars.Game.Items
{
    public partial class ItemAttribute
    {
        public static ItemAttribute None, Causality, Life, Water, Fire, Gold;

        public static void Init()
        {
            None = General.itemAttributes["None"];
            Causality = General.itemAttributes["Causality"];
            Life = General.itemAttributes["Life"];
            Water = General.itemAttributes["Water"];
            Fire = General.itemAttributes["Fire"];
            Gold = General.itemAttributes["Gold"];
        }
    }
}