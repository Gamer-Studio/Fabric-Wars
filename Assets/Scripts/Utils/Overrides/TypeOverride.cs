using System;
using System.Linq;
using System.Reflection;

namespace FabricWars.Utils.Overrides
{
    public static class TypeOverride
    {
        public static bool ImplicitlyConvertsTo(this Type type, Type destinationType)
        {
            if (type == destinationType)
                return true;


            return (from method in type.GetMethods(BindingFlags.Static |
                                                   BindingFlags.Public)
                    where method.Name == "op_Implicit" &&
                          method.ReturnType == destinationType
                    select method
                ).Any();
        }
        
        public static bool ExplicitlyConvertsTo(this Type type, Type destinationType)
        {
            if (type == destinationType)
                return true;


            return (from method in type.GetMethods(BindingFlags.Static |
                                                   BindingFlags.Public)
                    where method.Name == "op_Explicit" &&
                          method.ReturnType == destinationType
                    select method
                ).Any();
        }
    }
}