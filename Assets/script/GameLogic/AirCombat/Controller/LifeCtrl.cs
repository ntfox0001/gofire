using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire
{
    public class LifeCtrl : MonoBehaviour
    {
        /// <summary>
        /// 第一个参数表示当前的血量，第二个参数表示伤害
        /// 返回false将重置本次伤害
        /// </summary>
        public Func<float, float, bool> OnInjury;
        public float life;
        
        public void AddLife(float l)
        {
            if (l == 0)
            {
                return;
            }
            
            life += l;
            if (OnInjury != null && !OnInjury(life, l))
            {
                life -= l;
            }
        }
        
        public float GetLife()
        {
            return life;
        }

        public bool IsDead()
        {
            return life <= 0;
        }
    }
}