using BulletPro;

namespace GoFire
{
    public struct Ammo
    {
        public EmitterProfile EmitterProfile;
        public float Damage;

        public Ammo Clone()
        {
            return new Ammo
            {
                EmitterProfile = ScriptableObjectClone.CloneEmitterProfile(EmitterProfile),
                Damage = Damage
            };
        }
    }
} 