using System;
using BulletPro;
using Script.GameLogic.BulletPatternOrigin;
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
                patternOrigin = new GameObject("PatternOrigin").transform;
                patternOrigin.SetParent(transform);
                patternOrigin.localPosition = Vector3.zero;
                patternOrigin.localRotation = Quaternion.AngleAxis(Mathf.PI, Vector3.right);
            }
            
            GetComponent<BulletEmitter>().patternOrigin = patternOrigin;
        }
    }
}