using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoFire.Kernel
{
    public class WaitForObjects : IEnumerator
    {
        private readonly IEnumerator[] _waitObjects;
        private readonly Dictionary<int, bool> _isDone = new();

        public WaitForObjects(IEnumerator[] objects)
        {
            _waitObjects = objects;
            for (int i = 0; i < _waitObjects.Length; i++)
            {
                CoroutineManager.GetSingleton().StartCoroutine(DoWait(i));
            }
        }

        private IEnumerator DoWait(int idx)
        {
            yield return _waitObjects[idx];
            _isDone[idx] = true;
        }
        
        public bool MoveNext()
        {
            return _isDone.Any(isDone => !isDone.Value);
        }

        public void Reset()
        {
            
        }

        public object Current => null;
    }
}