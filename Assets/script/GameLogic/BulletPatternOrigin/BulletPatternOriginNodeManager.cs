using System.Collections;
using GoFire.Kernel;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace GoFire
{
    public class BulletPatternOriginNodeManager : Singleton<BulletPatternOriginNodeManager>, IManager
    {
        private const string BulletPatternOriginNodeName = "BulletPatternOriginNode";
        public Transform WorldUp { get; private set; }

        private Transform _patternRootNode;
        public Transform Get(Transform parent)
        {
            var go = Pool.GetSingleton().Get(BulletPatternOriginNodeName, null);
            go.GetComponent<BulletPatternOriginNode>().SetFollowUp(parent);
            return go.transform;
        }

        public IEnumerator Init()
        {
            _patternRootNode = new GameObject("BulletPatternRootNode").transform;
            
            Pool.GetSingleton().Register(BulletPatternOriginNodeName, () =>
            {
                var patternOrigin = new GameObject("PatternOrigin");
                patternOrigin.transform.SetParent(_patternRootNode);
                patternOrigin.AddComponent<BulletPatternOriginNode>();
                // patternOrigin.transform.localPosition = Vector3.zero;
                // patternOrigin.transform.localRotation = Quaternion.AngleAxis(Mathf.PI, Vector3.right);
                return patternOrigin;
            });
            yield return null;
        }

        public void Release()
        {
            Destroy(_patternRootNode.gameObject);
        }

        public void SetWorldUp(Transform up)
        {
            WorldUp = up;
        }

        public void UnsetWorldUp()
        {
            WorldUp = null;
        } 
    }
}