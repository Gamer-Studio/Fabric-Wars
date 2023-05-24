using System.Reflection;

namespace FabricWars.Utils
{
    public static class ObjectUtil
    {
        public static object GetParentObject(string path, object obj)
        {
            while (true)
            {
                var fields = path.Split('.');

                if (fields.Length == 1) return obj;

                var info = obj.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                obj = info.GetValue(obj);

                path = string.Join(".", fields, 1, fields.Length - 1);
            }
        }
    }
}