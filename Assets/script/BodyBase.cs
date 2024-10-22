using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoFire
{
    public abstract class BodyBase : MonoBehaviour, IBody
    {
        private Action<IBody> onDead;
        public abstract void Dead();

        public void OnDead()
        {
            if (onDead != null)
            {
                onDead(this);
            }
        }
        public void RegisterOnDead(Action<IBody> onDead)
        {
            onDead += onDead;
        }
    }
}