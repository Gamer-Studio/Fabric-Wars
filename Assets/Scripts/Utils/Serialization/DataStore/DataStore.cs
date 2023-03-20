using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace FabricWars.Utils.Serialization.DataStore
{
    public abstract class DataStore
    {
        protected const string MainDbName = "convertible.json";
        public DirectoryInfo path;
        protected readonly Dictionary<string, (bool @readonly, object value)> _dataSet;

        public DataStore(DirectoryInfo path)
        {
            this.path = path;
            _dataSet = new Dictionary<string, (bool @readonly, object value)>();

            if(!this.path.Exists) return;
        
            Initialize();
        }

        public DataStore(string path) : this(new DirectoryInfo(path)) {}

        protected abstract void Initialize();

        public bool TryGet(string key, out object result)
        {
            result = null!;

            if (!_dataSet.TryGetValue(key, out var t)) return false;
            result = t.value;
        
            return true;
        }

        public bool TryGet<T>(string key, out T result)
        {
            result = default!;
        
            if (!TryGet(key, out var tr) || tr is not T tr1) return false;
            result = tr1;
        
            return true;

        }

        public object Get(string key, object defaultValue) =>
            _dataSet.TryGetValue(key, out var result) ? result.value : defaultValue;

        public T GetByType<T>(string key, T defaultValue)
        {
            var result = Get(key, defaultValue!);
            return result.GetType() == typeof(T) ? (T)result : defaultValue;
        }

        public T Get<T>(string key) where T : IConvertible => (T)Get(key, default!);

        public virtual void Set(string key, dynamic value, bool @readonly)
        {
            if (_dataSet.ContainsKey(key))
            {
                if (!_dataSet[key].@readonly) _dataSet[key] = (@readonly, value);
                else throw new ReadOnlyException($"this key({key}) is readonly");
            }
            else _dataSet[key] = (@readonly, value);
        }

        public void Set(string key, object value) => Set(key, value, false);

        public abstract void Save();
    }
}