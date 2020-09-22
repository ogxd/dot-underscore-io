using System.Collections.Generic;

namespace System
{
    public readonly ref struct ReadOnlySpan<T>
    {
        private readonly T[] array;
        private readonly int position;
        private readonly int size;

        public ReadOnlySpan(T[] array, int position, int size)
        {
            this.array = array;
            this.position = position;
            this.size = size;
        }

        public T this[int i]
        {
            get { return array[position + i]; }
        }

        internal T[] ToArray()
        {
            T[] result = new T[size];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = this[i];
            }
            return result;
        }
    }
}
