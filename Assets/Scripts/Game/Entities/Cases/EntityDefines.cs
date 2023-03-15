namespace FabricWars.Game.Entities.Cases
{
    public partial class Entity
    {
        private static bool _inited = false;

        public static Entity None;
        
        
        public static void InitEntities()
        {
            if(_inited) return;

            _inited = true;
        }
    }
}