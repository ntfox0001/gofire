using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using uTools;

namespace GoFire
{
    [RequireComponent(typeof(TweenMaterial))]
    public class GroundLayer : TweenMat
    {
        public float Layer;        
        float _speed = 1f;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                setTexAniSpeed(_speed);
            }
        }

        public float Height
        {
            get => transform.localPosition.y;
            set
            {
                var pos = transform.localPosition;
                pos.y = value;
                transform.localPosition = pos;
            }
        }
    }

}
