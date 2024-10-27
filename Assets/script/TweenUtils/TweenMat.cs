using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uTools;

namespace GoFire
{
    [RequireComponent(typeof(TweenMaterial))]
    public class TweenMat : MonoBehaviour
    {
        const float duration = 100;
        TweenMaterial _teenMaterial;

        protected virtual void Awake()
        {
            _teenMaterial = GetComponent<TweenMaterial>();
        }

        protected void setSpeed(float speed)
        {
            _teenMaterial.to = 0;
            _teenMaterial.from = speed;
            _teenMaterial.duration = duration;
            _teenMaterial.style = Tweener.Style.Loop;
            _teenMaterial.UV = TweenMaterial.UVType.V;
            _teenMaterial.ResetToBeginning();
        }
    }
}
