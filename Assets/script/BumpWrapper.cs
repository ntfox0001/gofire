using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoFire
{
    public class BumpWrapper : MonoBehaviour, IBump
    {
        IBumpRoot bumpRoot;
        private void Start()
        {
            bumpRoot = GetComponentInParent<IBumpRoot>();
        }

        public void OnBump(Collider other)
        {
            bumpRoot.OnBump(other);
        }
    }
}