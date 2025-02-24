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
            var beHitBody = data.BeHit.GetComponent<IBody>();
            var hitBody = data.Hit.gameObject.GetComponent<IBody>();
            
            if (beHitBody != null && hitBody != null)
            {
                beHitBody.AddLife(-hitBody.GetDamage());
                hitBody.AddLife(-beHitBody.GetDamage());    
            }
            
            BodyUtils.CalcBounce(beHitBody, hitBody);
        }
    }
}