namespace FabricWars.Game.Elements
{
    public partial class Element
    {
        public static Elements.Element None, Causality, Life, Water, Fire, Gold;

        public static void Init()
        {
            None = General.elements["None"];
            Causality = General.elements["Causality"];
            Life = General.elements["Life"];
            Water = General.elements["Water"];
            Fire = General.elements["Fire"];
            Gold = General.elements["Gold"];
        }
    }
}