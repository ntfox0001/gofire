using System.Collections;
using GoFire.Kernel;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace GoFire
{
    public class BulletPatternOriginNode : Singleton<BulletPatternOriginNode>, IManager
    {
        private const string BulletPatternOriginNodeName = "BulletPatternOriginNode";
        public GameObject Get(Transform parent)
        {
            return Pool.GetSingleton().Get(BulletPatternOriginNodeName, parent);
        }

        public IEnumerator Init()
        {
            Pool.GetSingleton().Register(BulletPatternOriginNodeName, () =>
            {
                var patternOrigin = new GameObject("PatternOrigin");
                patternOrigin.transform.SetParent(transform);
                patternOrigin.transform.localPosition = Vector3.zero;
                patternOrigin.transform.localRotation = Quaternion.AngleAxis(Mathf.PI, Vector3.right);
                return patternOrigin;
            });
            yield return null;
        }

        public void Release()
        {
            
        }
    }
}