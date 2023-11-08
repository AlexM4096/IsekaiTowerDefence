﻿namespace Tools
{
    public class Ref<T> where T : struct
    {
        private T _value;

        public Ref(T value)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator T(Ref<T> wrapper)
        {
            return wrapper.Value;
        }

        public static implicit operator Ref<T>(T value)
        {
            return new Ref<T>(value);
        }
    }
}