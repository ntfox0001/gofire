using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace GoFire
{
    public abstract class EventBase : MonoBehaviour
    {
        public float Time;
        public abstract void OnTouch(Transform root);
    }

}