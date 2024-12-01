using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uTools;

namespace GoFire
{
    [RequireComponent(typeof(TweenMaterial))]
    public class Ground : TweenMat, IGround
    {
        public struct BodyPair
        {
            public Track Track;
            public IBody Body;
        }
        public float HeightPerLayer = 0.01f;
        public float Speed = 0f; // 移动速度

        public float TotalDuration; // 当前地形总时间
        public float TotalLength; // 当前地形总长度
        public AnimationCurve MoveCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

        public float ZScale = 1f;

        GroundLayer[] layers;
        private float _playPos = 0;
        IBody[] bodies;

        protected void Start()
        {
            layers = GetComponentsInChildren<GroundLayer>();
            setTexAniSpeed(Speed);
            SetTexAniSpeedToLayers();
            bodies = GetComponentsInChildren<IBody>();
            Array.Sort(bodies, (IBody a, IBody b) =>
            {
                return a.GameObject.transform.position.z < b.GameObject.transform.position.z ? -1 : 1;
            });
        }
        [ContextMenu("re-init")]
        private void ReInitialize()
        {
            if (!Mathf.Approximately(ZScale, transform.localScale.z))
            {
                transform.localScale = new Vector3(1, 1, ZScale);
                bodies = GetComponentsInChildren<IBody>();
                foreach (var b in bodies)
                {
                    var scale = b.GameObject.transform.localScale;
                    scale.z = 1 / ZScale;
                    b.GameObject.transform.localScale = scale;
                }
            }
            
            TotalLength = GetComponent<Collider>().bounds.size.z;
        }

        public void SetTexAniSpeedToLayers()
        {
            foreach (var layer in layers)
            {
                (layer.Speed, layer.Height) = GetTexAniInfoByLayer(layer.Layer, Speed);
            }
        }

        public (float, float) GetTexAniInfoByLayer(float layer, float speed)
        {
            var rate = layer;
            return (rate * speed, rate * HeightPerLayer);
        }

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
            return TotalLength;
        }

        public float GetDuration()
        {
            return TotalDuration;
        }

        public void SetPosition(float z)
        {
            var pos = transform.localPosition;
            pos.z = z + GetLength() * 0.5f;
            transform.localPosition = pos;
        }
        public float GetDeltaPos(float deltaTime)
        {
            var prePos = _playPos;
            _playPos += deltaTime;
            if (_playPos > GetDuration())
            {
                _playPos = GetDuration();
            }
            else if (_playPos < 0)
            {
                _playPos = 0;
            }

            return GetPlayPos(_playPos) - GetPlayPos(prePos);
        }
        public float GetPlayPos(float sec)
        {
            var e = MoveCurve.Evaluate(sec / GetDuration());
            return e * GetLength();
        }
        public void SetPlayPos(float pos)
        {
            _playPos = pos;
        }
    }
}
