using System;
using BulletPro;
using UnityEngine;

namespace GoFire
{
    public class BulletPatternOriginNode : MonoBehaviour
    {
        public Transform followParent;
        public Quaternion rotation;
        private void Update()
        {
            if (!followParent)
            {
                return;
            }

            transform.position = followParent.position;
            transform.eulerAngles = new Vector3(90, 0, -followParent.eulerAngles.y);
        }

        public void SetFollowUp(Transform parent)
        {
            followParent = parent;
            transform.forward = -Vector3.up;
            Update();
        }
    }
}