using UnityEngine;

namespace GoFire
{
    public struct BounceData
    {
        public float Dampening; // 反弹衰减
        public float Mass; // 质量
    }
    
    public class BounceCtrl : MonoBehaviour
    {
        public BounceData Bounce;
        
        public BounceData GetBounce()
        {
            return Bounce;
        }
        
        public void SetBounce(BounceData bounce)
        {
            Bounce = bounce;
        }
    }
}