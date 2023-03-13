using System;
using FabricWars.Utils.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.UI
{
    [Serializable]
    public class ReSizer
    {
        public UnityEvent<int, int> resizeEvent;
        [SerializeField, GetSet("width")] private int _width;
        [SerializeField, GetSet("height")] private int _height;
        
        public int width
        {
            get => _width;
            set
            {
                _width = value;
                resizeEvent.Invoke(value, _height);
            }
        }

        public int height
        {
            get => _width;
            set
            {
                _height = value;
                resizeEvent.Invoke(_width, value);
            }
        }

        public ReSizer()
        {
            resizeEvent = new();
        }

        public void Resize(int rWidth, int rHeight)
        {
            _width = rWidth;
            _height = rHeight;
            
            resizeEvent.Invoke(rWidth, rHeight);
        }
    }
}