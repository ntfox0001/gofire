using UnityEngine;

namespace GoFire
{
    public class Ground : MonoBehaviour
    {
        private GfBounds _bounds;
        public void RefreshBounds()
        {
            _bounds = new GfBounds();
            // 合并所有子节点中的collider的bounds
            foreach (var child in transform.GetComponentsInChildren<Collider>())
            {
                _bounds.Base.Encapsulate(child.bounds);
            }
            // 调整父节点中心的位置，放在所有子节点的bounds的中心
            transform.position = _bounds.Base.center;
        }

        public GfBounds GetBounds()
        {
            return _bounds;
        }
    }
}
