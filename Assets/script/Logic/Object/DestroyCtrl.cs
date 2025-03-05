using System;
using GoFire.Kernel;
using UnityEngine;

namespace GoFire
{
    public class DestroyCtrl : MonoBehaviour
    {
        private MonoBehaviour[] _needRemoveObjects;
        private void OnDestroy()
        {
            foreach (var obj in _needRemoveObjects)
            {
                Destroy(obj);
            }
            Destroy(this);
        }

        public void AddRemoveObject(MonoBehaviour obj)
        {
            if (obj.gameObject != gameObject)
            {
                Log.Error("DestroyCtrl: AddRemoveObject must be called on the gameObject");
                return;
            }
            
            if (_needRemoveObjects == null)
            {
                _needRemoveObjects = new MonoBehaviour[1];
                _needRemoveObjects[0] = obj;
            }
            else
            {
                Array.Resize(ref _needRemoveObjects, _needRemoveObjects.Length + 1);
                _needRemoveObjects[^1] = obj;
            }
        }
    }
}