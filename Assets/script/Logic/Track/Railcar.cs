using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class Railcar : MonoBehaviour, IRailcar
    {
        public IMovable movable;
        
        Vector3 _posOffset;
        private Quaternion _rotOffset;
        private void Awake()
        {
            if (movable == null) return;
            movable = GetComponent<IMovable>();
        }

        public void SetOffset(Vector3 pos, Quaternion rot)
        {
            _posOffset = pos;
            _rotOffset = rot;
        }
        public void SetPosition(Vector3 pos)
        {
            movable?.SetPos((_rotOffset * pos) + _posOffset);
        }

        public void OnArrive()
        {
            
        }
    }
}
