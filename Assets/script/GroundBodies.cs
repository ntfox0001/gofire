using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class GroundBodies
    {
        private IBody[] _bodies;
        private int _preIdx = 0;
        public GroundBodies(IBody[] bodies)
        {
            this._bodies = bodies;
        }

        public void SetPos(float top)
        {
            int i = _preIdx;
            for (; i < _bodies.Length; i++)
            {
                if (_bodies[i].GameObject.transform.position.z < top)
                {
                    _bodies[i].Born();
                }
            }
            _preIdx = i;
        }
    }

}