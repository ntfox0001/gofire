using System;

namespace GoFire
{
    public class GeneralHitHandler : IHitHandler
    {
        public struct BodyHitData
        {
            public HitData Data;
            public IBody BeHitBody;
            public IBody HitBody;
        }

        Action<BodyHitData> _preBodyHitAction;
        Action<BodyHitData> _postBodyHitAction;

        public void RegisterPostBodyHit(Action<BodyHitData> postBodyHitAction)
        {
            _postBodyHitAction = postBodyHitAction;
        }
        public void RegisterPreBodyHit(Action<BodyHitData> preBodyHitAction)
        {
            _preBodyHitAction = preBodyHitAction;
        }
        
        public void OnHit(HitData data)
        {
            BodyUtils.CalcLife(data.Hit.gameObject, data.BeHit);
            BodyUtils.CalcBounce(data.Hit.gameObject, data.BeHit);
        }
    }
}