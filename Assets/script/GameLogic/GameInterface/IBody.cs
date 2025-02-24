namespace GoFire
{
    public interface IBodyDeadReason
    {
        string GetReason();
    }

    public struct BodyData
    {
        public float Life;
        public float Damage;
        public Speed Speed;
        public BounceData Bounce;
    }
    public struct BounceData
    {
        public float Dampening; // 反弹衰减
        public float Mass; // 质量
    }
    public interface IBody : IMovable
    {
        float GetLife();
        void AddLife(float life);
        float GetDamage();
        BounceData GetBounce(); // todo: 这个也许是皮肤属性
    }
}