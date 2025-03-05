using UnityEngine;

namespace GoFire
{
    public class DamageCtrl : MonoBehaviour
    {
        public float damage;

        public float GetDamage()
        {
            return damage;
        }
        public void SetDamage(float dam)
        {
            damage = dam;
        }
    }
}