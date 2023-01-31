using System;
using System.Collections.Generic;
using System.Linq;

namespace _Script.SQL.Stores.Util
{
    public class BitMask<T> where T : Enum
    {
        public long Value;

        public BitMask(long value = 0)
        {
            Value = value;
        }

        public bool this[T eEnum]
        {
            get => (Value & (1L << (int)(object)eEnum)) != 0;
            set
            {
                int e = (int)(object)eEnum;
                if (value)
                {
                    Value |= (1L << e);
                }
                else
                {
                    Value &= ~(1L << e);
                }
            }
        }

        public static BitMask<T> operator |(BitMask<T> left, BitMask<T> right)
        {
            return new BitMask<T>(left.Value | right.Value);
        }

        public static BitMask<T> operator &(BitMask<T> left, BitMask<T> right)
        {
            return new BitMask<T>(left.Value & right.Value);
        }

        public static bool operator ==(BitMask<T> left, BitMask<T> right)
        {
            return right != null && left != null && left.Value == right.Value;
        }

        public static bool operator !=(BitMask<T> left, BitMask<T> right)
        {
            return right != null && left != null && left.Value != right.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is BitMask<T> mask && Value == mask.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public void ClearAll()
        {
            Value = 0;
        }

        public void SetAll()
        {
            Value = AllValues().Aggregate(0L, (current, value) => current | (1L << (int)(object)value));
        }

        public IEnumerable<T> GetFlags()
        {
            return AllValues().Where(HasFlag);
        }

        public bool HasFlag(T value)
        {
            return this[value];
        }

        public void SetTrue(T value)
        {
            this[value] = true;
        }

        public void SetFalse(T value)
        {
            this[value] = false;
        }

        private static IEnumerable<T> AllValues()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}