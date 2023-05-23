using System;

namespace FabricWars.Graphics.EditorUI
{
    /// <summary>
    /// Inspector button metadata
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public readonly string name;

        public ButtonAttribute(string name)
        {
            this.name = name;
        }
    }
}