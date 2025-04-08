using BulletPro;

namespace GoFire
{
    public struct Ammo
    {
        public EmitterProfile EmitterProfile;
        public cfg.Ammo Config;
        
        public float Damage => Config.Damage;
        public float Life => Config.Life;
        
        public Ammo Clone()
        {
            return new Ammo
            {
                EmitterProfile = CloneUtils.CloneEmitterProfile(EmitterProfile),
                Config = Config,
            };
        }
    }
} 