using UnityEngine;
using UnityEngine.Serialization;

namespace GoFire.Kernel
{
    public class PoolData : MonoBehaviour
    {
        public string cacheName;
        [System.NonSerialized]
        public int UniqueId;
    }
}