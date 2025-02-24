using UnityEngine;

namespace GoFire
{
    public struct GfBounds
    {
        public Bounds Base;

        public float Length => Base.size.z;
        public float Width => Base.size.x;
    }
}