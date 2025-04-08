using System;
using System.Collections;
using System.Collections.Generic;

namespace GoFire
{
    public class ListOfEvent<T> : System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        private List<T> _list = new();
        
        public Action<T> OnAdd;
        public Action<T> OnRemove;
        public Action OnClear;
        
        public void Clear()
        {
            _list.Clear();
            OnClear?.Invoke();
        }
        
        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }
        
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            _list.Add(item);
            OnAdd?.Invoke(item);
        }
        public void Remove(T item)
        {
            _list.Remove(item);
            OnRemove?.Invoke(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }
        
        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
        
        public int Count => _list.Count;

        public void TrimExcess()
        {
            _list.TrimExcess();
        }
    }
}