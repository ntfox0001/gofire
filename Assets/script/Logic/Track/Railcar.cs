using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    [ExecuteInEditMode]
    public class Railcar : MonoBehaviour, IRailcar
    {
        IMovable _movable;
        Vector3 _posOffset;
        private Quaternion _rotOffset;
        private ITrack _track;
        private void Awake()
        {
            _movable = GetComponent<IMovable>();
        }

        public void Init(ITrack track)
        {
            var pos = track.GetPosition(0);
            
            _posOffset = pos;
            
        }
        public void SetPosition(Vector3 pos)
        {
            _movable?.SetPos((_rotOffset * pos) + _posOffset);
        }

        public void OnArrive()
        {
            
        }
    }
}
