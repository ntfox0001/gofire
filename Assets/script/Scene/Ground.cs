using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using uTools;

namespace GoFire
{
    public class Ground : MonoBehaviour
    {
        // public float HeightPerLayer = 0.01f;
        // public float Speed = 0f; // 移动速度

        public float totalDuration; // 当前地形总时间
        public AnimationCurve moveCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        
        private float _totalLength; // 当前地形总长度
        // private GroundLayer[] _layers;
        private float _playTimePos = 0;
        private IBody[] _bodies;
        private float _posOffset = 0;

        // protected void Start()
        // {
            // _layers = GetComponentsInChildren<GroundLayer>();
            // setTexAniSpeed(Speed);
            // SetTexAniSpeedToLayers();
        // }
        public void AdjustChildrenPos()
        {
            // if (!Mathf.Approximately(ZScale, transform.localScale.z))
            // {
            //     transform.localScale = new Vector3(1, 1, ZScale);
            //     bodies = GetComponentsInChildren<IBody>();
            //     foreach (var b in bodies)
            //     {
            //         var scale = b.GameObject.transform.localScale;
            //         scale.z = 1 / ZScale;
            //         b.GameObject.transform.localScale = scale;
            //     }
            // }
            var colliders = GetComponentsInChildren<Collider>();
            float top = float.MaxValue, bottom = float.MinValue;
            foreach (var c in colliders)
            {
                var front = c.bounds.center.z - c.bounds.size.z * 0.5f;
                if (front < top)
                {
                    top = front;
                } 
                
                var back = c.bounds.center.z + c.bounds.size.z * 0.5f;
                if (back > bottom)
                {
                    bottom = back;
                }
            }

            _totalLength = bottom - top;
            
            // 计算当前节点和所有collider的z的差
            var zOfcolliders = top + _totalLength * 0.5f;
            _posOffset = zOfcolliders - transform.position.z;
            
            _bodies = GetComponentsInChildren<IBody>();
            Array.Sort(_bodies, (a, b) =>
            {
                var az = a.GetPosition().z;
                var bz = b.GetPosition().z;
                return az < bz ? -1 : 1;
            });
        }

        // public void SetTexAniSpeedToLayers()
        // {
        //     foreach (var layer in _layers)
        //     {
        //         (layer.Speed, layer.Height) = GetTexAniInfoByLayer(layer.Layer, Speed);
        //     }
        // }
        //
        // public (float, float) GetTexAniInfoByLayer(float layer, float speed)
        // {
        //     var rate = layer;
        //     return (rate * speed, rate * HeightPerLayer);
        // }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }
        public void OnView(float pos, float top)
        {

        }
        public float GetLength()
        {
            return _totalLength;
        }

        public float GetDuration()
        {
            return totalDuration;
        }

        public void SetPosition(float z)
        {
            var pos = transform.localPosition;
            pos.z = z + GetLength() * 0.5f - _posOffset;
            transform.localPosition = pos;
        }
        public float GetDeltaPos(float deltaTime)
        {
            var prePos = _playTimePos;
            _playTimePos += deltaTime;
            if (_playTimePos > GetDuration())
            {
                _playTimePos = GetDuration();
            }
            else if (_playTimePos < 0)
            {
                _playTimePos = 0;
            }
            
            return GetPlayPos(_playTimePos) - GetPlayPos(prePos);
        }
        public float GetPlayPos(float sec)
        {
            var e = moveCurve.Evaluate(sec / GetDuration());
            return e * GetLength();
        }
        public void SetPlayTimePos(float pos)
        {
            _playTimePos = pos;
        }
    }
}
