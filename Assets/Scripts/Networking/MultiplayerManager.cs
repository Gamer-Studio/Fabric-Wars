namespace FabricWars.Networking
{
    public static class MultiplayerManager
    {
        public static bool isConnected = false;
        public static bool isHost = false;
        public static bool isClient => !isHost && isConnected;
        public static ConnectionConfig config { get; private set; }

        public static void Host(ConnectionConfig connectionConfig)
        {
            config = connectionConfig;
        }

        public static void Join()
        {
        }
    }
}