using BulletPro;
using UnityEngine;

namespace GoFire
{
    public class BulletPatternOrigin : MonoBehaviour
    {
        public Transform patternOrigin;
        private void Awake()
        {
            if (patternOrigin == null)
            {
                patternOrigin = BulletPatternOriginNode.GetSingleton().Get(transform).transform;
            }
            
            GetComponent<BulletEmitter>().patternOrigin = patternOrigin;
        }
    }
}