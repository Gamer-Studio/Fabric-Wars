using System;
using FabricWars.Utils.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Utils
{
    [Serializable]
    public abstract class Gauge<T> where T : struct, IComparable<T>, IEquatable<T>, IFormattable
    {
        public T max;
        public T min;
        [GetSet("value"), SerializeField] protected T _value;
        public virtual T value
        {
            get => _value;
            set
            {
                var t = value.CompareTo(min) < 0 ? min :
                    value.CompareTo(max) > 0 ? max : value;

                if (t.Equals(_value)) return;
                
                _value = t;
                onChange?.Invoke(this);
            }
        }

        public UnityEvent<Gauge<T>> onChange;

        public Gauge(T min, T max, T val)
        {
            if (min.CompareTo(max) > 0)
            {
                this.max = min;
                this.min = max;
            }
            else
            {
                this.max = max;
                this.min = min;
            }
            _value = val.CompareTo(min) < 0 ? min :
                val.CompareTo(max) > 0 ? max : val;
            onChange = new UnityEvent<Gauge<T>>();
        }

        public Gauge(T min, T max) : this(min, max, min) { }

        public Gauge(T max) : this(default, max) { }

        public abstract float GetFillRatio();

        public virtual Gauge<T> Calculate(Func<Gauge<T>, Gauge<T>> oper) => oper(this);

        public static implicit operator T(Gauge<T> obj) { return obj._value; }
    }

    [Serializable]
    public class GaugeInt : Gauge<int>
    {
        public GaugeInt(int min, int max, int val) : base(min, max, val) { }

        public GaugeInt(int min, int max) : base(min, max) { }

        public GaugeInt(int max) : base(max) { }

        public override float GetFillRatio() => (float)(_value - min) / (max - min);

        public static int operator +(GaugeInt obj) { return obj._value; }

        public static int operator -(GaugeInt obj) { return -obj._value; }

        public static GaugeInt operator *(GaugeInt obj, int multiplier)
        {
            obj.value = obj._value * multiplier;
            return obj;
        }

        public static GaugeInt operator /(GaugeInt obj, int multiplier)
        {
            obj.value = obj._value / multiplier;
            return obj;
        }

        public static GaugeInt operator +(GaugeInt obj, int value)
        {
            obj.value = obj._value + value;
            return obj;
        }

        public static GaugeInt operator -(GaugeInt obj, int value)
        {
            obj.value = obj._value - value;
            return obj;
        }

        public override string ToString()
        {
            return _value - min + "/" + (max - min);
        }
    }

    [Serializable]
    public class GaugeFloat : Gauge<float>
    {
        public GaugeFloat(float min, float max, float val) : base(min, max, val) { }

        public GaugeFloat(float min, float max) : base(min, max) { }

        public GaugeFloat(float max) : base(max) { }

        public override float GetFillRatio() => (_value - min) / (max - min);

        public static float operator +(GaugeFloat obj) { return obj._value; }

        public static float operator -(GaugeFloat obj) { return -obj._value; }

        public static GaugeFloat operator *(GaugeFloat obj, float multiplier)
        {
            obj.value = obj._value * multiplier;
            return obj;
        }

        public static GaugeFloat operator /(GaugeFloat obj, float multiplier)
        {
            obj.value = obj._value / multiplier;
            return obj;
        }

        public static GaugeFloat operator +(GaugeFloat obj, float value)
        {
            obj.value = obj._value + value;
            return obj;
        }

        public static GaugeFloat operator -(GaugeFloat obj, float value)
        {
            obj.value = obj._value - value;
            return obj;
        }

        public override string ToString()
        {
            return _value - min + "/" + (max - min);
        }
    }
}
