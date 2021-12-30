using System.Collections;
using System;

namespace Collections
{
    public class ListEnumerator<T> : IEnumerator
    {
        private T[] _array;
        private int _position = -1;

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                try
                {
                    return _array[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public ListEnumerator(T[] array)
        {
            _array = array;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _array.Length;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
