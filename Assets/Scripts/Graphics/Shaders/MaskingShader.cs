using UnityEngine;

namespace FabricWars.Graphics.Shaders
{
    [CreateAssetMenu(fileName = "new Mask Config", menuName = "Shader/Mask Config", order = 0)]
    public class MaskingShader : ScriptableObject
    {
        public Color maskColor;
        public Texture texture;
    }
}