using UnityEngine;

namespace GoFire
{
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