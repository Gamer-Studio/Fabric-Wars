using System.Collections.Generic;
using FabricWars.Game.Entities.Functions;
using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Entities
{
    public partial class Entity : MonoBehaviour
    {
        public GaugeInt health;

        [SerializeField] private EntityFunction[] AddFunctionOnStartup;

        [SerializeField] private List<EntityFunction> activeFunctions = new();
        private readonly Dictionary<EntityFunction, Coroutine> _functions = new ();

        protected virtual void Awake()
        {
            
        }
        
        protected virtual void Start()
        {
            if (AddFunctionOnStartup != null)
            {
                foreach (var function in AddFunctionOnStartup)
                {
                    AddFunction(function);
                }
            }
        }

        public void AddFunction(EntityFunction function)
        {
            activeFunctions.Add(function);
            _functions.Add(function, StartCoroutine(function.GetFunction()));
        }

        public void RemoveFunction(EntityFunction function)
        {
            if (!_functions.ContainsKey(function) || _functions[function] == null) return;
            
            StopCoroutine(_functions[function]);
            _functions.Remove(function);
            activeFunctions.Remove(function);
        }

        public void ClearFunctions()
        {
            foreach (var function in activeFunctions)
            {
                RemoveFunction(function);
            }
        }
        
        public EntityFunction[] GetFunctions() => activeFunctions.ToArray();

        // broadcast event interface
        private void OnBreak()
        {
        }
    }
}