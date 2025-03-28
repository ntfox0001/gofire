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

    public interface IBody : IMovable
    {
        float GetLife();
        void AddLife(float life);
        float GetDamage();
        BounceData GetBounce(); // todo: 这个也许是皮肤属性
    }
}